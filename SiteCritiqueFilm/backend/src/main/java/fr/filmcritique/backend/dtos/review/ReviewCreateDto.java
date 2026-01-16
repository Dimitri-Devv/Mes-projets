package fr.filmcritique.backend.dtos.review;

import jakarta.validation.constraints.*;
import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class ReviewCreateDto {

    @NotBlank(message = "Le titre est obligatoire")
    @Size(min = 1, max = 150, message = "Le titre doit faire entre 1 et 150 caractères")
    private String title;

    @NotBlank(message = "Le contenu est obligatoire")
    @Size(min = 10, max = 3000, message = "La critique doit faire entre 10 et 3000 caractères")
    private String content;

    @NotNull(message = "La note est obligatoire")
    @Min(value = 1, message = "La note minimale est 1")
    @Max(value = 10, message = "La note maximale est 10")
    private Integer rating;

}