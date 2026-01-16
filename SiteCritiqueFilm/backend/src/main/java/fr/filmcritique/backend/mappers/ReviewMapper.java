package fr.filmcritique.backend.mappers;

import fr.filmcritique.backend.dtos.review.ReviewCreateDto;
import fr.filmcritique.backend.dtos.review.ReviewResponseDtoProfil;
import fr.filmcritique.backend.dtos.review.ReviewUpdateDto;
import fr.filmcritique.backend.entities.Film;
import fr.filmcritique.backend.entities.Review;
import fr.filmcritique.backend.entities.User;

public class ReviewMapper {

    // Convertit Review -> DTO réponse
    public static ReviewResponseDtoProfil toResponse(Review review) {
        if (review == null) return null;

        return new ReviewResponseDtoProfil(
                review.getId(),
                review.getTitle(),
                review.getContent(),
                review.getRating(),
                review.getCreatedAt().toString(),
                review.getFilm().getId(),
                review.getFilm().getTitle(),
                review.getFilm().getAfficheUrl(),
                review.getUser().getId(),
                review.getUser().getUsername(),
                review.getUser().getAvatarUrl(),
                review.getLikesCount(),
                review.getDislikesCount()
        );
    }

    // Convertit DTO création -> Review
    public static Review toEntity(ReviewCreateDto dto, Film film, User user) {
        if (dto == null) return null;

        Review review = new Review();
        review.setTitle(dto.getTitle());
        review.setContent(dto.getContent());
        review.setRating(dto.getRating());
        review.setFilm(film);
        review.setUser(user);

        return review;
    }

    // Met à jour une Review existante
    public static void updateEntity(Review review, ReviewUpdateDto dto) {
        if (review == null || dto == null) return;

        review.setTitle(dto.getTitle());
        review.setContent(dto.getContent());
        review.setRating(dto.getRating());
    }
}