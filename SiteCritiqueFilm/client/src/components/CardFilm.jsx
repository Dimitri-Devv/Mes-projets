import { useState } from "react";
import { Link, useNavigate } from "react-router";
import Modal from "react-modal";
import { Heart, Star } from "lucide-react";

import { addFavorite, removeFavorite } from "../api/user";

export const CardFilm = ({ film, user, isFavorites, setIsFavorites }) => {
  const navigate = useNavigate();
  const [showLoginModal, setShowLoginModal] = useState(false);

  // Fonction pour formatter le(s) genre(s) du film
  const genresSource = Array.isArray(film?.genres)
    ? film.genres
    : Array.isArray(film?.genre)
    ? film.genre
    : null;
  const genreText = genresSource
    ? genresSource
        .map((g) => (typeof g === "string" ? g : g?.name))
        .filter(Boolean)
        .join(", ")
    : typeof film?.genre === "string"
    ? film.genre
    : film?.genre?.name || "";

  // Fonction pour ajouter ou retirer un film des favoris
  const handleFavorite = async (e) => {
    e.preventDefault();
    e.stopPropagation();
    if (!user?.id) {
      setShowLoginModal(true);
      return;
    }

    const isAlreadyFavorite = isFavorites.some((fav) => fav.id === film.id);

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

  return (
    <article key={film.id} className="group">
      <Link to={`/detailFilm/${film.id}`} className="block">
        <div className="relative w-full pt-[150%] overflow-hidden shadow-md shadow-black/80">
          <Heart
            onClick={handleFavorite}
            className="absolute z-10 top-2 right-2 w-7 h-7 text-accentuation dark:text-accentuation bg-black/80 rounded-full p-1 hover:text-red-500"
            fill={
              isFavorites.some((fav) => fav.id === film.id)
                ? "currentColor"
                : "none"
            }
            aria-pressed={isFavorites.some((fav) => fav.id === film.id)}
          />
          <img
            src={film.afficheUrl}
            alt={film.title}
            loading="lazy"
            className="absolute inset-0 w-full h-full object-cover object-center transition-transform duration-300 group-hover:scale-105"
          />
          <div className="absolute bottom-0 left-0 w-full h-[17%] bg-black/90 px-3  flex items-center justify-between">
            <div className="flex flex-col gap-1">
              <span className="text-white text-sm font-bold line-clamp-1">
                {film.title}
              </span>
              <span className="text-white/80 text-sm line-clamp-1">
                {genreText}
              </span>
            </div>
            <div className="flex items-center gap-1">
              <p className="inline-flex items-center gap-1 text-white/80 text-lg">
                <Star
                  className="w-5 h-5 inline-block text-accentuation dark:text-accentuation"
                  fill="currentColor"
                  stroke="none"
                />
                {film.ratingAverage != null
                  ? Number(film.ratingAverage).toLocaleString("fr-FR", {
                      minimumFractionDigits: 0,
                      maximumFractionDigits: 1,
                    })
                  : "—"}
              </p>
            </div>
          </div>
        </div>
      </Link>

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
    </article>
  );
};
