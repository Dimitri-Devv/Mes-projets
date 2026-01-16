import { useEffect, useMemo, useRef, useState } from "react";
import { getFilms, createFilm, updateFilm, deleteFilm } from "../../api/films";
import { getGenres } from "../../api/genre";
import { useForm } from "react-hook-form";
import FilmCardAdmin from "./FilmCardAdmin";
import ConfirmModal from "../ConfirmModal.jsx";

const PAGE_SIZE = 20;
export default function FilmManager() {

  const [films, setFilms] = useState([]);
  const [allFilms, setAllFilms] = useState([]);
  const [page, setPage] = useState(0);
  const [hasMore, setHasMore] = useState(true);
  const [loading, setLoading] = useState(false);

  const [search, setSearch] = useState("");
  const [selectedGenreId, setSelectedGenreId] = useState("");
  const [genres, setGenres] = useState([]);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMode, setModalMode] = useState("create");
  const [currentFilm, setCurrentFilm] = useState(null);

  const [confirmOpen, setConfirmOpen] = useState(false);
  const [toDelete, setToDelete] = useState(null);

  const sentinelRef = useRef(null);

  useEffect(() => {
    const fetchGenres = async () => {
      try {
        const data = await getGenres();
        setGenres(data || []);
      } catch (e) {
        console.error("Erreur chargement genres:", e);
      }
    };
    fetchGenres();
  }, []);

  const fetchAllFilms = async () => {
    setLoading(true);
    try {
      const data = await getFilms();
      const list = Array.isArray(data)
        ? data
        : data?.films || data?.content || [];
      setAllFilms(list);
    } catch (e) {
      console.error("Erreur API films:", e);
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    fetchAllFilms();
  }, []);

  const filteredFilms = useMemo(() => {
    let list = allFilms;

    if (search.trim()) {
      const q = search.trim().toLowerCase();
      list = list.filter((f) => (f.title || "").toLowerCase().includes(q));
    }

    if (selectedGenreId) {
      const gid = Number(selectedGenreId);
      const selectedGenre = genres.find((g) => Number(g.id) === gid);
      const selectedName = selectedGenre?.name?.toLowerCase();

      list = list.filter((f) => {
        if (!Array.isArray(f.genres)) return false;

        return f.genres.some((g) => {
          if (!g) return false;


          if (typeof g === "object") {
            if (g.id && Number(g.id) === gid) return true;
            if (g.name && selectedName && g.name.toLowerCase() === selectedName)
              return true;
            return false;
          }


          if (typeof g === "string" && selectedName) {
            return g.toLowerCase() === selectedName;
          }

          return false;
        });
      });
    }

    return list;
  }, [allFilms, search, selectedGenreId, genres]);

  useEffect(() => {
    const first = filteredFilms.slice(0, PAGE_SIZE);
    setFilms(first);
    setPage(0);
    setHasMore(filteredFilms.length > PAGE_SIZE);
  }, [filteredFilms]);

  useEffect(() => {
    if (!sentinelRef.current) return;

    const observer = new IntersectionObserver(
      (entries) => {
        const first = entries[0];
        if (first.isIntersecting && hasMore && !loading) {
          const nextPage = page + 1;
          const start = nextPage * PAGE_SIZE;
          const nextChunk = filteredFilms.slice(start, start + PAGE_SIZE);

          if (nextChunk.length > 0) {
            setFilms((prev) => [...prev, ...nextChunk]);
            setPage(nextPage);
            setHasMore(filteredFilms.length > (nextPage + 1) * PAGE_SIZE);
          } else {
            setHasMore(false);
          }
        }
      },
      { rootMargin: "200px" }
    );

    observer.observe(sentinelRef.current);
    return () => observer.disconnect();
  }, [hasMore, loading, page, selectedGenreId, filteredFilms]);

  const openCreateModal = () => {
    setModalMode("create");
    setCurrentFilm(null);
    setIsModalOpen(true);
  };

  const openEditModal = (film) => {
    setModalMode("edit");
    setCurrentFilm(film);
    setIsModalOpen(true);
  };

  const openDeleteConfirm = (film) => {
    setToDelete(film);
    setConfirmOpen(true);
  };

  const handleDelete = async () => {
    if (!toDelete) return;
    try {
      await deleteFilm(toDelete.id);
      setFilms((prev) => prev.filter((f) => f.id !== toDelete.id));
      setAllFilms((prev) => prev.filter((f) => f.id !== toDelete.id));
    } catch (e) {
      console.error("Suppression échouée:", e);
    } finally {
      setConfirmOpen(false);
      setToDelete(null);
    }
  };

  return (
    <section
      aria-labelledby="admin-films-title"
      className="text-white px-4 py-6"
    >
      <h2
        id="admin-films-title"
        className="text-3xl font-bold text-center mb-8"
      >
        GESTION DES FILMS
      </h2>

      <div className="w-full max-w-6xl mx-auto grid grid-cols-1 sm:grid-cols-3 items-center gap-3 mb-6">
        <button
          onClick={openCreateModal}
          className="px-4 py-2 rounded-lg bg-accentuation text-black font-semibold hover:opacity-90 transition w-full cursor-pointer"
        >
          Ajouter un film
        </button>
        <input
          type="text"
          placeholder="Rechercher par titre..."
          className="px-3 py-2 rounded-lg bg-gray-800 text-white outline-none w-full"
          value={search}
          onChange={(e) => setSearch(e.target.value.trimStart())}
        />
        <select
          className="w-full px-3 py-2 rounded-lg bg-gray-800 text-white"
          value={selectedGenreId}
          onChange={(e) => setSelectedGenreId(e.target.value)}
        >
          <option value="">Tous les genres</option>
          {genres.map((g) => (
            <option key={g.id} value={g.id}>
              {g.name}
            </option>
          ))}
        </select>
      </div>

      {loading && films.length === 0 ? (
        <div className="flex flex-col justify-center items-center min-h-[280px]">
          <div className="animate-spin w-10 h-10 border-4 border-t-transparent border-accentuation rounded-full"></div>
          <p className="mt-4 text-sm text-gray-300">Chargement des films...</p>
        </div>
      ) : films.length > 0 ? (
        <div className="w-full max-w-6xl mx-auto grid grid-cols-2 lg:grid-cols-4 gap-6">
          {films.map((film) => (
            <div key={film.id}>
              <FilmCardAdmin
                film={film}
                onEdit={() => openEditModal(film)}
                onDelete={() => openDeleteConfirm(film)}
              />
            </div>
          ))}
        </div>
      ) : (
        <p className="text-center text-gray-400">Aucun film trouvé...</p>
      )}

      <div ref={sentinelRef} className="h-12" />

      {isModalOpen && (
        <Modal
          onClose={() => setIsModalOpen(false)}
          title={
            modalMode === "create" ? "Ajouter un film" : "Modifier le film"
          }
        >
          <ModalFilmForm
            key={genres.length}
            mode={modalMode}
            initialFilm={currentFilm}
            genres={genres}
            onCancel={() => setIsModalOpen(false)}
            onSaved={async () => {
              setIsModalOpen(false);
              await fetchAllFilms();
            }}
          />
        </Modal>
      )}

      {confirmOpen && toDelete && (
        <ConfirmModal
          title="Supprimer ce film ?"
          description={`Voulez-vous vraiment supprimer "${toDelete.title}" ?`}
          onCancel={() => {
            setConfirmOpen(false);
            setToDelete(null);
          }}
          onConfirm={handleDelete}
        />
      )}
    </section>
  );
}

