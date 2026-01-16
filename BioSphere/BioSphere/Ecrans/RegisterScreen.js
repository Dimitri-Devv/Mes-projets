import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Alert } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import api from './services/api';

export default function RegisterScreen({ navigation }) {
  const [email, setEmail] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);

  const validateEmail = (email) =>
    /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);

  const validatePassword = (password) =>
    password.length >= 6;

  const handleRegister = async () => {
    if (!email || !username || !password) return Alert.alert('Erreur', 'Tous les champs sont requis');
    if (!validateEmail(email))
      return Alert.alert('Email invalide', 'Veuillez entrer une adresse email valide.');

    if (!validatePassword(password))
      return Alert.alert('Mot de passe trop court', 'Minimum 6 caractères requis.');
    try {
      const res = await api.post('/auth/register', { email, username, password });
      navigation.replace('VerifyEmail', { email });
    } catch (err) {
      console.error(err);
      Alert.alert('Erreur', "Impossible de créer le compte.");
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Créer un compte</Text>

      <TextInput
        style={styles.input}
        placeholder="Email"
        placeholderTextColor="rgba(255,255,255,0.6)"
        value={email}
        onChangeText={setEmail}
        autoCapitalize="none"
      />
      <TextInput
        style={styles.input}
        placeholder="Nom d'utilisateur"
        placeholderTextColor="rgba(255,255,255,0.6)"
        value={username}
        onChangeText={setUsername}
        autoCapitalize="none"
      />
      <View style={styles.passwordContainer}>
        <TextInput
          style={[styles.input, { flex: 1 }]}
          placeholder="Mot de passe"
          placeholderTextColor="rgba(255,255,255,0.6)"
          secureTextEntry={!showPassword}
          value={password}
          onChangeText={setPassword}
        />
        <TouchableOpacity
          onPress={() => setShowPassword(!showPassword)}
          style={styles.eyeIcon}
        >
          <Ionicons
            name={showPassword ? "eye-off-outline" : "eye-outline"}
            size={22}
            color="#fff"
          />
        </TouchableOpacity>
      </View>

      <TouchableOpacity
        style={[styles.button, (!email || !username || !password) && { opacity: 0.4 }]}
        disabled={!email || !username || !password}
        onPress={handleRegister}
      >
        <Text style={styles.buttonText}>S'inscrire</Text>
      </TouchableOpacity>

      <Text style={styles.infoText}>
        Un code de vérification a été envoyé à votre email. Vous devrez confirmer votre adresse pour activer votre compte.
      </Text>

      <TouchableOpacity onPress={() => navigation.goBack()}>
        <Text style={styles.backText}>← Retour</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    padding: 20,
    backgroundColor: '#2a9d8f',
  },
  title: {
    fontSize: 32,
    fontWeight: '900',
    color: '#fff',
    textAlign: 'center',
    marginBottom: 30,
  },
  input: {
    width: '100%',
    borderWidth: 1,
    borderColor: 'rgba(255,255,255,0.4)',
    borderRadius: 14,
    padding: 14,
    marginVertical: 10,
    backgroundColor: 'rgba(255,255,255,0.15)',
    color: '#fff',
    fontWeight: '600',
  },
  button: {
    backgroundColor: '#fff',
    paddingVertical: 14,
    borderRadius: 14,
    marginTop: 15,
    width: '100%',
    alignItems: 'center',
    shadowColor: '#000',
    shadowOpacity: 0.15,
    shadowRadius: 6,
    elevation: 6,
  },
  buttonText: {
    color: '#2a9d8f',
    fontWeight: '900',
    fontSize: 16,
  },
  backText: {
    marginTop: 20,
    color: '#fff',
    fontWeight: '700',
    fontSize: 14,
    textAlign: 'center',
    textDecorationLine: 'underline'
  },
  infoText: {
    width: '100%',
    textAlign: 'center',
    color: '#fff',
    fontSize: 13,
    marginTop: 18,
    opacity: 0.85,
  },
  passwordContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    width: '100%',
  },
  eyeIcon: {
    position: 'absolute',
    right: 16,
  },
});

  