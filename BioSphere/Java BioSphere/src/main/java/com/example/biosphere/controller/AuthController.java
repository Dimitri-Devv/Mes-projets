package com.example.biosphere.controller;

import jakarta.transaction.Transactional;

import com.example.biosphere.model.User;
import com.example.biosphere.model.EmailVerification;
import com.example.biosphere.repository.EmailVerificationRepository;
import com.example.biosphere.repository.UserRepository;
import com.example.biosphere.service.AuthService;
import com.example.biosphere.service.EmailService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.http.HttpStatus;

import org.springframework.web.bind.annotation.*;

import java.util.Map;
import java.util.Optional;

@RestController
@RequestMapping("/api/auth")
@CrossOrigin(origins = "*")
public class AuthController {

    @Autowired private AuthService authService;
    @Autowired private UserRepository userRepository;
    @Autowired private EmailVerificationRepository emailVerificationRepository;
    @Autowired private EmailService emailService;
    @Autowired private  org.springframework.security.crypto.password.PasswordEncoder passwordEncoder;


    // ✅ REGISTER
    @Transactional
    @PostMapping("/register")
    public ResponseEntity<?> register(@RequestBody Map<String, String> req) {
        String email = req.get("email");
        String username = req.get("username");
        String password = req.get("password");

        if (userRepository.findByEmail(email).isPresent())
            return ResponseEntity.status(409).body(Map.of("error", "Email déjà utilisé"));

        User user = authService.registerUser(email, username, password);

        String code = String.valueOf((int) (Math.random() * 900000) + 100000);
        emailVerificationRepository.deleteByEmail(email);
        emailVerificationRepository.save(
                new EmailVerification(email, code, java.time.LocalDateTime.now().plusMinutes(15))
        );

        try { emailService.sendVerificationEmail(email, code); }
        catch (Exception e) { return ResponseEntity.status(500).body(Map.of("error","Envoi email échoué")); }

        return ResponseEntity.ok(Map.of("message", "📩 Email envoyé, vérifiez votre boîte !"));
    }


    // ✅ VERIFY EMAIL
    @Transactional
    @PostMapping("/verify")
    public ResponseEntity<?> verify(@RequestBody Map<String, String> req) {
        String email = req.get("email");
        String code = req.get("code");

        var verifOpt = emailVerificationRepository.findByEmailAndCode(email, code);

        if (verifOpt.isEmpty()) {
            return ResponseEntity.status(400)
                    .body(Map.of("error", "Code incorrect ou expiré"));
        }

        EmailVerification verif = verifOpt.get();

        if (java.time.LocalDateTime.now().isAfter(verif.getExpiresAt())) {
            emailVerificationRepository.deleteByEmail(email);
            return ResponseEntity.status(400)
                    .body(Map.of("error", "Code expiré, renvoyez-en un ✅"));
        }

        Optional<User> userOpt = userRepository.findByEmail(email);
        if (userOpt.isEmpty()) {
            return ResponseEntity.status(404)
                    .body(Map.of("error", "Utilisateur introuvable"));
        }

        User user = userOpt.get();
        user.setEmailVerified(true);
        userRepository.save(user);

        emailVerificationRepository.deleteByEmail(email);

        return ResponseEntity.ok(
                Map.of("message", "✅ Email vérifié ! Vous pouvez maintenant vous connecter.")
        );
    }


    // ✅ LOGIN BLOCK IF EMAIL NOT VERIFIED
    @Transactional
    @PostMapping("/login")
    public ResponseEntity<?> login(@RequestBody Map<String, String> req) {
        String email = req.get("email");
        String password = req.get("password");

        Optional<User> userOpt = authService.login(email, password);

        if (userOpt.isEmpty())
            return ResponseEntity.status(401).body(Map.of("error", "Identifiants incorrects"));

        User user = userOpt.get();

        if (!user.isEmailVerified())
            return ResponseEntity.status(403).body(Map.of("error", "Email non vérifié. Vérifiez votre boîte mail ✅"));

        return ResponseEntity.ok(user);
    }

    // ✅ GET USER BY ID
    @GetMapping("/{id}")
    public ResponseEntity<?> getUserById(@PathVariable Long id) {
        return userRepository.findById(id)
                .<ResponseEntity<?>>map(ResponseEntity::ok)
                .orElseGet(() -> ResponseEntity.status(HttpStatus.NOT_FOUND)
                        .body(Map.of("error", "Utilisateur non trouvé")));
    }

    // ✅ UPDATE USER PROFILE
    @Transactional
    @PutMapping("/{id}")
    public ResponseEntity<?> updateUser(@PathVariable Long id, @RequestBody Map<String, Object> body) {
        var userOpt = userRepository.findById(id);
        if (userOpt.isEmpty())
            return ResponseEntity.status(404).body(Map.of("error","Utilisateur introuvable"));

        User u = userOpt.get();

        if (body.containsKey("firstName")) u.setFirstName((String) body.get("firstName"));
        if (body.containsKey("lastName"))  u.setLastName((String) body.get("lastName"));
        if (body.containsKey("username"))  u.setUsername((String) body.get("username"));
        if (body.containsKey("bio"))       u.setBio((String) body.get("bio"));
        if (body.containsKey("photoUrl"))  u.setPhotoUrl((String) body.get("photoUrl"));

        userRepository.save(u);

        return ResponseEntity.ok(u);
    }

    // ✅ RESEND CODE
    @Transactional
    @PostMapping("/resend-code")
    public ResponseEntity<?> resendCode(@RequestBody Map<String, String> req) {
        String email = req.get("email");

        if (!userRepository.findByEmail(email).isPresent()) {
            return ResponseEntity.status(404).body(Map.of("error","Utilisateur introuvable"));
        }

        String code = String.valueOf((int)(Math.random() * 900000) + 100000);

        try {
            // Suppression d'ancien code avant d'en générer un nouveau ✅
            emailVerificationRepository.deleteByEmail(email);

            emailVerificationRepository.save(
                    new EmailVerification(email, code, java.time.LocalDateTime.now().plusMinutes(15))
            );

            emailService.sendVerificationEmail(email, code);
            return ResponseEntity.ok(Map.of("message","📩 Nouveau code envoyé !"));
        }
        catch(Exception e) {
            return ResponseEntity.status(500).body(Map.of("error","Envoi email échoué"));
        }
    }

    @PutMapping("/change-password/{id}")
    public ResponseEntity<?> changePassword(@PathVariable Long id, @RequestBody Map<String, String> body) {
        String oldPassword = body.get("oldPassword");
        String newPassword = body.get("newPassword");

        if (oldPassword == null || newPassword == null || oldPassword.isBlank() || newPassword.isBlank()) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body(Map.of("error", "Champs manquants : oldPassword et newPassword requis"));
        }

        Optional<User> userOpt = userRepository.findById(id);
        if (userOpt.isEmpty()) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body(Map.of("error", "Utilisateur introuvable"));
        }

        User user = userOpt.get();

        // 🔐 comparer l'ancien MDP avec le hash stocké
        if (!passwordEncoder.matches(oldPassword, user.getPassword())) {
            return ResponseEntity.status(HttpStatus.UNAUTHORIZED)
                    .body(Map.of("error", "Ancien mot de passe incorrect"));
        }

        // 🔐 enregistrer le NOUVEAU MDP hashé
        user.setPassword(passwordEncoder.encode(newPassword));
        userRepository.save(user);

        return ResponseEntity.ok(Map.of("message", "Mot de passe modifié avec succès"));
    }
}