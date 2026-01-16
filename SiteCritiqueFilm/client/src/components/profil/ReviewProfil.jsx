
import "../../App.css";
import { CardFilmProfil } from "./CardFilmProfil.jsx";

export default function ReviewProfil({ review }) {

    return (
        <div className="w-full flex justify-center py-3">
            <div className="w-full max-w-5xl grid grid-cols-[120px_1fr] gap-6 items-start">

                {/* --- CARD FILM (toujours à gauche) --- */}
                <div className="flex justify-center  sm:justify-start">
                    <CardFilmProfil
                        film={{
                            id: review.filmId,
                            afficheUrl: review.filmAfficheUrl,
                            title: review.filmTitle,
                            ratingAverage: review.rating
                        }}
                    />
                </div>

                {/* --- BUBBLE SCROLLABLE --- */}
                <div className="flex-1 max-h-[30vh] sm:max-h-[75vh] overflow-y-auto pr-2 custom-scroll ">

                    <div className="relative bg-black/25 dark:bg-white backdrop-blur-lg
                    text-white dark:text-black p-3 sm:p-6 rounded-xl shadow-md
                            text-xs sm:text-base leading-relaxed">
                        <div className="absolute -left-3 top-6 w-0 h-0 border-t-transparent border-b-transparent border-r-black/25 dark:border-r-white border-t-10 border-b-10 border-r-10 z-10"></div>

                        {/* Titre */}
                        <h3 className="font-bold text-sm sm:text-lg mb-2 text-white  dark:text-black">
                            {review.filmTitle}
                        </h3>

                        {/* Contenu */}
                        <p className="whitespace-pre-line">
                            {review.content}
                        </p>
                    </div>

                </div>
            </div>
        </div>
    );
}