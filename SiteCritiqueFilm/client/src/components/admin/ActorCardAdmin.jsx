export default function ActorCardAdmin({ actor, onEdit, onDelete }) {
    return (
        <article className="group cursor-pointer">
            <div className="relative w-full pt-[150%] overflow-hidden shadow-md rounded-md">
                <img
                    src={actor.avatarUrl}
                    alt={`${actor.nom} ${actor.prenom}`}
                    className="absolute inset-0 w-full h-full object-cover object-center group-hover:scale-105 transition-transform"
                />
                <div className="absolute bottom-0 left-0 right-0 bg-black/70 text-white text-center py-2 text-sm font-semibold">
                    {actor.prenom} {actor.nom}
                </div>
                <div className="absolute inset-0 bg-black/60 opacity-0 group-hover:opacity-100 flex flex-col items-center justify-center space-y-3 rounded-md transition-opacity">
                    <button
                        onClick={onEdit}
                        className="bg-blue-600 text-white text-sm rounded-lg px-4 py-2 hover:bg-blue-700 cursor-pointer"
                    >
                        Modifier
                    </button>
                    <button
                        onClick={onDelete}
                        className="bg-red-600 text-white text-sm rounded-lg px-4 py-2 hover:bg-red-700 cursor-pointer"
                    >
                        Supprimer
                    </button>
                </div>
            </div>
        </article>
    );
}