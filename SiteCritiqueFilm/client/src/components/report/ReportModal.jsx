import { useState } from "react";
import { createReport } from "../../api/report";

export default function ReportModal({ isOpen, reportedId, reporterId, onClose }) {
    const [message, setMessage] = useState("");
    const [error, setError] = useState("");
    const [success, setSuccess] = useState(false);

    const handleSubmit = async () => {
        if (message.trim().length < 5) {
            setError("Le message doit contenir au moins 5 caractères.");
            return;
        }

        try {
            await createReport(reporterId, reportedId, message);
            setSuccess(true);

            setTimeout(() => {
                setSuccess(false);
                onClose();
            }, 2000);

        } catch (err) {
            setError("Erreur lors de l’envoi du signalement.");
        }
    };

    if (!isOpen) return null;
    return (
        <div className="fixed inset-0 z-50 grid place-items-center bg-black/70 p-4">
            <div className="w-full max-w-md bg-anthracite dark:bg-gray-900 rounded-2xl shadow-xl border border-gray-700">
                <div className="flex items-center justify-between px-5 py-4 border-b border-gray-700">
                    <h3 className="text-lg font-bold text-accentuation">Signaler un utilisateur</h3>
                    <button onClick={onClose} className="text-gray-300 hover:text-white">✖</button>
                </div>
                <div className="p-5 space-y-4">
                    {success ? (
                        <p className="text-green-500 text-center font-semibold">
                            Votre signalement a bien été envoyé
                        </p>
                    ) : (
                        <>
                            <textarea
                                className="w-full px-3 py-2 rounded bg-gray-800 text-white outline-none"
                                rows="4"
                                placeholder="Expliquez la raison du signalement..."
                                value={message}
                                onChange={(e) => setMessage(e.target.value)}
                            />

                            {error && <p className="text-red-400 text-xs">{error}</p>}

                            <div className="flex justify-end gap-3 mt-5">
                                <button
                                    onClick={onClose}
                                    className="px-4 py-2 rounded text-accentuation bg-gray-700 hover:bg-gray-600 cursor-pointer"
                                >
                                    Annuler
                                </button>

                                <button
                                    onClick={handleSubmit}
                                    className="px-4 py-2 rounded bg-accentuation text-black font-semibold hover:opacity-90 cursor-pointer"
                                >
                                    Envoyer
                                </button>
                            </div>
                        </>
                    )}
                </div>
            </div>
        </div>
    );
}