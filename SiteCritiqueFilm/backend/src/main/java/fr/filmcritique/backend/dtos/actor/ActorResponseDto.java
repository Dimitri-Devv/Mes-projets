package fr.filmcritique.backend.dtos.actor;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class ActorResponseDto {
    private Long id;
    private String nom;
    private String prenom;
    private String bio;
    private String avatarUrl;
}