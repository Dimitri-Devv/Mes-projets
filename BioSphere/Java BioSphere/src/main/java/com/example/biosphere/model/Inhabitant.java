package com.example.biosphere.model;

import com.fasterxml.jackson.annotation.JsonBackReference;
import jakarta.persistence.*;

@Entity
@Table(name = "inhabitants")
public class Inhabitant {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String name; // ex: "Rasbora Arlequin"

    @ManyToOne
    @JoinColumn(name = "ecosystem_id")
    @JsonBackReference
    private Ecosystem ecosystem;

    // --- Getters / Setters ---
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public Ecosystem getEcosystem() { return ecosystem; }
    public void setEcosystem(Ecosystem ecosystem) { this.ecosystem = ecosystem; }
}