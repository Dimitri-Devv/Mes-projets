package com.example.biosphere.repository;

import com.example.biosphere.model.Inhabitant;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.List;

public interface InhabitantRepository extends JpaRepository<Inhabitant, Long> {
    List<Inhabitant> findByEcosystemId(Long ecosystemId);
}