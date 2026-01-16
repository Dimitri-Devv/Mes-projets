package com.example.biosphere.service;

import com.example.biosphere.controller.ParameterController;
import com.example.biosphere.controller.ParameterController.AddRecordRequest;
import com.example.biosphere.model.Ecosystem;
import com.example.biosphere.model.ParameterRecord;
import com.example.biosphere.repository.EcosystemRepository;
import com.example.biosphere.repository.ParameterRecordRepository;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
public class ParameterService {
    @Autowired private ParameterRecordRepository recordRepository;
    @Autowired private EcosystemRepository ecosystemRepository;

    public List<ParameterRecord> getHistory(Long ecosystemId, String name) {
        return recordRepository.findByEcosystem_IdAndNameOrderByMeasuredAtAsc(ecosystemId, name);
    }

    public ParameterRecord addRecord(ParameterController.AddRecordRequest req) {
        Ecosystem eco = ecosystemRepository.findById(req.getEcosystemId())
                .orElseThrow(() -> new RuntimeException("Ecosystème introuvable: " + req.getEcosystemId()));
        ParameterRecord r = new ParameterRecord();
        r.setEcosystem(eco); r.setName(req.getName()); r.setValue(req.getValue());
        r.setMeasuredAt(req.getMeasuredAt()!=null ? req.getMeasuredAt() : LocalDateTime.now());
        return recordRepository.save(r);
    }

    @Transactional
    public void deleteRecord(Long id) {
        recordRepository.deleteById(id);
    }
}