function Modal({ title, children, onClose }) {
  return (
    <div
      className="fixed inset-0 z-50 grid place-items-center bg-black/70 p-4"
      role="dialog"
      aria-modal="true"
    >
      <div className="w-full max-w-xl bg-anthracite dark:bg-gray-900 rounded-2xl shadow-xl border border-gray-700">
        <div className="flex items-center justify-between px-5 py-4 border-b border-gray-700">
          <h3 className="text-lg font-bold">{title}</h3>
          <button
            onClick={onClose}
            className="text-gray-300 hover:text-white cursor-pointer"
            aria-label="Fermer"
          >
            ✖
          </button>
        </div>
        <div className="p-5 max-h-[70vh] overflow-y-auto">{children}</div>
      </div>
    </div>
  );
}

function ModalFilmForm({ mode, initialFilm, genres, onCancel, onSaved }) {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
    watch,
  } = useForm({
    defaultValues: {
      title: initialFilm?.title || "",
      director: initialFilm?.director || "",
      dateSortie: initialFilm?.dateSortie
        ? initialFilm.dateSortie.substring(0, 10)
        : "",
      posterUrl: initialFilm?.posterUrl || "",
      afficheUrl: initialFilm?.afficheUrl || "",
      trailerUrl: initialFilm?.trailerUrl || "",
      synopsis: initialFilm?.synopsis || "",
      tendance: initialFilm?.tendance ?? false,
      baseRating: initialFilm?.baseRating ?? 0,
      genreIds:
        initialFilm?.genres
          ?.map((g) => {
            if (typeof g === "object" && g.id) return g.id.toString();
            const match = genres.find((x) => x.name === g);
            return match ? match.id.toString() : null;
          })
          .filter(Boolean) || [],
    },
  });

  useEffect(() => {
    reset({
      title: initialFilm?.title || "",
      director: initialFilm?.director || "",
      dateSortie: initialFilm?.dateSortie
        ? initialFilm.dateSortie.substring(0, 10)
        : "",
      posterUrl: initialFilm?.posterUrl || "",
      afficheUrl: initialFilm?.afficheUrl || "",
      trailerUrl: initialFilm?.trailerUrl || "",
      synopsis: initialFilm?.synopsis || "",
      tendance: initialFilm?.tendance ?? false,
      baseRating: initialFilm?.baseRating ?? 0,
      genreIds:
        initialFilm?.genres
          ?.map((g) => {
            if (typeof g === "object" && g.id) return g.id.toString();
            const match = genres.find((x) => x.name === g);
            return match ? match.id.toString() : null;
          })
          .filter(Boolean) || [],
    });
  }, [initialFilm, mode, reset, genres]);

  const onSubmit = async (values) => {
    try {
      const payload = {
        title: values.title,
        director: values.director,
        dateSortie: values.dateSortie,
        posterUrl: values.posterUrl,
        afficheUrl: values.afficheUrl,
        trailerUrl: values.trailerUrl,
        synopsis: values.synopsis,
        tendance: values.tendance,
        baseRating: Number(values.baseRating),
        genreIds: (values.genreIds || []).map(Number),
      };

      if (mode === "create") {
        await createFilm(payload);
      } else {
        await updateFilm(initialFilm.id, payload);
      }

      reset();
      onSaved && onSaved();
    } catch (e) {
      console.error("Sauvegarde échouée:", e);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div>
        <label className="block text-sm mb-1">Titre *</label>
        <input
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("title", { required: "Le titre est obligatoire" })}
        />
        {errors.title && (
          <p className="text-red-400 text-xs mt-1">{errors.title.message}</p>
        )}
      </div>

      <div>
        <label className="block text-sm mb-1">Réalisateur *</label>
        <input
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("director", {
            required: "Le nom du réalisateur est obligatoire",
          })}
        />
        {errors.director && (
          <p className="text-red-400 text-xs mt-1">{errors.director.message}</p>
        )}
      </div>

      <div>
        <label className="block text-sm mb-1">Date de sortie</label>
        <input
          type="date"
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("dateSortie")}
        />
      </div>

      <div>
        <label className="block text-sm mb-1">Poster (URL paysage)</label>
        <input
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("posterUrl")}
        />
      </div>

      <div>
        <label className="block text-sm mb-1">Affiche (URL portrait)</label>
        <input
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("afficheUrl")}
        />
      </div>

      <div>
        <label className="block text-sm mb-1">Bande‑annonce (URL YouTube)</label>
        <input
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("trailerUrl")}
        />
      </div>

      <div>
        <label className="block text-sm mb-1">Synopsis *</label>
        <textarea
          rows={4}
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("synopsis", {
            required: "Le synopsis est obligatoire",
          })}
        />
        {errors.synopsis && (
          <p className="text-red-400 text-xs mt-1">
            {errors.synopsis.message}
          </p>
        )}
      </div>

      <div>
        <label className="block text-sm font-semibold mb-1">
          Note moyenne de base : {watch("baseRating")} / 10
        </label>
        <input
          type="range"
          min="0"
          max="10"
          step="1"
          className="w-full accent-accentuation cursor-pointer"
          {...register("baseRating", {
            required: true,
            setValueAs: (v) => parseFloat(v)
          })}
        />
      </div>

      <div>
        <label className="block text-sm mb-1">Genres</label>
        <div className="grid grid-cols-2 md:grid-cols-3 gap-2">
          {genres.map((g) => (
            <label
              key={g.id}
              className="flex items-center gap-2 border border-gray-700 rounded-lg px-3 py-2 bg-gray-800 hover:bg-gray-700 transition-colors cursor-pointer"
            >
              <input
                type="checkbox"
                value={g.id}
                {...register("genreIds")}
                className="accent-accentuation"
              />
              <span>{g.name}</span>
            </label>
          ))}
        </div>
      </div>

      <div className="flex items-center justify-between">
        <label htmlFor="tendance" className="text-sm">
          Mettre en tendance
        </label>
        <label className="relative inline-flex items-center cursor-pointer">
          <input
            type="checkbox"
            id="tendance"
            className="sr-only peer"
            {...register("tendance", {
              setValueAs: (v) => !!v,
            })}
          />
          <div className="w-11 h-6 bg-gray-600 peer-focus:outline-none rounded-full peer peer-checked:bg-accentuation transition-all"></div>
          <span className="absolute left-1 top-1 w-4 h-4 bg-white rounded-full transition-all peer-checked:translate-x-5"></span>
        </label>
      </div>

      <div className="flex items-center justify-end gap-3 pt-2">
        <button
          type="button"
          onClick={onCancel}
          className="px-4 py-2 rounded bg-gray-700 hover:bg-gray-600 cursor-pointer"
        >
          Annuler
        </button>
        <button
          type="submit"
          disabled={isSubmitting}
          className="px-4 py-2 rounded bg-accentuation text-black font-semibold hover:opacity-90 cursor-pointer"
        >
          {mode === "create" ? "Ajouter" : "Enregistrer"}
        </button>
      </div>
    </form>
  );
}

