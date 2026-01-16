import React, { useLayoutEffect, useCallback, useContext, useMemo, useState, useRef, useEffect } from "react";
import {
  View,
  Text,
  StyleSheet,
  FlatList,
  TouchableOpacity,
  Image,
  RefreshControl,
  TextInput,
  Modal,
  Alert,
  ActivityIndicator,
  Platform,
  ScrollView,
  Animated,
  KeyboardAvoidingView,
  Keyboard,
  TouchableWithoutFeedback,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import { useFocusEffect } from "@react-navigation/native";
import * as ImagePicker from "expo-image-picker";
import * as ImageManipulator from "expo-image-manipulator";
import { Ionicons, MaterialCommunityIcons } from "@expo/vector-icons";
import { VideoView, useVideoPlayer } from "expo-video";
import api from "./services/api";
import { AppContext } from "../Ecrans/context/AppContext";

// 🌩️ Cloudinary
const CLOUD_NAME = "dvr95kazn";
const UPLOAD_PRESET = "biosphere_uploads";

// 🧭 Filtres disponibles
const FILTERS = [
  { label: "Tous", value: "all", icon: "earth" },
  { label: "Eau douce", value: "eau_douce", icon: "fish" },
  { label: "Eau de mer", value: "eau_de_mer", icon: "waves" },
  { label: "Terrarium", value: "terrarium", icon: "leaf" },
  { label: "Bassin", value: "bassin", icon: "water" },
  { label: "Plante", value: "plante", icon: "sprout" },
];

// 🔁 Convertit HEIC → JPEG automatiquement
async function convertHeicToJpeg(uri) {
  if (!uri.toLowerCase().endsWith(".heic")) return uri;
  try {
    const manipulated = await ImageManipulator.manipulateAsync(uri, [], {
      compress: 0.9,
      format: ImageManipulator.SaveFormat.JPEG,
    });
    console.log("🧩 Converti HEIC → JPEG :", manipulated.uri);
    return manipulated.uri;
  } catch (e) {
    console.warn("⚠️ Erreur conversion HEIC → JPEG :", e.message);
    return uri;
  }
}

// 🚀 Upload vers Cloudinary (images + vidéos)
async function uploadMediaToCloudinary(uri) {
  uri = await convertHeicToJpeg(uri);

  const match = /\.(\w+)$/.exec(uri);
  const ext = match ? match[1].toLowerCase() : "jpg";

  const isVideo = ["mp4", "mov", "webm"].includes(ext);
  const mimeType = isVideo
    ? ext === "mov"
      ? "video/quicktime"
      : `video/${ext}`
    : ext === "png"
    ? "image/png"
    : "image/jpeg";

  const formData = new FormData();
  formData.append("file", {
    uri,
    type: mimeType,
    name: `biosphere_${Date.now()}.${ext}`,
  });
  formData.append("upload_preset", UPLOAD_PRESET);

  const resourceType = isVideo ? "video" : "image";
  const endpoint = `https://api.cloudinary.com/v1_1/${CLOUD_NAME}/${resourceType}/upload`;

  console.log("📤 Upload →", endpoint, "type:", mimeType);

  try {
    const response = await fetch(endpoint, { method: "POST", body: formData });
    const json = await response.json();

    if (!response.ok) {
      console.error("❌ Erreur Cloudinary complète :", json);
      throw new Error(json.error?.message || "Erreur Cloudinary inconnue");
    }

    console.log("✅ Upload réussi :", json.secure_url);
    return json.secure_url;
  } catch (err) {
    console.error("❌ Erreur upload Cloudinary :", err.message);
    throw err;
  }
}

function VideoPlayer({ uri }) {
  const [playing, setPlaying] = useState(false);
  const player = useVideoPlayer(uri, (player) => {
    player.loop = false;
  });

  const togglePlay = () => {
    if (playing) player.pause();
    else player.play();
    setPlaying(!playing);
  };

  return (
    <TouchableOpacity
      style={styles.videoContainer}
      activeOpacity={0.9}
      onPress={togglePlay}
    >
      <VideoView
        player={player}
        style={styles.video}
        contentFit="contain"
      />
      {!playing && (
        <View style={styles.playButtonOverlay}>
          <Ionicons name="play-circle" size={56} color="rgba(255,255,255,0.8)" />
        </View>
      )}
    </TouchableOpacity>
  );
}
// 🧱 Carte d’un post
function PostCard({ post, onOpenProfile, onLike, onComment, onMessage, colors, user }) {
  // Permet d'utiliser un état local pour chaque vidéo
  const [playingStates, setPlayingStates] = useState({});
  // Animation du coeur
  const scaleAnim = useRef(new Animated.Value(1)).current;
  // Etat pour modal image plein écran
  const [selectedImage, setSelectedImage] = useState(null); // string ou null
  const [showImageModal, setShowImageModal] = useState(false);

  const handleLike = () => {
    Animated.sequence([
      Animated.timing(scaleAnim, { toValue: 1.4, duration: 150, useNativeDriver: true }),
      Animated.timing(scaleAnim, { toValue: 1, duration: 150, useNativeDriver: true }),
    ]).start();
    onLike(post.id);
  };

  // Ouvre la modal image
  const openImageModal = (uri) => {
    setSelectedImage(uri);
    setShowImageModal(true);
  };
  // Ferme la modal image
  const closeImageModal = () => {
    setShowImageModal(false);
    setSelectedImage(null);
  };

  // Pour l'effet d'ombre et espacement type Instagram
  return (
    <View style={styles.igCardShadow}>
      <View style={[
        styles.igCard,
        { backgroundColor: colors.bg }
      ]}>
        {/* Auteur */}
        <TouchableOpacity
          style={styles.igCardHeader}
          onPress={() => {
            Alert.alert(
              "Profil utilisateur",
              `Souhaitez-vous voir le profil de ${post.author?.username || "cet utilisateur"} ?`,
              [
                { text: "Non", style: "cancel" },
                {
                  text: "Oui",
                  onPress: () => onOpenProfile(post.author),
                },
              ]
            );
          }}
        >
          <Image
            source={{
              uri:
                post.author?.photoUrl ||
                "https://cdn-icons-png.flaticon.com/512/149/149071.png",
            }}
            style={styles.igAvatar}
          />
          <View style={{ flex: 1 }}>
            <Text style={[styles.igAuthor, { color: colors.text }]}>
              {post.author?.username || "Anonyme"}
            </Text>
            <Text style={[styles.igMeta, { color: colors.muted }]}>
              {post.typeLabel} ·{" "}
              {new Date(post.createdAt).toLocaleString("fr-FR", {
                dateStyle: "short",
                timeStyle: "short",
              })}
            </Text>
          </View>
        </TouchableOpacity>

        {/* Images / Vidéos */}
        {post.images?.length > 0 && (
          <View style={styles.igMediaWrap}>
            {post.images.map((uri, idx) => {
              if (!uri) return null;
              const isVideo =
                uri.includes("/video/") || uri.endsWith(".mp4") || uri.endsWith(".mov");
              const cleanUri = uri.replace("http://", "https://");
              if (isVideo) {
                return <VideoPlayer key={`${post.id}-${idx}`} uri={cleanUri} />;
              }
              // Image : touche pour ouvrir modal
              return (
                <TouchableOpacity
                  key={`${post.id}-${idx}`}
                  activeOpacity={0.9}
                  onPress={() => openImageModal(cleanUri)}
                  style={{ flex: 1 }}
                >
                  <Image
                    source={{ uri: cleanUri }}
                    style={styles.igPostImage}
                    resizeMode="cover"
                    onError={(e) =>
                      console.warn("❌ Erreur affichage image :", cleanUri, e.nativeEvent.error)
                    }
                  />
                </TouchableOpacity>
              );
            })}
          </View>
        )}
        {/* Modal image plein écran */}
        <Modal visible={showImageModal && !!selectedImage} transparent animationType="fade">
          <TouchableWithoutFeedback onPress={closeImageModal}>
            <View style={styles.igImageModalOverlay}>
              <Image
                source={{ uri: selectedImage }}
                style={styles.igImageModalImg}
                resizeMode="contain"
              />
            </View>
          </TouchableWithoutFeedback>
        </Modal>
        {/* Texte du post */}
        {!!post.text && (
          <View style={styles.igPostTextBox}>
            <Text style={styles.igPostText}>{post.text}</Text>
          </View>
        )}
        {/* Barre d’actions Instagram */}
        <View style={styles.igActionsRow}>
          <View style={styles.igActionsIcons}>
            <TouchableOpacity onPress={handleLike} style={styles.igActionBtn}>
              <Animated.View style={{ transform: [{ scale: scaleAnim }] }}>
                <Ionicons
                  name={post.isLiked ? "heart" : "heart-outline"}
                  size={30}
                  color={post.isLiked ? "#e74c3c" : colors.text}
                />
              </Animated.View>
              <Text style={styles.igActionCountSmall}>
                {post.likeCount} j’aime
              </Text>
            </TouchableOpacity>

            <TouchableOpacity onPress={() => onComment(post)} style={styles.igActionBtn}>
              <Ionicons name="chatbubble-outline" size={28} color={colors.text} />
              <Text style={styles.igActionCountSmall}>
                {post.commentCount} com.
              </Text>
            </TouchableOpacity>

            {post.author?.id !== user?.id && (
              <TouchableOpacity onPress={() => onMessage(post.author)} style={styles.igActionBtn}>
                <Ionicons name="paper-plane-outline" size={28} color={colors.text} />
                <Text style={styles.igActionCountSmall}>Message</Text>
              </TouchableOpacity>
            )}
          </View>
        </View>
      </View>
    </View>
  );
}

// 💬 MODAL COMMENTAIRES
function CommentsModal({ visible, onClose, post, user, colors, onNewComment }) {
  const [comments, setComments] = useState([]);
  const [loading, setLoading] = useState(false);
  const [commentText, setCommentText] = useState("");
  const [sending, setSending] = useState(false);

  // Charger les commentaires du post
  const fetchComments = async () => {
    if (!post?.id) return;
    setLoading(true);
    try {
      const res = await api.get(`/community/posts/${post.id}/comments`);
      setComments(Array.isArray(res.data) ? res.data : []);
    } catch (err) {
      console.log("❌ Erreur fetchComments :", err.message);
      setComments([]);
    }
    setLoading(false);
  };

  React.useEffect(() => {
    if (visible) {
      fetchComments();
    } else {
      setCommentText("");
      setComments([]);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [visible, post?.id]);

  // Publier un commentaire
  const submitComment = async () => {
    if (!user?.id) return Alert.alert("Erreur", "Utilisateur introuvable.");
    if (!commentText.trim()) return;
    setSending(true);
    try {
      await api.post(`/community/posts/${post.id}/comments`, {
        userId: user.id,
        text: commentText.trim(),
      });
      if (post?.id && typeof onNewComment === "function") {
        onNewComment(post.id);
      }
      setCommentText("");
      fetchComments();
    } catch (err) {
      Alert.alert("Erreur", "Impossible de publier le commentaire.");
    }
    setSending(false);
  };

  return (
    <Modal visible={visible} animationType="slide" transparent>
      <TouchableWithoutFeedback onPress={Keyboard.dismiss}>
        <KeyboardAvoidingView
          style={styles.modalCommentsOverlay}
          behavior={Platform.OS === "ios" ? "padding" : "height"}
        >
          <View style={[styles.modalComments, { backgroundColor: colors.bg }]}>
            <View style={styles.modalCommentsHeader}>
              <Text style={[styles.modalCommentsTitle, { color: colors.text }]}>Commentaires</Text>
              <TouchableOpacity onPress={onClose}>
                <Ionicons name="close" size={26} color={colors.text} />
              </TouchableOpacity>
            </View>
            <View style={styles.modalCommentsContent}>
              {loading ? (
                <ActivityIndicator color={colors.accent} style={{ marginTop: 20 }} />
              ) : (
                <FlatList
                  data={comments}
                  keyExtractor={(c) => String(c.id)}
                  renderItem={({ item }) => (
                    <View style={styles.commentRow}>
                      <Image
                        source={{
                          uri:
                            item.author?.photoUrl ||
                            "https://cdn-icons-png.flaticon.com/512/149/149071.png",
                        }}
                        style={styles.commentAvatar}
                      />
                      <View style={styles.commentBubble}>
                        <Text style={[styles.commentAuthor, { color: colors.accent }]}>
                          {item.author?.username || "Anonyme"}
                        </Text>
                        <Text style={[styles.commentText, { color: colors.text }]}>
                          {item.text}
                        </Text>
                        <Text style={[styles.commentDate, { color: colors.muted }]}>
                          {new Date(item.createdAt).toLocaleString("fr-FR", {
                            dateStyle: "short",
                            timeStyle: "short",
                          })}
                        </Text>
                      </View>
                    </View>
                  )}
                  ListEmptyComponent={
                    <Text style={{ color: colors.muted, textAlign: "center", marginTop: 20 }}>
                      Aucun commentaire.
                    </Text>
                  }
                  contentContainerStyle={{ paddingBottom: 20 }}
                  showsVerticalScrollIndicator={false}
                />
              )}
            </View>
            <View style={styles.modalCommentsInputRow}>
              <TextInput
                style={[styles.modalCommentsInput, { color: colors.text, borderColor: colors.border }]}
                placeholder="Ajouter un commentaire..."
                placeholderTextColor={colors.muted}
                value={commentText}
                onChangeText={setCommentText}
                editable={!sending}
                multiline
              />
              <TouchableOpacity
                style={styles.modalCommentsPublishBtn}
                onPress={submitComment}
                disabled={sending || !commentText.trim()}
              >
                {sending ? (
                  <ActivityIndicator color={colors.accent} size="small" />
                ) : (
                  <Text style={{ color: colors.accent, fontWeight: "bold" }}>Publier</Text>
                )}
              </TouchableOpacity>
            </View>
          </View>
        </KeyboardAvoidingView>
      </TouchableWithoutFeedback>
    </Modal>
  );
}

export default function CommunauteScreen({ route, navigation }) {
  const { user } = useContext(AppContext);
  const { theme } = useContext(AppContext);
  const isDark = theme === "dark";

  // Indicateur message non lu
  const [hasUnreadMessages, setHasUnreadMessages] = useState(false);

  useEffect(() => {
    const checkUnreadMessages = async () => {
      try {
        const res = await api.get(`/community/messages/unread/${user?.id}`);
        setHasUnreadMessages(res.data?.hasUnread || false);
      } catch (err) {
        console.warn("❌ Erreur récupération messages non lus :", err.message);
        setHasUnreadMessages(false);
      }
    };

    if (user?.id) {
      checkUnreadMessages();
      const interval = setInterval(checkUnreadMessages, 10000); // vérifie toutes les 10s
      return () => clearInterval(interval);
    }
  }, [user?.id]);

  useLayoutEffect(() => {
    navigation.setOptions({
      headerRight: () => (
        <TouchableOpacity
          style={{ marginRight: 15 }}
          onPress={async () => {
            try {
              await api.post(`/community/messages/mark-read/${user.id}`);
              setHasUnreadMessages(false);
              navigation.navigate("Conversations", { user });
            } catch (err) {
              console.warn("❌ Erreur marquage messages lus :", err.message);
              navigation.navigate("Conversations", { user });
            }
          }}
        >
          <View>
            <Ionicons name="chatbubbles-outline" size={26} color="#2a9d8f" />
            {hasUnreadMessages && (
              <View
                style={{
                  position: "absolute",
                  top: -3,
                  right: -3,
                  width: 10,
                  height: 10,
                  borderRadius: 5,
                  backgroundColor: "red",
                }}
              />
            )}
          </View>
        </TouchableOpacity>
      ),
    });
  }, [navigation, user, hasUnreadMessages]);

  const colors = useMemo(
    () => ({
      bg: isDark ? "#0f1113" : "#fff",
      text: isDark ? "#fff" : "#222",
      muted: isDark ? "#a7a7a7" : "#666",
      card: isDark ? "#171a1d" : "#f7f7f9",
      border: isDark ? "#2b2f34" : "#e5e5ea",
      accent: "#2a9d8f",
    }),
    [isDark]
  );

  const [filter, setFilter] = useState("all");
  const [posts, setPosts] = useState([]);
  const [refreshing, setRefreshing] = useState(false);
  const [composerVisible, setComposerVisible] = useState(false);
  const [composerType, setComposerType] = useState("eau_de_mer");
  const [composerText, setComposerText] = useState("");
  const [composerMedia, setComposerMedia] = useState([]);
  const [sending, setSending] = useState(false);

  const removeMedia = (i) => setComposerMedia((prev) => prev.filter((_, x) => x !== i));

  const typeLabel = (t) =>
    ({
      eau_de_mer: "Aquarium (eau de mer)",
      eau_douce: "Aquarium (eau douce)",
      terrarium: "Terrarium",
      bassin: "Bassin",
      plante: "Plante",
    }[t] || "Autre");

  // 🔁 Charger les posts
  const fetchPosts = async () => {
    try {
      const res = await api.get("/community/posts", {
        params: filter !== "all" ? { type: filter } : {},
      });
      const data = Array.isArray(res.data) ? res.data : [];

      const fixed = data.map((p) => {
        const isLiked = Array.isArray(p.likedBy)
          ? p.likedBy.some((u) => u.id === user?.id)
          : false;
        const likeCount = Array.isArray(p.likedBy) ? p.likedBy.length : 0;

        let images = [];
        if (Array.isArray(p.images)) images = p.images;
        else if (typeof p.images === "string") {
          try {
            images = JSON.parse(p.images);
          } catch {
            images = [p.images];
          }
        }
        const commentCount = Array.isArray(p.comments) ? p.comments.length : 0;
        return { ...p, typeLabel: typeLabel(p.type), images, isLiked, likeCount, commentCount };
      });

      setPosts(fixed);
      console.log("✅ Posts reçus :", fixed.length);
    } catch (err) {
      console.log("❌ Erreur fetchPosts :", err.message);
      Alert.alert("Erreur", "Impossible de charger les publications.");
    }
  };

  useFocusEffect(useCallback(() => { fetchPosts(); }, [filter]));

  // ✍️ Publier un post
  const submitPost = async () => {
    if (!user?.id) return Alert.alert("Erreur", "Utilisateur introuvable.");
    if (!composerText.trim() && composerMedia.length === 0)
      return Alert.alert("Erreur", "Ajoute un texte, une image ou une vidéo.");

    try {
      setSending(true);
      const uploadedUrls = [];
      for (const uri of composerMedia) {
        const url = await uploadMediaToCloudinary(uri);
        uploadedUrls.push(url);
      }

      await api.post("/community/posts", {
        userId: user.id,
        type: composerType,
        text: composerText.trim(),
        images: uploadedUrls,
      });

      setComposerVisible(false);
      setComposerText("");
      setComposerMedia([]);
      fetchPosts();
    } catch (err) {
      console.log("❌ Erreur post :", err.message);
      Alert.alert("Erreur", "Impossible de publier.");
    } finally {
      setSending(false);
    }
  };

  // 🖼️ Galerie
  const pickMedias = async () => {
    const { status } = await ImagePicker.requestMediaLibraryPermissionsAsync();
    if (status !== "granted") {
      Alert.alert("Permission refusée", "Autorise l'accès à la galerie.");
      return;
    }

    const res = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.All,
      allowsMultipleSelection: true,
      selectionLimit: 4,
      quality: 0.8,
    });

    if (!res.canceled && res.assets?.length) {
      const uris = res.assets.map((a) => a.uri);
      setComposerMedia((prev) => [...prev, ...uris].slice(0, 4));
    }
  };

  // 📸 Caméra
  const takePhoto = async () => {
    const { status } = await ImagePicker.requestCameraPermissionsAsync();
    if (status !== "granted") {
      Alert.alert("Permission refusée", "Autorise l'accès à la caméra.");
      return;
    }

    const res = await ImagePicker.launchCameraAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      quality: 0.8,
    });

    if (!res.canceled) {
      const uri = res.assets?.[0]?.uri ?? res.uri;
      if (uri) setComposerMedia((prev) => [...prev, uri].slice(0, 4));
    }
  };

  // Fonctions barre d’actions
  const onLike = async (postId) => {
    setPosts((prev) =>
      prev.map((p) =>
        p.id === postId
          ? {
              ...p,
              isLiked: !p.isLiked,
              likeCount: p.isLiked ? p.likeCount - 1 : p.likeCount + 1,
            }
          : p
      )
    );
    try {
      await api.post(`/community/posts/${postId}/like`, { userId: user.id });
    } catch (err) {
      console.log("❌ Erreur like :", err.message);
    }
  };

  // 💬 Etat modal commentaires
  const [commentsModalVisible, setCommentsModalVisible] = useState(false);
  const [selectedPost, setSelectedPost] = useState(null);

  const onComment = (post) => {
    setSelectedPost(post);
    setCommentsModalVisible(true);
  };

  const onMessage = (author) => {
  navigation.navigate("Chat", {
  toUser: author,          // la personne à qui on parle
  currentUser: user        // toi-même
});
};

  return (
    <View style={[styles.container, { backgroundColor: colors.bg }]}>
      {/* 🔹 Filtres (ScrollView pour éviter la hauteur variable du FlatList) */}
      <ScrollView
        horizontal
        showsHorizontalScrollIndicator={false}
        contentContainerStyle={styles.filtersBar}
      >
        {FILTERS.map((item) => {
          const active = filter === item.value;
          return (
            <TouchableOpacity
              key={item.value}
              onPress={() => setFilter(item.value)}
              style={[
                styles.chip,
                {
                  backgroundColor: active ? colors.accent : colors.card,
                  borderColor: colors.border,
                },
              ]}
              activeOpacity={0.8}
            >
              <MaterialCommunityIcons
                name={item.icon}
                size={14}
                color={active ? "#fff" : colors.text}
              />
              <Text
                style={{
                  color: active ? "#fff" : colors.text,
                  marginLeft: 6,
                  fontWeight: "600",
                }}
              >
                {item.label}
              </Text>
            </TouchableOpacity>
          );
        })}
      </ScrollView>

      {/* Liste des posts */}
      <FlatList
        data={posts}
        keyExtractor={(p) => String(p.id)}
        renderItem={({ item }) => (
          <PostCard
            post={item}
            onOpenProfile={(author) => navigation.navigate("ProfilPublic", { author })}
            onLike={onLike}
            onComment={onComment}
            onMessage={onMessage}
            colors={colors}
            user={user}
          />
        )}
        keyboardShouldPersistTaps="handled"
        contentContainerStyle={[
          styles.listContent,
          posts.length === 0 && { paddingTop: 20 },
        ]}
        refreshControl={<RefreshControl refreshing={refreshing} onRefresh={fetchPosts} />}
        ListEmptyComponent={
          <Text style={styles.emptyText}>
            Aucune publication pour cette catégorie.
          </Text>
        }
      />

      {/* Modal de publication */}
      <Modal visible={composerVisible} animationType="slide">
        <SafeAreaView style={[styles.modalWrap, { backgroundColor: colors.bg }]}>
          <View style={styles.modalHeader}>
            <TouchableOpacity onPress={() => setComposerVisible(false)}>
              <Text style={[styles.cancel, { color: colors.accent }]}>Annuler</Text>
            </TouchableOpacity>
            <TouchableOpacity onPress={submitPost} disabled={sending}>
              {sending ? (
                <ActivityIndicator color={colors.accent} />
              ) : (
                <Text style={[styles.publish, { color: colors.accent }]}>Publier</Text>
              )}
            </TouchableOpacity>
          </View>

          <View style={styles.typeRow}>
            {FILTERS.filter((f) => f.value !== "all").map((f) => {
              const active = composerType === f.value;
              return (
                <TouchableOpacity
                  key={f.value}
                  onPress={() => setComposerType(f.value)}
                  style={[
                    styles.typeChip,
                    {
                      backgroundColor: active ? colors.accent : colors.card,
                      borderColor: colors.border,
                    },
                  ]}
                >
                  <Text
                    style={{
                      color: active ? "#fff" : colors.text,
                      fontWeight: "600",
                    }}
                  >
                    {f.label}
                  </Text>
                </TouchableOpacity>
              );
            })}
          </View>

          <TextInput
            style={[styles.inputText, { color: colors.text, borderColor: colors.border }]}
            multiline
            placeholder="Exprime-toi..."
            placeholderTextColor={colors.muted}
            value={composerText}
            onChangeText={setComposerText}
          />

          <View style={styles.mediaPreviewRow}>
            {composerMedia.map((uri, i) => (
              <View key={i} style={styles.thumbWrap}>
                <Image source={{ uri }} style={styles.thumb} />
                <TouchableOpacity
                  style={styles.removeThumb}
                  onPress={() => removeMedia(i)}
                >
                  <Ionicons name="close" size={14} color="#fff" />
                </TouchableOpacity>
              </View>
            ))}
          </View>

          <View style={styles.buttonsRow}>
            <TouchableOpacity
              style={[styles.addPhoto, { borderColor: colors.border }]}
              onPress={pickMedias}
              activeOpacity={0.7}
            >
              <Ionicons name="images-outline" size={22} color={colors.accent} />
              <Text style={{ color: colors.text, fontSize: 12 }}>Galerie</Text>
            </TouchableOpacity>

            <TouchableOpacity
              style={[styles.addPhoto, { borderColor: colors.border }]}
              onPress={takePhoto}
              activeOpacity={0.7}
            >
              <Ionicons name="camera-outline" size={22} color={colors.accent} />
              <Text style={{ color: colors.text, fontSize: 12 }}>Caméra</Text>
            </TouchableOpacity>
          </View>
        </SafeAreaView>
      </Modal>

      {/* Modal commentaires */}
      <CommentsModal
        visible={commentsModalVisible}
        onClose={() => setCommentsModalVisible(false)}
        post={selectedPost}
        user={user}
        colors={colors}
        onNewComment={(postId) => {
          setPosts((prev) =>
            prev.map((p) =>
              p.id === postId
                ? { ...p, commentCount: (p.commentCount || 0) + 1 }
                : p
            )
          );
        }}
      />

      {/* Bouton flottant créer */}
      <TouchableOpacity
        style={[styles.fab, { backgroundColor: colors.accent }]}
        onPress={() => setComposerVisible(true)}
      >
        <Ionicons name="add" size={24} color="#fff" />
      </TouchableOpacity>
    </View>
  );
}

