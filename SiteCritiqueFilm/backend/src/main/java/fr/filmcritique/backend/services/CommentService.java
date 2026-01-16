package fr.filmcritique.backend.services;

import fr.filmcritique.backend.dtos.comment.CommentCreateDto;
import fr.filmcritique.backend.dtos.comment.CommentResponseDto;
import fr.filmcritique.backend.dtos.comment.CommentUpdateDto;
import fr.filmcritique.backend.entities.Comment;
import fr.filmcritique.backend.entities.Review;
import fr.filmcritique.backend.entities.User;
import fr.filmcritique.backend.mappers.CommentMapper;
import fr.filmcritique.backend.repositories.CommentRepo;
import fr.filmcritique.backend.repositories.ReviewRepo;
import fr.filmcritique.backend.repositories.UserRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class CommentService {

    private final CommentRepo commentRepo;
    private final ReviewRepo reviewRepo;
    private final UserRepo userRepo;

    public List<CommentResponseDto> getCommentsByReview(Long reviewId) {
        return commentRepo.findByReviewId(reviewId)
                .stream()
                .map(CommentMapper::toResponse)
                .toList();
    }

    public CommentResponseDto create(Long reviewId, Long userId, CommentCreateDto dto) {

        Review review = reviewRepo.findById(reviewId)
                .orElseThrow(() -> new RuntimeException("Critique introuvable"));

        User user = userRepo.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        Comment comment = CommentMapper.toEntity(dto, review, user);
        Comment saved = commentRepo.save(comment);

        return CommentMapper.toResponse(saved);
    }

    public CommentResponseDto update(Long commentId, Long userId, CommentUpdateDto dto, boolean isAdmin) {
        Comment comment = commentRepo.findById(commentId)
                .orElseThrow(() -> new RuntimeException("Commentaire introuvable"));

        // sécurité : user doit être propriétaire OU admin
        if (!isAdmin && !comment.getUser().getId().equals(userId)) {
            throw new RuntimeException("Vous n'avez pas le droit de modifier ce commentaire.");
        }

        CommentMapper.updateEntity(comment, dto);
        return CommentMapper.toResponse(commentRepo.save(comment));
    }

    public void delete(Long commentId, Long userId, boolean isAdmin) {
        Comment comment = commentRepo.findById(commentId)
                .orElseThrow(() -> new RuntimeException("Commentaire introuvable"));

        // sécurité : user doit être propriétaire OU admin
        if (!isAdmin && !comment.getUser().getId().equals(userId)) {
            throw new RuntimeException("Vous n'avez pas le droit de supprimer ce commentaire.");
        }

        commentRepo.delete(comment);
    }
}