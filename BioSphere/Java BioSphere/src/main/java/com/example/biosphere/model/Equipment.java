package com.example.biosphere.model;

import com.fasterxml.jackson.annotation.JsonBackReference;
import jakarta.persistence.*;

@Entity
@Table(name = "equipment")
public class Equipment {

    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String name;   // ex: "Pompe de brassage"
    private String brand;  // ex: "Tunze"
    private String details; // ex: "6055"

    @ManyToOne
    @JoinColumn(name = "ecosystem_id")
    @JsonBackReference
    private Ecosystem ecosystem;

    // Getters/Setters
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getBrand() { return brand; }
    public void setBrand(String brand) { this.brand = brand; }

    public String getDetails() { return details; }
    public void setDetails(String details) { this.details = details; }

    public Ecosystem getEcosystem() { return ecosystem; }
    public void setEcosystem(Ecosystem ecosystem) { this.ecosystem = ecosystem; }
}
