package fr.filmcritique.backend.entities;


import com.fasterxml.jackson.annotation.JsonIgnore;
import jakarta.persistence.*;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.Instant;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

@Entity
@Table(name = "films")
@Data
@NoArgsConstructor
public class Film {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @Column(nullable = false, updatable = false)
    private Instant createdAt = Instant.now();
    @Column(nullable = false, length = 254)
    private String title;
    @Column(length = 150)
    private String director;
    private LocalDate dateSortie;
    private String posterUrl;
    private String afficheUrl;
    private String trailerUrl;
    @Column(columnDefinition = "TEXT")
    private String synopsis;
    private boolean isTendance = false;
    @Column(nullable = false)
    private Double ratingAverage = 0.0;
    @Column(nullable = false)
    private Double baseRating = 0.0;

    @ManyToMany
    @JsonIgnoreProperties("films")
    @JoinTable(
            name = "film_genre",
            joinColumns = @JoinColumn(name = "film_id"),
            inverseJoinColumns = @JoinColumn(name = "genre_id")
    )
    private List<Genre> genres = new ArrayList<>();
    @ManyToMany
    @JoinTable(
            name = "film_actor",
            joinColumns = @JoinColumn(name = "film_id"),
            inverseJoinColumns = @JoinColumn(name = "actor_id")
    )
    @JsonIgnore
    private List<Actor> actors = new ArrayList<>();
    @OneToMany(mappedBy = "film", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<Review> reviews = new ArrayList<>();


}
