package fr.filmcritique.backend.repositories;

import fr.filmcritique.backend.entities.Genre;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface GenreRepo extends JpaRepository<Genre, Long> {
    boolean existsByNameIgnoreCase(String name);
}