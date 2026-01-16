import { CardFilm } from "../CardFilm";

export default function Recommande({ recommended, user, isFavorites, setIsFavorites }) {

    if (!recommended || recommended.length === 0) {
        return (
            <div className="text-center text-gray-500 mt-4">
                Aucune recommandation trouvée pour l’instant.
            </div>
        );
    }

    return (
        <div className="mt-8">

            <div className="grid grid-cols-2 lg:grid-cols-4 gap-5 sm:gap-10 w-[90%] lg:w-[80%] xl:w-[70%] mx-auto max-w-[500px] lg:max-w-[1500px]">
                {recommended.map(film => (
                    <CardFilm
                        key={film.id}
                        film={film}
                        user={user}
                        isFavorites={isFavorites}
                        setIsFavorites={setIsFavorites}
                    />
                ))}
            </div>
        </div>
    );
}