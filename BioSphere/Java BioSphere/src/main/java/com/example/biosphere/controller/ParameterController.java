package com.example.biosphere.controller;

import com.example.biosphere.model.ParameterRecord;
import com.example.biosphere.service.ParameterService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import java.time.LocalDateTime;
import java.util.List;

@RestController
@RequestMapping("/api/parameters")
@CrossOrigin(origins = "*")
public class ParameterController {
    @Autowired private ParameterService parameterService;

    @GetMapping("/history/{ecosystemId}/{name}")
    public List<ParameterRecord> getHistory(@PathVariable Long ecosystemId, @PathVariable String name) {
        return parameterService.getHistory(ecosystemId, name);
    }

    @PostMapping("/history")
    public ParameterRecord addRecord(@RequestBody AddRecordRequest req) {
        return parameterService.addRecord(req);
    }

    @DeleteMapping("/history/{id}")
    public void deleteRecord(@PathVariable Long id) {
        parameterService.deleteRecord(id);
    }

    public static class AddRecordRequest {
        private Long ecosystemId; private String name; private Double value; private LocalDateTime measuredAt;
        public Long getEcosystemId(){return ecosystemId;} public void setEcosystemId(Long ecosystemId){this.ecosystemId=ecosystemId;}
        public String getName(){return name;} public void setName(String name){this.name=name;}
        public Double getValue(){return value;} public void setValue(Double value){this.value=value;}
        public LocalDateTime getMeasuredAt(){return measuredAt;} public void setMeasuredAt(LocalDateTime measuredAt){this.measuredAt=measuredAt;}
    }
}
