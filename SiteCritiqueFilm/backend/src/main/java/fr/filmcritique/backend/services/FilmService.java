package fr.filmcritique.backend.services;

import fr.filmcritique.backend.dtos.film.*;
import fr.filmcritique.backend.entities.Actor;
import fr.filmcritique.backend.entities.Film;
import fr.filmcritique.backend.entities.Genre;
import fr.filmcritique.backend.entities.Review;
import fr.filmcritique.backend.mappers.FilmMapper;
import fr.filmcritique.backend.repositories.ActorRepo;
import fr.filmcritique.backend.repositories.FilmRepo;
import fr.filmcritique.backend.repositories.GenreRepo;
import fr.filmcritique.backend.repositories.ReviewRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import java.util.List;

@Service
@RequiredArgsConstructor
public class FilmService {

    private final FilmRepo filmRepo;
    private final GenreRepo genreRepo;
    private final ReviewRepo reviewRepo;
    private final ActorRepo actorRepo;

    public List<FilmResponseDto> getAllFilms(){
        return filmRepo.findAllByOrderByDateSortieDesc()
                .stream()
                .map(FilmMapper::toResponse)
                .toList();
    }

    public List<FilmUpdateUser> getShortFilms() {
        return filmRepo.findAllByOrderByDateSortieDesc()
                .stream()
                .map(film -> new FilmUpdateUser(
                        film.getId(),
                        film.getTitle(),
                        film.getAfficheUrl(),
                        film.getPosterUrl()
                ))
                .toList();
    }

    public FilmResponseDto getFilmById(Long id) {
        return filmRepo.findById(id)
                .map(FilmMapper::toResponse)
                .orElseThrow(() -> new RuntimeException("Film introuvable"));
    }

    public FilmResponseDto addFilm(FilmCreateDto dto) {

        Film film = FilmMapper.toEntity(dto);

        // Genres
        if (dto.getGenreIds() != null) {
            List<Genre> genres = dto.getGenreIds().stream()
                    .map(id -> genreRepo.findById(id)
                            .orElseThrow(() -> new RuntimeException("Genre introuvable : " + id)))
                    .toList();
            film.setGenres(genres);
        }

        // Actors
        if (dto.getActorIds() != null) {
            List<Actor> actors = dto.getActorIds().stream()
                    .map(id -> actorRepo.findById(id)
                            .orElseThrow(() -> new RuntimeException("Acteur introuvable : " + id)))
                    .toList();
            film.setActors(actors);
        }

        // Note initiale = baseRating (aucune review)
        film.setRatingAverage(film.getBaseRating());

        Film saved = filmRepo.save(film);
        return FilmMapper.toResponse(saved);
    }

    public FilmResponseDto updateFilm(Long id, FilmUpdateDto dto) {

        Film film = filmRepo.findById(id)
                .orElseThrow(() -> new RuntimeException("Film introuvable"));

        // --- Vérif tendance ---
        if (dto.getTendance() != null) {
            boolean newValue = dto.getTendance();
            boolean oldValue = film.isTendance();

            if (oldValue && !newValue) {
                int tendancesActuelles = filmRepo.countByIsTendanceTrue();
                if (tendancesActuelles <= 5) {
                    throw new RuntimeException(
                            "Impossible de retirer ce film de la tendance : il doit rester au moins 5 films en tendance."
                    );
                }
            }
        }

        // Sauvegarde ancienne base pour détecter changement
        Double oldBase = film.getBaseRating();

        // Update entity fields
        FilmMapper.updateEntity(film, dto);

        // Update genres
        if (dto.getGenreIds() != null) {
            film.getGenres().clear();
            List<Genre> genres = dto.getGenreIds().stream()
                    .map(gid -> genreRepo.findById(gid)
                            .orElseThrow(() -> new RuntimeException("Genre introuvable : " + gid)))
                    .toList();
            film.getGenres().addAll(genres);
        }

        // Update actors
        if (dto.getActorIds() != null) {
            film.getActors().clear();
            List<Actor> actors = dto.getActorIds().stream()
                    .map(aid -> actorRepo.findById(aid)
                            .orElseThrow(() -> new RuntimeException("Acteur introuvable : " + aid)))
                    .toList();
            film.getActors().addAll(actors);
        }

        Film updated = filmRepo.save(film);

        // Si baseRating a changé → recalcul de la moyenne
        boolean baseChanged = dto.getBaseRating() != null
                && !dto.getBaseRating().equals(oldBase);

        if (baseChanged) {
            List<Review> reviews = reviewRepo.findByFilmId(updated.getId());
            double base = updated.getBaseRating() != null ? updated.getBaseRating() : 0.0;

            if (reviews.isEmpty()) {
                updated.setRatingAverage(base);
            } else {
                double sum = reviews.stream().mapToDouble(Review::getRating).sum();
                double avg = (base + sum) / (reviews.size() + 1);
                updated.setRatingAverage(avg);
            }

            updated = filmRepo.save(updated);
        }

        return FilmMapper.toResponse(updated);
    }

    public void deleteFilm(Long id) {
        if (!filmRepo.existsById(id)) {
            throw new RuntimeException("Film introuvable");
        }
        filmRepo.deleteById(id);
    }

    // Utilisé par ReviewService si besoin
    public void updateFilmRating(Long filmId) {
        Film film = filmRepo.findById(filmId)
                .orElseThrow(() -> new RuntimeException("Film introuvable"));

        List<Review> reviews = reviewRepo.findByFilmId(filmId);
        double base = film.getBaseRating() != null ? film.getBaseRating() : 0.0;

        if (reviews.isEmpty()) {
            film.setRatingAverage(base);
        } else {
            double sum = reviews.stream().mapToDouble(Review::getRating).sum();
            film.setRatingAverage((base + sum) / (reviews.size() + 1));
        }

        filmRepo.save(film);
    }
}