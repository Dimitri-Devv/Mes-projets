package fr.filmcritique.backend.mappers;

import fr.filmcritique.backend.dtos.reviewLike.ReviewLikeResponseDto;
import fr.filmcritique.backend.entities.Review;
import fr.filmcritique.backend.entities.ReviewLike;

public class ReviewLikeMapper {

    public static ReviewLikeResponseDto toResponse(Review review, ReviewLike like, Long userId) {
        return new ReviewLikeResponseDto(
                review.getId(),
                userId,
                like != null && like.isLiked(),
                review.getLikesCount(),
                review.getDislikesCount()
        );
    }
}