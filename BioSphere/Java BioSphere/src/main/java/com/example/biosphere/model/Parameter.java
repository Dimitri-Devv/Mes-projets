package com.example.biosphere.model;

import com.fasterxml.jackson.annotation.JsonBackReference;
import jakarta.persistence.*;

@Entity
@Table(name = "parameters")
public class Parameter {

    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String name;   // ex: "pH"
    private String value;  // ex: "8.2" (tu peux passer en double si tu préfères)

    @ManyToOne
    @JoinColumn(name = "ecosystem_id")
    @JsonBackReference
    private Ecosystem ecosystem;

    // Getters/Setters
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getValue() { return value; }
    public void setValue(String value) { this.value = value; }

    public Ecosystem getEcosystem() { return ecosystem; }
    public void setEcosystem(Ecosystem ecosystem) { this.ecosystem = ecosystem; }
}
