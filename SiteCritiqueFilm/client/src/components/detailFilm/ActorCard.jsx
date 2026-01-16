export default function ActorCard({ actor }) {
    return (
        <div className="bg-[#2A2A2A] rounded-xl shadow-md p-3 flex flex-col items-center hover:scale-[1.02] transition cursor-pointer h-64">
            <img
                src={actor.avatarUrl}
                alt={`${actor.prenom} ${actor.nom}`}
                className="w-full h-48 object-cover rounded-lg mb-3"
            />

            <h3 className="text-white font-semibold text-center text-sm">
                {actor.prenom} {actor.nom}
            </h3>

            {actor.role && (
                <p className="text-gray-400 text-xs text-center">{actor.role}</p>
            )}
        </div>
    );
}