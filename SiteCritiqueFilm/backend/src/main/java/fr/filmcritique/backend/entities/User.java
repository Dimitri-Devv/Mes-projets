package fr.filmcritique.backend.entities;

import com.fasterxml.jackson.annotation.JsonIgnore;
import fr.filmcritique.backend.enums.UserRole;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.Instant;
import java.util.ArrayList;
import java.util.List;

@Entity
@Data
@Table(name = "users")
@NoArgsConstructor
@AllArgsConstructor
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @Column(nullable = false, updatable = false)
    private Instant createdAt = Instant.now();
    @Column(nullable = false , unique = true, length = 100)
    private String username;
    @Column(length = 254)
    private String avatarUrl;
    @Column
    private Long coverFilmId;
    @Column(unique = true, nullable = false, length = 150)
    private String email;
    @Column(nullable = false)
    @JsonIgnore
    private String passwordHash;
    @Column(length = 500)
    private String bio;
    @Enumerated(EnumType.STRING)
    private UserRole role = UserRole.USER;
    private boolean isEmailVerified = false;
    private boolean blocked = false;
    private int avertissement = 0;


    @OneToMany(mappedBy = "user", cascade = CascadeType.ALL, orphanRemoval = true)
    @JsonIgnore
    private List<Review> reviews;
    @OneToMany(mappedBy = "user", cascade = CascadeType.ALL, orphanRemoval = true)
    @JsonIgnore
    private List<Comment> comments;
    @OneToMany(mappedBy = "user", cascade = CascadeType.ALL, orphanRemoval = true)
    @JsonIgnore
    private List<ReviewLike> likes;
    @ManyToMany
    @JoinTable(
            name = "user_favorites",
            joinColumns = @JoinColumn(name = "user_id"),
            inverseJoinColumns = @JoinColumn(name = "film_id")
    )
    @JsonIgnore
    private List<Film> favoriteFilms = new ArrayList<>();


    public User(String username, String email, String passwordHash, UserRole role) {
        this.username = username;
        this.email = email;
        this.passwordHash = passwordHash;
        this.role = role;
    }
}
