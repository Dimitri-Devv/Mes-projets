package com.example.biosphere.repository;

import com.example.biosphere.model.Comment;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface CommentRepository extends JpaRepository<Comment, Long> {

    // 🔹 Récupère tous les commentaires d’un post, triés par date croissante
    List<Comment> findByPostIdOrderByCreatedAtAsc(Long postId);
}
