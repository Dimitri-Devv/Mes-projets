import { PenLine, Trash2 } from "lucide-react";

export default function CommentList({ comments, userId, isAdmin, onEdit, onDelete }) {

    if (!comments || comments.length === 0) {
        return (
            <p className="text-gray-400 italic text-sm">
                Aucun commentaire pour le moment.
            </p>
        );
    }

    return (
        <div className="space-y-6 mt-6">
            {comments.map((c) => (
                <div key={c.id} className="flex items-start gap-4">

                    {/* Avatar */}
                    <img
                        src={c.avatarUrl}
                        alt={c.username}
                        className="w-10 h-10 rounded-full object-cover"
                    />

                    {/* Bulle */}
                    <div className="bg-white text-black p-4 rounded-2xl shadow-md w-full relative">
                        {(c.user?.id === userId || c.userId === userId || isAdmin) && (
                            <div className="absolute top-2 right-3 flex gap-2">
                                <button
                                    onClick={() => onEdit(c)}
                                    className="text-gray-600 hover:text-black"
                                >
                                    <PenLine size={16} />
                                </button>
                                <button
                                    onClick={() => onDelete(c.id)}
                                    className="text-red-500 hover:text-red-700"
                                >
                                    <Trash2 size={16} />
                                </button>
                            </div>
                        )}
                        <p className="font-semibold text-sm">{c.username}</p>

                        <p className="text-xs text-gray-600 mb-2">
                            {new Date(c.createdAt).toLocaleDateString("fr-FR")} –
                            {new Date(c.createdAt).toLocaleTimeString("fr-FR")}
                        </p>

                        <p className="text-sm leading-relaxed">
                            {c.content}
                        </p>
                    </div>

                </div>
            ))}
        </div>
    );
}