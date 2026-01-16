package com.example.biosphere.controller;

import com.example.biosphere.model.Inhabitant;
import com.example.biosphere.model.Ecosystem;
import com.example.biosphere.repository.InhabitantRepository;
import com.example.biosphere.repository.EcosystemRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping("/api/inhabitants")
public class InhabitantController {

    @Autowired private InhabitantRepository inhabitantRepository;
    @Autowired private EcosystemRepository ecosystemRepository;

    @GetMapping("/{ecosystemId}")
    public List<Inhabitant> getByEcosystem(@PathVariable Long ecosystemId) {
        return inhabitantRepository.findByEcosystemId(ecosystemId);
    }

    @PostMapping
    public Inhabitant create(@RequestBody Inhabitant req) {
        Ecosystem eco = ecosystemRepository.findById(req.getEcosystem().getId())
                .orElseThrow(() -> new RuntimeException("Écosystème introuvable"));
        req.setEcosystem(eco);
        return inhabitantRepository.save(req);
    }

    @DeleteMapping("/{id}")
    public void delete(@PathVariable Long id) {
        inhabitantRepository.deleteById(id);
    }
}