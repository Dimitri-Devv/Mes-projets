package fr.filmcritique.backend.controllers;

import fr.filmcritique.backend.dtos.user.FavoriteFilmsResponse;
import fr.filmcritique.backend.dtos.user.UpdateUserDto;
import fr.filmcritique.backend.dtos.user.UserProfileResponse;
import fr.filmcritique.backend.dtos.film.FilmResponseDto;
import fr.filmcritique.backend.entities.User;
import fr.filmcritique.backend.services.UserService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/users")
public class UserController {

    private final UserService userService;

    @Operation(
            summary = "Récupère le profil public d’un utilisateur",
            description = "Accessible à tous. Ne retourne aucune donnée sensible."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Profil récupéré avec succès"),
            @ApiResponse(responseCode = "404", description = "Utilisateur introuvable", content = @Content)
    })
    @GetMapping("/{id}")
    public ResponseEntity<UserProfileResponse> getUserProfile(@PathVariable Long id) {
        return ResponseEntity.ok(userService.getUserProfile(id));
    }

    @Operation(
            summary = "Met à jour le profil de l’utilisateur connecté",
            description = "Modifie les données autorisées (avatar, etc.).",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Profil mis à jour"),
            @ApiResponse(responseCode = "400", description = "Données invalides", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content)
    })
    @PutMapping("/me")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<User> updateOwnProfile(
            @AuthenticationPrincipal UserDetails userDetails,
            @RequestBody UpdateUserDto dto
    ) {
        return ResponseEntity.ok(userService.updateSelf(userDetails.getUsername(), dto));
    }

    @Operation(
            summary = "Supprime définitivement le compte de l’utilisateur connecté",
            description = "L'identification se fait via le token JWT.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "204", description = "Compte supprimé", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content)
    })
    @DeleteMapping("/me")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<Void> deleteOwnAccount(
            @AuthenticationPrincipal UserDetails userDetails
    ) {
        userService.deleteByEmail(userDetails.getUsername());
        return ResponseEntity.noContent().build();
    }

    @Operation(
            summary = "Liste les films favoris d’un utilisateur",
            description = "Accessible publiquement. Ne retourne aucune donnée sensible."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Liste des favoris récupérée"),
            @ApiResponse(responseCode = "404", description = "Utilisateur introuvable", content = @Content)
    })
    @GetMapping("/{userId}/favorites")
    public ResponseEntity<FavoriteFilmsResponse> getFavorites(@PathVariable Long userId) {
        return ResponseEntity.ok(userService.getFavoriteFilms(userId));
    }

    @Operation(
            summary = "Ajoute un film aux favoris de l’utilisateur connecté",
            description = "Impossible de modifier les favoris d’un autre utilisateur.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Film ajouté aux favoris"),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "404", description = "Film ou utilisateur introuvable", content = @Content)
    })
    @PostMapping("/me/favorites/{filmId}")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<FavoriteFilmsResponse> addFavorite(
            @AuthenticationPrincipal UserDetails userDetails,
            @PathVariable Long filmId
    ) {
        Long userId = userService.getUserIdFromEmail(userDetails.getUsername());
        return ResponseEntity.ok(userService.addFavoriteFilm(userId, filmId));
    }

    @Operation(
            summary = "Retire un film des favoris de l’utilisateur connecté",
            description = "Strictement limité au propriétaire du compte.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Film retiré des favoris"),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "404", description = "Film ou utilisateur introuvable", content = @Content)
    })
    @DeleteMapping("/me/favorites/{filmId}")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<FavoriteFilmsResponse> removeFavorite(
            @AuthenticationPrincipal UserDetails userDetails,
            @PathVariable Long filmId
    ) {
        Long userId = userService.getUserIdFromEmail(userDetails.getUsername());
        return ResponseEntity.ok(userService.removeFavoriteFilm(userId, filmId));
    }

    @Operation(
            summary = "Recommandations de films pour l’utilisateur connecté",
            description = "Retourne une liste de films recommandés en fonction des genres présents dans ses favoris.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Recommandations générées avec succès"),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content)
    })
    @GetMapping("/me/recommended")
    @PreAuthorize("isAuthenticated()")
    public ResponseEntity<List<FilmResponseDto>> getRecommendedFilms(
            @AuthenticationPrincipal UserDetails userDetails
    ) {
        Long userId = userService.getUserIdFromEmail(userDetails.getUsername());
        return ResponseEntity.ok(userService.getRecommendedFilms(userId));
    }
}