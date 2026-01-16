import { Star } from "lucide-react";
import { Link } from "react-router";

export const CardFilmProfil = ({ film }) => {
  return (
    <article key={film.id} className="group w-35 sm:w-40 lg:w-50">
      <Link to={`/detailFilm/${film.id}`} className="block">
        <div className="relative w-full pt-[150%] overflow-hidden shadow-md">
          <img
            src={film.afficheUrl}
            alt={film.title}
            loading="lazy"
            className="absolute inset-0 w-full h-full object-cover object-center transition-transform duration-300 group-hover:scale-105"
          />

          <div className="absolute top-1 left-1 bg-black/80 p-2 py-px rounded flex items-center gap-1 text-[10px] font-bold z-20">
            <Star
              className="w-3 h-3 inline-block text-accentuation dark:text-accentuation"
              fill="currentColor"
            />
            <span className="text-white">
              {film.ratingAverage != null
                ? Number(film.ratingAverage).toLocaleString("fr-FR", {
                    minimumFractionDigits: 0,
                    maximumFractionDigits: 1,
                  })
                : "—"}
            </span>
          </div>
          <div className="absolute justify-center bottom-0 left-0 w-full h-[15%] bg-black/90 px-1 flex items-center">
            <div className="flex flex-col gap-1">
              <span className="text-white  text-sm font-bold line-clamp-1 ">
                {film.title}
              </span>
            </div>
          </div>
        </div>
      </Link>
    </article>
  );
};
