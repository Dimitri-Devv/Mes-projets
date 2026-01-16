package fr.filmcritique.backend.services;

import fr.filmcritique.backend.dtos.auth.LoginRequest;
import fr.filmcritique.backend.dtos.auth.LoginResponse;
import fr.filmcritique.backend.dtos.auth.RegisterRequest;
import fr.filmcritique.backend.entities.User;
import fr.filmcritique.backend.enums.UserRole;
import fr.filmcritique.backend.repositories.UserRepo;
import fr.filmcritique.backend.securities.JwtUtil;
import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class AuthService {

    private final UserRepo userRepo;
    private final AuthenticationManager authenticationManager;
    private final JwtUtil jwtUtil;
    private final UserService userService;

    public User register(RegisterRequest request) {
        User user = new User(
                request.getUsername(),
                request.getEmail(),
                request.getPassword(),
                UserRole.USER
        );
        return userService.registerUser(user);
    }

    public LoginResponse login(LoginRequest request) {
        String email = request.getEmail();
        String rawPassword = request.getPassword();

        User user = userRepo.findByEmail(email)
                .orElseThrow(() -> new RuntimeException("Les identifiants sont erronés"));

        if (!user.isEmailVerified()) {
            throw new RuntimeException("Veuillez vérifier votre adresse email avant de vous connecter");
        }

        Authentication authentication = authenticationManager.authenticate(
                new UsernamePasswordAuthenticationToken(email, rawPassword)
        );

        UserDetails userDetails = (UserDetails) authentication.getPrincipal();
        String role = userDetails.getAuthorities().stream()
                .findFirst()
                .map(auth -> auth.getAuthority().replace("ROLE_", ""))
                .orElse(UserRole.USER.name());

        String token = jwtUtil.generateToken(user.getId(), user.getEmail(), role);

        return new LoginResponse(
                user.getId(),
                token,
                user.getEmail(),
                role
        );
    }

    public String verifyEmail(String email, String code) {
        return userService.verifyEmail(email, code);
    }
}