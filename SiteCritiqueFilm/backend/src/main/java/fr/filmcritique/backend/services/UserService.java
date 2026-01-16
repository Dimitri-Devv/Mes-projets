package fr.filmcritique.backend.services;

import fr.filmcritique.backend.dtos.admin.AdminUpdateUserDto;
import fr.filmcritique.backend.dtos.film.FilmResponse;
import fr.filmcritique.backend.dtos.film.FilmResponseDto;
import fr.filmcritique.backend.dtos.user.FavoriteFilmsResponse;
import fr.filmcritique.backend.dtos.user.UpdateUserDto;
import fr.filmcritique.backend.dtos.user.UserProfileResponse;
import fr.filmcritique.backend.entities.Film;
import fr.filmcritique.backend.entities.Genre;
import fr.filmcritique.backend.entities.User;
import fr.filmcritique.backend.entities.VerificationCode;
import fr.filmcritique.backend.enums.UserRole;
import fr.filmcritique.backend.mappers.FilmMapper;
import fr.filmcritique.backend.mappers.UserMapper;
import fr.filmcritique.backend.repositories.FilmRepo;
import fr.filmcritique.backend.repositories.UserRepo;
import fr.filmcritique.backend.repositories.VerificationCodeRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;


import java.time.LocalDateTime;
import java.util.List;

@Service
@RequiredArgsConstructor
public class UserService implements UserDetailsService {

    private final UserRepo userRepo;
    private final BCryptPasswordEncoder passwordEncoder = new BCryptPasswordEncoder();
    private final VerificationCodeRepo codeRepo;
    private final EmailService emailService;
    private final FilmRepo filmRepo;

    public User registerUser(User user) {

        if (userRepo.findByEmail(user.getEmail()).isPresent()) {
            throw new RuntimeException("Email déjà utilisé");
        }

        user.setPasswordHash(passwordEncoder.encode(user.getPasswordHash()));

        if (user.getRole() == null) {
            user.setRole(UserRole.USER);
        }

        user.setEmailVerified(false);

        User savedUser = userRepo.save(user);

        // Génération du code OTP
        String code = String.valueOf((int) ((Math.random() * 900000) + 100000));
        VerificationCode otp = new VerificationCode();
        otp.setEmail(savedUser.getEmail());
        otp.setCode(code);
        otp.setExpiration(LocalDateTime.now().plusMinutes(10));
        codeRepo.save(otp);

        // Envoi de l’email
        emailService.sendVerificationEmail(savedUser.getEmail(), savedUser.getUsername(), code);

        return savedUser;
    }

    @Transactional
    public String verifyEmail(String email, String code) {
        VerificationCode otp = codeRepo.findByEmailAndCode(email, code)
                .orElseThrow(() -> new RuntimeException("Code invalide ou email inconnu"));

        if (otp.isExpired()) {
            codeRepo.delete(otp);
            throw new RuntimeException("Code expiré");
        }

        User user = userRepo.findByEmail(email)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        user.setEmailVerified(true);
        userRepo.save(user);

        codeRepo.deleteByEmail(email);

        return "Email vérifié avec succès";
    }

    public UserProfileResponse getUserProfile(Long id) {
        User user = userRepo.findById(id)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        return UserMapper.toProfile(user);
    }

    public User updateSelf(String email, UpdateUserDto dto) {
        User user = userRepo.findByEmail(email)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        if (dto.getAvatarUrl() != null) {
            user.setAvatarUrl(dto.getAvatarUrl());
        }

        if (dto.getCoverFilmId() != null) {
            Film film = filmRepo.findById(dto.getCoverFilmId())
                    .orElseThrow(() -> new RuntimeException("Film de couverture introuvable"));
            user.setCoverFilmId(film.getId());
        }

        if (dto.getCoverFilmId() != null) {
            Film coverFilm = filmRepo.findById(dto.getCoverFilmId())
                    .orElseThrow(() -> new RuntimeException("Film de couverture introuvable"));
            user.setCoverFilmId(coverFilm.getId());
        }

        if (dto.getBio() != null) {
            user.setBio(dto.getBio());
        }

        return userRepo.save(user);
    }

    public void deleteByEmail(String email) {
        User user = userRepo.findByEmail(email)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        userRepo.delete(user);
    }

    public FavoriteFilmsResponse getFavoriteFilms(Long userId) {
        User user = userRepo.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        List<FilmResponse> favorites = user.getFavoriteFilms()
                .stream()
                .map(FilmMapper::toLightResponse)
                .toList();

        return new FavoriteFilmsResponse(user.getId(), favorites);
    }

    public FavoriteFilmsResponse addFavoriteFilm(Long userId, Long filmId) {
        User user = userRepo.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        Film film = filmRepo.findById(filmId)
                .orElseThrow(() -> new RuntimeException("Film introuvable"));

        if (!user.getFavoriteFilms().contains(film)) {
            user.getFavoriteFilms().add(film);
            userRepo.save(user);
        }

        List<FilmResponse> favorites = user.getFavoriteFilms()
                .stream()
                .map(FilmMapper::toLightResponse)
                .toList();

        return new FavoriteFilmsResponse(user.getId(), favorites);
    }

    public FavoriteFilmsResponse removeFavoriteFilm(Long userId, Long filmId) {
        User user = userRepo.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        Film film = filmRepo.findById(filmId)
                .orElseThrow(() -> new RuntimeException("Film introuvable"));

        user.getFavoriteFilms().remove(film);

        userRepo.save(user);

        List<FilmResponse> favorites = user.getFavoriteFilms()
                .stream()
                .map(FilmMapper::toLightResponse)
                .toList();

        return new FavoriteFilmsResponse(user.getId(), favorites);
    }

    public Long getUserIdFromEmail(String email) {
        return userRepo.findByEmail(email)
                .map(User::getId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));
    }

    @Override
    public UserDetails loadUserByUsername(String email) throws UsernameNotFoundException {
        User user = userRepo.findByEmail(email)
                .orElseThrow(() -> new UsernameNotFoundException("Utilisateur non trouvé : " + email));

        return org.springframework.security.core.userdetails.User
                .builder()
                .username(user.getEmail())
                .password(user.getPasswordHash())
                .roles(user.getRole().name())
                .build();
    }
    public List<FilmResponseDto> getRecommendedFilms(Long userId) {

        User user = userRepo.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        List<Film> favorites = user.getFavoriteFilms();

        if (favorites.isEmpty()) {
            return List.of();
        }

        List<String> genreNames = favorites.stream()
                .flatMap(f -> f.getGenres().stream())
                .map(Genre::getName)
                .distinct()
                .toList();

        List<Film> recommended = filmRepo.findByGenres_NameIn(genreNames)
                .stream()
                .filter(f -> !favorites.contains(f))
                .distinct()
                .limit(4)
                .toList();

        return recommended.stream()
                .map(FilmMapper::toResponse)
                .toList();
    }
}