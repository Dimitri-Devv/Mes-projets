package fr.filmcritique.backend.dtos.review;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class ReviewResponseDto {
    private Long id;
    private String content;
    private int rating;
    private Long filmId;
    private String filmTitle;


}

