package fr.filmcritique.backend.dtos.admin;

import fr.filmcritique.backend.enums.UserRole;
import jakarta.validation.constraints.NotNull;
import lombok.Data;

@Data
public class UpdateRoleDto {

    @NotNull
    private UserRole role;
}
