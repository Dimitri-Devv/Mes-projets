package com.example.biosphere.model;

import com.fasterxml.jackson.annotation.JsonBackReference;
import com.fasterxml.jackson.annotation.JsonIgnore;
import jakarta.persistence.*;
import java.util.List;
import java.util.ArrayList;

@Entity
@Table(name = "ecosystems")
public class Ecosystem {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String name;
    private String type;
    private String photoUrl;

    // 🔹 Lien vers l'utilisateur propriétaire
    @ManyToOne
    @JoinColumn(name = "user_id")
    @JsonBackReference
    private User user;

    // 🔹 Liste des paramètres
    @OneToMany(mappedBy = "ecosystem", cascade = CascadeType.ALL, orphanRemoval = true, fetch = FetchType.LAZY)
    @JsonIgnore
    private List<Parameter> parameters;

    // 🔹 Liste des enregistrements d’historique (⚠️ c’est celle qui bloquait la suppression)
    @OneToMany(mappedBy = "ecosystem", cascade = CascadeType.ALL, orphanRemoval = true, fetch = FetchType.LAZY)
    @JsonIgnore
    private List<ParameterRecord> parameterRecords;

    // 🔹 Liste du matériel associé
    @OneToMany(mappedBy = "ecosystem", cascade = CascadeType.ALL, orphanRemoval = true, fetch = FetchType.LAZY)
    @JsonIgnore
    private List<Equipment> equipments;

    // 🔹 Paramètres affichés dans le résumé rapide
    @ElementCollection
    @CollectionTable(name = "ecosystem_summary_params", joinColumns = @JoinColumn(name = "ecosystem_id"))
    @Column(name = "param_name")
    private List<String> summaryParams = new ArrayList<>();

    @OneToMany(mappedBy = "ecosystem", cascade = CascadeType.ALL, orphanRemoval = true, fetch = FetchType.LAZY)
    @JsonIgnore
    private List<Inhabitant> inhabitants = new ArrayList<>();

    public List<Inhabitant> getInhabitants() { return inhabitants; }
    public void setInhabitants(List<Inhabitant> inhabitants) { this.inhabitants = inhabitants; }

    // --- Getters & Setters ---
    public List<String> getSummaryParams() { return summaryParams; }
    public void setSummaryParams(List<String> summaryParams) { this.summaryParams = summaryParams; }

    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getType() { return type; }
    public void setType(String type) { this.type = type; }

    public String getPhotoUrl() { return photoUrl; }
    public void setPhotoUrl(String photoUrl) { this.photoUrl = photoUrl; }

    public User getUser() { return user; }
    public void setUser(User user) { this.user = user; }

    public List<Parameter> getParameters() { return parameters; }
    public void setParameters(List<Parameter> parameters) { this.parameters = parameters; }

    public List<ParameterRecord> getParameterRecords() { return parameterRecords; }
    public void setParameterRecords(List<ParameterRecord> parameterRecords) { this.parameterRecords = parameterRecords; }

    public List<Equipment> getEquipments() { return equipments; }
    public void setEquipments(List<Equipment> equipments) { this.equipments = equipments; }
}