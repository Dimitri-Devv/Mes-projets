import { useEffect, useState } from "react";
import { getActorsByFilm } from "../api/actor";
import { getReviewsForFilm } from "../api/review";
import { getFilmById } from "../api/films";

export const useDetailFilm = (id) => {
    const [film, setFilm] = useState(null);
    const [genres, setGenres] = useState([]);
    const [actors, setActors] = useState([]);
    const [reviews, setReviews] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchFilm = async () => {
            setLoading(true);

            const filmData = await getFilmById(id);
            setFilm(filmData);

            const genresList = Array.isArray(filmData.genres)
                ? filmData.genres.map((g) => (typeof g === "string" ? g : g.name))
                : [filmData.genre];

            setGenres(genresList);

            const actorsData = await getActorsByFilm(id);
            setActors(actorsData);

            const reviewsData = await getReviewsForFilm(id);
            setReviews(reviewsData);

            setLoading(false);
        };

        fetchFilm();
    }, [id]);

    return { film, genres, actors, reviews, setReviews, loading };
};