import { useEffect, useState, useMemo } from "react";
import {
  getActors,
  createActor,
  updateActor,
  deleteActor,
  getActorsByFilm,
  addFilmToActor,
  removeFilmFromActor,
} from "../../api/actor";

import { getFilms } from "../../api/films";
import ConfirmModal from "../ConfirmModal";
import ActorCard from "../detailFilm/ActorCard.jsx";

import { useForm } from "react-hook-form";
import ActorCardAdmin from "./ActorCardAdmin";

export default function ActorManager() {
  const [actors, setActors] = useState([]);
  const [loading, setLoading] = useState(false);

  const [search, setSearch] = useState("");

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [modalMode, setModalMode] = useState("create");
  const [currentActor, setCurrentActor] = useState(null);

  const [confirmOpen, setConfirmOpen] = useState(false);
  const [toDelete, setToDelete] = useState(null);

  const [isAssignModalOpen, setIsAssignModalOpen] = useState(false);
  const [films, setFilms] = useState([]);
  const [selectedFilmId, setSelectedFilmId] = useState("");
  const [assignSearch, setAssignSearch] = useState("");
  const [assignedActors, setAssignedActors] = useState([]);
  const [assignPage, setAssignPage] = useState(1);
  const itemsPerPage = 6;

  const openAssignModal = () => {
    setIsAssignModalOpen(true);
  };

  const fetchActors = async () => {
    setLoading(true);
    try {
      const data = await getActors();
      setActors(data || []);
    } catch (e) {
      console.error("Erreur chargement acteurs", e);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchActors();
    const fetchFilms = async () => {
      try {
        const data = await getFilms();
        setFilms(data || []);
      } catch (e) {
        console.error("Erreur chargement films", e);
      }
    };
    fetchFilms();
  }, []);

  const filteredActors = useMemo(() => {
    if (!search.trim()) return actors;
    const q = search.toLowerCase();
    return actors.filter((a) => `${a.prenom ?? ""} ${a.nom ?? ""}`.toLowerCase().includes(q));
  }, [search, actors]);

  useEffect(() => {
    const loadFilmActors = async () => {
      if (!selectedFilmId) return;
      try {
        const data = await getActorsByFilm(selectedFilmId);
        setAssignedActors(data.map((a) => a.id));
      } catch (e) {
        console.error("Erreur chargement acteurs du film", e);
      }
    };
    loadFilmActors();
  }, [selectedFilmId]);

  const openCreateModal = () => {
    setModalMode("create");
    setCurrentActor(null);
    setIsModalOpen(true);
  };

  const openEditModal = (actor) => {
    setModalMode("edit");
    setCurrentActor(actor);
    setIsModalOpen(true);
  };

  const openDeleteConfirm = (actor) => {
    setToDelete(actor);
    setConfirmOpen(true);
  };

  const handleDelete = async () => {
    if (!toDelete) return;
    try {
      await deleteActor(toDelete.id);
      setActors((prev) => prev.filter((a) => a.id !== toDelete.id));
    } catch (e) {
      console.error("Erreur suppression", e);
    } finally {
      setConfirmOpen(false);
      setToDelete(null);
    }
  };

  const toggleAssignActor = (actorId) => {
    setAssignedActors((prev) =>
      prev.includes(actorId)
        ? prev.filter((id) => id !== actorId)
        : [...prev, actorId]
    );
  };

  const saveAssignments = async () => {
    try {
      const filmId = selectedFilmId;
      const current = await getActorsByFilm(filmId);
      const currentIds = current.map((a) => a.id);

      const toAdd = assignedActors.filter((id) => !currentIds.includes(id));
      const toRemove = currentIds.filter((id) => !assignedActors.includes(id));

      for (const actorId of toAdd) {
        await addFilmToActor(actorId, filmId);
      }

      for (const actorId of toRemove) {
        await removeFilmFromActor(actorId, filmId);
      }

      setIsAssignModalOpen(false);
    } catch (e) {
      console.error("Erreur sauvegarde attributions", e);
    }
  };

  return (
    <section className="text-white px-4 py-6">
      <h2 className="text-3xl font-bold text-center mb-8">
        GESTION DES ACTEURS
      </h2>

      <div className="w-full max-w-4xl mx-auto grid grid-cols-1 sm:grid-cols-2 gap-3 mb-6">
        <button
          onClick={openCreateModal}
          className="px-4 py-2 bg-accentuation text-black rounded-lg font-bold hover:opacity-80 w-full cursor-pointer"
        >
          Ajouter un acteur
        </button>
        <button
          onClick={openAssignModal}
          className="px-4 py-2 bg-accentuation text-black rounded-lg font-bold hover:opacity-80 w-full cursor-pointer"
        >
          Attribuer un film
        </button>
        <input
          type="text"
          placeholder="Rechercher un acteur..."
          className="px-3 py-2 bg-gray-800 rounded-lg w-full text-white outline-none"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
      </div>

      {loading ? (
        <div className="flex flex-col items-center justify-center min-h-[200px]">
          <div className="w-10 h-10 border-r-transparent border-4 border-accentuation rounded-full animate-spin" />
          <p className="text-sm text-gray-300 mt-4">Chargement...</p>
        </div>
      ) : filteredActors.length > 0 ? (
        <div className="grid grid-cols-2 lg:grid-cols-4 gap-6 max-w-6xl mx-auto">
          {filteredActors.map((actor) => (
            <ActorCardAdmin
              key={actor.id}
              actor={actor}
              onEdit={() => openEditModal(actor)}
              onDelete={() => openDeleteConfirm(actor)}
            />
          ))}
        </div>
      ) : (
        <p className="text-center text-gray-400">Aucun acteur trouvé…</p>
      )}

      {isModalOpen && (
        <Modal
          title={modalMode === "create" ? "Ajouter un acteur" : "Modifier l'acteur"}
          onClose={() => setIsModalOpen(false)}
        >
          <ActorForm
            mode={modalMode}
            initialActor={currentActor}
            onCancel={() => setIsModalOpen(false)}
            onSaved={async () => {
              setIsModalOpen(false);
              await fetchActors();
            }}
          />
        </Modal>
      )}

      {isAssignModalOpen && (
        <Modal
          title="Attribuer un acteur à un film"
          onClose={() => {
            setSelectedFilmId("");
            setAssignSearch("");
            setAssignPage(1);
            setAssignedActors([]);
            setIsAssignModalOpen(false);
          }}
        >
          <div className="space-y-4">
            <label className="block text-sm">Sélectionner un film</label>
            <select
              className="w-full px-3 py-2 bg-gray-800 rounded-lg text-white outline-none"
              value={selectedFilmId}
              onChange={(e) => setSelectedFilmId(e.target.value)}
            >
              <option value="">-- Choisir un film --</option>
              {films.map((f) => (
                <option key={f.id} value={f.id}>{f.title}</option>
              ))}
            </select>

            <input
              type="text"
              placeholder="Rechercher un acteur..."
              className="px-3 py-2 bg-gray-800 rounded-lg w-full text-white outline-none mb-4"
              value={assignSearch}
              onChange={(e) => setAssignSearch(e.target.value)}
            />

            {selectedFilmId && (
              <div className="text-gray-300 space-y-4">
                <p className="text-sm">Sélectionner les acteurs à attribuer :</p>

                <div className="grid grid-cols-2 lg:grid-cols-3 gap-4">
                  {actors
                    .filter((actor) =>
                      `${actor.prenom} ${actor.nom}`.toLowerCase().includes(assignSearch.toLowerCase())
                    )
                    .sort((a, b) => {
                      const aAssigned = assignedActors.includes(a.id);
                      const bAssigned = assignedActors.includes(b.id);
                      return aAssigned === bAssigned ? 0 : aAssigned ? -1 : 1;
                    })
                    .slice((assignPage - 1) * itemsPerPage, assignPage * itemsPerPage)
                    .map((actor) => {
                      const isSelected = assignedActors.includes(actor.id);
                      return (
                        <div
                          key={actor.id}
                          onClick={() => toggleAssignActor(actor.id)}
                          className={`cursor-pointer transition rounded-xl border-2 ${
                            isSelected
                              ? "border-accentuation bg-accentuation/20 scale-105"
                              : "border-transparent opacity-80 hover:opacity-100"
                          }`}
                        >
                          <ActorCard actor={actor} />
                        </div>
                      );
                    })}
                </div>

                <div className="flex justify-center gap-4 mt-4">
                  <button
                    disabled={assignPage === 1}
                    onClick={() => setAssignPage(assignPage - 1)}
                    className="px-3 py-1 bg-gray-700 rounded disabled:opacity-50"
                  >
                    Précédent
                  </button>
                  <button
                    disabled={
                      assignPage * itemsPerPage >=
                      actors.filter((actor) =>
                        `${actor.prenom} ${actor.nom}`
                          .toLowerCase()
                          .includes(assignSearch.toLowerCase())
                      ).length
                    }
                    onClick={() => setAssignPage(assignPage + 1)}
                    className="px-3 py-1 bg-gray-700 rounded disabled:opacity-50"
                  >
                    Suivant
                  </button>
                </div>

                <div className="flex justify-end pt-3">
                  <button
                    className="px-4 py-2 bg-accentuation text-black rounded-lg font-bold hover:opacity-80"
                    onClick={saveAssignments}
                  >
                    Enregistrer
                  </button>
                </div>
              </div>
            )}
          </div>
        </Modal>
      )}

      {confirmOpen && toDelete && (
        <ConfirmModal
          title="Supprimer cet acteur ?"
          message={`Êtes-vous sûr de vouloir supprimer ${toDelete.prenom} ${toDelete.nom} ?`}
          onCancel={() => setConfirmOpen(false)}
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
            className="text-gray-300 hover:text-white"
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

// Formulaire d'ajout / édition d'acteur
function ActorForm({ mode, initialActor, onCancel, onSaved }) {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm({
    defaultValues: {
      nom: initialActor?.nom || "",
      prenom: initialActor?.prenom || "",
      bio: initialActor?.bio || "",
      avatarUrl: initialActor?.avatarUrl || "",
    },
  });

  useEffect(() => {
    reset({
      nom: initialActor?.nom || "",
      prenom: initialActor?.prenom || "",
      bio: initialActor?.bio || "",
      avatarUrl: initialActor?.avatarUrl || "",
    });
  }, [initialActor, mode, reset]);

  const onSubmit = async (values) => {
    try {
      const payload = {
        nom: values.nom,
        prenom: values.prenom,
        bio: values.bio,
        avatarUrl: values.avatarUrl,
      };

      if (mode === "create") {
        await createActor(payload);
      } else if (initialActor?.id) {
        await updateActor(initialActor.id, payload);
      }

      reset();
      onSaved && onSaved();
    } catch (e) {
      console.error("Erreur sauvegarde acteur", e);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div>
        <label className="block text-sm mb-1">Prénom *</label>
        <input
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("prenom", { required: "Le prénom est obligatoire" })}
        />
        {errors.prenom && (
          <p className="text-red-400 text-xs mt-1">{errors.prenom.message}</p>
        )}
      </div>

      <div>
        <label className="block text-sm mb-1">Nom *</label>
        <input
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("nom", { required: "Le nom est obligatoire" })}
        />
        {errors.nom && (
          <p className="text-red-400 text-xs mt-1">{errors.nom.message}</p>
        )}
      </div>

      <div>
        <label className="block text-sm mb-1">Photo (URL)</label>
        <input
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("avatarUrl")}
        />
      </div>

      <div>
        <label className="block text-sm mb-1">Bio</label>
        <textarea
          rows={4}
          className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
          {...register("bio")}
        />
      </div>

      <div className="flex items-center justify-end gap-3 pt-2">
        <button
          type="button"
          onClick={onCancel}
          className="px-4 py-2 rounded bg-gray-700 hover:bg-gray-600"
        >
          Annuler
        </button>
        <button
          type="submit"
          disabled={isSubmitting}
          className="px-4 py-2 rounded bg-accentuation text-black font-semibold hover:opacity-90"
        >
          {mode === "create" ? "Ajouter" : "Enregistrer"}
        </button>
      </div>
    </form>
  );
}

// Modal de confirmation de suppression
function ConfirmDeleteModal({ title, description, onCancel, onConfirm }) {
  return (
    <div
      className="fixed inset-0 z-50 grid place-items-center bg-black/70 p-4"
      role="dialog"
      aria-modal="true"
    >
      <div className="w-full max-w-md bg-anthracite dark:bg-gray-900 rounded-2xl shadow-xl border border-gray-700">
        <div className="px-5 py-4 border-b border-gray-700">
          <h3 className="text-lg font-bold">{title}</h3>
        </div>
        <div className="p-5 space-y-4">
          <p className="text-sm text-gray-300">{description}</p>
          <div className="flex items-center justify-end gap-3">
            <button
              onClick={onCancel}
              className="px-4 py-2 rounded bg-gray-700 hover:bg-gray-600"
            >
              Annuler
            </button>
            <button
              onClick={onConfirm}
              className="px-4 py-2 rounded bg-red-600 hover:bg-red-500"
            >
              Supprimer
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}