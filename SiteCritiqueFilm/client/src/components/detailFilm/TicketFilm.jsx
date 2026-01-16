import { useEffect, useState } from "react";
import Modal from "react-modal";
import { Heart, Play, Star, X } from "lucide-react";
import { useNavigate } from "react-router";

import { addFavorite, getFavorites, removeFavorite } from "../../api/user";
import { useAuth } from "../../context/AuthContext";

export const TicketFilm = ({ film, genres }) => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [showLoginModal, setShowLoginModal] = useState(false);
  const [isFavorites, setIsFavorites] = useState([]);

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

  // Fonction pour ajouter ou retirer un film des favoris
  const handleFavorite = async (e) => {
    e.preventDefault();
    e.stopPropagation();
    if (!user?.id) {
      setShowLoginModal(true);
      return;
    }

    const isAlreadyFavorite = isFavorites.some((fav) => fav.id === film.id);

    // Optimistic update
    setIsFavorites((prev) =>
      isAlreadyFavorite
        ? prev.filter((fav) => fav.id !== film.id)
        : [...prev, film]
    );

    try {
      if (isAlreadyFavorite) {
        await removeFavorite(film.id);
      } else {
        await addFavorite(film.id);
      }
    } catch (error) {
      // Revert on failure
      setIsFavorites((prev) =>
        isAlreadyFavorite
          ? [...prev, film]
          : prev.filter((fav) => fav.id !== film.id)
      );
      console.error(error);
    }
  };

  // Fonction pour obtenir l'URL d'embed d'une video a partir de l'URL originale - ChatGPT
  function getEmbedUrl(rawUrl) {
    try {
      if (!rawUrl) return "";
      const url = new URL(rawUrl);
      const host = url.hostname.replace(/^www\./, "");

      if (
        host === "youtu.be" ||
        host === "youtube.com" ||
        host === "m.youtube.com"
      ) {
        let videoId = "";
        if (host === "youtu.be") {
          videoId = url.pathname.split("/").filter(Boolean)[0] || "";
        } else if (url.pathname.startsWith("/watch")) {
          videoId = url.searchParams.get("v") || "";
        } else if (url.pathname.startsWith("/shorts/")) {
          videoId = url.pathname.split("/").filter(Boolean)[1] || "";
        } else if (url.pathname.startsWith("/embed/")) {
          videoId = url.pathname.split("/").filter(Boolean)[1] || "";
        }
        if (videoId) {
          const start =
            url.searchParams.get("t") || url.searchParams.get("start");
          const startParam = start
            ? `?start=${parseInt(start, 10) || 0}&rel=0&autoplay=1`
            : "?rel=0&autoplay=1";
          return `https://www.youtube.com/embed/${videoId}${startParam}`;
        }
      }

      if (host.endsWith("vimeo.com")) {
        const parts = url.pathname.split("/").filter(Boolean);
        const id = parts[parts.length - 1];
        if (id) return `https://player.vimeo.com/video/${id}?autoplay=1`;
      }

      return rawUrl;
    } catch {
      return rawUrl || "";
    }
  }
  return (
    <section className="relative w-[90%] max-w-6xl mx-auto flex flex-col md:flex-row h-auto overflow-hidden">
      {/* Coins Haut-gauche */}
      <div className="absolute bg-white dark:bg-[#2B2B2B] rounded-full w-[100px] h-[120px] top-[-60px] left-[-46px] border-r-4 border-black dark:border-white "></div>
      {/* Coins Bas-gauche */}
      <div className="absolute bg-white dark:bg-[#2B2B2B] rounded-full w-[100px] h-[120px] bottom-[-70px] left-[-46px] border-r-4 border-black dark:border-white"></div>
      {/* Coins Centres-haut */}
      <div className="absolute bg-white dark:bg-[#2B2B2B] rounded-full w-[100px] h-[120px] hidden md:block md:top-[-90px] md:left-[29%] border-r-2 md:border-b-4 border-black dark:border-white"></div>
      {/* Coins Centres-bas */}
      <div className="absolute bg-white dark:bg-[#2B2B2B] rounded-full w-[100px] h-[120px] hidden md:block md:bottom-[-90px] md:left-[29%] border-l-2 md:border-t-4 border-black dark:border-white"></div>
      {/* Coins Droits-haut */}
      <div className="absolute bg-white dark:bg-[#2B2B2B] rounded-full w-[100px] h-[120px] top-[-60px] right-[-26px] border-l-4 border-black dark:border-white"></div>
      {/* Coins Droits-bas */}
      <div className="absolute bg-white dark:bg-[#2B2B2B] rounded-full w-[100px] h-[120px] bottom-[-60px] -right-6 border-l-4 border-black dark:border-white"></div>
      {/* Poster Section */}
      <div className="w-full md:w-1/3 h-[600px] md:h-auto border-b-4 md:border-b-0 md:border-r-4 border-dashed border-black dark:border-white">
        <img
          src={film.afficheUrl}
          alt={film.title}
          className="w-full h-[600px] lg:h-full object-cover"
        />
      </div>
      {/* Content Section */}
      <div
        className="w-full md:w-2/3 p-6 sm:p-8 flex flex-col justify-around rounded-b-[15px] md:rounded-r-[15px]"
        style={{
          backgroundImage: `url(${film.posterUrl})`,
          backgroundSize: "cover",
          backgroundPosition: "center",
          backgroundBlendMode: "overlay",
          backgroundColor: "rgba(0, 0, 0, 0.7)",
        }}
      >
        <div className="flex text-3xl font-bold items-center justify-between w-[90%]">
          <h2 className="text-white uppercase">{film.title}</h2>
          <p className="text-white flex items-center gap-2">
            <Star
              className="w-7 h-7 inline-block text-accentuation dark:text-accentuation"
              fill="currentColor"
              stroke="none"
            />{" "}
            {film.ratingAverage != null
              ? Number(film.ratingAverage).toLocaleString("fr-FR", {
                  minimumFractionDigits: 0,
                  maximumFractionDigits: 1,
                })
              : "—"}
          </p>
        </div>
        <div>
          <p className="text-sm text-white">
            {film.dateSortie} | {genres.join(", ")}
          </p>
          <p className="text-sm text-white">De {film.director}</p>
          <p className="text-sm text-white">Avec </p>
        </div>
        <div className="font-semibold">
          <h2 className="text-accentuation dark:text-accentuation text-xl">
            Sysnopsis
          </h2>
          <p className="text-sm text-white">{film.synopsis}</p>
        </div>
        <div className="flex items-center gap-10 w-full justify-center my-10 sm:my-0">
          <button
            onClick={() => {
              setIsModalOpen(true);
            }}
            className="bg-accentuation dark:bg-accentuation text-black dark:text-black font-bold rounded-xl hover:opacity-90 transition border-3 border-accentuation dark:border-accentuation cursor-pointer px-10 py-2 flex items-center justify-center"
          >
            <Play className="w-6 h-6 inline-block text-black dark:text-black" />
          </button>
          <button
            onClick={handleFavorite}
            className="bg-transparent dark:bg-transparent text-accentuation dark:text-accentuation font-bold rounded-xl hover:opacity-90 transition border-3 border-accentuation dark:border-accentuation cursor-pointer px-10 py-2 flex items-center justify-center"
          >
            <Heart
              className="w-6 h-6 inline-block text-accentuation dark:text-accentuation"
              fill={
                isFavorites.some((fav) => fav.id === film.id)
                  ? "currentColor"
                  : "none"
              }
              aria-pressed={isFavorites.some((fav) => fav.id === film.id)}
            />
          </button>
        </div>
      </div>

      <Modal
        isOpen={showLoginModal}
        onRequestClose={() => setShowLoginModal(false)}
        overlayClassName="fixed inset-0 bg-black/50 flex items-center justify-center z-50"
        className="bg-white dark:bg-anthracite text-black dark:text-white p-6 rounded-lg shadow-xl w-[90%] max-w-sm outline-none"
        contentLabel="Connexion requise"
      >
        <h3 className="text-lg font-bold mb-2">Connexion requise</h3>
        <p className="mb-4">
          Vous devez vous connecter pour ajouter ce film aux favoris.
        </p>
        <div className="flex justify-end gap-2">
          <button
            type="button"
            className="px-4 py-2 rounded-3xl border border-gray-400 text-gray-800 dark:text-gray-200 hover:bg-gray-100 dark:hover:bg-white/10"
            onClick={() => setShowLoginModal(false)}
          >
            Annuler
          </button>
          <button
            type="button"
            className="px-4 py-2 rounded-3xl bg-accentuation dark:bg-accentuation text-black dark:text-black hover:bg-accentuation/80"
            onClick={() => {
              setShowLoginModal(false);
              navigate("/login");
            }}
          >
            Se connecter
          </button>
        </div>
      </Modal>

      <Modal
        isOpen={isModalOpen}
        onRequestClose={() => setIsModalOpen(false)}
        contentLabel={film ? `Bande-annonce: ${film.title}` : "Bande-annonce"}
        style={{
          overlay: {
            zIndex: 50,
            backgroundColor: "rgba(0,0,0,0.5)",
            backdropFilter: "blur(2px)",
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
          },
          content: {
            inset: "auto",
            background: "transparent",
            border: "none",
            padding: 0,
            overflow: "visible",
          },
        }}
      >
        <div className="relative w-[90vw] max-w-[960px] h-auto aspect-video rounded-xl overflow-hidden shadow-2xl">
          {film?.trailerUrl ? (
            <iframe
              src={getEmbedUrl(film.trailerUrl)}
              title={film.title}
              className="w-full h-full"
              loading="lazy"
              referrerPolicy="strict-origin-when-cross-origin"
              allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
              allowFullScreen
            />
          ) : (
            <div className="w-full h-full flex flex-col justify-center items-center bg-black/90 dark:bg-black/90">
              <p className="text-white dark:text-white text-center">
                Aucune bande-annonce disponible
              </p>
            </div>
          )}
          <button
            type="button"
            aria-label="Fermer la modale"
            onClick={() => setIsModalOpen(false)}
            className="absolute top-0 right-0 md:top-2 md:right-2 p-2 rounded-full bg-black/80 dark:bg-black/80 text-white dark:text-white hover:bg-black"
          >
            <X />
          </button>
        </div>
      </Modal>
    </section>
  );
};
