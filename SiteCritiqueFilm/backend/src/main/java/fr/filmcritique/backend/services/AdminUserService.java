package fr.filmcritique.backend.services;

import fr.filmcritique.backend.dtos.admin.AdminUpdateUserDto;
import fr.filmcritique.backend.dtos.admin.UpdateRoleDto;
import fr.filmcritique.backend.dtos.user.UserResponse;
import fr.filmcritique.backend.entities.User;
import fr.filmcritique.backend.enums.UserRole;
import fr.filmcritique.backend.mappers.UserMapper;
import fr.filmcritique.backend.repositories.UserRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class AdminUserService {

    private final UserRepo userRepo;

    public List<UserResponse> getAllUsers() {
        return userRepo.findAll()
                .stream()
                .map(UserMapper::toResponse)
                .toList();
    }

    public User updateRole(Long idUser, UpdateRoleDto dto) {

        User user = userRepo.findById(idUser)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        if (user.getRole() == UserRole.ADMIN && dto.getRole() == UserRole.USER) {
            throw new RuntimeException("Un admin ne peut pas retirer son propre rôle.");
        }

        user.setRole(dto.getRole());

        return userRepo.save(user);
    }

    public UserResponse updateUser(Long id, AdminUpdateUserDto dto) {
        User user = userRepo.findById(id)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        if (dto.getAvatarUrl() != null) user.setAvatarUrl(dto.getAvatarUrl());
        if (dto.getBlocked() != null) user.setBlocked(dto.getBlocked());
        if (dto.getAvertissement() != null) user.setAvertissement(dto.getAvertissement());
        if (dto.getEmailVerified() != null) user.setEmailVerified(dto.getEmailVerified());

        return UserMapper.toResponse(userRepo.save(user));
    }

    public void deleteUser(Long id) {
        User user = userRepo.findById(id)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        if (user.getRole() == UserRole.ADMIN) {
            throw new RuntimeException("Impossible de supprimer un admin.");
        }

        userRepo.delete(user);
    }
}