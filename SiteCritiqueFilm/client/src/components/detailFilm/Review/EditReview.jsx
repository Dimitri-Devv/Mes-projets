// components/detailFilm/Review/EditReviewModal.jsx
export default function EditReviewModal({
    isOpen,
    onClose,
    onSubmit,
    editTitle,
    editContent,
    editRating,
    setEditTitle,
    setEditContent,
    setEditRating
}) {
    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">

            <div className="bg-white dark:bg-[#2B2B2B] text-black dark:text-white
                            p-6 sm:p-7 rounded-2xl shadow-xl w-[90%] max-w-lg relative">

                {/* HEADER */}
                <div className="flex justify-between items-center mb-4">
                    <h2 className="text-xl font-bold dark:text-white text-black">
                        Modifier ma critique
                    </h2>

                    <button
                        onClick={onClose}
                        className="text-gray-500 hover:text-red-500 text-lg"
                    >
                        ✖
                    </button>
                </div>

                {/* FORM */}
                <form onSubmit={onSubmit} className="space-y-4">

                    {/* TITRE */}
                    <div>
                        <label className="block text-sm font-semibold mb-1 text-gray-700 dark:text-gray-300">
                            Titre
                        </label>
                        <input
                            type="text"
                            value={editTitle}
                            onChange={e => setEditTitle(e.target.value)}
                            className="w-full bg-gray-100 dark:bg-gray-700 dark:text-white
                                       px-3 py-2 rounded-lg"
                        />
                    </div>

                    {/* NOTE */}
                    <div>
                        <label className="block text-sm font-semibold mb-1 text-gray-700 dark:text-gray-300">
                            Note : {editRating}/10
                        </label>
                        <input
                            type="range"
                            min="0"
                            max="10"
                            value={editRating}
                            onChange={e => setEditRating(Number(e.target.value))}
                            className="w-full accent-accentuation"
                        />
                    </div>

                    {/* CONTENU */}
                    <div>
                        <label className="block text-sm font-semibold mb-1 text-gray-700 dark:text-gray-300">
                            Votre critique
                        </label>
                        <textarea
                            rows={4}
                            value={editContent}
                            onChange={e => setEditContent(e.target.value)}
                            className="w-full bg-gray-100 dark:bg-gray-700 dark:text-white
                                       p-3 rounded-lg resize-none"
                        />
                    </div>

                    {/* ACTIONS */}
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