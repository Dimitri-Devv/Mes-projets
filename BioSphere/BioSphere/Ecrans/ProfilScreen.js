import React, { useState, useEffect, useContext } from 'react';
import { View, Text, TextInput, StyleSheet, Image, Alert, ScrollView, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import * as ImagePicker from 'expo-image-picker';
import api from './services/api';
import { AppContext } from './context/AppContext';

export default function ProfilScreen({ navigation }) {
  const { user } = useContext(AppContext);
  const [profile, setProfile] = useState(user);
  const [originalProfile, setOriginalProfile] = useState(user);
  const [loading, setLoading] = useState(false);
  const [editing, setEditing] = useState(false);

  // 🎨 Thème global
  const { theme } = useContext(AppContext);
  const isDark = theme === 'dark';

  const colors = {
    bg: isDark ? '#121212' : '#fff',
    text: isDark ? '#fff' : '#333',
    card: isDark ? '#1e1e1e' : '#f7f7f7',
    inputBg: isDark ? '#222' : '#fff',
    border: isDark ? '#444' : '#ddd',
  };

  // 🔄 Recharge les données utilisateur depuis le back
  const fetchProfile = async () => {
  try {
    const res = await api.get(`/auth/${profile.id}`);
    if (res.data) {
      setProfile({
        ...profile,
        username: res.data.username ?? '',
        firstName: res.data.firstName ?? '',
        lastName: res.data.lastName ?? '',
        email: res.data.email ?? profile.email,
        bio: res.data.bio ?? '',
        photoUrl: res.data.photoUrl ?? profile.photoUrl,
      });
      setOriginalProfile({
        id: profile.id,
        email: res.data.email ?? profile.email,
        username: res.data.username ?? '',
        firstName: res.data.firstName ?? '',
        lastName: res.data.lastName ?? '',
        bio: res.data.bio ?? '',
        photoUrl: res.data.photoUrl ?? profile.photoUrl,
      });
    }
  } catch (e) {
    console.error(e);
    Alert.alert("Erreur", "Impossible de charger les informations du profil");
  }
};

  useEffect(() => { fetchProfile(); }, []);

  // 📸 Changer la photo
  const pickImage = async () => {
    const res = await ImagePicker.launchImageLibraryAsync({
      allowsEditing: true,
      quality: 1,
    });
    if (!res.canceled) {
      setProfile({ ...profile, photoUrl: res.assets[0].uri });
    }
  };

const handleSave = async () => {
  try {
    const updatedUser = {
      firstName: profile.firstName,
      lastName: profile.lastName,
      username: profile.username,
      bio: profile.bio,
      photoUrl: profile.photoUrl,
    };

    const res = await api.put(`/auth/${profile.id}`, updatedUser);

    Alert.alert('✅ Succès', 'Profil mis à jour');
    setProfile(res.data);
    setOriginalProfile(res.data);
    setEditing(false);
  } catch (e) {
    console.log("Erreur update profil :", e);
    Alert.alert('❌ Erreur', "Impossible de mettre à jour le profil.");
  }
};

  return (
    <ScrollView style={[styles.container, { backgroundColor: colors.bg }]}>
      {/* --- PHOTO DE PROFIL --- */}
      <View style={styles.header}>
        <View>
          {editing ? (
            <TouchableOpacity onPress={pickImage} activeOpacity={0.8}>
              <View style={{ position: 'relative' }}>
                <Image
                  source={{ uri: profile.photoUrl ? profile.photoUrl : 'https://cdn-icons-png.flaticon.com/512/149/149071.png' }}
                  style={styles.avatar}
                />
                <View style={styles.editBadge}>
                  <Ionicons name="create-outline" size={16} color="#fff" />
                </View>
              </View>
            </TouchableOpacity>
          ) : (
            <View style={{ position: 'relative' }}>
              <Image
                source={{ uri: profile.photoUrl ? profile.photoUrl : 'https://cdn-icons-png.flaticon.com/512/149/149071.png' }}
                style={styles.avatar}
              />
            </View>
          )}
        </View>

        <View style={{ alignItems: 'center' }}>
          <Text style={[styles.usernameText, { color: colors.text }]}>
            {profile.username ? `@${profile.username}` : 'Utilisateur'}
          </Text>
          <Text style={[styles.emailText, { color: colors.text }]}>{profile.email}</Text>
        </View>
      </View>

      {!editing && (
        <TouchableOpacity
          style={[styles.saveButton, { backgroundColor: '#2a9d8f', marginTop: 10 }]}
          onPress={() => setEditing(true)}
        >
          <Text style={styles.saveButtonText}>Modifier les informations</Text>
        </TouchableOpacity>
      )}

      {/* --- CHAMPS DE PROFIL --- */}
      <View style={[styles.infoBlock, { backgroundColor: colors.card }]}>
        <Text style={[styles.label, { color: colors.text }]}>Prénom</Text>
        <TextInput
          style={[
            styles.input,
            { backgroundColor: colors.inputBg, borderColor: colors.border, color: colors.text },
          ]}
          value={profile.firstName || ''}
          editable={editing}
          placeholder="Entrez votre prénom"
          placeholderTextColor={isDark ? '#aaa' : '#666'}
          onChangeText={(text) => setProfile({ ...profile, firstName: text })}
        />

        <Text style={[styles.label, { color: colors.text }]}>Nom</Text>
        <TextInput
          style={[
            styles.input,
            { backgroundColor: colors.inputBg, borderColor: colors.border, color: colors.text },
          ]}
          value={profile.lastName || ''}
          editable={editing}
          placeholder="Entrez votre nom"
          placeholderTextColor={isDark ? '#aaa' : '#666'}
          onChangeText={(text) => setProfile({ ...profile, lastName: text })}
        />

        <Text style={[styles.label, { color: colors.text }]}>Nom d’utilisateur</Text>
        <TextInput
          style={[
            styles.input,
            { backgroundColor: colors.inputBg, borderColor: colors.border, color: colors.text },
          ]}
          value={profile.username || ''}
          editable={editing}
          placeholder="Entrez un nom d’utilisateur"
          placeholderTextColor={isDark ? '#aaa' : '#666'}
          onChangeText={(text) => setProfile({ ...profile, username: text })}
        />

        <Text style={[styles.label, { color: colors.text }]}>Bio</Text>
        <TextInput
          style={[
            styles.input,
            styles.textarea,
            { backgroundColor: colors.inputBg, borderColor: colors.border, color: colors.text },
          ]}
          value={profile.bio || ''}
          editable={editing}
          placeholder="Parlez un peu de vous..."
          placeholderTextColor={isDark ? '#aaa' : '#666'}
          multiline
          onChangeText={(text) => setProfile({ ...profile, bio: text })}
        />
      </View>

      {editing && (
        <>
          <TouchableOpacity
            style={[styles.saveButton, { backgroundColor: '#2a9d8f' }]}
            onPress={handleSave}
          >
            <Text style={styles.saveButtonText}>Enregistrer</Text>
          </TouchableOpacity>

          <TouchableOpacity
            style={[styles.saveButton, { backgroundColor: '#e63946', marginTop: 10 }]}
            onPress={() => { setProfile(originalProfile); setEditing(false); }}
          >
            <Text style={styles.saveButtonText}>Annuler</Text>
          </TouchableOpacity>
        </>
      )}

      {/* 🔐 Modifier mot de passe */}
      <TouchableOpacity
        style={[styles.saveButton, { backgroundColor: '#0077b6', marginTop: 10 }]}
        onPress={() => navigation.navigate('ChangePassword', { userId: profile.id })}
      >
        <Text style={styles.saveButtonText}>Modifier le mot de passe</Text>
      </TouchableOpacity>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 20 },
  header: {
    alignItems: 'center',
    marginBottom: 20,
    position: 'relative',
  },
  avatar: {
    width: 120,
    height: 120,
    borderRadius: 100,
    borderWidth: 3,
    borderColor: '#2a9d8f',
    marginBottom: 10,
  },
  editBadge: {
    position: 'absolute',
    right: 6,
    bottom: 6,
    width: 28,
    height: 28,
    borderRadius: 14,
    backgroundColor: '#2a9d8f',
    alignItems: 'center',
    justifyContent: 'center',
    borderWidth: 2,
    borderColor: '#fff',
  },
  usernameText: { fontSize: 20, fontWeight: 'bold' },
  emailText: { fontSize: 14 },
  infoBlock: {
    padding: 15,
    borderRadius: 12,
  },
  label: { fontWeight: '600', marginTop: 10 },
  input: {
    borderWidth: 1,
    borderRadius: 8,
    padding: 10,
    marginTop: 4,
  },
  textarea: {
    height: 80,
    textAlignVertical: 'top',
  },
  disabledInput: {
    opacity: 0.7,
  },
  saveButton: {
    backgroundColor: '#2a9d8f',
    padding: 14,
    borderRadius: 10,
    alignItems: 'center',
    marginTop: 20,
  },
  saveButtonText: { color: '#fff', fontWeight: 'bold' },
});
