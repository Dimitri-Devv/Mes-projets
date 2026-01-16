package fr.filmcritique.backend.repositories;

import fr.filmcritique.backend.entities.Film;
import fr.filmcritique.backend.entities.Genre;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface FilmRepo extends JpaRepository<Film, Long> {

    List<Film> findAllByOrderByDateSortieDesc();

    List<Film> findByGenres_NameIn(List<String> genreNames);

    int countByIsTendanceTrue();
}
