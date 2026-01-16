package com.example.biosphere.service;

import com.example.biosphere.controller.EquipmentController;
import com.example.biosphere.controller.EquipmentController.CreateEquipmentRequest;
import com.example.biosphere.model.Ecosystem;
import com.example.biosphere.model.Equipment;
import com.example.biosphere.repository.EcosystemRepository;
import com.example.biosphere.repository.EquipmentRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class EquipmentService {
    @Autowired private EquipmentRepository equipmentRepository;
    @Autowired private EcosystemRepository ecosystemRepository;

    public List<Equipment> listByEcosystem(Long ecosystemId) {
        return equipmentRepository.findByEcosystemId(ecosystemId);
    }

    public Equipment addEquipment(EquipmentController.CreateEquipmentRequest req) {
        Ecosystem eco = ecosystemRepository.findById(req.getEcosystemId())
                .orElseThrow(() -> new RuntimeException("Ecosystème introuvable: " + req.getEcosystemId()));
        Equipment e = new Equipment();
        e.setName(req.getName()); e.setBrand(req.getBrand()); e.setDetails(req.getDetails()); e.setEcosystem(eco);
        return equipmentRepository.save(e);
    }

    public void deleteEquipment(Long id) { equipmentRepository.deleteById(id); }


}



