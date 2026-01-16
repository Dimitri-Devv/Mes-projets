package fr.filmcritique.backend.dtos.film;

import com.fasterxml.jackson.annotation.JsonFormat;
import jakarta.validation.constraints.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import org.hibernate.validator.constraints.URL;
import jakarta.validation.constraints.DecimalMin;
import jakarta.validation.constraints.DecimalMax;

import java.time.LocalDate;
import java.util.List;

@Data
@AllArgsConstructor
public class FilmCreateDto {

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
    @URL(message = "URL de l'affiche invalide")
    private String afficheUrl;

    @URL(message = "URL de la bande-annonce invalide")
    private String trailerUrl;

    @NotBlank(message = "Le synopsis est obligatoire")
    @Size(min = 10, max = 1500, message = "Le synopsis doit faire entre 10 et 1500 caractères")
    private String synopsis;

    @NotNull(message = "Le champ tendance doit être défini")
    private Boolean tendance;

    @NotNull(message = "La note de base est obligatoire")
    @DecimalMin(value = "0.0", message = "La note doit être au minimum 0")
    @DecimalMax(value = "10.0", message = "La note doit être au maximum 10")
    private Double baseRating;

    @NotEmpty(message = "Au moins un genre doit être sélectionné")
    private List<Long> genreIds;

    @NotEmpty(message = "Au moins un acteur doit être sélectionné")
    private List<Long> actorIds;
}

