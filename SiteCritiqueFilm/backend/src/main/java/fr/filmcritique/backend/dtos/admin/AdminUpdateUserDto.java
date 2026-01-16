package fr.filmcritique.backend.dtos.admin;

import lombok.Data;

@Data
public class AdminUpdateUserDto {
    private String avatarUrl;
    private Boolean blocked;
    private Integer avertissement;
    private Boolean emailVerified;
}

