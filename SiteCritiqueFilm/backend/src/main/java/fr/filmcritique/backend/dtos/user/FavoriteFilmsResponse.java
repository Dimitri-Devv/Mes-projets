package fr.filmcritique.backend.dtos.user;

import fr.filmcritique.backend.dtos.film.FilmResponse;
import lombok.AllArgsConstructor;
import lombok.Data;

import java.util.List;

@Data
@AllArgsConstructor
public class FavoriteFilmsResponse {
    private Long userId;
    private List<FilmResponse> favorites;
}

