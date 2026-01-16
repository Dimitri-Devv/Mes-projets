package fr.filmcritique.backend.entities;

import jakarta.persistence.*;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.Instant;
import java.util.ArrayList;
import java.util.List;

@Entity
@Data
@NoArgsConstructor
@Table(name = "reviews")
public class Review {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false, updatable = false)
    private Instant createdAt = Instant.now();

    @Column(nullable = false, length = 255)
    private String title;

    @Column(columnDefinition = "TEXT", nullable = false)
    private String content;

    @Column(nullable = false)
    private Integer rating; // Note de 1 à 10

    private String imageUrl; // Optionnel : image/affiche liée à la critique

    private int likesCount = 0;
    private int dislikesCount = 0;

    // === Relations ===

    @ManyToOne(optional = false)
    @JoinColumn(name = "film_id")
    private Film film;

    @ManyToOne(optional = false)
    @JoinColumn(name = "user_id", nullable = false)

    private User user;

    @OneToMany(mappedBy = "review", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<Comment> comments = new ArrayList<>();

    @OneToMany(mappedBy = "review", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<ReviewLike> likes = new ArrayList<>();

    public Review(String title, String content, Integer rating, Film film, User user) {
        this.title = title;
        this.content = content;
        this.rating = rating;
        this.film = film;
        this.user = user;
    }
}
