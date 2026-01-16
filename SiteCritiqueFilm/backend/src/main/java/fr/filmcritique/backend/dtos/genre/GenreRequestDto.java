package fr.filmcritique.backend.dtos.genre;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.Data;

@Data
public class GenreRequestDto {

    @NotBlank(message = "Le nom du genre est obligatoire")
    @Size(max = 100, message = "Le nom du genre doit faire maximum 100 caractères")
    private String name;
}

