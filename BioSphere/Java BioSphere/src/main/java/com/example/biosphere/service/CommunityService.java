package com.example.biosphere.service;

import com.example.biosphere.model.Comment;
import com.example.biosphere.model.Post;
import com.example.biosphere.model.User;
import com.example.biosphere.model.Message;
import com.example.biosphere.repository.CommentRepository;
import com.example.biosphere.repository.PostRepository;
import com.example.biosphere.repository.UserRepository;
import com.example.biosphere.repository.MessageRepository;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.HashMap;
import java.util.Map;
import java.util.List;
import java.util.stream.Collectors;

@Service
public class CommunityService {

    @Autowired
    private PostRepository postRepository;

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private CommentRepository commentRepository;

    @Autowired
    private MessageRepository messageRepository;

    // 🔹 Récupérer les posts avec leurs auteurs et commentaires
    @Transactional
    public List<Post> getPosts(String type) {
        List<Post> posts;

        if (type == null || type.equals("all")) {
            posts = postRepository.findAllByOrderByCreatedAtDesc();
        } else {
            posts = postRepository.findByTypeOrderByCreatedAtDesc(type);
        }

        // ⚙️ Force le chargement des relations utiles
        posts.forEach(post -> {
            if (post.getAuthor() != null) {
                post.getAuthor().getId();
                post.getAuthor().getUsername();
                post.getAuthor().getPhotoUrl();
                post.getAuthor().getBio();
                post.getAuthor().getEmail();
            }

            if (post.getComments() != null) {
                post.getComments().forEach(c -> {
                    if (c.getAuthor() != null) {
                        c.getAuthor().getId();
                        c.getAuthor().getUsername();
                        c.getAuthor().getPhotoUrl();
                    }
                });
            }

            if (post.getLikedBy() != null) post.getLikedBy().size(); // charge les likes
        });

        return posts;
    }

    // 🔹 Créer un post
    @Transactional
    public Post createPost(Long userId, String type, String text, List<String> images) {
        User user = userRepository.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        Post post = new Post();
        post.setAuthor(user);
        post.setType(type);
        post.setText(text);
        post.setImages(images);

        return postRepository.save(post);
    }

    // 🔹 Récupérer les commentaires d’un post
    @Transactional
    public List<Comment> getComments(Long postId) {
        List<Comment> comments = commentRepository.findByPostIdOrderByCreatedAtAsc(postId);
        comments.forEach(c -> {
            if (c.getAuthor() != null) {
                c.getAuthor().getId();
                c.getAuthor().getUsername();
                c.getAuthor().getPhotoUrl();
            }
        });
        return comments;
    }

