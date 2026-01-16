import React, { useState, useEffect, useContext } from "react";
import { View, Text, TextInput, TouchableOpacity, FlatList, StyleSheet, Image, KeyboardAvoidingView, Platform, ScrollView } from "react-native";
import { Ionicons } from "@expo/vector-icons";
import api from "./services/api";
import { AppContext } from "../Ecrans/context/AppContext";

export default function CommentsScreen({ post, user, navigation }) {
  const { theme } = useContext(AppContext);
  const isDark = theme === "dark";
  const [comments, setComments] = useState([]);
  const [text, setText] = useState("");

  const colors = {
    bg: isDark ? "#0f1113" : "#fff",
    text: isDark ? "#fff" : "#222",
    card: isDark ? "#171a1d" : "#f7f7f9",
    border: isDark ? "#2b2f34" : "#e5e5ea",
    muted: isDark ? "#a7a7a7" : "#666",
    accent: "#2a9d8f",
  };

  useEffect(() => {
    if (post?.id) fetchComments();
  }, [post]);

  const fetchComments = async () => {
    try {
      const res = await api.get(`/community/posts/${post.id}/comments`);
      setComments(res.data || []);
    } catch (err) {
      console.log("❌ Erreur chargement commentaires :", err.message);
    }
  };

  const sendComment = async () => {
    if (!text.trim()) return;
    try {
      await api.post(`/community/posts/${post.id}/comments`, {
        userId: user.id,
        text: text.trim(),
      });
      setText("");
      fetchComments();
    } catch (err) {
      console.log("❌ Erreur envoi commentaire :", err.message);
    }
  };

  const renderComment = ({ item }) => (
    <View style={styles.commentRow}>
      <Image
        source={{ uri: item.author?.photoUrl || "https://cdn-icons-png.flaticon.com/512/149/149071.png" }}
        style={styles.avatar}
      />
      <View style={{ flex: 1 }}>
        <Text style={[styles.username, { color: colors.text }]}>{item.author?.username || "Anonyme"}</Text>
        <Text style={[styles.commentText, { color: colors.muted }]}>{item.text}</Text>
      </View>
    </View>
  );

  return (
    <View style={[styles.container, { backgroundColor: colors.bg }]}>
      <View style={styles.header}>
        <TouchableOpacity onPress={() => navigation.goBack()}>
          <Ionicons name="close" size={24} color={colors.text} />
        </TouchableOpacity>
        <Text style={[styles.headerTitle, { color: colors.text }]}>Commentaires</Text>
        <View style={{ width: 24 }} />
      </View>

      <FlatList
        data={comments}
        keyExtractor={(item) => String(item.id)}
        renderItem={renderComment}
        contentContainerStyle={{ padding: 16 }}
        showsVerticalScrollIndicator={true}
        style={{ flex: 1 }}
        scrollEnabled={true}
      />

      <KeyboardAvoidingView
        behavior={Platform.OS === "ios" ? "padding" : "height"}
      >
        <View style={[styles.inputRow, { borderColor: colors.border }]}>
          <TextInput
            style={[styles.input, { color: colors.text }]}
            placeholder="Ajouter un commentaire..."
            placeholderTextColor={colors.muted}
            value={text}
            onChangeText={setText}
          />
          <TouchableOpacity onPress={sendComment}>
            <Text style={[styles.publish, { color: colors.accent }]}>Publier</Text>
          </TouchableOpacity>
        </View>
      </KeyboardAvoidingView>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  header: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    padding: 16,
    borderBottomWidth: 1,
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: "700",
  },
  commentRow: {
    flexDirection: "row",
    alignItems: "flex-start",
    marginBottom: 12,
  },
  avatar: {
    width: 36,
    height: 36,
    borderRadius: 18,
    marginRight: 10,
  },
  username: {
    fontWeight: "700",
  },
  commentText: {
    fontSize: 14,
  },
  inputRow: {
    flexDirection: "row",
    alignItems: "center",
    borderTopWidth: 1,
    paddingHorizontal: 12,
    paddingVertical: 8,
  },
  input: {
    flex: 1,
    fontSize: 15,
    marginRight: 10,
  },
  publish: {
    fontWeight: "700",
  },
});
