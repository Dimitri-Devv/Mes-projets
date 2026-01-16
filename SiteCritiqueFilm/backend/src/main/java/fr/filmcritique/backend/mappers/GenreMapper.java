package fr.filmcritique.backend.mappers;

import fr.filmcritique.backend.dtos.genre.GenreRequestDto;
import fr.filmcritique.backend.dtos.genre.GenreResponseDto;
import fr.filmcritique.backend.entities.Genre;

public class GenreMapper {

    public static GenreResponseDto toResponse(Genre genre) {
        if (genre == null) return null;

        return new GenreResponseDto(
                genre.getId(),
                genre.getName()
        );
    }

    public static Genre toEntity(GenreRequestDto dto) {
        if (dto == null) return null;

        Genre genre = new Genre();
        genre.setName(dto.getName());

        return genre;
    }

    public static void updateEntity(Genre genre, GenreRequestDto dto) {
        if (genre == null || dto == null) return;

        genre.setName(dto.getName());
    }
}