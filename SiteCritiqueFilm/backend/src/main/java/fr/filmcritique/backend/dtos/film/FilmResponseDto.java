package fr.filmcritique.backend.dtos.film;

import lombok.AllArgsConstructor;
import lombok.Data;

import java.util.List;

@Data
@AllArgsConstructor
public class FilmResponseDto {

    private Long id;
    private String title;
    private String director;
    private String dateSortie;
    private String posterUrl;
    private String synopsis;
    private String afficheUrl;
    private String trailerUrl;
    private Double ratingAverage;
    private Double baseRating;
    private boolean isTendance;
    private List<String> genres;
}

