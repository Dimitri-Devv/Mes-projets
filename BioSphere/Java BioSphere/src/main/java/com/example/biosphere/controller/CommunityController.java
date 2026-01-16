package com.example.biosphere.controller;

import com.example.biosphere.repository.UserRepository;

import com.example.biosphere.model.Comment;
import com.example.biosphere.model.Post;
import com.example.biosphere.model.Message;
import com.example.biosphere.service.CommunityService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api/community")
@CrossOrigin(origins = "*")
public class CommunityController {

    @Autowired
    private CommunityService communityService;

    @Autowired
    private UserRepository userRepository;

    // 🔹 Récupérer tous les posts (avec filtre)
    @GetMapping("/posts")
    public List<Post> getPosts(@RequestParam(required = false, defaultValue = "all") String type) {
        return communityService.getPosts(type);
    }

    // 🔹 Créer un post
    @PostMapping("/posts")
    public Post createPost(@RequestBody Map<String, Object> body) {
        Long userId = Long.valueOf(body.get("userId").toString());
        String type = (String) body.get("type");
        String text = (String) body.get("text");
        List<String> images = (List<String>) body.get("images");
        return communityService.createPost(userId, type, text, images);
    }

    // 🔹 Récupérer les commentaires d’un post
    @GetMapping("/posts/{postId}/comments")
    public List<Comment> getComments(@PathVariable Long postId) {
        return communityService.getComments(postId);
    }

    // 🔹 Ajouter un commentaire
    @PostMapping("/posts/{postId}/comments")
    public Comment addComment(@PathVariable Long postId, @RequestBody Map<String, Object> body) {
        Long userId = Long.valueOf(body.get("userId").toString());
        String text = (String) body.get("text");
        return communityService.addComment(postId, userId, text);
    }

    // 🔹 Like / Unlike un post
    @PostMapping("/posts/{postId}/like")
    public Map<String, Object> toggleLike(@PathVariable Long postId, @RequestBody Map<String, Object> body) {
        Long userId = Long.valueOf(body.get("userId").toString());
        communityService.toggleLike(postId, userId);
        return communityService.getLikes(postId);
    }

    // 🔹 Récupérer une conversation entre deux utilisateurs
    @GetMapping("/messages/{userId}/{otherUserId}")
    public List<Message> getConversation(@PathVariable Long userId, @PathVariable Long otherUserId) {
        return communityService.getConversation(userId, otherUserId);
    }

    // 🔹 Envoyer un message privé
    @PostMapping("/messages/send")
    public Message sendMessage(@RequestBody Map<String, Object> body) {
        Long senderId = Long.valueOf(body.get("senderId").toString());
        Long receiverId = Long.valueOf(body.get("receiverId").toString());
        String text = (String) body.get("text");
        return communityService.sendMessage(senderId, receiverId, text);
    }

    // 🔹 Récupérer un utilisateur par ID
    @GetMapping("/users/{id}")
    public Object getUserById(@PathVariable Long id) {
        var user = userRepository.findById(id)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));
        // Forcer le chargement des écosystèmes
        if (user.getEcosystems() != null) user.getEcosystems().size();
        return user;
    }

    // 🔹 Récupérer un utilisateur par username
    @GetMapping("/users/username/{username}")
    public Object getUserByUsername(@PathVariable String username) {
        var user = userRepository.findByUsername(username)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));
        // Forcer le chargement des écosystèmes
        if (user.getEcosystems() != null) user.getEcosystems().size();
        return user;
    }

    // 🔹 Récupérer la liste des conversations d’un utilisateur
    @GetMapping("/messages/list/{userId}")
    public List<Map<String, Object>> getUserConversations(@PathVariable Long userId) {
        return communityService.getUserConversations(userId);
    }

    // ✅ Vérifier si l’utilisateur a des messages non lus
    @GetMapping("/messages/unread/{userId}")
    public Map<String, Boolean> hasUnreadMessages(@PathVariable Long userId) {
        boolean hasUnread = communityService.hasUnreadMessages(userId);
        return Map.of("hasUnread", hasUnread);
    }

    // ✅ Marquer les messages comme lus
    @PostMapping("/messages/mark-read/{userId}")
    public void markMessagesAsRead(@PathVariable Long userId) {
        communityService.markMessagesAsRead(userId);
    }


}