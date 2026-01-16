package fr.filmcritique.backend.controllers;

import fr.filmcritique.backend.dtos.admin.AdminUpdateUserDto;
import fr.filmcritique.backend.dtos.admin.UpdateRoleDto;
import fr.filmcritique.backend.dtos.user.UserResponse;
import fr.filmcritique.backend.entities.User;
import fr.filmcritique.backend.services.AdminUserService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import io.swagger.v3.oas.annotations.security.SecurityRequirement;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.security.access.prepost.PreAuthorize;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/admin")
@PreAuthorize("hasRole('ADMIN')")
public class AdminUserController {

    private final AdminUserService adminUserService;

    @Operation(
            summary = "Liste tous les utilisateurs",
            description = "Accessible uniquement aux administrateurs.",
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Liste récupérée avec succès"),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content)
    })
    @GetMapping("/users")
    public ResponseEntity<List<UserResponse>> getAllUsers() {
        return ResponseEntity.ok(adminUserService.getAllUsers());
    }


    @Operation(
            summary = "Met à jour un utilisateur",
            description = """
                Modifie les informations d’un utilisateur (avatar, avertissements, statut, etc.).
                Le rôle n'est pas modifié ici. Accessible uniquement aux administrateurs.
            """,
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Utilisateur mis à jour avec succès"),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content),
            @ApiResponse(responseCode = "404", description = "Utilisateur introuvable", content = @Content),
    })
    @PutMapping("/users/{id}")
    public ResponseEntity<UserResponse> updateUser(
            @PathVariable Long id,
            @RequestBody AdminUpdateUserDto dto
    ) {
        return ResponseEntity.ok(adminUserService.updateUser(id, dto));
    }


    @Operation(
            summary = "Supprime un utilisateur",
            description = """
                Supprime un utilisateur du système.
                Un garde-fou empêche la suppression d'un administrateur.
                Accessible uniquement aux administrateurs.
            """,
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "204", description = "Utilisateur supprimé avec succès", content = @Content),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content),
            @ApiResponse(responseCode = "404", description = "Utilisateur introuvable", content = @Content),
    })
    @DeleteMapping("/users/{id}")
    public ResponseEntity<Void> deleteUser(@PathVariable Long id) {
        adminUserService.deleteUser(id);
        return ResponseEntity.noContent().build();
    }


    @Operation(
            summary = "Modifie le rôle d’un utilisateur",
            description = """
                Permet de changer le rôle d’un utilisateur (USER → ADMIN, etc.).
                La logique métier empêche des cas sensibles (ex : retirer son propre rôle admin).
                Accessible uniquement aux administrateurs.
            """,
            security = @SecurityRequirement(name = "bearerAuth")
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Rôle mis à jour avec succès"),
            @ApiResponse(responseCode = "401", description = "Token manquant ou invalide", content = @Content),
            @ApiResponse(responseCode = "403", description = "Accès refusé : rôle ADMIN requis", content = @Content),
            @ApiResponse(responseCode = "404", description = "Utilisateur introuvable", content = @Content),
    })
    @PutMapping("/users/{id}/role")
    public ResponseEntity<User> updateRole(
            @PathVariable Long id,
            @RequestBody UpdateRoleDto dto
    ) {
        return ResponseEntity.ok(adminUserService.updateRole(id, dto));
    }
}