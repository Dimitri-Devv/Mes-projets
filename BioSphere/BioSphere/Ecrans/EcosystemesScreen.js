import React, { useState, useCallback, useContext } from 'react';
import { View, Text, FlatList, Image, StyleSheet, TouchableOpacity, Alert, Animated } from 'react-native';
import { useFocusEffect } from '@react-navigation/native';
import { Ionicons, MaterialCommunityIcons } from '@expo/vector-icons';
import { LinearGradient } from 'expo-linear-gradient';
import api from './services/api';
import { AppContext } from './context/AppContext';

export default function EcosystemesScreen({ route, navigation }) {
  const { user } = useContext(AppContext);
  const [ecosystems, setEcosystems] = useState([]);
  const [loading, setLoading] = useState(false);

  // 🟢 Récupération du thème global
  const { theme } = useContext(AppContext);
  const isDark = theme === 'dark';

  const colors = {
    bg: isDark ? '#121212' : '#fff',
    text: isDark ? '#fff' : '#333',
    secondaryText: isDark ? '#bbb' : '#666',
    cardBg: isDark ? '#1e1e1e' : '#f9f9f9',
    border: isDark ? '#333' : '#ddd',
    accent: isDark ? '#2aefb0' : '#2aefb0',
  };

  // Animation fadeAnim globale
  const fadeAnim = React.useRef(new Animated.Value(0)).current;

  // Utilitaire pour extraire la dernière valeur d'un historique de paramètres
  const getLastParamValue = (history) => {
    if (!Array.isArray(history) || history.length === 0) return "-";
    return history[history.length - 1]?.value ?? "-";
  };

  // 🧭 Traduction du type
  const formatType = (type) => {
    switch (type) {
      case 'eau_de_mer': return 'Eau de mer';
      case 'eau_douce': return 'Eau douce';
      case 'terrarium': return 'Terrarium';
      case 'bassin': return 'Bassin';
      case 'plante': return 'Plante';
      default: return type;
    }
  };

  // 🚦 Détermine la couleur de statut selon la valeur et type de paramètre
  const getStatusColor = (param, value, type) => {
    if (value == null) return 'gray';
    switch (param) {
      case 'Température':
        if (type === 'eau_de_mer') {
          if (value >= 22 && value <= 28) return 'green';
          if ((value >= 20 && value < 22) || (value > 28 && value <= 30)) return 'orange';
          return 'red';
        } else if (type === 'eau_douce') {
          if (value >= 20 && value <= 25) return 'green';
          if ((value >= 18 && value < 20) || (value > 25 && value <= 27)) return 'orange';
          return 'red';
        } else {
          if (value >= 18 && value <= 26) return 'green';
          if ((value >= 16 && value < 18) || (value > 26 && value <= 28)) return 'orange';
          return 'red';
        }
      case 'pH':
        if (type === 'eau_de_mer') {
          if (value >= 7.8 && value <= 8.4) return 'green';
          if ((value >= 7.5 && value < 7.8) || (value > 8.4 && value <= 8.6)) return 'orange';
          return 'red';
        } else {
          if (value >= 6.5 && value <= 7.5) return 'green';
          if ((value >= 6.0 && value < 6.5) || (value > 7.5 && value <= 8.0)) return 'orange';
          return 'red';
        }
      case 'Densité':
        if (type === 'eau_de_mer') {
          if (value >= 1020 && value <= 1026) return 'green';
          if ((value >= 1018 && value < 1020) || (value > 1026 && value <= 1028)) return 'orange';
          return 'red';
        } else {
          // Densité non applicable ou neutre pour eau douce/terrarium
          return 'gray';
        }
      default:
        return 'gray';
    }
  };

  // 🔄 Récupération des écosystèmes et paramètres rapides avec mise à jour partielle
  const refreshEcosystems = async (silent = false) => {
    if (!user?.id) return;
    if (!silent) setLoading(true);
    try {
      const res = await api.get(`/ecosystems/${user.id}`);
      const data = res.data;

      const defaultParamsByType = {
        eau_de_mer: ["Température", "pH", "Densité"],
        eau_douce: ["Température", "pH", "NO2"],
        terrarium: ["Température", "Humidité", "Luminosité"],
        plante: ["Humidité", "Luminosité", "Température"],
        bassin: ["Température", "pH", "NO3"],
      };

      // Créer une map des écosystèmes actuels par id pour comparaison
      const currentMap = new Map(ecosystems.map(e => [e.id, e]));

      const updated = [];
      for (const eco of data) {
        const summaryParams = eco.summaryParams?.length
          ? eco.summaryParams
          : defaultParamsByType[eco.type] || ["Température", "pH"];
        const summaryValues = {};

        for (const param of summaryParams) {
          try {
            const res = await api.get(`/parameters/history/${eco.id}/${encodeURIComponent(param)}`);
            summaryValues[param] = getLastParamValue(res.data);
          } catch {
            summaryValues[param] = "-";
          }
        }

        const newEco = { ...eco, summaryValues, summaryParams };

        // Vérifier si l'écosystème est nouveau ou a changé (shallow compare)
        const existingEco = currentMap.get(eco.id);
        if (!existingEco) {
          // Nouvel écosystème
          updated.push(newEco);
        } else {
          // Comparer summaryValues et summaryParams
          const hasChanged =
            JSON.stringify(existingEco.summaryValues) !== JSON.stringify(newEco.summaryValues) ||
            JSON.stringify(existingEco.summaryParams) !== JSON.stringify(newEco.summaryParams) ||
            existingEco.name !== newEco.name ||
            existingEco.photoUrl !== newEco.photoUrl ||
            existingEco.type !== newEco.type;

          if (hasChanged) {
            updated.push(newEco);
          } else {
            updated.push(existingEco);
          }
          currentMap.delete(eco.id);
        }
      }

      // currentMap contient les écosystèmes supprimés, on ne les ajoute pas

      setEcosystems(updated);
    } catch (err) {
      console.error(err);
      Alert.alert('Erreur', "Impossible de charger les écosystèmes");
    } finally {
      if (!silent) setLoading(false);
    }
  };

  useFocusEffect(useCallback(() => {
    refreshEcosystems(true);
    Animated.timing(fadeAnim, {
      toValue: 1,
      duration: 500,
      useNativeDriver: true,
    }).start();
  }, [user?.id]));

  // Chargement initial des écosystèmes uniquement au montage
  React.useEffect(() => {
    refreshEcosystems();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  // ❌ Suppression d’un écosystème (mise à jour immédiate)
  const handleDelete = (ecoId, name) => {
    Alert.alert("Supprimer", `Supprimer "${name}" ?`, [
      { text: 'Annuler', style: 'cancel' },
      {
        text: 'Supprimer',
        style: 'destructive',
        onPress: async () => {
          try {
            await api.delete(`/ecosystems/${ecoId}`);
            setEcosystems(prev => prev.filter(e => e.id !== ecoId));
          } catch (e) {
            console.error(e);
            Alert.alert('Erreur', "Suppression impossible");
          }
        }
      }
    ]);
  };

  // 🪴 Composant interne pour l'affichage d’un écosystème avec animation
  const EcosystemCard = ({ eco, colors, onPress, onDelete, fadeAnim }) => {
    return (
      <Animated.View style={{ opacity: fadeAnim, transform: [{ scale: fadeAnim.interpolate({ inputRange: [0, 1], outputRange: [0.9, 1] }) }] }}>
        <TouchableOpacity
          style={[styles.card, { backgroundColor: colors.cardBg }]}
          onPress={onPress}
        >
          {!!eco.photoUrl && (
            <View>
              <Image source={{ uri: eco.photoUrl }} style={styles.cardImage} />
              <TouchableOpacity style={styles.deleteIcon} onPress={onDelete}>
                <Ionicons name="trash" size={22} color="white" />
              </TouchableOpacity>
              <LinearGradient
                colors={['rgba(0,0,0,0)', 'rgba(0,0,0,0.7)']}
                style={styles.overlay}
              >
                <Text style={styles.overlayName}>{eco.name}</Text>
                <View style={styles.overlayTypeRow}>
                  <Ionicons name="water-outline" size={16} color="white" />
                  <Text style={styles.overlayType}>  {formatType(eco.type)}</Text>
                </View>
                {/* Paramètres résumés */}
                <View style={styles.paramContainer}>
                  {eco.summaryParams?.map((param) => (
                    <View key={param} style={styles.paramRow}>
                      <Text style={styles.paramName}>{param}</Text>
                      <Text style={styles.paramValue}>
                        {eco.summaryValues?.[param] ?? "-"}
                      </Text>
                    </View>
                  ))}
                </View>
              </LinearGradient>
            </View>
          )}
        </TouchableOpacity>
      </Animated.View>
    );
  };

  return (
    <View style={[styles.container, { backgroundColor: colors.bg }]}>
      <Text style={[styles.title, { color: colors.text }]}>
        Bienvenue {user?.username || user?.email} 👋
      </Text>
      <View style={{ alignItems: 'center', marginBottom: 15 }}>
        <Text style={[styles.subtitle, { color: colors.text, fontWeight: '700', fontSize: 18 }]}>
          🌿 Mes Écosystèmes
        </Text>
        <View style={{ width: 60, height: 3, backgroundColor: colors.accent, borderRadius: 3, marginTop: 4 }} />
      </View>

      {loading ? (
        <Text style={[styles.loadingText, { color: colors.secondaryText }]}>Chargement…</Text>
      ) : ecosystems.length === 0 ? (
        <Text style={[styles.empty, { color: colors.secondaryText }]}>
          Aucun écosystème trouvé.
        </Text>
      ) : (
        <FlatList
          data={ecosystems}
          keyExtractor={(i) => i.id.toString()}
          renderItem={({ item: eco }) => (
            <EcosystemCard
              eco={eco}
              colors={colors}
              onPress={() => navigation.navigate('EcosystemDetail', { ecosystem: eco, user })}
              onDelete={() => handleDelete(eco.id, eco.name)}
              fadeAnim={fadeAnim}
            />
          )}
          showsVerticalScrollIndicator={false}
        />
      )}
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 20 },
  title: { fontSize: 22, fontWeight: 'bold', marginBottom: 10, textAlign: 'center' },
  subtitle: { fontSize: 16, marginBottom: 15 },
  loadingText: { textAlign: 'center', marginTop: 40 },
  empty: { textAlign: 'center', marginTop: 40 },
  card: {
    borderRadius: 12,
    marginBottom: 10,
    overflow: 'hidden',
  },
  cardImage: {
    width: '100%',
    height: 220,
  },
  deleteIcon: {
    position: 'absolute',
    top: 8,
    right: 8,
    backgroundColor: 'rgba(0,0,0,0.5)',
    borderRadius: 16,
    padding: 4,
    zIndex: 2,
  },
  overlay: {
    position: 'absolute',
    bottom: 0,
    width: '100%',
    paddingVertical: 10,
    paddingHorizontal: 15,
    borderBottomLeftRadius: 12,
    borderBottomRightRadius: 12,
  },
  overlayName: {
    color: 'white',
    fontSize: 18,
    fontWeight: 'bold',
  },
  overlayTypeRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 4,
  },
  overlayType: {
    color: 'white',
    fontSize: 14,
  },
  summaryRow: {
    flexDirection: 'row',
    marginTop: 6,
  },
  summaryItem: {
    flexDirection: 'row',
    alignItems: 'center',
    marginRight: 15,
  },
  statusDot: {
    marginRight: 4,
  },
  paramContainer: {
    flexDirection: "row",
    justifyContent: "space-around",
    paddingVertical: 4,
    borderBottomLeftRadius: 10,
    borderBottomRightRadius: 10,
  },
  paramRow: {
    alignItems: "center",
    justifyContent: "center",
    width: "33%",
  },
  paramName: {
    fontSize: 10,
    color: "#fff",
    fontWeight: "500", 
    textAlign: "center",
  },
  paramValue: {
    fontSize: 11,
    fontWeight: "bold",
    color: "#2aefb0",
    textAlign: "center",
  },
});
