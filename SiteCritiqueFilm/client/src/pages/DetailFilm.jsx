import { useParams } from "react-router";
import { TicketFilm } from "../components/detailFilm/TicketFilm.jsx";
import { Spinner } from "../components/Spinner";
import ActorCard from "../components/detailFilm/ActorCard.jsx";
import Review from "../components/detailFilm/Review/Review.jsx";
import AddReview from "../components/detailFilm/Review/AddReview.jsx";
import { useAuth } from "../context/AuthContext";
import SectionTitle from "../components/SectionTitle.jsx";
import { useDetailFilm } from "../hooks/useDetailFilm";
import {useNavigate} from "react-router";
import { useState } from "react";

export const DetailFilm = () => {
    const { id } = useParams();
    const { user, userId } = useAuth();
    const navigate = useNavigate();

    const {
        film,
        genres,
        actors,
        reviews,
        setReviews,
        loading
    } = useDetailFilm(id);

    const [showAddReview, setShowAddReview] = useState(false);

    if (loading || !film) {
        return (
            <div className="h-screen flex items-center justify-center">
                <Spinner />
            </div>
        );
    }

    return (
        <div className="pb-8 sm:pb-16">
            {/* FILM */}
            <section className="py-20 sm:pt-24 bg-white dark:bg-[#2B2B2B]">
                <TicketFilm film={film} genres={genres} />
            </section>

            {/* CASTING */}
            <section className="mt-8 sm:mt-12 px-4 max-w-6xl mx-auto">
                <SectionTitle
                    title="CASTING"
                    subtitle="Tous les acteurs du film"
                />

                {actors.length === 0 ? (
                    <p className="text-center text-gray-400 italic mt-8">
                        Il n’y a pas encore d’acteurs enregistrés pour ce film.
                    </p>
                ) : (
                    <div className="grid grid-cols-2 sm:grid-cols-3 lg:grid-cols-4 gap-4">
                        {actors.map((actor) => (
                            <ActorCard key={actor.id} actor={actor} />
                        ))}
                    </div>
                )}
            </section>

            {/* CRITIQUES */}
            <section className="mt-10 sm:mt-16 px-4 max-w-6xl mx-auto">
                <SectionTitle
                    title="LES CRITIQUES"
                    subtitle="Toutes les critiques du film"
                />
                <div className="flex items-center justify-end mb-4 sm:mb-6">
                    <button
                        onClick={() => {
                            if (user) {
                                setShowAddReview(true);
                            } else {
                                navigate("/login");
                            }
                        }}
                        className="px-4 py-2 bg-accentuation hover:opacity-90 text-white rounded-xl font-semibold cursor-pointer transition"
                    >
                        Publier une critique
                    </button>
                </div>

                {reviews.length === 0 && (
                    <p className="text-gray-400 italic text-center text-sm mb-10">
                        Aucune critique pour ce film. Soyez le premier !
                    </p>
                )}

                <div className="space-y-10 mx-auto">
                    {reviews.map((review) => (
                        <Review key={review.id} review={review} />
                    ))}
                </div>
            </section>

            {showAddReview && (
                <AddReview
                    filmId={id}
                    onClose={() => setShowAddReview(false)}
                    onCreated={(newReview) => setReviews((prev) => [...prev, newReview])}
                    hasReview={reviews.some((r) => r.userId === userId)}
                />
            )}
        </div>
    );
};