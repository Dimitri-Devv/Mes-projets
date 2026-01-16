import React, { useState } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  Alert,
  Image,
  Animated,
  KeyboardAvoidingView,
  TouchableWithoutFeedback,
  Platform,
  Keyboard,
} from 'react-native';
import { LinearGradient } from 'expo-linear-gradient';
import { Ionicons } from "@expo/vector-icons";
import api from './services/api';
import { useContext } from 'react';
import { AppContext } from './context/AppContext';

export default function LoginScreen({ navigation }) {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const { setUser } = useContext(AppContext);

  // Connexion simple
  const handleLogin = async () => {
    if (!email || !password) return Alert.alert('Erreur', 'Veuillez remplir tous les champs');
    try {
      const res = await api.post('/auth/login', { email, password });
      console.log("Réponse Login:", res.data);
      setUser(res.data);
      navigation.replace('Main');
    } catch (err) {
      console.error(err?.response?.data);

      const errorMessage = err?.response?.data?.error;

      if (errorMessage?.includes("non vérifié")) {
        Alert.alert(
          "Email non vérifié",
          "Veuillez vérifier votre email avant de vous connecter.",
          [
            { text: "OK" },
            {
              text: "Vérifier maintenant",
              onPress: () => navigation.navigate('VerifyEmail', { email }),
            },
          ]
        );
      } else {
        Alert.alert('Erreur', errorMessage || 'Email ou mot de passe incorrect');
      }
    }
  };

  // Animation fond (dégradé en mouvement)
  const colorAnim = new Animated.Value(0);
  Animated.loop(
    Animated.sequence([
      Animated.timing(colorAnim, { toValue: 1, duration: 5000, useNativeDriver: false }),
      Animated.timing(colorAnim, { toValue: 0, duration: 5000, useNativeDriver: false }),
    ])
  ).start();

  const bg1 = colorAnim.interpolate({
    inputRange: [0, 1],
    outputRange: ['#56CCF2', '#2a9d8f'],
  });
  const bg2 = colorAnim.interpolate({
    inputRange: [0, 1],
    outputRange: ['#2F80ED', '#00B4D8'],
  });

  return (
    <TouchableWithoutFeedback onPress={Keyboard.dismiss} accessible={false}>
      <KeyboardAvoidingView
        style={{ flex: 1 }}
        behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      >
        <Animated.View style={[styles.container]}>
          <LinearGradient
            colors={[bg1, bg2]}
            style={StyleSheet.absoluteFillObject}
            start={{ x: 0, y: 0 }}
            end={{ x: 1, y: 1 }}
          />

          <View style={styles.card}>
            <Image
              source={require('../assets/logo.png')}
              style={{ width: 110, height: 110, marginBottom: 20 }}
            />
            <Text style={styles.title}>Connexion</Text>

            <TextInput
              style={styles.input}
              placeholder="Email"
              placeholderTextColor="#999"
              value={email}
              onChangeText={setEmail}
            />
            <View style={{ width: '100%', position: 'relative' }}>
              <TextInput
                style={styles.input}
                placeholder="Mot de passe"
                placeholderTextColor="#999"
                secureTextEntry={!showPassword}
                value={password}
                onChangeText={setPassword}
              />
              <TouchableOpacity
                onPress={() => setShowPassword(prev => !prev)}
                style={{ position: 'absolute', right: 15, top: 18 }}
              >
                <Ionicons
                  name={showPassword ? "eye-off-outline" : "eye-outline"}
                  size={22}
                  color="#2a9d8f"
                />
              </TouchableOpacity>
            </View>

            <TouchableOpacity style={styles.loginButton} onPress={handleLogin}>
              <Text style={styles.loginText}>Se connecter</Text>
            </TouchableOpacity>

            <TouchableOpacity
              onPress={() => navigation.navigate('Register')}
              style={styles.signupButton}
            >
              <Text style={styles.signupText}>Créer un compte</Text>
            </TouchableOpacity>
          </View>
        </Animated.View>
      </KeyboardAvoidingView>
    </TouchableWithoutFeedback>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  card: {
    backgroundColor: 'rgba(255,255,255,0.9)',
    borderRadius: 16,
    padding: 25,
    width: '85%',
    alignItems: 'center',
    shadowColor: '#000',
    shadowOpacity: 0.2,
    shadowRadius: 8,
    elevation: 4,
  },
  title: { fontSize: 26, fontWeight: 'bold', color: '#2a9d8f', marginBottom: 15 },
  input: {
    width: '100%',
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 10,
    padding: 12,
    marginVertical: 8,
    backgroundColor: '#fff',
    color: '#333',
  },
  loginButton: {
    backgroundColor: '#2a9d8f',
    padding: 14,
    borderRadius: 10,
    marginTop: 10,
    width: '100%',
    alignItems: 'center',
  },
  loginText: { color: '#fff', fontWeight: 'bold' },
  signupButton: { marginTop: 18 },
  signupText: { color: '#2a9d8f', fontWeight: 'bold' },
});
