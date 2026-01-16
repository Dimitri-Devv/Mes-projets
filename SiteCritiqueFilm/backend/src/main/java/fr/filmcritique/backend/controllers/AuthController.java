package fr.filmcritique.backend.controllers;


import fr.filmcritique.backend.dtos.auth.LoginRequest;
import fr.filmcritique.backend.dtos.auth.LoginResponse;
import fr.filmcritique.backend.dtos.auth.RegisterRequest;
import fr.filmcritique.backend.dtos.auth.VerifyEmailRequest;
import fr.filmcritique.backend.entities.User;

import fr.filmcritique.backend.services.AuthService;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.media.Content;
import io.swagger.v3.oas.annotations.responses.ApiResponse;
import io.swagger.v3.oas.annotations.responses.ApiResponses;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import org.springframework.web.bind.annotation.*;

@RestController
@RequiredArgsConstructor
@RequestMapping("/api/auth")
public class AuthController {

    private final AuthService authService;

    @Operation(
            summary = "Inscription d’un nouvel utilisateur",
            description = """
                Crée un nouveau compte utilisateur.
                Valide les données (email, mot de passe, username),
                enregistre le compte, génère un code OTP et l’envoie par email.
            """
    )
    @ApiResponses({
            @ApiResponse(responseCode = "201", description = "Utilisateur créé avec succès"),
            @ApiResponse(responseCode = "400", description = "Données invalides", content = @Content),
            @ApiResponse(responseCode = "409", description = "Email déjà utilisé", content = @Content)
    })
    @PostMapping("/register")
    public ResponseEntity<?> register(@Valid @RequestBody RegisterRequest request) {
        try {
            User savedUser = authService.register(request);
            savedUser.setPasswordHash(null); // masque le mot de passe
            return ResponseEntity.status(HttpStatus.CREATED).body(savedUser);
        } catch (RuntimeException e) {
            return ResponseEntity.status(HttpStatus.CONFLICT).body(e.getMessage());
        }
    }

    @Operation(
            summary = "Authentification utilisateur",
            description = """
                Vérifie les identifiants (email + mot de passe),
                s’assure que l’email est vérifié via OTP,
                puis génère un JWT signé contenant l'identité et le rôle.
            """
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Authentification réussie (JWT retourné)"),
            @ApiResponse(responseCode = "400", description = "Données invalides", content = @Content),
            @ApiResponse(responseCode = "401", description = "Identifiants incorrects ou email non vérifié", content = @Content)
    })
    @PostMapping("/login")
    public ResponseEntity<?> login(@Valid @RequestBody LoginRequest request) {
        try {
            LoginResponse response = authService.login(request);
            return ResponseEntity.ok(response);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(e.getMessage());
        }
    }

    @Operation(
            summary = "Vérification de l’adresse email (OTP)",
            description = """
                Vérifie qu'un code OTP à 6 chiffres correspond à l'email fourni.
                Active le compte utilisateur si le code est correct et non expiré.
            """
    )
    @ApiResponses({
            @ApiResponse(responseCode = "200", description = "Email vérifié avec succès"),
            @ApiResponse(responseCode = "400", description = "Code invalide ou expiré", content = @Content)
    })
    @PostMapping("/verify")
    public ResponseEntity<?> verify(@Valid @RequestBody VerifyEmailRequest body) {
        try {
            String message = authService.verifyEmail(body.getEmail(), body.getCode());
            return ResponseEntity.ok(message);
        } catch (Exception e) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(e.getMessage());
        }
    }
}