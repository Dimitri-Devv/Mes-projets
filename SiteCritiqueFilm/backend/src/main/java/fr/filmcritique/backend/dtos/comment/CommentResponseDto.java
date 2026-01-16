package fr.filmcritique.backend.dtos.comment;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class CommentResponseDto {

    private Long id;
    private String content;
    private String createdAt;

    private Long userId;
    private String username;
    private String avatarUrl;
}