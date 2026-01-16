package fr.filmcritique.backend.dtos.user;

import lombok.Data;

@Data
public class UpdateUserDto {
    private String avatarUrl;
    private Long coverFilmId;
    private String bio;

}

