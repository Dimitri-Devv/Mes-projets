package com.example.biosphere.repository;

import com.example.biosphere.model.ParameterRecord;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.List;

public interface ParameterRecordRepository extends JpaRepository<ParameterRecord, Long> {

    // ✅ méthode correcte
    List<ParameterRecord> findByEcosystem_IdAndNameOrderByMeasuredAtAsc(Long ecosystemId, String name);
}

