import React, { useContext, useRef, useState } from 'react';
import {
  View,
  Text,
  Switch,
  TouchableOpacity,
  StyleSheet,
  Alert,
  Animated,
} from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Ionicons } from '@expo/vector-icons';
import { AppContext } from './context/AppContext';

export default function ParametresScreen({ navigation }) {
  const { theme, toggleTheme, language, toggleLanguage } = useContext(AppContext);
  const [showToast, setShowToast] = useState(false);
  const fadeAnim = useRef(new Animated.Value(1)).current; // animation de sortie

  const isDark = theme === 'dark';
  const colors = {
    bg: isDark ? '#121212' : '#fff',
    text: isDark ? '#fff' : '#333',
    card: isDark ? '#1e1e1e' : '#f4f4f4',
  };

  const handleLogout = async () => {
    Alert.alert(
      'Se déconnecter',
      'Voulez-vous vraiment vous déconnecter ?',
      [
        { text: 'Annuler', style: 'cancel' },
        {
          text: 'Déconnexion',
          style: 'destructive',
          onPress: async () => {
            // 💨 Animation de fondu
            Animated.timing(fadeAnim, {
              toValue: 0,
              duration: 400,
              useNativeDriver: true,
            }).start(async () => {
              await AsyncStorage.clear();

              // ✅ Afficher le toast
              setShowToast(true);
              setTimeout(() => setShowToast(false), 2000);

              // 🔁 Retour à la connexion
              navigation.reset({
                index: 0,
                routes: [{ name: 'Auth' }],
              });
            });
          },
        },
      ]
    );
  };

  return (
    <Animated.View
      style={[
        styles.container,
        { backgroundColor: colors.bg, opacity: fadeAnim },
      ]}
    >
      <Text style={[styles.title, { color: colors.text }]}>Paramètres</Text>

      {/* 🌗 Mode sombre */}
      <View style={[styles.row, { backgroundColor: colors.card }]}>
        <Ionicons name="moon-outline" size={22} color={colors.text} />
        <Text style={[styles.label, { color: colors.text }]}>Mode sombre</Text>
        <Switch
          value={isDark}
          onValueChange={toggleTheme}
          trackColor={{ false: '#ccc', true: '#2a9d8f' }}
        />
      </View>

      {/* 🌍 Langue */}
      <TouchableOpacity
        style={[styles.row, { backgroundColor: colors.card }]}
        onPress={toggleLanguage}
      >
        <Ionicons name="language-outline" size={22} color={colors.text} />
        <Text style={[styles.label, { color: colors.text }]}>Langue</Text>
        <Text style={[styles.value, { color: colors.text }]}>
          {language === 'fr' ? 'Français' : 'English'}
        </Text>
      </TouchableOpacity>

      {/* 🚪 Déconnexion */}
      <TouchableOpacity style={styles.logoutButton} onPress={handleLogout}>
        <Ionicons name="log-out-outline" size={22} color="#fff" />
        <Text style={styles.logoutText}>Se déconnecter</Text>
      </TouchableOpacity>

      {/* 🍞 Toast de confirmation */}
      {showToast && (
        <View style={styles.toastContainer}>
          <Text style={styles.toastText}>Déconnexion réussie ✅</Text>
        </View>
      )}
    </Animated.View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 20 },
  title: { fontSize: 24, fontWeight: 'bold', marginBottom: 20 },
  row: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    borderRadius: 10,
    padding: 15,
    marginBottom: 12,
  },
  label: { flex: 1, marginLeft: 10, fontSize: 16 },
  value: { fontWeight: '600', fontSize: 15 },
  logoutButton: {
    marginTop: 40,
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#e63946',
    padding: 14,
    borderRadius: 10,
  },
  logoutText: {
    color: '#fff',
    fontWeight: 'bold',
    fontSize: 16,
    marginLeft: 8,
  },
  toastContainer: {
    position: 'absolute',
    bottom: 40,
    alignSelf: 'center',
    backgroundColor: '#2a9d8f',
    paddingVertical: 10,
    paddingHorizontal: 20,
    borderRadius: 25,
    shadowColor: '#000',
    shadowOpacity: 0.2,
    shadowRadius: 6,
    elevation: 4,
  },
  toastText: { color: '#fff', fontWeight: '600' },
});
