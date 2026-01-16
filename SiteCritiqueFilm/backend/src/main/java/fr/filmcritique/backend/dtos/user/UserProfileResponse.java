package fr.filmcritique.backend.dtos.user;

import fr.filmcritique.backend.dtos.film.FilmResponseDto;
import fr.filmcritique.backend.dtos.review.ReviewResponseDtoProfil;
import lombok.AllArgsConstructor;
import lombok.Data;

import java.util.List;

@Data
@AllArgsConstructor
public class UserProfileResponse {

    private Long id;
    private String username;
    private String email;
    private String avatarUrl;
    private Long coverFilmId;
    private String bio;
    private String role;


    private List<FilmResponseDto> favoriteFilms;
    private List<ReviewResponseDtoProfil> reviews;
}
