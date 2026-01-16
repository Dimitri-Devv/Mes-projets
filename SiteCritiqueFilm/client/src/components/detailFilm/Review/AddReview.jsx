import { useState } from "react";
import { createReview } from "../../../api/review";
import { useAuth } from "../../../context/AuthContext";

export default function AddReview({ filmId, onClose, onCreated, hasReview }) {
    const { userId, token } = useAuth();

    if (hasReview) {
        return (
            <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
                <div className="bg-white dark:bg-[#2B2B2B] text-black dark:text-white
                                p-6 sm:p-7 rounded-2xl shadow-xl w-[90%] max-w-md relative">

                    <h2 className="text-xl font-bold mb-4 text-center">
                        Vous avez déjà publié une critique
                    </h2>

                    <p className="text-sm text-gray-700 dark:text-gray-300 mb-6 text-center">
                        Vous ne pouvez publier qu’une seule critique pour ce film.
                    </p>

                    <div className="flex justify-center">
                        <button
                            onClick={onClose}
                            className="px-4 py-2 rounded-lg bg-purple-500 hover:bg-purple-600
                                       text-white font-semibold"
                        >
                            OK
                        </button>
                    </div>

                </div>
            </div>
        );
    }

    const [title, setTitle] = useState("");
    const [rating, setRating] = useState(5);
    const [content, setContent] = useState("");
    const [loading, setLoading] = useState(false);

    const handleSubmit = async () => {
        if (!title.trim() || !content.trim()) return;

        try {
            setLoading(true);

            const dto = { title, rating, content };

            const newReview = await createReview(filmId, userId, dto, token);

            onCreated(newReview); // ajoute direct sur la page
            onClose();

        } catch (e) {
            console.error("Erreur création critique :", e);
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">

            <div className="bg-white dark:bg-[#2B2B2B] text-black dark:text-white
                            p-6 sm:p-7 rounded-2xl shadow-xl w-[90%] max-w-lg relative">

                {/* HEADER */}
                <div className="flex justify-between items-center mb-4">
                    <h2 className="text-xl font-bold dark:text-white text-black">
                        Publier une critique
                    </h2>

                    <button
                        onClick={onClose}
                        className="text-gray-500 hover:text-red-500 text-lg"
                    >
                        ✖
                    </button>
                </div>

                {/* FORM */}
                <div className="space-y-4">

                    {/* TITRE */}
                    <div>
                        <label className="block text-sm font-semibold mb-1 text-gray-700 dark:text-gray-300">
                            Titre
                        </label>
                        <input
                            value={title}
                            onChange={(e) => setTitle(e.target.value)}
                            className="w-full bg-gray-100 dark:bg-gray-700 dark:text-white
                                       px-3 py-2 rounded-lg"
                            placeholder="Ex: Chef d’œuvre visuel"
                        />
                    </div>

                    {/* NOTE */}
                    <div>
                        <label className="block text-sm font-semibold mb-1 text-gray-700 dark:text-gray-300">
                            Note : {rating}/10
                        </label>
                        <input
                            type="range"
                            min="0"
                            max="10"
                            value={rating}
                            onChange={(e) => setRating(e.target.value)}
                            className="w-full accent-accentuation"
                        />
                    </div>

                    {/* CONTENU */}
                    <div>
                        <label className="block text-sm font-semibold mb-1 text-gray-700 dark:text-gray-300">
                            Votre critique
                        </label>

                        <textarea
                            value={content}
                            onChange={(e) => setContent(e.target.value)}
                            rows={4}
                            className="w-full bg-gray-100 dark:bg-gray-700 dark:text-white
                                       p-3 rounded-lg resize-none"
                            placeholder="Développe ton avis…"
                        ></textarea>
                    </div>
                </div>

                {/* ACTIONS */}
                <div className="flex justify-end gap-3 mt-6">

                    <button
                        onClick={onClose}
                        className="px-4 py-2 rounded-lg bg-gray-300 dark:bg-gray-600
                                   text-black dark:text-white"
                    >
                        Annuler
                    </button>

                    <button
                        disabled={loading}
                        onClick={handleSubmit}
                        className="px-4 py-2 rounded-lg bg-accentuation hover:bg-accentuation
                                   text-white font-semibold"
                    >
                        {loading ? "Envoi..." : "Publier"}
                    </button>
                </div>

            </div>
        </div>
    );
}