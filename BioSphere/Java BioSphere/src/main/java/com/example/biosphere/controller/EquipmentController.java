package com.example.biosphere.controller;

import com.example.biosphere.model.Equipment;
import com.example.biosphere.service.EquipmentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
@RequestMapping("/api/equipment")
@CrossOrigin(origins = "*")
public class EquipmentController {
    @Autowired private EquipmentService equipmentService;

    @GetMapping("/{ecosystemId}")
    public List<Equipment> list(@PathVariable Long ecosystemId) {
        return equipmentService.listByEcosystem(ecosystemId);
    }

    @PostMapping
    public Equipment add(@RequestBody CreateEquipmentRequest req) {
        return equipmentService.addEquipment(req);
    }

    @DeleteMapping("/{id}")
    public void delete(@PathVariable Long id) { equipmentService.deleteEquipment(id); }

    public static class CreateEquipmentRequest {
        private Long ecosystemId; private String name; private String brand; private String details;
        public Long getEcosystemId(){return ecosystemId;} public void setEcosystemId(Long ecosystemId){this.ecosystemId=ecosystemId;}
        public String getName(){return name;} public void setName(String name){this.name=name;}
        public String getBrand(){return brand;} public void setBrand(String brand){this.brand=brand;}
        public String getDetails(){return details;} public void setDetails(String details){this.details=details;}
    }
}

