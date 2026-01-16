package com.example.biosphere.service;

import com.example.biosphere.controller.EcosystemController;
import com.example.biosphere.controller.EcosystemController.EcosystemCreateRequest;
import com.example.biosphere.controller.EcosystemController.EcosystemUpdateRequest;
import com.example.biosphere.model.Ecosystem;
import com.example.biosphere.model.User;
import com.example.biosphere.repository.EcosystemRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import jakarta.transaction.Transactional;

import java.util.List;

@Service
public class EcosystemService {
    @Autowired private EcosystemRepository ecosystemRepository;
    @Autowired private AuthService authService;

    public List<Ecosystem> getEcosystemsByUser(Long userId) {
        return ecosystemRepository.findByUserId(userId); // ou findByUserIdWithRelations si tu veux
    }

    public Ecosystem createEcosystem(EcosystemController.EcosystemCreateRequest r) {
        User user = authService.findById(r.getUserId())
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable: " + r.getUserId()));
        Ecosystem e = new Ecosystem();
        e.setName(r.getName()); e.setType(r.getType()); e.setPhotoUrl(r.getPhotoUrl()); e.setUser(user);
        return ecosystemRepository.save(e);
    }

    public Ecosystem updateEcosystem(Long id, EcosystemController.EcosystemUpdateRequest r) {
        Ecosystem e = ecosystemRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Ecosystème introuvable: " + id));
        if (r.getName()!=null) e.setName(r.getName());
        if (r.getType()!=null) e.setType(r.getType());
        if (r.getPhotoUrl()!=null) e.setPhotoUrl(r.getPhotoUrl());
        if (r.getSummaryParams() != null) {
            e.setSummaryParams(r.getSummaryParams());
        }
        return ecosystemRepository.save(e);
    }

    @Transactional
    public void deleteEcosystem(Long id) {
        Ecosystem e = ecosystemRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Écosystème introuvable : " + id));

        // ⚙️ Supprimer manuellement les entités liées (si ton repository les gère)
        try {
            // Exemple si tu as des repositories pour ces entités :
            // equipmentRepository.deleteAllByEcosystem(e);
            // parameterRecordRepository.deleteAllByEcosystem(e);
            // inhabitantRepository.deleteAllByEcosystem(e);
        } catch (Exception ex) {
            throw new RuntimeException("Erreur lors du nettoyage des données associées : " + ex.getMessage());
        }

        // ✅ Supprimer l’écosystème une fois les relations nettoyées
        ecosystemRepository.delete(e);
    }


    @Transactional
    public Ecosystem updateSummaryParams(Long ecosystemId, List<String> summaryParams) {
        Ecosystem eco = ecosystemRepository.findById(ecosystemId)
            .orElseThrow(() -> new RuntimeException("Écosystème introuvable"));
        eco.setSummaryParams(summaryParams);
        return ecosystemRepository.save(eco);
    }
}


