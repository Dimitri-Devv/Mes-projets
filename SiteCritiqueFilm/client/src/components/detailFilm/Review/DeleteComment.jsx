// components/detailFilm/Review/DeleteComment.jsx
export default function DeleteComment({
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
                        Supprimer le commentaire
                    </h2>

                    <button
                        onClick={onClose}
                        className="text-gray-500 hover:text-red-500 text-lg"
                    >
                        ✖
                    </button>
                </div>

                {/* MESSAGE */}
                <p className="text-sm text-gray-700 dark:text-gray-300 mb-6">
                    Es-tu sûr de vouloir supprimer ce commentaire ?
                    <br />
                </p>

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
                        onClick={onConfirm}
                        className="px-4 py-2 rounded-lg bg-red-500 hover:bg-red-600
                                   text-white font-semibold"
                    >
                        Supprimer
                    </button>
                </div>

            </div>
        </div>
    );
}