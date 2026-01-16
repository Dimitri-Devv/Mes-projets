package com.example.biosphere.repository;

import com.example.biosphere.model.Ecosystem;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;

public interface EcosystemRepository extends JpaRepository<Ecosystem, Long> {

    List<Ecosystem> findByUserId(Long userId);

    // Si tu veux charger aussi les relations (parameters/equipments) en une fois:
    @Query("SELECT e FROM Ecosystem e " +
            "LEFT JOIN FETCH e.parameters " +
            "LEFT JOIN FETCH e.equipments " +
            "WHERE e.user.id = :userId")
    List<Ecosystem> findByUserIdWithRelations(@Param("userId") Long userId);
}
