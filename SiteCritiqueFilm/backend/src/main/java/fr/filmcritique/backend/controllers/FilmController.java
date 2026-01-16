package fr.filmcritique.backend.controllers;

import fr.filmcritique.backend.dtos.film.*;
import fr.filmcritique.backend.services.FilmService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestBody;
import java.util.List;

@RestController
@RequestMapping("/api/films")
@RequiredArgsConstructor
public class FilmController {

    private final FilmService filmService;

    @Operation(
            summary = "Liste tous les films",
            description = "Retourne la liste complète des films disponibles."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Liste récupérée avec succès")
    })
    @GetMapping
    public List<FilmResponseDto> getAllFilms() {
        return filmService.getAllFilms();
    }

    @Operation(
            summary = "Liste allégée des films",
            description = "Retourne uniquement l’identifiant, le titre et l’URL d’affiche de chaque film (utile pour la sélection d’images de fond)."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Liste récupérée avec succès")
    })
    @GetMapping("/short")
    public List<FilmUpdateUser> getShortFilms() {
        return filmService.getShortFilms();
    }

    @Operation(
            summary = "Récupère un film par son ID",
            description = "Retourne le détail complet d’un film via son identifiant."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Film trouvé"),
            @ApiResponse(responseCode = "404", description = "Film introuvable", content = @Content)
    })
    @GetMapping("/{id}")
    public FilmResponseDto getFilmById(@PathVariable Long id) {
        return filmService.getFilmById(id);
    }

    @Operation(
            summary = "Ajoute un nouveau film",
            description = "Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "201", description = "Film créé avec succès"),
            @ApiResponse(responseCode = "400", description = "Données invalides", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content)
    })
    @PostMapping
    @PreAuthorize("hasRole('ADMIN')")
    public ResponseEntity<FilmResponseDto> addFilm(@RequestBody FilmCreateDto dto) {
        FilmResponseDto created = filmService.addFilm(dto);
        return ResponseEntity.status(HttpStatus.CREATED).body(created);
    }

    @Operation(
            summary = "Met à jour un film",
            description = "Modifie les informations d’un film existant. Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Film mis à jour"),
            @ApiResponse(responseCode = "400", description = "Données invalides", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content),
            @ApiResponse(responseCode = "404", description = "Film introuvable", content = @Content)
    })
    @PreAuthorize("hasRole('ADMIN')")
    @PutMapping("/{id}")
    public FilmResponseDto updateFilm(
            @PathVariable Long id,
            @RequestBody FilmUpdateDto dto
    ) {
        return filmService.updateFilm(id, dto);
    }

    @Operation(
            summary = "Supprime un film",
            description = "Supprime un film par son ID. Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "204", description = "Film supprimé avec succès", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content),
            @ApiResponse(responseCode = "404", description = "Film introuvable", content = @Content)
    })
    @PreAuthorize("hasRole('ADMIN')")
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteFilm(@PathVariable Long id) {
        filmService.deleteFilm(id);
        return ResponseEntity.noContent().build();
    }
}