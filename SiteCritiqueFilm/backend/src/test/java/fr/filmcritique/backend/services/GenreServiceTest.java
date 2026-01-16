package fr.filmcritique.backend.services;

import fr.filmcritique.backend.dtos.genre.GenreRequestDto;
import fr.filmcritique.backend.dtos.genre.GenreResponseDto;
import fr.filmcritique.backend.entities.Genre;
import fr.filmcritique.backend.repositories.GenreRepo;
import jakarta.persistence.EntityNotFoundException;
import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

@ExtendWith(MockitoExtension.class)
class GenreServiceTest {

    @Mock
    private GenreRepo genreRepo;

    @InjectMocks
    private GenreService genreService;

    @Test
    @DisplayName("Créer un genre lorsque le nom est unique")
    void creerGenre_quandNomUnique() {
        GenreRequestDto dto = new GenreRequestDto();
        dto.setName("Action");

        when(genreRepo.existsByNameIgnoreCase("Action")).thenReturn(false);
        when(genreRepo.save(any(Genre.class))).thenAnswer(invocation -> {
            Genre g = invocation.getArgument(0);
            g.setId(1L);
            return g;
        });

        GenreResponseDto resultat = genreService.addGenre(dto);

        assertEquals("Action", resultat.getName());
        assertEquals(1L, resultat.getId());
        verify(genreRepo).save(any(Genre.class));
    }

    @Test
    @DisplayName("Doit refuser la création si le nom existe déjà")
    void creerGenre_lorsqueNomExisteDeja() {
        GenreRequestDto dto = new GenreRequestDto();
        dto.setName("Action");

        when(genreRepo.existsByNameIgnoreCase("Action")).thenReturn(true);

        assertThrows(IllegalArgumentException.class, () -> genreService.addGenre(dto));
    }

    @Test
    @DisplayName("Récupérer un genre par ID – doit réussir si le genre existe")
    void getGenreById_doitReussir_siGenreExiste() {

        Genre genre = new Genre();
        genre.setId(1L);
        genre.setName("Action");

        when(genreRepo.findById(1L)).thenReturn(Optional.of(genre));

        GenreResponseDto resultat = genreService.getGenreById(1L);

        assertEquals(1L, resultat.getId());
        assertEquals("Action", resultat.getName());
    }
    @Test
    @DisplayName("Récupérer un genre par ID – doit échouer si le genre n'existe pas")
    void getGenreById_doitEchouer_siGenreInexistant() {

        when(genreRepo.findById(1L)).thenReturn(Optional.empty());

        assertThrows(EntityNotFoundException.class, () -> genreService.getGenreById(1L));
    }

    @Test
    @DisplayName("Mettre à jour un genre – doit échouer si le genre n'existe pas")
    void updateGenre_doitEchouer_siGenreInexistant() {

        GenreRequestDto dto = new GenreRequestDto();
        dto.setName("Action");

        when(genreRepo.findById(99L)).thenReturn(Optional.empty());

        assertThrows(EntityNotFoundException.class, () -> genreService.updateGenre(99L, dto));
    }

    @Test
    @DisplayName("Mettre à jour un genre – doit échouer si le nouveau nom existe déjà pour un autre genre")
    void updateGenre_doitEchouer_siNomDejaExistant() {

        Genre existing = new Genre();
        existing.setId(1L);
        existing.setName("Action");

        GenreRequestDto dto = new GenreRequestDto();
        dto.setName("Comédie");

        when(genreRepo.findById(1L)).thenReturn(Optional.of(existing));
        when(genreRepo.existsByNameIgnoreCase("Comédie")).thenReturn(true);

        assertThrows(IllegalArgumentException.class, () -> genreService.updateGenre(1L, dto));
    }

    @Test
    @DisplayName("Mettre à jour un genre – doit réussir si le nom reste identique")
    void updateGenre_doitReussir_siNomIdentique() {

        Genre existing = new Genre();
        existing.setId(1L);
        existing.setName("Action");

        GenreRequestDto dto = new GenreRequestDto();
        dto.setName("Action");

        when(genreRepo.findById(1L)).thenReturn(Optional.of(existing));
        when(genreRepo.existsByNameIgnoreCase("Action")).thenReturn(true);

        when(genreRepo.save(any(Genre.class)))
                .thenAnswer(invocation -> invocation.getArgument(0));

        GenreResponseDto resultat = genreService.updateGenre(1L, dto);

        assertEquals("Action", resultat.getName());
        verify(genreRepo, times(1)).save(existing);
    }

    @Test
    @DisplayName("Mettre à jour un genre – doit réussir si le nom est valide et non utilisé")
    void updateGenre_doitReussir_siNomValide() {

        Genre existing = new Genre();
        existing.setId(1L);
        existing.setName("Action");

        GenreRequestDto dto = new GenreRequestDto();
        dto.setName("Aventure");

        when(genreRepo.findById(1L)).thenReturn(Optional.of(existing));
        when(genreRepo.existsByNameIgnoreCase("Aventure")).thenReturn(false);

        when(genreRepo.save(any(Genre.class)))
                .thenAnswer(invocation -> invocation.getArgument(0));

        GenreResponseDto resultat = genreService.updateGenre(1L, dto);

        assertEquals("Aventure", resultat.getName());
        verify(genreRepo, times(1)).save(existing);
    }

    @Test
    @DisplayName("Supprimer un genre – doit échouer si le genre n'existe pas")
    void deleteGenre_doitEchouer_siGenreInexistant() {

        when(genreRepo.existsById(1L)).thenReturn(false);

        assertThrows(EntityNotFoundException.class, () -> genreService.deleteGenre(1L));
    }

    @Test
    @DisplayName("Supprimer un genre – doit réussir si le genre existe")
    void deleteGenre_doitReussir_siGenreExiste() {

        when(genreRepo.existsById(1L)).thenReturn(true);

        genreService.deleteGenre(1L);

        verify(genreRepo).deleteById(1L);
    }
}