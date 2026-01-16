package fr.filmcritique.backend.controllers;

import fr.filmcritique.backend.dtos.review.ReviewCreateDto;
import fr.filmcritique.backend.dtos.review.ReviewResponseDtoProfil;
import fr.filmcritique.backend.dtos.review.ReviewUpdateDto;
import fr.filmcritique.backend.services.ReviewService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/reviews")
@RequiredArgsConstructor
public class ReviewController {

    private final ReviewService reviewService;

    @Operation(
            summary = "Liste toutes les critiques d’un film",
            description = "Accessible par tout le monde."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Critiques récupérées")
    })
    @GetMapping("/film/{filmId}")
    public List<ReviewResponseDtoProfil> getReviewsByFilm(@PathVariable Long filmId) {
        return reviewService.getReviewsByFilm(filmId);
    }

    @Operation(
            summary = "Créer une critique sur un film",
            description = "Un utilisateur ne peut publier qu'une seule critique par film.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "201", description = "Critique créée"),
            @ApiResponse(responseCode = "400", description = "Erreur de validation", content = @Content),
            @ApiResponse(responseCode = "401", description = "Non authentifié", content = @Content)
    })
    @PostMapping("/film/{filmId}/user/{userId}")
    @PreAuthorize("#userId == authentication.principal.id")
    public ResponseEntity<ReviewResponseDtoProfil> createReview(
            @PathVariable Long filmId,
            @PathVariable Long userId,
            @RequestBody ReviewCreateDto dto
    ) {
        System.out.println(dto);
        ReviewResponseDtoProfil created = reviewService.createReview(filmId, userId, dto);
        return ResponseEntity.status(HttpStatus.CREATED).body(created);
    }

    @Operation(
            summary = "Modifier une critique",
            description = "Un utilisateur peut modifier uniquement sa critique. L'admin peut tout modifier.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Critique modifiée"),
            @ApiResponse(responseCode = "401", description = "Non authentifié", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé", content = @Content),
            @ApiResponse(responseCode = "404", description = "Critique introuvable", content = @Content)
    })
    @PutMapping("/{reviewId}/user/{userId}")
    @PreAuthorize("#userId == authentication.principal.id or hasRole('ADMIN')")
    public ResponseEntity<ReviewResponseDtoProfil> updateReview(
            @PathVariable Long reviewId,
            @PathVariable Long userId,
            @RequestBody ReviewUpdateDto dto
    ) {
        return ResponseEntity.ok(reviewService.updateReview(reviewId, userId, dto));
    }

    @Operation(
            summary = "Supprimer une critique",
            description = "Un utilisateur peut supprimer uniquement sa critique. L'admin peut supprimer n'importe laquelle.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "204", description = "Critique supprimée"),
            @ApiResponse(responseCode = "401", description = "Non authentifié", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé", content = @Content),
            @ApiResponse(responseCode = "404", description = "Critique introuvable", content = @Content)
    })
    @DeleteMapping("/{reviewId}/user/{userId}")
    @PreAuthorize("#userId == authentication.principal.id or hasRole('ADMIN')")
    public ResponseEntity<Void> deleteReview(
            @PathVariable Long reviewId,
            @PathVariable Long userId
    ) {
        reviewService.deleteReview(reviewId, userId);
        return ResponseEntity.noContent().build();
    }

    @Operation(
            summary = "Liste les critiques les plus acclamées",
            description = "Retourne les critiques triées par nombre de likes décroissant.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Liste des critiques récupérée avec succès"),
            @ApiResponse(responseCode = "401", description = "Non authentifié", content = @Content)
    })
    @GetMapping("/top")
    public List<ReviewResponseDtoProfil> getTopReviews() {
        return reviewService.getTopReviews();
    }

    @Operation(
            summary = "Liste toutes les critiques d’un utilisateur",
            description = "Accessible par tout le monde."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Critiques récupérées"),
            @ApiResponse(responseCode = "404", description = "Utilisateur introuvable", content = @Content)
    })
    @GetMapping("/user/{userId}")
    public List<ReviewResponseDtoProfil> getReviewsByUser(@PathVariable Long userId) {
        return reviewService.getReviewsByUser(userId);
    }
}
