// components/detailFilm/Review/DeleteReviewModal.jsx
export default function DeleteReviewModal({
                                              isOpen,
                                              onClose,
                                              onConfirm
                                          }) {
    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
            <div className="bg-white dark:bg-[#2B2B2B] text-black dark:text-white
                        p-6 sm:p-7 rounded-2xl shadow-xl w-[90%] max-w-md relative">

                {/* HEADER */}
                <div className="flex justify-between items-center mb-4">
                    <h2 className="text-xl font-bold dark:text-white text-black">
                        Supprimer la critique
                    </h2>

                    <button
                        onClick={onClose}
                        className="text-gray-500 hover:text-red-500 text-lg"
                    >
                        ✖
                    </button>
                </div>

                {/* TEXTE */}
                <p className="text-sm text-gray-700 dark:text-gray-300 mb-6">
                    Veux-tu vraiment supprimer cette critique ? Cette action est définitive.
                </p>

                {/* ACTIONS */}
                <div className="flex justify-end gap-3 mt-2">
                    <button
                        className="px-4 py-2 rounded-lg bg-gray-300 dark:bg-gray-600
                               text-black dark:text-white"
                        onClick={onClose}
                    >
                        Annuler
                    </button>

                    <button
                        className="px-4 py-2 rounded-lg bg-red-500 hover:bg-red-600
                               text-white font-semibold"
                        onClick={onConfirm}
                    >
                        Supprimer
                    </button>
                </div>

            </div>
        </div>
    );
}