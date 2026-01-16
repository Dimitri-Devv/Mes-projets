import { useEffect, useMemo, useRef, useState } from "react";
import { Link } from "react-router";
import { User, Moon, Search, Sun, LogOut, LayoutDashboard } from "lucide-react";

import { useAuth } from "../../context/AuthContext";
import { getFilms } from "../../api/films";

export const Navbar = () => {
  const { user, isAdmin, logout } = useAuth();
  const [filmsData, setFilmsData] = useState([]);
  const [isDarkMode, setIsDarkMode] = useState(true);
  const [query, setQuery] = useState("");
  const [open, setOpen] = useState(false);
  const inputRef = useRef(null);
  const listboxId = "search-suggestions";

  useEffect(() => {
    const getAllData = async () => {
      const filmsData = await getFilms();
      setFilmsData(filmsData);
    };
    getAllData();
    if (isDarkMode) {
      document.documentElement.classList.add("dark");
    } else {
      document.documentElement.classList.remove("dark");
    }
  }, [isDarkMode]);

  // Search suggestions
  const suggestions = useMemo(() => {
    const q = query.trim().toLowerCase();
    if (q.length < 3) return [];
    const results = filmsData
      .filter((m) => m.title.toLowerCase().includes(q))
      .slice(0, 8);

    return results;
  }, [query]);

  return (
    <header className="fixed top-0 left-0 w-full z-50" role="banner">
      <nav
        role="navigation"
        aria-label="Barre de navigation"
        className="flex item-center justify-between py-2 sm:py-4 px-2 md:px-4 lg:px-6 bg-anthracite dark:bg-anthracite m-auto opacity-90"
      >
        <Link to="/" className="my-auto" aria-label="Aller à l'accueil">
          <img
            src="/logo.webp"
            alt="Logo du site"
            className="w-8 h-8 sm:w-12 sm:h-12 hover:animate-spin"
          ></img>
        </Link>
        <div
          className="relative flex items-center rounded-3xl my-auto mx-2 h-10 sm:h-12 w-fit sm:w-full sm:max-w-sm lg:max-w-lg bg-accentuation dark:bg-accentuation"
          role="search"
        >
          <label htmlFor="search">
            <span className="sr-only">Rechercher un film</span>
            <Search
              aria-hidden="true"
              className="mx-2 m-auto w-6 h-6 hover:cursor-pointer hover:animate-pulse"
            />
          </label>
          <div className="m-auto search-input w-full">
            <input
              id="search"
              type="search"
              placeholder="Rechercher un film"
              className="w-[90%] focus:outline-none"
              aria-label="Rechercher un film"
              role="combobox"
              aria-autocomplete="list"
              aria-expanded={open && suggestions.length > 0}
              aria-controls={listboxId}
              value={query}
              onChange={(e) => {
                setQuery(e.target.value);
                setOpen(true);
              }}
              onFocus={() => setOpen(true)}
              onBlur={() => {
                // Laisse le temps au clic sur une suggestion
                setTimeout(() => setOpen(false), 120);
              }}
              ref={inputRef}
            />
          </div>

          {open && (
            <>
              {suggestions.length > 0 ? (
                <ul
                  id={listboxId}
                  role="listbox"
                  className="absolute left-0 right-0 top-[calc(100%+4px)] z-60 max-h-80 overflow-auto rounded-xl bg-white dark:bg-neutral-900 shadow-lg border border-black/10 dark:border-white/10 p-1"
                >
                  {suggestions.map((film) => (
                    <Link
                      to={`/detailFilm/${film.id}`}
                      onClick={() => {
                        setQuery("");
                        setOpen(false);
                        inputRef.current?.blur();
                      }}
                    >
                      <li
                        key={film.id}
                        className="flex items-center gap-3 px-3 py-2 rounded-lg hover:bg-black/5 dark:hover:bg-white/10"
                        role="option"
                      >
                        <img
                          src={film.afficheUrl}
                          alt={film.title}
                          className="w-8 h-8 object-cover rounded"
                          loading="lazy"
                        />
                        <span className="text-sm text-black dark:text-white">
                          {film.title}
                        </span>
                      </li>
                    </Link>
                  ))}
                </ul>
              ) : (
                query.length >= 3 && (
                  <div className="absolute left-0 right-0 top-[calc(100%+4px)] z-60 rounded-xl bg-white dark:bg-neutral-900 shadow-lg border border-black/10 dark:border-white/10 p-3 text-sm text-gray-500 dark:text-gray-400">
                    Aucun résultat
                  </div>
                )
              )}
            </>
          )}
        </div>
        <div className="flex w-fit gap-0.5 sm:gap-3 items-center justify-between">
          {isAdmin && (
            <Link
              to="/dashboard"
              className="my-auto hover:cursor-pointer"
              aria-label="Aller à la page de dashboard"
            >
              <LayoutDashboard
                aria-hidden="true"
                className="w-8 h-8 sm:w-11 sm:h-11 text-accentuation dark:text-accentuation"
              />
            </Link>
          )}
          <button
            onClick={() => setIsDarkMode(!isDarkMode)}
            className="w-8 h-8 sm:w-12 sm:h-12 hover:cursor-pointer text-accentuation"
            aria-pressed={isDarkMode}
            aria-label={
              isDarkMode
                ? "Désactiver le mode sombre"
                : "Activer le mode sombre"
            }
          >
            {isDarkMode ? (
              <Sun aria-hidden="true" className="w-8 h-8 sm:w-11 sm:h-11" />
            ) : (
              <Moon aria-hidden="true" className="w-8 h-8 sm:w-11 sm:h-11" />
            )}
          </button>
          {user ? (
            <Link
              to={`/profil/${user.id}`}
              className="my-auto hover:cursor-pointer"
              aria-label="Aller à la page de profil"
            >
              <img
                src={user.avatarUrl || "/assets/avatars/user-placeholder.jpg"}
                onError={(e) => (e.target.src = "/placeholder.jpg")}
                alt={`Avatar de ${user.username || "l'utilisateur"}`}
                className="w-8 h-8 sm:w-11 sm:h-11 rounded-full object-cover bg-white"
              />
            </Link>
          ) : (
            <Link
              to="/login"
              className="my-auto hover:cursor-pointer"
              aria-label="Aller à la page de connexion"
            >
              <User
                aria-hidden="true"
                className="w-8 h-8 sm:w-11 sm:h-11 text-accentuation dark:text-accentuation"
              />
            </Link>
          )}
          {user && (
            <button
              onClick={() => logout()}
              className="hover:cursor-pointer hover:animate-pulse"
              aria-label="Déconnexion"
              aria-pressed={false}
              aria-labelledby="Déconnexion"
            >
              <LogOut
                aria-hidden="true"
                className="w-8 h-8 sm:w-11 sm:h-11 text-accentuation dark:text-accentuation"
              />
            </button>
          )}
        </div>
      </nav>
    </header>
  );
};
