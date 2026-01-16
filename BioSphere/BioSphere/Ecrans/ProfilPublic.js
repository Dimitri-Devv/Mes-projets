import React, { useEffect, useState, useContext } from "react";
import { View, Text, Image, StyleSheet, FlatList, ActivityIndicator, ScrollView, TouchableOpacity } from "react-native";
import * as Animatable from 'react-native-animatable';
import { LinearGradient } from 'expo-linear-gradient';
import api from "./services/api"; 
import { AppContext } from "./context/AppContext";

const ecosystemTypeLabels = {
  eau_douce: "Aquarium d’eau douce",
  eau_de_mer: "Aquarium marin",
  terrarium: "Terrarium",
  bassin: "Bassin",
  plante: "Plante",
};

export default function ProfilPublic({ route, navigation }) {
  const { author } = route.params || {};
  const { user } = useContext(AppContext);
  const [userData, setUserData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchUserData = async () => {
      setLoading(true);
      setError(null);
      try {
        let response;
        if (author?.id) {
          response = await api.get(`/community/users/${author.id}`);
        } else if (author?.username) {
          response = await api.get(`/community/users/username/${author.username}`);
        } else {
          setError("Aucun identifiant utilisateur fourni.");
          setLoading(false);
          return;
        }
        if (response.data) {
          setUserData(response.data);
        } else {
          setError("Profil utilisateur introuvable.");
        }
      } catch (err) {
        console.error("❌ Erreur chargement profil :", err.message);
        setError("Erreur lors du chargement du profil.");
      } finally {
        setLoading(false);
      }
    };
    fetchUserData();
  }, [author]);

  if (loading) {
    return (
      <View style={styles.loadingContainer}>
        <ActivityIndicator size="large" color="#2a9d8f" />
      </View>
    );
  }

  if (error) {
    return (
      <View style={styles.loadingContainer}>
        <Text>{error}</Text>
      </View>
    );
  }

  if (!userData) {
    return (
      <View style={styles.loadingContainer}>
        <Text>Utilisateur introuvable.</Text>
      </View>
    );
  }

  return (
    <LinearGradient
      colors={['#b3e5d9', '#ffffff']}
      start={{ x: 0, y: 0 }}
      end={{ x: 0, y: 1 }}
      style={{ flex: 1 }}
    >
      <ScrollView style={styles.container}>
        <View>
          <Image
            source={{
              uri: userData?.photoUrl || "https://cdn-icons-png.flaticon.com/512/149/149071.png",
            }}
            style={styles.avatar}
          />
          <Text style={styles.username}>{userData?.username || "Utilisateur inconnu"}</Text>
          {userData?.bio && <Text style={styles.bio}>{userData.bio}</Text>}

          <Text style={styles.sectionTitle}>
            Les écosystèmes de {userData?.username || "cet utilisateur"}
          </Text>
          <FlatList
            data={userData?.ecosystems || []}
            keyExtractor={(item) => String(item.id)}
            renderItem={({ item }) => (
              <Animatable.View animation="fadeIn" duration={500} style={styles.animatableView}>
                <TouchableOpacity
                  style={styles.ecoCard}
                  onPress={() => navigation.navigate("EcosystemPublic", { ecosystem: item })}
                  activeOpacity={0.85}
                >
                  {item.photoUrl && (
                    <Image
                      source={{ uri: item.photoUrl }}
                      style={styles.ecoImage}
                      resizeMode="cover"
                    />
                  )}
                  <View style={styles.ecoText}>
                    <Text style={styles.ecoName}>{item.name}</Text>
                    <Text style={styles.ecoType}>{ecosystemTypeLabels[item.type] || "Type inconnu"}</Text>
                  </View>
                </TouchableOpacity>
              </Animatable.View>
            )}
            ListEmptyComponent={<Text style={styles.empty}>Aucun écosystème public</Text>}
            scrollEnabled={false}
          />
        </View>
      </ScrollView>
    </LinearGradient>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: 'transparent',
    paddingHorizontal: 20,
    paddingTop: 20,
  },
  loadingContainer: { flex: 1, justifyContent: "center", alignItems: "center" },
  avatarBackground: {
    position: "absolute",
    top: 0,
    left: 0,
    right: 0,
    height: 200,
    borderBottomLeftRadius: 30,
    borderBottomRightRadius: 30,
  },
  avatar: {
    width: 130,
    height: 130,
    borderRadius: 65,
    alignSelf: "center",
    marginBottom: 15,
    borderWidth: 3,
    borderColor: "#2a9d8f",
    marginTop: 40,
  },
  username: { fontSize: 26, fontWeight: "800", textAlign: "center", marginBottom: 8, color: '#2a2a2a' },
  bio: { textAlign: "center", marginVertical: 12, color: "#555", fontSize: 16, lineHeight: 22, paddingHorizontal: 10 },
  sectionTitle: { fontSize: 22, fontWeight: "800", marginTop: 30, marginBottom: 15, color: '#2a2a2a', textAlign: 'center' },
  ecoCard: {
    backgroundColor: '#f5f9f8',
    borderRadius: 16,
    padding: 15,
    marginBottom: 15,
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.05,
    shadowRadius: 6,
    elevation: 2,
  },
  ecoImage: { width: '100%', height: 160, borderRadius: 12, marginBottom: 10 },
  ecoText: { alignItems: "center" },
  ecoName: { fontSize: 20, fontWeight: '700', color: '#2a2a2a', textAlign: 'center' },
  ecoType: { fontSize: 14, color: '#2a9d8f', textAlign: 'center', marginTop: 4 },
  empty: { textAlign: "center", color: "#999", marginTop: 10 },
  animatableView: {
    width: '100%',
  }
});