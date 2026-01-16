package fr.filmcritique.backend.controllers;

import fr.filmcritique.backend.dtos.comment.CommentCreateDto;
import fr.filmcritique.backend.dtos.comment.CommentResponseDto;
import fr.filmcritique.backend.dtos.comment.CommentUpdateDto;
import fr.filmcritique.backend.services.CommentService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.http.HttpStatus;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/comments")
@RequiredArgsConstructor
public class CommentController {

    private final CommentService commentService;

    @Operation(
            summary = "Liste les commentaires d’une critique",
            description = "Accessible à tout le monde."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Commentaires récupérés")
    })
    @GetMapping("/review/{reviewId}")
    public List<CommentResponseDto> getCommentsByReview(@PathVariable Long reviewId) {
        return commentService.getCommentsByReview(reviewId);
    }

    @Operation(
            summary = "Créer un commentaire sur une critique",
            description = "Un utilisateur authentifié peut commenter une critique.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "201", description = "Commentaire créé"),
            @ApiResponse(responseCode = "400", description = "Erreur de validation", content = @Content),
            @ApiResponse(responseCode = "401", description = "Non authentifié", content = @Content)
    })
    @PostMapping("/review/{reviewId}/user/{userId}")
    @PreAuthorize("#userId == authentication.principal.id")
    public ResponseEntity<CommentResponseDto> createComment(
            @PathVariable Long reviewId,
            @PathVariable Long userId,
            @RequestBody CommentCreateDto dto
    ) {
        CommentResponseDto created = commentService.create(reviewId, userId, dto);
        return ResponseEntity.status(HttpStatus.CREATED).body(created);
    }

    @Operation(
            summary = "Modifier un commentaire",
            description = """
                Un utilisateur peut modifier uniquement son propre commentaire.
                Un administrateur peut modifier n'importe quel commentaire.
                """,
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Commentaire modifié"),
            @ApiResponse(responseCode = "401", description = "Non authentifié", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé", content = @Content),
            @ApiResponse(responseCode = "404", description = "Commentaire introuvable", content = @Content)
    })
    @PutMapping("/{commentId}/user/{userId}")
    @PreAuthorize("#userId == authentication.principal.id or hasRole('ADMIN')")
    public ResponseEntity<CommentResponseDto> updateComment(
            @PathVariable Long commentId,
            @PathVariable Long userId,
            @RequestBody CommentUpdateDto dto,
            Authentication authentication
    ) {
        boolean isAdmin = authentication.getAuthorities().stream()
                .anyMatch(a -> a.getAuthority().equals("ROLE_ADMIN"));

        CommentResponseDto updated = commentService.update(commentId, userId, dto, isAdmin);
        return ResponseEntity.ok(updated);
    }

    @Operation(
            summary = "Supprimer un commentaire",
            description = """
                Un utilisateur peut supprimer uniquement son propre commentaire.
                Un administrateur peut supprimer n'importe quel commentaire.
                """,
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "204", description = "Commentaire supprimé"),
            @ApiResponse(responseCode = "401", description = "Non authentifié", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé", content = @Content),
            @ApiResponse(responseCode = "404", description = "Commentaire introuvable", content = @Content)
    })
    @DeleteMapping("/{commentId}/user/{userId}")
    @PreAuthorize("#userId == authentication.principal.id or hasRole('ADMIN')")
    public ResponseEntity<Void> deleteComment(
            @PathVariable Long commentId,
            @PathVariable Long userId,
            Authentication authentication
    ) {
        boolean isAdmin = authentication.getAuthorities().stream()
                .anyMatch(a -> a.getAuthority().equals("ROLE_ADMIN"));

        commentService.delete(commentId, userId, isAdmin);
        return ResponseEntity.noContent().build();
    }
}