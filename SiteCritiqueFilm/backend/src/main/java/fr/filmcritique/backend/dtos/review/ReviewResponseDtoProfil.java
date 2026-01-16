package fr.filmcritique.backend.dtos.review;

import lombok.AllArgsConstructor;
import lombok.Data;

import java.util.Date;

@Data
@AllArgsConstructor
public class ReviewResponseDtoProfil {

    private Long id;
    private String title;
    private String content;
    private Integer rating;
    private String createdAt;

    private Long filmId;
    private String filmTitle;
    private String filmAfficheUrl;

    private Long userId;
    private String username;
    private String avatarUrl;
    private int likesCount;
    private int dislikesCount;

}