package fr.filmcritique.backend.dtos.actor;

import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.hibernate.validator.constraints.URL;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class ActorCreateDto {
    @NotBlank
    private String nom;

    @NotBlank
    private String prenom;

    @NotBlank
    @Size(min = 10, max = 500, message = "La bio doit contenir entre 10 et 500 caractères")
    private String bio;

    @URL(message = "L'URL de l'avatar doit être valide")
    private String avatarUrl;
}