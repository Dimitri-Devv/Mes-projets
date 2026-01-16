package com.example.biosphere.repository;

import com.example.biosphere.model.Message;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;

public interface MessageRepository extends JpaRepository<Message, Long> {

    // 🔹 Récupère la conversation entre deux utilisateurs
    @Query("""
        SELECT m FROM Message m 
        WHERE (m.sender.id = :userId AND m.receiver.id = :otherId)
           OR (m.sender.id = :otherId AND m.receiver.id = :userId)
        ORDER BY m.createdAt ASC
    """)
    List<Message> findConversation(
            @Param("userId") Long userId,
            @Param("otherId") Long otherUserId
    );

    // 🔹 Vérifie si un utilisateur a des messages non lus
    boolean existsByReceiver_IdAndIsReadFalse(Long receiverId);

    // 🔹 Récupère les dernières conversations (distinct expéditeurs)
    @Query("""
        SELECT m FROM Message m
        WHERE m.receiver.id = :userId OR m.sender.id = :userId
        ORDER BY m.createdAt DESC
    """)
    List<Message> findAllByUser(@Param("userId") Long userId);

    List<Message> findByReceiver_IdAndIsReadFalse(Long receiverId);



}