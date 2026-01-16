package com.example.biosphere.repository;

import com.example.biosphere.model.EmailVerification;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface EmailVerificationRepository extends JpaRepository<EmailVerification, Long> {
    Optional<EmailVerification> findByEmailAndCode(String email, String code);
    void deleteByEmail(String email);
}