package fr.filmcritique.backend.repositories;

import fr.filmcritique.backend.entities.Review;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface ReviewRepo extends JpaRepository<Review, Long> {
    List<Review> findByFilmId(Long filmId);
    List<Review> findTop10ByOrderByLikesCountDesc();
    List<Review> findByUserId(Long userId);
}
