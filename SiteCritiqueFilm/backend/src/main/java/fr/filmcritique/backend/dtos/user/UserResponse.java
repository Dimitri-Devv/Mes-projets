package fr.filmcritique.backend.dtos.user;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class UserResponse {
    private Long id;
    private String username;
    private String email;
    private String avatarUrl;
    private Long coverFilmId;
    private String role;
    private boolean emailVerified;
    private boolean blocked;
    private int avertissement;

}
