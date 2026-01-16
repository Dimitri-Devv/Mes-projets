package fr.filmcritique.backend.dtos.review;

import jakarta.validation.constraints.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import org.hibernate.validator.constraints.URL;

@Data
@AllArgsConstructor
public class ReviewUpdateDto {

    @NotBlank(message = "Le titre est obligatoire")
    @Size(min = 1, max = 150)
    private String title;

    @NotBlank(message = "Le contenu est obligatoire")
    @Size(min = 10, max = 3000)
    private String content;

    @NotNull(message = "La note est obligatoire")
    @Min(1)
    @Max(10)
    private Integer rating;

}