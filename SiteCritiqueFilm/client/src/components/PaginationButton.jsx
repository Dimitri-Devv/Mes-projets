export const PaginationButton = ({ nbrButton = 0, handleClick, page }) => {
  const numbers = Array.from({ length: nbrButton }, (_, index) => index + 1);
  const canGoPrev = page > 1;
  const canGoNext = page < nbrButton;

  const baseBtn =
    "inline-flex items-center justify-center w-9 h-9 rounded-lg border transition focus:outline-none focus:ring-2 focus:ring-accentuation/50 focus:ring-offset-2 focus:ring-offset-white dark:focus:ring-offset-gray-900";
  const inactiveBtn =
    "text-black dark:text-white border-gray-300 dark:border-gray-700 hover:bg-gray-100 dark:hover:bg-gray-800";
  const activeBtn =
    "bg-accentuation text-black dark:text-black font-bold border-black dark:border-black";
  const navBtn =
    "px-3 w-auto gap-1 text-sm " +
    "text-black dark:text-white border-gray-300 dark:border-gray-700 " +
    "disabled:opacity-40 disabled:cursor-not-allowed hover:bg-gray-100 dark:hover:bg-gray-800";

  return (
    <nav className="inline-flex items-center gap-2" aria-label="Pagination">
      <button
        type="button"
        className={`${baseBtn} ${navBtn}`}
        onClick={() => canGoPrev && handleClick(page - 1)}
        disabled={!canGoPrev}
        aria-label="Page précédente"
      >
        ‹
      </button>

      {numbers.map((number) => {
        const isActive = page === number;
        return (
          <button
            type="button"
            key={number}
            className={`${baseBtn} ${isActive ? activeBtn : inactiveBtn}`}
            onClick={() => handleClick(number)}
            aria-current={isActive ? "page" : undefined}
            aria-label={`Aller à la page ${number}`}
          >
            {number}
          </button>
        );
      })}

      <button
        type="button"
        className={`${baseBtn} ${navBtn}`}
        onClick={() => canGoNext && handleClick(page + 1)}
        disabled={!canGoNext}
        aria-label="Page suivante"
      >
        ›
      </button>
    </nav>
  );
};
