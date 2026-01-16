package fr.filmcritique.backend.dtos.film;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class FilmUpdateUser {
    private Long id;
    private String titre;
    private String afficheUrl;
    private String posterUrl;
}
