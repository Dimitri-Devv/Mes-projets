package fr.filmcritique.backend.services;

import fr.filmcritique.backend.dtos.genre.GenreRequestDto;
import fr.filmcritique.backend.dtos.genre.GenreResponseDto;
import fr.filmcritique.backend.entities.Genre;
import fr.filmcritique.backend.mappers.GenreMapper;
import fr.filmcritique.backend.repositories.GenreRepo;
import jakarta.persistence.EntityNotFoundException;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class GenreService {

    private final GenreRepo genreRepo;

    public List<GenreResponseDto> getAllGenres() {
        return genreRepo.findAll().stream()
                .map(GenreMapper::toResponse)
                .toList();
    }

    public GenreResponseDto getGenreById(Long id) {
        Genre genre = genreRepo.findById(id)
                .orElseThrow(() ->
                        new EntityNotFoundException("Genre introuvable avec l'id : " + id)
                );

        return GenreMapper.toResponse(genre);
    }

    public GenreResponseDto addGenre(GenreRequestDto dto) {

        if (genreRepo.existsByNameIgnoreCase(dto.getName())) {
            throw new IllegalArgumentException("Ce genre existe déjà : " + dto.getName());
        }

        Genre genre = GenreMapper.toEntity(dto);
        Genre saved = genreRepo.save(genre);

        return GenreMapper.toResponse(saved);
    }

    public GenreResponseDto updateGenre(Long id, GenreRequestDto dto) {

        Genre existing = genreRepo.findById(id)
                .orElseThrow(() ->
                        new EntityNotFoundException("Genre introuvable avec l'id : " + id)
                );

        boolean nameAlreadyUsed = genreRepo.existsByNameIgnoreCase(dto.getName());
        boolean isSameName = existing.getName().equalsIgnoreCase(dto.getName());

        if (nameAlreadyUsed && !isSameName) {
            throw new IllegalArgumentException("Un genre avec ce nom existe déjà : " + dto.getName());
        }

        GenreMapper.updateEntity(existing, dto);
        Genre updated = genreRepo.save(existing);

        return GenreMapper.toResponse(updated);
    }

    public void deleteGenre(Long id) {
        if (!genreRepo.existsById(id)) {
            throw new EntityNotFoundException("Impossible de supprimer : genre inexistant");
        }
        genreRepo.deleteById(id);
    }
}