package fr.filmcritique.backend.dtos.film;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class FilmResponse {
    private Long id;
    private String title;
    private String afficheUrl;
    private String dateSortie;
    private Double ratingAverage;
}
