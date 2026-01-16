package fr.filmcritique.backend.services;

import fr.filmcritique.backend.entities.Review;
import fr.filmcritique.backend.entities.ReviewLike;
import fr.filmcritique.backend.entities.User;
import fr.filmcritique.backend.repositories.ReviewLikeRepo;
import fr.filmcritique.backend.repositories.ReviewRepo;
import fr.filmcritique.backend.repositories.UserRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import org.springframework.transaction.annotation.Transactional;

@Service
@RequiredArgsConstructor
public class ReviewLikeService {

    private final ReviewLikeRepo reviewLikeRepo;
    private final ReviewRepo reviewRepo;
    private final UserRepo userRepo;

    @Transactional
    public ReviewLike toggleLike(Long reviewId, Long userId, boolean liked) {

        // Vérification critique + user
        Review review = reviewRepo.findById(reviewId)
                .orElseThrow(() -> new RuntimeException("Critique introuvable : " + reviewId));

        User user = userRepo.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable : " + userId));

        // Recherche éventuelle réaction précédente
        ReviewLike existing = reviewLikeRepo.findByUserIdAndReviewId(userId, reviewId).orElse(null);

        // 🔵 Pas encore de like → création
        if (existing == null) {
            ReviewLike rl = new ReviewLike();
            rl.setReview(review);
            rl.setUser(user);
            rl.setLiked(liked);

            updateCounts(review, liked, true); // +1

            return reviewLikeRepo.save(rl);
        }

        // 🟡 Même réaction → suppression (toggle OFF)
        if (existing.isLiked() == liked) {
            reviewLikeRepo.delete(existing);
            updateCounts(review, liked, false); // -1
            return null;
        }

        // 🔴 Réaction opposée → switch like ↔ dislike
        // Décrémenter l'ancienne réaction
        updateCounts(review, existing.isLiked(), false);

        existing.setLiked(liked);
        reviewLikeRepo.save(existing);

        // Incrémenter la nouvelle réaction
        updateCounts(review, liked, true);
        return existing;
    }

    private void updateCounts(Review review, boolean liked, boolean increment) {

        if (liked) {
            review.setLikesCount(review.getLikesCount() + (increment ? 1 : -1));
        } else {
            review.setDislikesCount(review.getDislikesCount() + (increment ? 1 : -1));
        }

        reviewRepo.save(review);
    }

    public ReviewLike getUserReaction(Long reviewId, Long userId) {
        return reviewLikeRepo.findByUserIdAndReviewId(userId, reviewId).orElse(null);
    }
}