// 🎨 Styles
const styles = StyleSheet.create({
  container: { flex: 1 },
  chip: {
    flexDirection: "row",
    alignItems: "center",
    paddingVertical: 6,
    paddingHorizontal: 10,
    borderRadius: 20,
    marginRight: 8,
    borderWidth: 1,
  },
  // Instagram style post card
  igCardShadow: {
    marginBottom: 24,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 3 },
    shadowOpacity: 0.16,
    shadowRadius: 8,
    elevation: 6,
    borderRadius: 18,
    backgroundColor: "transparent",
  },
  igCard: {
    borderRadius: 18,
    overflow: "hidden",
    paddingBottom: 0,
    backgroundColor: "#fff",
  },
  igCardHeader: {
    flexDirection: "row",
    alignItems: "center",
    paddingHorizontal: 12,
    paddingTop: 12,
    paddingBottom: 8,
  },
  igAvatar: { width: 38, height: 38, borderRadius: 19, marginRight: 10, backgroundColor: "#eee" },
  igAuthor: { fontWeight: "bold", fontSize: 15 },
  igMeta: { fontSize: 12, color: "#888" },
  igMediaWrap: {
    width: "100%",
    aspectRatio: 1,
    backgroundColor: "#111",
    justifyContent: "center",
    alignItems: "center",
    flexDirection: "row",
    gap: 0,
    minHeight: 260,
    maxHeight: 420,
    overflow: "hidden",
  },
  igPostImage: {
    flex: 1,
    width: "100%",
    height: undefined,
    aspectRatio: 1,
    borderRadius: 0,
    backgroundColor: "#111",
    minHeight: 260,
    maxHeight: 420,
  },
  igImageModalOverlay: {
    flex: 1,
    backgroundColor: "#000",
    justifyContent: "center",
    alignItems: "center",
  },
  igImageModalImg: {
    width: "100%",
    height: "100%",
  },
  igActionsRow: {
    paddingTop: 14,
    paddingBottom: 6,
    alignItems: "center",
    justifyContent: "center",
  },
  igActionsIcons: {
    flexDirection: "row",
    justifyContent: "space-around",
    alignItems: "center",
    width: "100%",
  },
  igActionBtn: {
    alignItems: "center",
    justifyContent: "center",
    paddingHorizontal: 14,
  },
  igActionCount: {
    fontSize: 14,
    fontWeight: "600",
  },
  igActionCountSmall: {
    fontSize: 12,
    fontWeight: "600",
    marginTop: 4,
    color: "#666",
    textAlign: "center",
  },
  igPostText: {
    fontSize: 15,
    textAlign: "left",
    color: "#2a2a2a",
  },
  playOverlay: {
    position: "absolute",
    top: "40%",
    left: "40%",
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "rgba(0,0,0,0.3)",
    borderRadius: 100,
    padding: 10,
  },

  // ✅ Correction du layout vide
  listContent: {
  flexGrow: 1,
  paddingHorizontal: 16,
  paddingBottom: 100,
  paddingTop: 10,
},
  emptyText: {
    textAlign: "center",
    color: "#888",
    fontSize: 15,
    marginTop: 18,
  },

  // 🧱 Modal
  modalWrap: {
    flex: 1,
    paddingTop: Platform.OS === "ios" ? 60 : 30,
    paddingHorizontal: 16,
  },
  modalHeader: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: 16,
  },
  typeRow: {
    flexDirection: "row",
    flexWrap: "wrap",
    gap: 8,
    marginBottom: 10,
  },
  typeChip: {
    borderWidth: 1,
    paddingVertical: 6,
    paddingHorizontal: 12,
    borderRadius: 14,
  },
  inputText: {
    minHeight: 100,
    borderWidth: 1,
    borderRadius: 12,
    padding: 10,
    marginBottom: 10,
  },
  mediaPreviewRow: { flexDirection: "row", flexWrap: "wrap", gap: 8 },
  thumbWrap: { position: "relative" },
  thumb: { width: 100, height: 100, borderRadius: 10 },
  removeThumb: {
    position: "absolute",
    top: 4,
    right: 4,
    backgroundColor: "rgba(0,0,0,0.6)",
    borderRadius: 10,
    padding: 3,
  },
  buttonsRow: {
    flexDirection: "row",
    justifyContent: "space-around",
    marginTop: 10,
  },
  addPhoto: {
    alignItems: "center",
  },
  fab: {
    position: "absolute",
    bottom: 20,
    right: 20,
    width: 60,
    height: 60,
    borderRadius: 30,
    alignItems: "center",
    justifyContent: "center",
  },
  filtersBar: {
    paddingHorizontal: 12,
    height: 60, // hauteur fixe
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "flex-start",
    backgroundColor: "#fff",
    borderBottomWidth: 1,
    borderColor: "#ddd",
  },
    videoContainer: {
    width: "100%",
    height: 260,
    borderRadius: 12,
    overflow: "hidden",
    backgroundColor: "#000",
    justifyContent: "center",
    alignItems: "center",
  },
  video: {
    width: "100%",
    height: "100%",
  },
  playButtonOverlay: {
    position: "absolute",
    top: "40%",
    left: "40%",
    justifyContent: "center",
    alignItems: "center",
  },
  // 💬 Modal commentaires
  modalCommentsOverlay: {
    flex: 1,
    backgroundColor: "rgba(0,0,0,0.22)",
    justifyContent: "flex-end",
  },
  modalComments: {
    height: "70%",
    borderTopLeftRadius: 18,
    borderTopRightRadius: 18,
    paddingHorizontal: 12,
    paddingTop: 12,
    paddingBottom: 6,
    backgroundColor: "#fff",
    shadowColor: "#000",
    shadowOffset: { width: 0, height: -2 },
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 12,
  },
  modalCommentsHeader: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    marginBottom: 6,
    paddingBottom: 4,
    borderBottomWidth: 1,
    borderColor: "#ececec",
  },
  modalCommentsTitle: {
    fontSize: 18,
    fontWeight: "bold",
  },
  modalCommentsContent: {
    flex: 1,
    marginTop: 8,
    marginBottom: 8,
  },
  commentRow: {
    flexDirection: "row",
    alignItems: "flex-start",
    marginBottom: 12,
    gap: 6,
  },
  commentAvatar: {
    width: 32,
    height: 32,
    borderRadius: 16,
    marginRight: 7,
    backgroundColor: "#eee",
  },
  commentBubble: {
    flex: 1,
    backgroundColor: "#f2f2f2",
    borderRadius: 10,
    padding: 8,
    minHeight: 36,
  },
  commentAuthor: {
    fontWeight: "bold",
    fontSize: 14,
    marginBottom: 2,
  },
  commentText: {
    fontSize: 15,
  },
  commentDate: {
    fontSize: 11,
    marginTop: 2,
    textAlign: "right",
  },
  modalCommentsInputRow: {
    flexDirection: "row",
    alignItems: "center",
    borderTopWidth: 1,
    borderColor: "#ececec",
    paddingTop: 6,
    paddingBottom: Platform.OS === "ios" ? 16 : 12,
    paddingHorizontal: 2,
    gap: 4,
  },
  modalCommentsInput: {
    flex: 1,
    minHeight: 36,
    maxHeight: 80,
    borderWidth: 1,
    borderRadius: 14,
    paddingHorizontal: 10,
    paddingVertical: 6,
    fontSize: 15,
    backgroundColor: "#fafbfc",
    marginRight: 6,
  },
  modalCommentsPublishBtn: {
    paddingHorizontal: 8,
    paddingVertical: 5,
    borderRadius: 10,
    justifyContent: "center",
    alignItems: "center",
  },
  igPostTextBox: {
    backgroundColor: "#ffffff",
    borderRadius: 14,
    marginHorizontal: 12,
    marginTop: 10,
    paddingVertical: 10,
    paddingHorizontal: 14,
    shadowColor: "#000",
    shadowOffset: { width: 0, height: 3 },
    shadowOpacity: 0.08,
    shadowRadius: 6,
    elevation: 4,
    borderWidth: 1,
    borderColor: "#e6e6e6",
  },
});
  


  
 