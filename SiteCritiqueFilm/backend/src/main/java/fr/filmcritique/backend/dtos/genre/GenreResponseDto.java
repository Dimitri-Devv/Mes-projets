package fr.filmcritique.backend.dtos.genre;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class GenreResponseDto {
    private Long id;
    private String name;
}
