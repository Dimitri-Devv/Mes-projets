package fr.filmcritique.backend.repositories;

import fr.filmcritique.backend.entities.Report;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ReportRepo extends JpaRepository<Report, Long> {
}