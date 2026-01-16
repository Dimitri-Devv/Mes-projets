package com.example.biosphere.service;

import com.example.biosphere.model.User;
import com.example.biosphere.repository.UserRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
public class AuthService {

    @Autowired private UserRepository userRepository;
    @Autowired private PasswordService passwordService;

    // ✅ Login avec Hash + email vérifié ?
    public Optional<User> login(String email, String password) {
        Optional<User> userOpt = userRepository.findByEmail(email);

        if (userOpt.isEmpty()) return Optional.empty();
        User user = userOpt.get();

        if (!passwordService.verifyPassword(password, user.getPassword()))
            return Optional.empty();

        return Optional.of(user);
    }

    // ✅ Register avec hash
    public User registerUser(String email, String username, String rawPassword) {
        String hashed = passwordService.hashPassword(rawPassword);
        User u = new User();
        u.setEmail(email);
        u.setUsername(username);
        u.setPassword(hashed);
        u.setEmailVerified(false);
        return userRepository.save(u);
    }

    public Optional<User> findById(Long id) {
        return userRepository.findById(id);
    }
}