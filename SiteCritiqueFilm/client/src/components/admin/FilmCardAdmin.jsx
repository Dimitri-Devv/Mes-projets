import { Link } from "react-router";

export default function FilmCardAdmin({ film, onEdit, onDelete }) {
  return (
    <article key={film.id} className="group relative">
      <div className="absolute inset-0 bg-black/60 opacity-0 group-hover:opacity-100 transition flex flex-col justify-center items-center gap-4 z-20">
        <button
          onClick={() => onEdit(film)}
          className="bg-blue-500 hover:bg-blue-600 text-white text-sm px-4 py-2 rounded-lg font-semibold cursor-pointer"
        >
          Modifier
        </button>
        <button
          onClick={() => onDelete(film.id)}
          className="bg-red-500 hover:bg-red-600 text-white text-sm px-4 py-2 rounded-lg font-semibold cursor-pointer"
        >
          Supprimer
        </button>
      </div>

      <Link to={`/detailFilm/${film.id}`} className="block">
        <div className="relative w-full pt-[150%] overflow-hidden shadow-md rounded-md">
          <img
            src={film.afficheUrl}
            alt={film.title}
            loading="lazy"
            className="absolute inset-0 w-full h-full object-cover object-center transition-transform duration-300 group-hover:scale-105"
          />
          <div className="absolute bottom-0 left-0 right-0 bg-black/70 text-white px-2 py-1 text-xs sm:text-sm font-semibold text-center">
            {film.title}
          </div>
        </div>
      </Link>
    </article>
  );
}