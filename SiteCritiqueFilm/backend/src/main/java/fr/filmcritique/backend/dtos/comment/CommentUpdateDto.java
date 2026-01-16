package fr.filmcritique.backend.dtos.comment;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.Data;

@Data
public class CommentUpdateDto {

    @NotBlank(message = "Le contenu du commentaire est obligatoire")
    @Size(min = 1, max = 1000)
    private String content;
}