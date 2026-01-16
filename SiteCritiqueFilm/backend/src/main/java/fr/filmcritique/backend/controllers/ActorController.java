package fr.filmcritique.backend.controllers;

import fr.filmcritique.backend.dtos.actor.ActorCreateDto;
import fr.filmcritique.backend.dtos.actor.ActorResponseDto;
import fr.filmcritique.backend.dtos.actor.ActorUpdateDto;
import fr.filmcritique.backend.dtos.film.FilmShortDto;
import fr.filmcritique.backend.services.ActorService;
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
@RequestMapping("/api/actors")
@RequiredArgsConstructor
public class ActorController {

    private final ActorService actorService;

    @Operation(
            summary = "Liste tous les acteurs",
            description = "Retourne la liste complète des acteurs enregistrés."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Liste récupérée avec succès")
    })
    @GetMapping
    public List<ActorResponseDto> getAllActors() {
        return actorService.getAllActors();
    }

    @Operation(
            summary = "Récupère un acteur par son ID",
            description = "Retourne les informations publiques d’un acteur via son identifiant."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Acteur trouvé"),
            @ApiResponse(responseCode = "404", description = "Acteur introuvable", content = @Content)
    })
    @GetMapping("/{id}")
    public ActorResponseDto getActorById(@PathVariable Long id) {
        return actorService.getActorById(id);
    }

    @Operation(
            summary = "Liste les films d’un acteur",
            description = "Retourne tous les films associés à un acteur donné."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Films récupérés"),
            @ApiResponse(responseCode = "404", description = "Acteur introuvable", content = @Content)
    })
    @GetMapping("/{id}/films")
    public List<FilmShortDto> getFilmsByActor(@PathVariable Long id) {
        return actorService.getFilmsByActor(id);
    }

    @Operation(
            summary = "Liste les acteurs d’un film",
            description = "Retourne la liste des acteurs associés à un film donné."
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Acteurs récupérés"),
            @ApiResponse(responseCode = "404", description = "Film introuvable", content = @Content)
    })
    @GetMapping("/film/{filmId}")
    public List<ActorResponseDto> getActorsByFilm(@PathVariable Long filmId) {
        return actorService.getActorsByFilmId(filmId);
    }

    @Operation(
            summary = "Ajoute un acteur",
            description = "Crée un nouvel acteur. Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "201", description = "Acteur créé"),
            @ApiResponse(responseCode = "400", description = "Données invalides", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : ADMIN requis", content = @Content)
    })
    @PreAuthorize("hasRole('ADMIN')")
    @PostMapping
    public ResponseEntity<ActorResponseDto> addActor(@Valid @RequestBody ActorCreateDto dto) {
        ActorResponseDto created = actorService.addActor(dto);
        return ResponseEntity.status(HttpStatus.CREATED).body(created);
    }

    @Operation(
            summary = "Met à jour un acteur",
            description = "Modifie les informations d’un acteur existant. Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Acteur mis à jour"),
            @ApiResponse(responseCode = "400", description = "Données invalides", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : ADMIN requis", content = @Content),
            @ApiResponse(responseCode = "404", description = "Acteur introuvable", content = @Content)
    })
    @PreAuthorize("hasRole('ADMIN')")
    @PutMapping("/{id}")
    public ActorResponseDto updateActor(
            @PathVariable Long id,
            @Valid @RequestBody ActorUpdateDto dto
    ) {
        return actorService.updateActor(id, dto);
    }

    @Operation(
            summary = "Supprime un acteur",
            description = "Supprime un acteur via son ID. Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "204", description = "Acteur supprimé", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : ADMIN requis", content = @Content),
            @ApiResponse(responseCode = "404", description = "Acteur introuvable", content = @Content)
    })
    @PreAuthorize("hasRole('ADMIN')")
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteActor(@PathVariable Long id) {
        actorService.deleteActor(id);
        return ResponseEntity.noContent().build();
    }

    @Operation(
            summary = "Associe un film à un acteur",
            description = "Crée une relation Acteur <-> Film. Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Association créée"),
            @ApiResponse(responseCode = "404", description = "Film ou acteur introuvable", content = @Content)
    })
    @PreAuthorize("hasRole('ADMIN')")
    @PostMapping("/{actorId}/films/{filmId}")
    public ActorResponseDto addFilmToActor(
            @PathVariable Long actorId,
            @PathVariable Long filmId
    ) {
        return actorService.addFilmToActor(actorId, filmId);
    }

    @Operation(
            summary = "Retire un film d’un acteur",
            description = "Supprime la relation entre un acteur et un film. Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Association supprimée"),
            @ApiResponse(responseCode = "404", description = "Film ou acteur introuvable", content = @Content)
    })
    @PreAuthorize("hasRole('ADMIN')")
    @DeleteMapping("/{actorId}/films/{filmId}")
    public ActorResponseDto removeFilmFromActor(
            @PathVariable Long actorId,
            @PathVariable Long filmId
    ) {
        return actorService.removeFilmFromActor(actorId, filmId);
    }
}