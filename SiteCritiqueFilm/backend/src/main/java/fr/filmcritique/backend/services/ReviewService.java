package fr.filmcritique.backend.services;

import fr.filmcritique.backend.dtos.review.ReviewCreateDto;
import fr.filmcritique.backend.dtos.review.ReviewResponseDto;
import fr.filmcritique.backend.dtos.review.ReviewResponseDtoProfil;
import fr.filmcritique.backend.dtos.review.ReviewUpdateDto;
import fr.filmcritique.backend.entities.Film;
import fr.filmcritique.backend.entities.Review;
import fr.filmcritique.backend.entities.User;
import fr.filmcritique.backend.mappers.ReviewMapper;
import fr.filmcritique.backend.repositories.FilmRepo;
import fr.filmcritique.backend.repositories.ReviewRepo;
import fr.filmcritique.backend.repositories.UserRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class ReviewService {

    private final ReviewRepo reviewRepo;
    private final FilmRepo filmRepo;
    private final UserRepo userRepo;

    private void calculateRating(Film film) {
        double baseRating = film.getBaseRating() != null ? film.getBaseRating() : 0.0;

        List<Review> reviews = reviewRepo.findByFilmId(film.getId());
        int count = reviews.size();

        if (count == 0) {
            film.setRatingAverage(baseRating);
            filmRepo.save(film);
            return;
        }

        double sum = reviews.stream()
                .mapToDouble(Review::getRating)
                .sum();

        double avg = (baseRating + sum) / (count + 1);

        film.setRatingAverage(avg);
        filmRepo.save(film);
    }

    public List<ReviewResponseDtoProfil> getReviewsByFilm(Long filmId) {
        return reviewRepo.findByFilmId(filmId)
                .stream()
                .map(ReviewMapper::toResponse)
                .toList();
    }

    public ReviewResponseDtoProfil createReview(Long filmId, Long userId, ReviewCreateDto dto) {

        Film film = filmRepo.findById(filmId)
                .orElseThrow(() -> new RuntimeException("Film introuvable : " + filmId));

        User user = userRepo.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable : " + userId));

        boolean exists = reviewRepo.findByFilmId(filmId)
                .stream()
                .anyMatch(r -> r.getUser().getId().equals(userId));

        if (exists) {
            throw new RuntimeException("Vous avez déjà publié une critique pour ce film.");
        }

        Review review = ReviewMapper.toEntity(dto, film, user);
        Review saved = reviewRepo.save(review);


        calculateRating(film);

        return ReviewMapper.toResponse(saved);
    }

    // ------------------------------------------
    public ReviewResponseDtoProfil updateReview(Long reviewId, Long userId, ReviewUpdateDto dto) {

        Review review = reviewRepo.findById(reviewId)
                .orElseThrow(() -> new RuntimeException("Critique introuvable : " + reviewId));

        ReviewMapper.updateEntity(review, dto);
        Review updated = reviewRepo.save(review);


        calculateRating(review.getFilm());

        return ReviewMapper.toResponse(updated);
    }

    // ------------------------------------------
    public void deleteReview(Long reviewId, Long userId) {

        Review review = reviewRepo.findById(reviewId)
                .orElseThrow(() -> new RuntimeException("Critique introuvable : " + reviewId));

        Film film = review.getFilm();

        reviewRepo.delete(review);


        calculateRating(film);
    }

    // ------------------------------------------
    public List<ReviewResponseDtoProfil> getTopReviews() {
        return reviewRepo.findTop10ByOrderByLikesCountDesc()
                .stream()
                .map(ReviewMapper::toResponse)
                .toList();
    }

    // ------------------------------------------
    public List<ReviewResponseDtoProfil> getReviewsByUser(Long userId) {
        if (!userRepo.existsById(userId)) {
            throw new RuntimeException("Utilisateur introuvable");
        }

        return reviewRepo.findByUserId(userId)
                .stream()
                .map(ReviewMapper::toResponse)
                .toList();
    }
}