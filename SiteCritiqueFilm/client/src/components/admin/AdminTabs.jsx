export default function AdminTabs({ onSelect }) {
  return (
    <div className="w-full flex justify-center mt-10">
      <div className="flex flex-wrap justify-center items-center gap-2 sm:gap-4 w-full max-w-6xl px-2">

        <button
          onClick={() => onSelect("film")}
          className="px-3 py-2 sm:px-6 sm:py-4 bg-accentuation rounded-lg hover:opacity-80
                     transition text-white font-semibold text-xs sm:text-base cursor-pointer whitespace-nowrap"
        >
          Gérer les Films
        </button>

        <button
          onClick={() => onSelect("genre")}
          className="px-3 py-2 sm:px-6 sm:py-4 bg-accentuation rounded-lg hover:opacity-80
                     transition text-white font-semibold text-xs sm:text-base cursor-pointer whitespace-nowrap"
        >
          Gérer les Genres
        </button>

        <button
          onClick={() => onSelect("actor")}
          className="px-3 py-2 sm:px-6 sm:py-4 bg-accentuation rounded-lg hover:opacity-80
                     transition text-white font-semibold text-xs sm:text-base cursor-pointer whitespace-nowrap"
        >
          Gérer les Acteurs
        </button>

        <button
          onClick={() => onSelect("user")}
          className="px-3 py-2 sm:px-6 sm:py-4 bg-accentuation rounded-lg hover:opacity-80
                     transition text-white font-semibold text-xs sm:text-base cursor-pointer whitespace-nowrap"
        >
          Gérer les Utilisateurs
        </button>

      </div>
    </div>
  );
}