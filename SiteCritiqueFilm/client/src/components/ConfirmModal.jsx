// src/components/common/ConfirmModal.jsx
import React from "react";

export default function ConfirmModal({
                                         title = "Confirmation",
                                         message = "Êtes-vous sûr ?",
                                         confirmLabel = "Confirmer",
                                         cancelLabel = "Annuler",
                                         onConfirm,
                                         onCancel,
                                     }) {
    return (
        <div
            className="fixed inset-0 z-50 grid place-items-center bg-black/70 p-4"
            onClick={onCancel}
            role="dialog"
            aria-modal="true"
        >
            <div
                className="w-full max-w-md bg-anthracite dark:bg-gray-900 rounded-2xl shadow-xl border border-gray-700"
                onClick={(e) => e.stopPropagation()}
            >
                {/* Header */}
                <div className="flex items-center justify-between px-5 py-4 border-b border-gray-700">
                    <h2 className="text-xl font-semibold text-white">{title}</h2>
                    <button
                        onClick={onCancel}
                        className="text-gray-400 hover:text-white text-2xl font-bold"
                    >
                        &times;
                    </button>
                </div>

                {/* Message */}
                <div className="p-5 space-y-4 text-white">
                    <p>{message}</p>

                    <div className="flex justify-end gap-3 pt-2">
                        <button
                            onClick={onCancel}
                            className="px-4 py-2 rounded bg-gray-700 hover:bg-gray-600 transition"
                        >
                            {cancelLabel}
                        </button>

                        <button
                            onClick={onConfirm}
                            className="px-4 py-2 rounded bg-accentuation text-black font-semibold hover:opacity-90 transition"
                        >
                            {confirmLabel}
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
}