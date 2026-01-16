package com.example.biosphere.model;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import jakarta.persistence.*;
import java.time.LocalDateTime;
import java.util.List;
import java.util.ArrayList;

@Entity
@Table(name = "posts")
@JsonIgnoreProperties({"hibernateLazyInitializer", "handler"})
public class Post {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    private String type;
    private String text;
    private LocalDateTime createdAt = LocalDateTime.now();

    @ElementCollection
    private List<String> images = new ArrayList<>();

    // 👇 IMPORTANT : on ignore les sous-relations du user pour ne pas tout embarquer
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_id")
    @JsonIgnoreProperties({"posts", "comments", "ecosystems", "password"})
    private User author;

    @OneToMany(mappedBy = "post", cascade = CascadeType.ALL, orphanRemoval = true)
    @JsonIgnoreProperties({"post"}) // empêche la boucle post → comment → post
    private List<Comment> comments = new ArrayList<>();

    // ✅ Relation des likes
    @ManyToMany
    @JoinTable(
            name = "post_likes",
            joinColumns = @JoinColumn(name = "post_id"),
            inverseJoinColumns = @JoinColumn(name = "user_id")
    )
    @JsonIgnoreProperties({"posts", "comments", "ecosystems", "password"})
    private List<User> likedBy = new ArrayList<>();

    // --- Getters & Setters ---
    public Long getId() { return id; }
    public void setId(Long id) { this.id = id; }

    public String getType() { return type; }
    public void setType(String type) { this.type = type; }

    public String getText() { return text; }
    public void setText(String text) { this.text = text; }

    public LocalDateTime getCreatedAt() { return createdAt; }
    public void setCreatedAt(LocalDateTime createdAt) { this.createdAt = createdAt; }

    public List<String> getImages() { return images; }
    public void setImages(List<String> images) { this.images = images; }

    public User getAuthor() { return author; }
    public void setAuthor(User author) { this.author = author; }

    public List<Comment> getComments() { return comments; }
    public void setComments(List<Comment> comments) { this.comments = comments; }

    // ✅ Getters / Setters manquants pour les likes
    public List<User> getLikedBy() { return likedBy; }
    public void setLikedBy(List<User> likedBy) { this.likedBy = likedBy; }
}