import React, { useState, useRef, useEffect } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Alert } from 'react-native';
import api from './services/api';

export default function VerifyEmailScreen({ route, navigation }) {
  const { email } = route.params;
  const [code, setCode] = useState(["", "", "", "", "", ""]);
  const inputs = useRef([]);

  const [timer, setTimer] = useState(60);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    let interval = setInterval(() => {
      setTimer((prev) => {
        if (prev <= 1) {
          clearInterval(interval);
          return 0;
        }
        return prev - 1;
      });
    }, 1000);
    return () => clearInterval(interval);
  }, []);

  const handleChange = (text, index) => {
    const newCode = [...code];
    newCode[index] = text;
    setCode(newCode);

    if (text && index < 5) {
      inputs.current[index + 1].focus();
    }
  };

  const verify = async () => {
    const codeStr = code.join('');
    if (codeStr.length !== 6) {
      return Alert.alert("Erreur", "Code incomplet");
    }

    try {
      setLoading(true);
      const res = await api.post('/auth/verify', { email, code: codeStr });

      Alert.alert("Succès ✅", "Email vérifié !");
      navigation.replace("Login");
    } catch (err) {
      const message = err.response?.data?.error || "Code incorrect ou expiré.";
      Alert.alert("Erreur", message);
    } finally {
      setLoading(false);
    }
  };

  const resendCode = async () => {
    if (timer > 0) return;
    try {
      await api.post('/auth/resend-code', { email });
      Alert.alert("✅ Nouveau code envoyé !");
      setTimer(60);
    } catch (err) {
      Alert.alert("Erreur", "Impossible de renvoyer le code.");
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Vérifiez votre email ✉️</Text>
      <Text style={styles.subtitle}>
        Nous avons envoyé un code à :{'\n'}
        <Text style={styles.email}>{email}</Text>
      </Text>

      <View style={styles.codeCard}>
        <View style={styles.codeContainer}>
          {code.map((value, idx) => (
            <TextInput
              key={idx}
              ref={(el) => (inputs.current[idx] = el)}
              style={styles.input}
              keyboardType="number-pad"
              maxLength={1}
              value={value}
              onChangeText={(t) => handleChange(t, idx)}
            />
          ))}
        </View>
      </View>

      <TouchableOpacity
        style={[styles.button, loading && { opacity: 0.4 }]}
        onPress={verify}
        disabled={loading}
      >
        <Text style={styles.buttonText}>Valider</Text>
      </TouchableOpacity>

      <TouchableOpacity
        disabled={timer > 0}
        onPress={resendCode}
      >
        <Text style={[styles.resendText, timer > 0 && { color: "#aaa" }]}>
          {timer > 0 ? `Renvoyer le code dans ${timer}s` : "Renvoyer le code"}
        </Text>
      </TouchableOpacity>

      <TouchableOpacity onPress={() => navigation.navigate("Login")}>
        <Text style={styles.backText}>Retour</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
    backgroundColor: '#e8faf8'
  },
  title: { fontSize: 24, fontWeight: 'bold', color: '#2a9d8f', marginBottom: 10, textAlign: 'center' },
  subtitle: { fontSize: 14, color: '#555', textAlign: 'center', marginBottom: 20 },
  email: { fontWeight: 'bold', color: '#2a9d8f' },
  codeCard: {
    backgroundColor: '#ffffff',
    paddingVertical: 20,
    paddingHorizontal: 10,
    borderRadius: 20,
    marginBottom: 25,
    width: '100%',
    alignItems: 'center',
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.12,
    shadowRadius: 8,
    elevation: 5,
  },
  codeContainer: { flexDirection: 'row', justifyContent: 'center', marginBottom: 20 },
  input: {
    width: 50,
    height: 60,
    borderWidth: 2,
    borderColor: '#2a9d8f',
    borderRadius: 14,
    marginHorizontal: 8,
    fontSize: 24,
    fontWeight: 'bold',
    textAlign: 'center',
    color: '#2a9d8f',
    backgroundColor: '#f3fdfa',
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.08,
    shadowRadius: 6,
    elevation: 3,
  },
  button: { backgroundColor: '#2a9d8f', padding: 15, borderRadius: 10, width: '100%', alignItems: 'center' },
  buttonText: { color: '#fff', fontWeight: 'bold', fontSize: 16 },
  resendText: { marginTop: 20, color: '#2a9d8f', fontWeight: '600' },
  backText: { marginTop: 15, color: '#666', fontWeight: '600' },
});