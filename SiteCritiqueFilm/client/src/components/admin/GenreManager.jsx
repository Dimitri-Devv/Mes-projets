import React, { useState, useEffect } from "react";
import { useForm } from "react-hook-form";
import { Trash2, Edit3 } from "lucide-react";
import {
    getGenres,
    createGenre,
    deleteGenre,
    updateGenre,
} from "../../api/genre";

import ConfirmModal from "../ConfirmModal";

export default function GenreManager() {
    const [genres, setGenres] = useState([]);
    const [loading, setLoading] = useState(true);

    const [isModalOpen, setIsModalOpen] = useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);
    const [genreToEdit, setGenreToEdit] = useState(null);

    const [confirmOpen, setConfirmOpen] = useState(false);
    const [toDelete, setToDelete] = useState(null);

    const {
        register,
        handleSubmit,
        reset,
        formState: { errors },
    } = useForm({
        defaultValues: { genre: "" },
    });

    useEffect(() => {
        const fetchGenres = async () => {
            setLoading(true);
            try {
                const data = await getGenres();
                setGenres(data);
            } finally {
                setLoading(false);
            }
        };
        fetchGenres();
    }, []);

    const onAddGenre = async (data) => {
        const name = data.genre.trim();
        if (!name) return;

        const exists = genres.some(
            (g) => g.name.toLowerCase() === name.toLowerCase()
        );

        if (exists) {
            reset();
            setIsModalOpen(false);
            return;
        }

        try {
            const newGenre = await createGenre({ name });
            setGenres((prev) => [...prev, newGenre]);
            reset();
            setIsModalOpen(false);
        } catch (error) {
            console.error("Erreur ajout genre :", error);
        }
    };

    const openEditModal = (genre) => {
        setGenreToEdit(genre);
        setIsEditModalOpen(true);
    };

    const onEditGenre = async (data) => {
        if (!genreToEdit) return;

        const name = data.genre.trim();
        if (!name) return;

        try {
            const updated = await updateGenre({ id: genreToEdit.id, name });
            setGenres((prev) =>
                prev.map((g) =>
                    g.id === genreToEdit.id ? { ...g, name: updated.name } : g
                )
            );
        } catch (error) {
            console.error("Erreur modification genre :", error);
        } finally {
            setIsEditModalOpen(false);
            setGenreToEdit(null);
        }
    };

    const openDeleteConfirm = (genre) => {
        setToDelete(genre);
        setConfirmOpen(true);
    };

    const closeDeleteModal = () => {
        setConfirmOpen(false);
        setToDelete(null);
    };

    const handleConfirmDelete = async () => {
        if (!toDelete) return;

        try {
            await deleteGenre(toDelete.id);
            setGenres((prev) => prev.filter((g) => g.id !== toDelete.id));
        } catch (error) {
            console.error("Erreur suppression genre :", error);
        } finally {
            closeDeleteModal();
        }
    };

    return (
        <section aria-labelledby="admin-genres-title" className="text-white px-4 py-6">
            <h2 id="admin-genres-title" className="text-3xl font-bold text-center mb-8">
                GESTION DES GENRES
            </h2>

            <div className="w-full max-w-6xl mx-auto flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between mb-6">
                <button
                    onClick={() => setIsModalOpen(true)}
                    className="px-4 py-2 rounded-lg bg-accentuation text-black font-semibold hover:opacity-90 transition cursor-pointer"
                >
                    Ajouter un genre
                </button>
            </div>

            {loading ? (
                <div className="flex flex-col justify-center items-center min-h-[280px]">
                    <div className="animate-spin w-10 h-10 border-4 border-t-transparent border-accentuation rounded-full"></div>
                    <p className="mt-4 text-sm text-gray-300">Chargement des genres...</p>
                </div>
            ) : (
                <ul className="grid gap-4 grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 xl:grid-cols-6 max-w-6xl mx-auto">
                    {genres.map((genre) => (
                        <li
                            key={genre.id}
                            className="rounded-xl border border-white/10 bg-[#2A2A2A] p-4 shadow flex items-center justify-between"
                        >
              <span className="block font-semibold text-white truncate">
                {genre.name}
              </span>
                            <div className="flex gap-2 items-center">
                                <button
                                    type="button"
                                    onClick={() => openEditModal(genre)}
                                    className="text-accentuation hover:text-accentuation/80 cursor-pointer"
                                >
                                    <Edit3 size={18} />
                                </button>
                                <button
                                    type="button"
                                    onClick={() => openDeleteConfirm(genre)}
                                    className="text-red-500 hover:text-red-700 cursor-pointer"
                                >
                                    <Trash2 size={18} />
                                </button>
                            </div>
                        </li>
                    ))}
                </ul>
            )}

            {isModalOpen && (
                <div
                    className="fixed inset-0 z-50 grid place-items-center bg-black/70 p-4"
                    onClick={() => {
                        reset();
                        setIsModalOpen(false);
                    }}
                >
                    <div
                        className="w-full max-w-md rounded-2xl bg-anthracite border border-gray-700 shadow-lg"
                        onClick={(e) => e.stopPropagation()}
                    >
                        <div className="flex justify-between items-center px-6 pt-6 pb-3 border-b border-gray-700">
                            <h3 className="text-xl font-bold">Ajouter un nouveau genre</h3>
                            <button
                                onClick={() => {
                                    reset();
                                    setIsModalOpen(false);
                                }}
                                className="text-gray-300 hover:text-white text-lg"
                            >
                                ✖
                            </button>
                        </div>

                        <form onSubmit={handleSubmit(onAddGenre)} className="px-6 py-6 space-y-4">
                            <div>
                                <label className="block text-sm mb-1">Nom du genre</label>
                                <input
                                    type="text"
                                    {...register("genre", {
                                        required: "Nom requis",
                                        minLength: { value: 2, message: "Min 2 caractères" },
                                        maxLength: { value: 40, message: "Max 40 caractères" },
                                    })}
                                    className={`w-full px-3 py-2 rounded bg-gray-800 text-white outline-none ${
                                        errors.genre ? "border border-red-500" : "border border-gray-700"
                                    }`}
                                />
                                {errors.genre && (
                                    <p className="text-red-400 text-sm">{errors.genre.message}</p>
                                )}
                            </div>

                            <div className="flex justify-end gap-2">
                                <button
                                    type="button"
                                    onClick={() => {
                                        reset();
                                        setIsModalOpen(false);
                                    }}
                                    className="px-4 py-2 bg-gray-700 rounded hover:bg-gray-600"
                                >
                                    Annuler
                                </button>
                                <button
                                    type="submit"
                                    className="px-4 py-2 bg-accentuation text-black font-semibold rounded hover:opacity-90"
                                >
                                    Ajouter
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}

            {isEditModalOpen && genreToEdit && (
                <div
                    className="fixed inset-0 z-50 grid place-items-center bg-black/70 p-4"
                    onClick={() => {
                        setIsEditModalOpen(false);
                        setGenreToEdit(null);
                    }}
                >
                    <div
                        className="w-full max-w-md rounded-2xl bg-anthracite border border-gray-700 shadow-lg"
                        onClick={(e) => e.stopPropagation()}
                    >
                        <div className="flex justify-between items-center px-6 pt-6 pb-3 border-b border-gray-700">
                            <h3 className="text-xl font-bold">Modifier un genre</h3>
                            <button
                                onClick={() => {
                                    setIsEditModalOpen(false);
                                    setGenreToEdit(null);
                                }}
                                className="text-gray-300 hover:text-white text-lg"
                            >
                                ✖
                            </button>
                        </div>

                        <form onSubmit={handleSubmit(onEditGenre)} className="px-6 py-6 space-y-4">
                            <div>
                                <label className="block text-sm mb-1">Nom du genre</label>
                                <input
                                    type="text"
                                    placeholder={genreToEdit.name}
                                    {...register("genre", {
                                        required: "Nom requis",
                                        minLength: { value: 2, message: "Min 2 caractères" },
                                        maxLength: { value: 40, message: "Max 40 caractères" },
                                    })}
                                    className={`w-full px-3 py-2 rounded bg-gray-800 text-white outline-none ${
                                        errors.genre ? "border border-red-500" : "border border-gray-700"
                                    }`}
                                />
                            </div>

                            <div className="flex justify-end gap-2">
                                <button
                                    type="button"
                                    onClick={() => {
                                        setIsEditModalOpen(false);
                                        setGenreToEdit(null);
                                    }}
                                    className="px-4 py-2 bg-gray-700 rounded hover:bg-gray-600"
                                >
                                    Annuler
                                </button>
                                <button
                                    type="submit"
                                    className="px-4 py-2 bg-accentuation text-black font-semibold rounded hover:opacity-90"
                                >
                                    Enregistrer
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}

            {confirmOpen && toDelete && (
                <ConfirmModal
                    title="Supprimer ce genre ?"
                    description={`Voulez-vous vraiment supprimer "${toDelete.name}" ?`}
                    onCancel={closeDeleteModal}
                    onConfirm={handleConfirmDelete}
                />
            )}
        </section>
    );
}