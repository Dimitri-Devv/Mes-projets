// components/detailFilm/Review/EditComment.jsx
export default function EditComment({
                                        isOpen,
                                        onClose,
                                        onSubmit,
                                        content,
                                        setContent
                                    }) {
    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black/60 flex items-center justify-center z-50">
            <div className="bg-white dark:bg-[#2B2B2B] text-black dark:text-white
                p-6 sm:p-7 rounded-2xl shadow-xl w-[90%] max-w-lg relative">
                <form onSubmit={onSubmit}>
                    <div className="flex justify-between items-center mb-4">
                        <h2 className="text-xl font-bold dark:text-white text-black">Modifier mon commentaire</h2>
                        <button
                            onClick={onClose}
                            className="text-gray-500 hover:text-red-500 text-lg"
                        >
                            ✖
                        </button>
                    </div>
                    <div>
                        <label className="block text-sm font-semibold mb-1 text-gray-700 dark:text-gray-300">
                            Commentaire
                        </label>
                        <textarea
                            rows={4}
                            value={content}
                            onChange={e => setContent(e.target.value)}
                            className="w-full bg-gray-100 dark:bg-gray-700 dark:text-white
                                       p-3 rounded-lg resize-none"
                        />
                    </div>
                    <div className="flex justify-end gap-3 mt-6">
                        <button
                            type="button"
                            className="px-4 py-2 rounded-lg bg-gray-300 dark:bg-gray-600
                                       text-black dark:text-white"
                            onClick={onClose}
                        >
                            Annuler
                        </button>
                        <button
                            type="submit"
                            className="px-4 py-2 rounded-lg bg-accentuation hover:bg-accentuation
                                       text-white font-semibold"
                        >
                            Enregistrer
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}