// 📁 Ecrans/ConversationsScreen.js
import React, { useEffect, useState, useContext } from "react";
import { View, Text, TouchableOpacity, FlatList, Image, StyleSheet, ActivityIndicator } from "react-native";
import api from "./services/api";
import { AppContext } from "./context/AppContext";

export default function ConversationsScreen({ navigation }) {
  const { user, theme } = useContext(AppContext);
  const isDark = theme === "dark";
  const [loading, setLoading] = useState(true);
  const [conversations, setConversations] = useState([]);

  const colors = {
    bg: isDark ? "#0f1113" : "#fff",
    text: isDark ? "#fff" : "#222",
    muted: isDark ? "#aaa" : "#666",
    accent: "#2a9d8f",
  };

  useEffect(() => {
    const fetchConversations = async () => {
      try {
        const res = await api.get(`/community/messages/list/${user.id}`);
        setConversations(res.data || []);
      } catch (err) {
        console.log("Erreur chargement conversations :", err);
      } finally {
        setLoading(false);
      }
    };
    fetchConversations();
  }, []);

  if (loading) {
    return (
      <View style={[styles.center, { backgroundColor: colors.bg }]}>
        <ActivityIndicator color={colors.accent} size="large" />
      </View>
    );
  }

  return (
    <View style={[styles.container, { backgroundColor: colors.bg }]}>
      <FlatList
        data={conversations}
        keyExtractor={(item) => String(item.otherUser.id)}
        renderItem={({ item }) => (
          <TouchableOpacity
            style={styles.row}
            onPress={() =>
              navigation.navigate("Chat", {
                toUser: item.otherUser,
              })
            }
          >
            <Image
              source={{
                uri: item.otherUser.photoUrl || "https://cdn-icons-png.flaticon.com/512/149/149071.png",
              }}
              style={styles.avatar}
            />
            <View style={{ flex: 1 }}>
              <Text style={[styles.username, { color: colors.text }]}>
                {item.otherUser.username}
              </Text>
              <Text style={{ color: colors.muted }} numberOfLines={1}>
                {item.lastMessage || "Aucun message"}
              </Text>
            </View>
          </TouchableOpacity>
        )}
        ListEmptyComponent={<Text style={{ color: colors.muted, textAlign: "center" }}>Aucune conversation</Text>}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 10 },
  center: { flex: 1, justifyContent: "center", alignItems: "center" },
  row: { flexDirection: "row", alignItems: "center", paddingVertical: 12 },
  avatar: { width: 50, height: 50, borderRadius: 25, marginRight: 10 },
  username: { fontWeight: "bold", fontSize: 16 },
});