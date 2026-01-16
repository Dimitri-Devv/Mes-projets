package fr.filmcritique.backend.repositories;

import fr.filmcritique.backend.entities.Actor;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface ActorRepo extends JpaRepository<Actor, Long> {

    boolean existsByNomIgnoreCaseAndPrenomIgnoreCase(String nom, String prenom);

}