package fr.filmcritique.backend.mappers;

import fr.filmcritique.backend.dtos.film.FilmCreateDto;
import fr.filmcritique.backend.dtos.film.FilmResponse;
import fr.filmcritique.backend.dtos.film.FilmResponseDto;
import fr.filmcritique.backend.dtos.film.FilmUpdateDto;
import fr.filmcritique.backend.entities.Film;
import fr.filmcritique.backend.entities.Genre;

public class FilmMapper {

    public static FilmResponse toLightResponse(Film film) {
        return new FilmResponse(
                film.getId(),
                film.getTitle(),
                film.getAfficheUrl(),
                film.getDateSortie() != null ? film.getDateSortie().toString() : null,
                film.getRatingAverage()
        );
    }

    public static FilmResponseDto toResponse(Film film) {
        return new FilmResponseDto(
                film.getId(),
                film.getTitle(),
                film.getDirector(),
                film.getDateSortie() != null ? film.getDateSortie().toString() : null,
                film.getPosterUrl(),
                film.getSynopsis(),
                film.getAfficheUrl(),
                film.getTrailerUrl(),
                film.getRatingAverage(),
                film.getBaseRating(),
                film.isTendance(),
                film.getGenres().stream().map(Genre::getName).toList()
        );
    }

    public static Film toEntity(FilmCreateDto dto) {
        Film film = new Film();
        film.setTitle(dto.getTitle());
        film.setDirector(dto.getDirector());
        film.setDateSortie(dto.getDateSortie());
        film.setPosterUrl(dto.getPosterUrl());
        film.setAfficheUrl(dto.getAfficheUrl());
        film.setTrailerUrl(dto.getTrailerUrl());
        film.setSynopsis(dto.getSynopsis());
        film.setBaseRating(dto.getBaseRating());
        film.setTendance(dto.getTendance());
        return film;
    }

    public static void updateEntity(Film film, FilmUpdateDto dto) {
        if (dto.getTitle() != null) film.setTitle(dto.getTitle());
        if (dto.getDirector() != null) film.setDirector(dto.getDirector());
        if (dto.getDateSortie() != null) film.setDateSortie(dto.getDateSortie());
        if (dto.getPosterUrl() != null) film.setPosterUrl(dto.getPosterUrl());
        if (dto.getAfficheUrl() != null) film.setAfficheUrl(dto.getAfficheUrl());
        if (dto.getTrailerUrl() != null) film.setTrailerUrl(dto.getTrailerUrl());
        if (dto.getSynopsis() != null) film.setSynopsis(dto.getSynopsis());
        if (dto.getBaseRating() != null) film.setBaseRating(dto.getBaseRating());
        if (dto.getTendance() != null) film.setTendance(dto.getTendance());
    }
}