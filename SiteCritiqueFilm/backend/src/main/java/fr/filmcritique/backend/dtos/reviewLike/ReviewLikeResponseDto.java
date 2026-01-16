package fr.filmcritique.backend.dtos.reviewLike;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class ReviewLikeResponseDto {
    private Long reviewId;
    private Long userId;
    private boolean liked;
    private int likesCount;
    private int dislikesCount;
}