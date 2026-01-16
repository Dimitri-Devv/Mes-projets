import React, { useState, useContext } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  Alert,
  StyleSheet,
  KeyboardAvoidingView,
  Platform,
  TouchableWithoutFeedback,
  Keyboard,
} from "react-native";
import { Ionicons } from "@expo/vector-icons";
import { LinearGradient } from "expo-linear-gradient";
import api from "./services/api";
import { AppContext } from "./context/AppContext";

export default function ChangePasswordScreen({ navigation }) {
  const { user } = useContext(AppContext);
const userId = user?.id;

console.log("DEBUG USER:", user, "USER ID:", userId);

  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPass, setConfirmPass] = useState("");

  const [showOldPass, setShowOldPass] = useState(false);
  const [showNewPass, setShowNewPass] = useState(false);
  const [showConfirmPass, setShowConfirmPass] = useState(false);

  const handleSubmit = async () => {
  if (!userId) {
    return Alert.alert("Erreur", "Utilisateur non identifié");
  }

  if (!oldPassword || !newPassword || !confirmPass)
    return Alert.alert("Erreur", "Tous les champs sont obligatoires");

  if (newPassword !== confirmPass)
    return Alert.alert("Erreur", "Les nouveaux mots de passe ne correspondent pas");

  try {
    await api.put(`/auth/change-password/${userId}`, {
      oldPassword,
      newPassword,
    });

    Alert.alert("✅ Succès", "Mot de passe mis à jour !");
    navigation.goBack();
  } catch (e) {
    Alert.alert("Erreur", e?.response?.data?.error || "Modification impossible");
  }
};
  return (
    <TouchableWithoutFeedback onPress={Keyboard.dismiss}>
      <KeyboardAvoidingView
        style={{ flex: 1 }}
        behavior={Platform.OS === "ios" ? "padding" : "height"}
      >
        <LinearGradient
          colors={["#2a9d8f", "#00B4D8"]}
          style={styles.container}
        >
          <View style={styles.card}>
            <Text style={styles.title}>Modifier le mot de passe</Text>

            {/* Ancien mot de passe */}
            <View style={styles.inputWrapper}>
              <TextInput
                style={styles.input}
                placeholder="Mot de passe actuel"
                placeholderTextColor="#888"
                secureTextEntry={!showOldPass}
                value={oldPassword}
                onChangeText={setOldPassword}
              />
              <TouchableOpacity
                onPress={() => setShowOldPass(!showOldPass)}
                style={styles.eye}
              >
                <Ionicons
                  name={showOldPass ? "eye-off-outline" : "eye-outline"}
                  size={22}
                  color="#2a9d8f"
                />
              </TouchableOpacity>
            </View>

            {/* Nouveau */}
            <View style={styles.inputWrapper}>
              <TextInput
                style={styles.input}
                placeholder="Nouveau mot de passe"
                placeholderTextColor="#888"
                secureTextEntry={!showNewPass}
                value={newPassword}
                onChangeText={setNewPassword}
              />
              <TouchableOpacity
                onPress={() => setShowNewPass(!showNewPass)}
                style={styles.eye}
              >
                <Ionicons
                  name={showNewPass ? "eye-off-outline" : "eye-outline"}
                  size={22}
                  color="#2a9d8f"
                />
              </TouchableOpacity>
            </View>

            {/* Confirmation */}
            <View style={styles.inputWrapper}>
              <TextInput
                style={styles.input}
                placeholder="Confirmer le nouveau mot de passe"
                placeholderTextColor="#888"
                secureTextEntry={!showConfirmPass}
                value={confirmPass}
                onChangeText={setConfirmPass}
              />
              <TouchableOpacity
                onPress={() => setShowConfirmPass(!showConfirmPass)}
                style={styles.eye}
              >
                <Ionicons
                  name={showConfirmPass ? "eye-off-outline" : "eye-outline"}
                  size={22}
                  color="#2a9d8f"
                />
              </TouchableOpacity>
            </View>

            <TouchableOpacity style={styles.button} onPress={handleSubmit}>
              <Text style={styles.buttonText}>Confirmer</Text>
            </TouchableOpacity>
          </View>
        </LinearGradient>
      </KeyboardAvoidingView>
    </TouchableWithoutFeedback>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, justifyContent: "center", alignItems: "center" },
  card: {
    backgroundColor: "rgba(255,255,255,0.95)",
    width: "85%",
    padding: 20,
    borderRadius: 16,
    alignItems: "center",
  },
  title: { fontSize: 22, fontWeight: "900", color: "#2a9d8f", marginBottom: 20 },
  inputWrapper: { width: "100%", position: "relative" },
  input: {
    width: "100%",
    padding: 12,
    backgroundColor: "#fff",
    borderRadius: 10,
    marginVertical: 8,
    borderColor: "#ddd",
    borderWidth: 1,
    color: "#444",
  },
  eye: { position: "absolute", right: 15, top: 18 },
  button: {
    backgroundColor: "#2a9d8f",
    paddingVertical: 14,
    borderRadius: 10,
    width: "100%",
    alignItems: "center",
    marginTop: 10,
  },
  buttonText: { color: "#fff", fontWeight: "bold" },
});