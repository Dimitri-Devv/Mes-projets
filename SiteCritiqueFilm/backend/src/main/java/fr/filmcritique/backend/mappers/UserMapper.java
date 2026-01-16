package fr.filmcritique.backend.mappers;

import fr.filmcritique.backend.dtos.film.FilmResponseDto;
import fr.filmcritique.backend.dtos.review.ReviewResponseDtoProfil;
import fr.filmcritique.backend.dtos.user.UserProfileResponse;
import fr.filmcritique.backend.dtos.user.UserResponse;
import fr.filmcritique.backend.entities.User;

public class UserMapper {

    public static UserResponse toResponse(User user) {
        return new UserResponse(
                user.getId(),
                user.getUsername(),
                user.getEmail(),
                user.getAvatarUrl(),
                user.getCoverFilmId(),
                user.getRole().name(),
                user.isEmailVerified(),
                user.isBlocked(),
                user.getAvertissement()
        );
    }

    public static UserProfileResponse toProfile(User user) {
        return new UserProfileResponse(
                user.getId(),
                user.getUsername(),
                user.getEmail(),
                user.getAvatarUrl(),
                user.getCoverFilmId(),
                user.getBio(),
                user.getRole().name(),


                // FAVORIS AVEC GENRES !!
                user.getFavoriteFilms()
                        .stream()
                        .map(f -> new FilmResponseDto(
                                f.getId(),
                                f.getTitle(),
                                f.getDirector(),
                                f.getDateSortie().toString(),
                                f.getPosterUrl(),
                                f.getSynopsis(),
                                f.getAfficheUrl(),
                                f.getTrailerUrl(),
                                f.getRatingAverage(),
                                f.getBaseRating(),
                                f.isTendance(),
                                f.getGenres().stream()
                                        .map(g -> g.getName())
                                        .toList()
                        ))
                        .toList(),

                // CRITIQUES
                user.getReviews()
                        .stream()
                        .map(r -> new ReviewResponseDtoProfil(
                                r.getId(),
                                r.getTitle(),
                                r.getContent(),
                                r.getRating(),
                                r.getCreatedAt().toString(),
                                r.getFilm().getId(),
                                r.getFilm().getTitle(),
                                r.getFilm().getAfficheUrl(),
                                r.getUser().getId(),
                                r.getUser().getUsername(),
                                r.getUser().getAvatarUrl(),
                                r.getLikesCount(),
                                r.getDislikesCount()
                        ))
                        .toList()
        );
    }
}