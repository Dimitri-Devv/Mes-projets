package fr.filmcritique.backend.controllers;

import fr.filmcritique.backend.dtos.reviewLike.ReviewLikeResponseDto;
import fr.filmcritique.backend.entities.Review;
import fr.filmcritique.backend.entities.ReviewLike;
import fr.filmcritique.backend.mappers.ReviewLikeMapper;
import fr.filmcritique.backend.services.ReviewLikeService;
import fr.filmcritique.backend.repositories.ReviewRepo;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/reviews/likes")
@RequiredArgsConstructor
public class ReviewLikeController {

    private final ReviewLikeService reviewLikeService;
    private final ReviewRepo reviewRepo;

    public record ToggleLikeRequest(Long userId, boolean liked) {}

    // ============================================================
    //  🔵 TOGGLE LIKE / DISLIKE
    // ============================================================
    @PutMapping("/toggle/{reviewId}")
    @Operation(
            summary = "Like / Dislike une critique",
            description = """
                Permet à un utilisateur d’aimer ou de ne pas aimer une critique.
                - liked = true  → like
                - liked = false → dislike
                Toggle auto si même réaction.
                """,
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Statut mis à jour"),
            @ApiResponse(responseCode = "401", description = "Non authentifié", content = @Content),
            @ApiResponse(responseCode = "403", description = "Refusé", content = @Content),
            @ApiResponse(responseCode = "404", description = "Critique introuvable", content = @Content)
    })
    public ResponseEntity<ReviewLikeResponseDto> toggleLike(
            @PathVariable Long reviewId,
            @RequestBody ToggleLikeRequest request
    ) {
        ReviewLike like = reviewLikeService.toggleLike(reviewId, request.userId(), request.liked());

        Review review = reviewRepo.findById(reviewId)
                .orElseThrow(() -> new RuntimeException("Critique introuvable"));

        ReviewLikeResponseDto response = ReviewLikeMapper.toResponse(review, like, request.userId());
        return ResponseEntity.ok(response);
    }

    // ============================================================
    //  🟣 STATUS LIKE / DISLIKE POUR UN UTILISATEUR
    // ============================================================
    @GetMapping("/status/{reviewId}/user/{userId}")
    @Operation(
            summary = "Récupère la réaction d’un utilisateur sur une critique",
            description = "Renvoie like/dislike ou null si aucune réaction.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Statut récupéré"),
            @ApiResponse(responseCode = "401", description = "Non authentifié", content = @Content)
    })
    public ResponseEntity<ReviewLikeResponseDto> getUserReaction(
            @PathVariable Long reviewId,
            @PathVariable Long userId
    ) {
        ReviewLike like = reviewLikeService.getUserReaction(reviewId, userId);

        Review review = reviewRepo.findById(reviewId)
                .orElseThrow(() -> new RuntimeException("Critique introuvable"));

        ReviewLikeResponseDto response = ReviewLikeMapper.toResponse(review, like, userId);
        return ResponseEntity.ok(response);
    }
}