package fr.filmcritique.backend.dtos.film;

import com.fasterxml.jackson.annotation.JsonFormat;
import jakarta.validation.constraints.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import org.hibernate.validator.constraints.URL;

import java.time.LocalDate;
import java.util.List;

@Data
@AllArgsConstructor
public class FilmUpdateDto {

    @NotBlank(message = "Le titre est obligatoire")
    @Size(min = 1, max = 150)
    private String title;

    @NotBlank(message = "Le nom du réalisateur est obligatoire")
    @Size(min = 1, max = 100)
    private String director;

    @JsonFormat(pattern = "yyyy-MM-dd")
    @PastOrPresent(message = "La date de sortie doit être passée ou aujourd'hui")
    private LocalDate dateSortie;

    @NotBlank(message = "L'URL du poster est obligatoire")
    @URL(message = "URL de poster invalide")
    private String posterUrl;

    @NotBlank(message = "L'URL de l'affiche est obligatoire")
    @URL(message = "URL d'affiche invalide")
    private String afficheUrl;

    @URL(message = "URL de bande-annonce invalide")
    private String trailerUrl;

    @NotBlank(message = "Le synopsis est obligatoire")
    @Size(min = 10, max = 1500)
    private String synopsis;

    @NotNull(message = "La note de base est obligatoire")
    @DecimalMin(value = "0.0", message = "La note doit être au minimum 0")
    @DecimalMax(value = "10.0", message = "La note doit être au maximum 10")
    private Double baseRating;

    @NotNull(message = "Le champ tendance doit être défini")
    private Boolean tendance;

    @NotEmpty(message = "La liste des genres ne peut pas être vide")
    private List<Long> genreIds;

    @NotEmpty(message = "La liste des acteurs ne peut pas être vide")
    private List<Long> actorIds;
}

