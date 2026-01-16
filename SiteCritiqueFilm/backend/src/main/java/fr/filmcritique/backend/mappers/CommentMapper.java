package fr.filmcritique.backend.mappers;

import fr.filmcritique.backend.dtos.comment.CommentCreateDto;
import fr.filmcritique.backend.dtos.comment.CommentResponseDto;
import fr.filmcritique.backend.dtos.comment.CommentUpdateDto;
import fr.filmcritique.backend.entities.Comment;
import fr.filmcritique.backend.entities.Review;
import fr.filmcritique.backend.entities.User;

public class CommentMapper {

    public static CommentResponseDto toResponse(Comment c) {
        return new CommentResponseDto(
                c.getId(),
                c.getContent(),
                c.getCreatedAt().toString(),
                c.getUser().getId(),
                c.getUser().getUsername(),
                c.getUser().getAvatarUrl()
        );
    }

    public static Comment toEntity(CommentCreateDto dto, Review review, User user) {
        Comment c = new Comment();
        c.setContent(dto.getContent());
        c.setReview(review);
        c.setUser(user);
        return c;
    }

    public static void updateEntity(Comment comment, CommentUpdateDto dto) {
        comment.setContent(dto.getContent());
    }
}