    // 🔹 Ajouter un commentaire
    @Transactional
    public Comment addComment(Long postId, Long userId, String text) {
        User user = userRepository.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));
        Post post = postRepository.findById(postId)
                .orElseThrow(() -> new RuntimeException("Post introuvable"));

        Comment comment = new Comment();
        comment.setAuthor(user);
        comment.setPost(post);
        comment.setText(text);

        return commentRepository.save(comment);
    }

    // 🔹 Ajouter / retirer un like
    @Transactional
    public void toggleLike(Long postId, Long userId) {
        Post post = postRepository.findById(postId)
                .orElseThrow(() -> new RuntimeException("Post introuvable"));
        User user = userRepository.findById(userId)
                .orElseThrow(() -> new RuntimeException("Utilisateur introuvable"));

        if (post.getLikedBy().contains(user)) {
            post.getLikedBy().remove(user);
        } else {
            post.getLikedBy().add(user);
        }

        postRepository.save(post);
    }

    // 🔹 Récupérer le nombre de likes
    @Transactional
    public Map<String, Object> getLikes(Long postId) {
        Post post = postRepository.findById(postId)
                .orElseThrow(() -> new RuntimeException("Post introuvable"));
        Map<String, Object> response = new HashMap<>();
        response.put("postId", postId);
        response.put("likeCount", post.getLikedBy().size());
        return response;
    }

    // 🔹 Envoyer un message privé
    @Transactional
    public Message sendMessage(Long senderId, Long receiverId, String text) {
        if (messageRepository == null) {
            throw new IllegalStateException("Le dépôt MessageRepository n'est pas correctement initialisé.");
        }

        User sender = userRepository.findById(senderId)
                .orElseThrow(() -> new RuntimeException("Expéditeur introuvable"));
        User receiver = userRepository.findById(receiverId)
                .orElseThrow(() -> new RuntimeException("Destinataire introuvable"));

        if (sender.equals(receiver)) {
            throw new IllegalArgumentException("Impossible d’envoyer un message à soi-même.");
        }

        Message message = new Message();
        message.setSender(sender);
        message.setReceiver(receiver);
        message.setText(text);
        message.setCreatedAt(java.time.LocalDateTime.now());

        Message saved = messageRepository.save(message);

        // 🔹 Forcer le chargement des infos pour renvoyer un JSON complet
        saved.getSender().getUsername();
        saved.getReceiver().getUsername();
        saved.getSender().getId();
        saved.getSender().getPhotoUrl();
        saved.getSender().getBio();
        saved.getReceiver().getId();
        saved.getReceiver().getPhotoUrl();
        saved.getReceiver().getBio();

        return saved;
    }

    // 🔹 Récupérer une conversation entre deux utilisateurs
    @Transactional
    public List<Message> getConversation(Long userId, Long otherUserId) {
        if (messageRepository == null)
            throw new RuntimeException("MessageRepository non configuré");

        List<Message> messages = messageRepository.findConversation(userId, otherUserId);
        messages.forEach(m -> {
            if (m.getSender() != null) {
                m.getSender().getId();
                m.getSender().getUsername();
                m.getSender().getPhotoUrl();
                m.getSender().getBio();
            }
            if (m.getReceiver() != null) {
                m.getReceiver().getId();
                m.getReceiver().getUsername();
                m.getReceiver().getPhotoUrl();
                m.getReceiver().getBio();
            }
        });
        return messages;
    }

    // 🔹 Récupérer la liste des conversations d’un utilisateur, triées par date du dernier message
    @Transactional
    public List<Map<String, Object>> getUserConversations(Long userId) {
        List<Message> allMessages = messageRepository.findAll();
        Map<Long, Map<String, Object>> convMap = new HashMap<>();

        for (Message m : allMessages) {
            if (!m.getSender().getId().equals(userId) && !m.getReceiver().getId().equals(userId)) continue;

            var other = m.getSender().getId().equals(userId) ? m.getReceiver() : m.getSender();
            Map<String, Object> conv = convMap.computeIfAbsent(other.getId(), k -> {
                Map<String, Object> newConv = new HashMap<>();
                newConv.put("otherUser", other);
                newConv.put("lastMessage", m.getText());
                newConv.put("lastDate", m.getCreatedAt());
                return newConv;
            });

            if (m.getCreatedAt().isAfter((java.time.LocalDateTime) conv.get("lastDate"))) {
                conv.put("lastMessage", m.getText());
                conv.put("lastDate", m.getCreatedAt());
            }
        }

        return convMap.values().stream()
                .sorted((a, b) -> ((LocalDateTime) b.get("lastDate")).compareTo((LocalDateTime) a.get("lastDate")))
                .collect(Collectors.toList());
    }

    @Transactional
    public void markMessagesAsRead(Long userId) {
        List<Message> unread = messageRepository.findByReceiver_IdAndIsReadFalse(userId);
        unread.forEach(m -> m.setRead(true));
        messageRepository.saveAll(unread);
    }

    public boolean hasUnreadMessages(Long userId) {
        return messageRepository.existsByReceiver_IdAndIsReadFalse(userId);
    }
}