import { useEffect, useState } from "react";
import Modal from "react-modal";
import Recommande from "../components/home/Recommande.jsx";
import { Carrousel } from "../components/home/Carrousel.jsx";
import { CardFilm } from "../components/CardFilm";
import { Spinner } from "../components/Spinner";
import { PaginationButton } from "../components/PaginationButton";
import { Pellicule } from "../components/home/Pellicule.jsx";
import FAQ from "../components/home/Faq.jsx";
import SectionTitle from "../components/SectionTitle.jsx";

import { useAuth } from "../context/AuthContext";

import { getFilms } from "../api/films";
import { getFavorites, getRecommended } from "../api/user";

import { Filter } from "lucide-react";
import { getTopReviews } from "../api/review.jsx";

export const Home = () => {
  const { user } = useAuth();
  const [allFilms, setAllFilms] = useState([]);
  const [filmsTendance, setFilmsTendance] = useState([]);
  const [displayedFilms, setDisplayedFilms] = useState([]);
  const [isFavorites, setIsFavorites] = useState([]);
  const [critique, setCritique] = useState([]);
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [allGenres, setAllGenres] = useState([]);
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [selectedGenres, setSelectedGenres] = useState([]);
  const [responseApi, setResponseApi] = useState({
    pages: 1,
    films: [],
  });
  const [recommended, setRecommended] = useState([]);
  const PAGE_SIZE = 8;

  const handleClickPagination = (page) => {
    setPage(page);
    const el = document.getElementById("films-title");
    if (el) {
      el.scrollIntoView({ behavior: "smooth", block: "start" });
    }
  };

  useEffect(() => {
    const getAllData = async () => {
      try {
        const filmsData = await getFilms();

        const all = filmsData.filter((film) => film);

        setFilmsTendance(all.filter((film) => film.tendance));
        setAllFilms(all);

        const genres = [
          ...new Set(
              all.flatMap((f) =>
                  Array.isArray(f.genres)
                      ? f.genres.map((g) => (typeof g === "string" ? g : g.name))
                      : f.genre
                          ? [typeof f.genre === "string" ? f.genre : f.genre.name]
                          : []
              )
          ),
        ].filter(Boolean);
        setAllGenres(genres);

        setResponseApi({
          pages: Math.ceil(all.length / PAGE_SIZE),
          films: all,
        });

        setDisplayedFilms(all.slice(0, PAGE_SIZE));

        const top = await getTopReviews();
        setCritique(top);
      } catch (e) {
        console.error(e);
      } finally {
        setLoading(false);
      }
    };
    getAllData();
  }, []);

  // Charger les favoris quand l'utilisateur change; si pas d'utilisateur, vider proprement
  useEffect(() => {
    const fetchFavorites = async () => {
      if (!user?.id) {
        setIsFavorites([]);
        return;
      }
      try {
        const favorites = (await getFavorites(user.id)) || [];
        setIsFavorites(favorites.favorites);
      } catch (e) {
        console.error(e);
        setIsFavorites([]);
      }
    };
    fetchFavorites();
  }, [user?.id]);

  useEffect(() => {
    const loadRecommended = async () => {
      if (!localStorage.getItem("token")) {
        setRecommended([]);
        return;
      }
      try {
        const rec = await getRecommended();
        setRecommended(rec);
      } catch (e) {
        console.error("Erreur chargement recommandations :", e);
        setRecommended([]);
      }
    };
    loadRecommended();
  }, [user?.token]);

  // Gestion de la pagination
  useEffect(() => {
    const source = responseApi.films;
    if (source.length) {
      const start = (page - 1) * PAGE_SIZE;
      const end = start + PAGE_SIZE;
      setDisplayedFilms(source.slice(start, end));
    }
  }, [page, responseApi.films]);

  const handleApplyFilter = (maybeGenres) => {
    const activeGenres = Array.isArray(maybeGenres)
        ? maybeGenres
        : selectedGenres;

    const filtered = allFilms.filter((film) => {
      const filmGenres = Array.isArray(film.genres)
          ? film.genres.map((g) => (typeof g === "string" ? g : g.name))
          : film.genre
              ? [typeof film.genre === "string" ? film.genre : film.genre.name]
              : [];

      return activeGenres.length === 0
          ? true
          : filmGenres.some((g) => activeGenres.includes(g));
    });

    setResponseApi({
      pages: Math.ceil(filtered.length / PAGE_SIZE),
      films: filtered,
    });

    setPage(1);
    setModalIsOpen(false);
  };

  const toggleGenre = (genre) => {
    setSelectedGenres((prev) =>
        prev.includes(genre) ? prev.filter((g) => g !== genre) : [...prev, genre]
    );
  };

  return (
      <div className="pt-16 sm:pt-20">
        {/* SEO */}
        <title>Page d'accueil - Site critique - MovieLab</title>
        <h1 className="sr-only">Page d'accueil - Site critique - MovieLab</h1>
        <meta
            name="description"
            content="Bienvenue sur notre site web de critique de films, vous trouverez ici des critiques de films populaires et des critiques de films classiques."
        />
        <link rel="canonical" href={`${window.location.origin}/`} />

        {/* Tendance */}
        <section
            aria-labelledby="tendance-title"
        >
          <SectionTitle
              title="TENDANCE"
              subtitle="Les films les plus populaires du moment"
          />
          {loading ? (
              <div className="flex flex-col justify-center items-center min-h-[360px]">
                <Spinner />
                <p className="mt-4 text-sm text-gray-600 dark:text-gray-300 text-center">
                  Les films en tendance se chargent, popcorn prêt ?
                </p>
              </div>
          ) : (
              <Carrousel films={filmsTendance} />
          )}
        </section>

        {/* User connecté */}
        {user && (
            <section aria-labelledby="user-recommandation">
              <SectionTitle
                  title="Recommandation"
                  subtitle="Les films recommandés pour vous !"
              />
              <Recommande
                  recommended={recommended}
                  user={user}
                  isFavorites={isFavorites}
                  setIsFavorites={setIsFavorites}
              />
            </section>
        )}

        {/* Films */}
        <section aria-labelledby="films-title">
          <SectionTitle
              title="FILMS"
              subtitle="Tous les films"
          />
          {loading ? (
              <div className="flex flex-col justify-center items-center min-h-[360px]">
                <Spinner />
                <p className="mt-4 text-sm text-gray-600 dark:text-gray-300 text-center">
                  Les films se chargent, popcorn prêt ?
                </p>
              </div>
          ) : (
              <div className="flex flex-col items-center gap-10 lg:mt-20">
                <div className="flex justify-start items-center w-[90%] lg:w-[80%] xl:w-[70%] mx-auto max-w-[500px] lg:max-w-[1500px]">
                  <button
                      onClick={() => setModalIsOpen(true)}
                      className="flex items-center gap-2 px-4 py-2 bg-accentuation dark:bg-accentuation text-black dark:text-black font-bold rounded-xl hover:opacity-90 transition border border-black dark:border-black cursor-pointer"
                  >
                    Filtrer{" "}
                    <Filter className="w-5 h-5 fill-current text-black dark:text-black" />
                  </button>
                </div>
                <div className="grid grid-cols-2 lg:grid-cols-4 gap-5 sm:gap-10 w-[90%] lg:w-[80%] xl:w-[70%] mx-auto max-w-[500px] lg:max-w-[1500px]">
                  {displayedFilms.map((film) => (
                      <CardFilm
                          key={film.id}
                          film={film}
                          user={user}
                          isFavorites={isFavorites}
                          setIsFavorites={setIsFavorites}
                      />
                  ))}
                </div>
                <div className="flex justify-center items-center gap-2">
                  <PaginationButton
                      nbrButton={responseApi.pages}
                      handleClick={handleClickPagination}
                      page={page}
                  />
                </div>
              </div>
          )}
        </section>
        {/* --- MODAL FILTRE --- */}
        <Modal
            isOpen={modalIsOpen}
            onRequestClose={() => setModalIsOpen(false)}
            className="bg-white dark:bg-gray-900 p-6 rounded-2xl shadow-2xl max-w-md mx-auto mt-32 text-black dark:text-white"
            overlayClassName="fixed inset-0 bg-black/60 flex justify-center items-center z-50"
        >
          <h3 className="text-xl font-semibold mb-4 text-center">
            Sélectionnez un ou plusieurs genres
          </h3>
          <div className="flex flex-wrap gap-2 justify-between items-center">
            {allGenres.map((genre) => (
                <button
                    key={genre}
                    onClick={() => toggleGenre(genre)}
                    className={`px-3 py-1 rounded-full border ${
                        selectedGenres.includes(genre)
                            ? "bg-accentuation text-white"
                            : "bg-transparent border-gray-400 text-gray-700 dark:text-gray-300"
                    } transition`}
                >
                  {genre}
                </button>
            ))}
          </div>
          <div className="flex justify-end gap-3 mt-6">
            <button
                onClick={() => {
                  setSelectedGenres([]);
                  handleApplyFilter([]);
                }}
                className="px-4 py-2 bg-gray-300 dark:bg-gray-700 rounded-lg"
            >
              Réinitialiser
            </button>
            <button
                onClick={() => handleApplyFilter()}
                className="px-4 py-2 bg-accentuation text-white rounded-lg"
            >
              Appliquer
            </button>
          </div>
        </Modal>
        {/* Critique les plus acclamés */}
        <section aria-labelledby="critique-title" className="mt-10">
          <SectionTitle
              title="LES PLUS ACCLAMÉES"
              subtitle="Les critiques les plus aimées et commentées"
          />

          {loading ? (
              <div className="flex flex-col justify-center items-center min-h-[360px]">
                <Spinner />
                <p className="mt-4 text-sm text-gray-600 dark:text-gray-300 text-center">
                  Les critiques se chargent..
                </p>
              </div>
          ) : (
              <Pellicule critiques={critique} />
          )}
        </section>

        {/* FAQ */}
        <SectionTitle
            title="FOIRE AUX QUESTIONS"
            subtitle="Les questions récurentes sur l'utilisation du site"
        />
        <FAQ />
      </div>
  );
};