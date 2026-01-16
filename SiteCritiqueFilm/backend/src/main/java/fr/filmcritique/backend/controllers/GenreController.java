package fr.filmcritique.backend.controllers;

import fr.filmcritique.backend.dtos.genre.GenreRequestDto;
import fr.filmcritique.backend.dtos.genre.GenreResponseDto;
import fr.filmcritique.backend.services.GenreService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/genres")
@RequiredArgsConstructor
public class GenreController {

    private final GenreService genreService;

    @Operation(
            summary = "Liste tous les genres",
            description = "Retourne la liste complète des genres disponibles."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Liste récupérée avec succès")
    })
    @GetMapping
    public List<GenreResponseDto> getAllGenres() {
        return genreService.getAllGenres();
    }

    @Operation(
            summary = "Récupère un genre par son ID",
            description = "Retourne le genre correspondant à l’identifiant fourni."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Genre trouvé"),
            @ApiResponse(responseCode = "404", description = "Genre introuvable", content = @Content)
    })
    @GetMapping("/{id}")
    public GenreResponseDto getGenreById(@PathVariable Long id) {
        return genreService.getGenreById(id);
    }

    @Operation(
            summary = "Ajoute un nouveau genre",
            description = "Création d’un genre. Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "201", description = "Genre créé avec succès"),
            @ApiResponse(responseCode = "400", description = "Données invalides", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content)
    })
    @PostMapping
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<GenreResponseDto> addGenre(
            @Valid @RequestBody GenreRequestDto dto
    ) {
        GenreResponseDto created = genreService.addGenre(dto);
        return ResponseEntity.status(HttpStatus.CREATED).body(created);
    }

    @Operation(
            summary = "Met à jour un genre",
            description = "Modification du nom d’un genre existant. Réservé aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Genre mis à jour"),
            @ApiResponse(responseCode = "400", description = "Données invalides", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content),
            @ApiResponse(responseCode = "404", description = "Genre introuvable", content = @Content)
    })
    @PutMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")
    public GenreResponseDto updateGenre(
            @PathVariable Long id,
            @Valid @RequestBody GenreRequestDto dto
    ) {
        return genreService.updateGenre(id, dto);
    }

    @Operation(
            summary = "Supprime un genre",
            description = "Supprime un genre via son identifiant. Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "204", description = "Genre supprimé avec succès", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content),
            @ApiResponse(responseCode = "404", description = "Genre introuvable", content = @Content)
    })
    @DeleteMapping("/{id}")
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<Void> deleteGenre(@PathVariable Long id) {
        genreService.deleteGenre(id);
        return ResponseEntity.noContent().build();
    }
}