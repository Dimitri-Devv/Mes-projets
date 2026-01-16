import React, { useState, useContext } from 'react';
import { View, Text, TextInput, TouchableOpacity, ActivityIndicator, Alert, StyleSheet, Image } from 'react-native';
import * as ImagePicker from 'expo-image-picker';
import { Picker } from '@react-native-picker/picker';
import { AppContext } from './context/AppContext';

const TYPE_OPTIONS = [
  { label: 'Eau douce', value: 'eau_douce' },
  { label: 'Eau de mer', value: 'eau_de_mer' },
  { label: 'Terrarium', value: 'terrarium' },
  { label: 'Bassin', value: 'bassin' },
  { label: 'Plante', value: 'plante' },
];

export default function AddEcosystemScreen({ navigation, route }) {
  const { user } = useContext(AppContext);
  const userId = user?.id;
  const [name, setName] = useState('');
  const [type, setType] = useState(TYPE_OPTIONS[0].value);
  const [photoUri, setPhotoUri] = useState(null);
  const [loading, setLoading] = useState(false);

  const { theme } = useContext(AppContext);
  const isDark = theme === 'dark';
  const colors = {
    bg: isDark ? '#121212' : '#fff',
    text: isDark ? '#fff' : '#333',
    card: isDark ? '#1e1e1e' : '#f9f9f9',
    accent: '#2a9d8f',
    border: isDark ? '#333' : '#ccc',
  };

  const pickImage = async () => {
    const res = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true, aspect: [4, 3], quality: 1,
    });
    if (!res.canceled) setPhotoUri(res.assets[0].uri);
  };

  const takePhoto = async () => {
    const cam = await ImagePicker.requestCameraPermissionsAsync();
    if (cam.status !== 'granted') return Alert.alert('Permission caméra refusée');
    const res = await ImagePicker.launchCameraAsync({ allowsEditing: true, quality: 1 });
    if (!res.canceled) setPhotoUri(res.assets[0].uri);
  };

  async function handleSubmit() {
    if (!userId) return Alert.alert('Erreur', 'Aucun utilisateur');
    if (!name.trim()) return Alert.alert('Erreur', 'Le nom est requis');
    setLoading(true);
    try {
      const body = { name: name.trim(), type, photoUrl: photoUri || null, userId };
      const r = await fetch('http://192.168.1.76:8081/api/ecosystems', {
        method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify(body),
      });
      if (!r.ok) throw new Error(await r.text());
      Alert.alert('Succès', 'Écosystème ajouté'); navigation.goBack();
    } catch (e) {
      Alert.alert('Erreur', e.message || "Ajout impossible");
    } finally { setLoading(false); }
  }

  return (
    <View style={[styles.container, { backgroundColor: colors.bg }]}>
      <Text style={[styles.title, { color: colors.accent }]}>Ajouter un écosystème</Text>

      <TextInput style={[styles.input, { borderColor: colors.border, color: colors.text }]} placeholder="Nom" placeholderTextColor={colors.border} value={name} onChangeText={setName} />

      <View style={[styles.pickerWrapper, { borderColor: colors.border }]}>
        <Picker selectedValue={type} onValueChange={setType} dropdownIconColor={colors.text}>
          {TYPE_OPTIONS.map(o => <Picker.Item key={o.value} label={o.label} value={o.value} color={colors.text} />)}
        </Picker>
      </View>

      {photoUri && <Image source={{ uri: photoUri }} style={styles.preview} />}

      <View style={{ flexDirection: 'row', gap: 10 }}>
        <TouchableOpacity style={[styles.photoButton, { backgroundColor: colors.accent }]} onPress={pickImage}>
          <Text style={styles.photoButtonText}>Depuis la galerie</Text>
        </TouchableOpacity>
        <TouchableOpacity style={[styles.photoButton, { backgroundColor: '#1b6e61' }]} onPress={takePhoto}>
          <Text style={styles.photoButtonText}>Prendre une photo</Text>
        </TouchableOpacity>
      </View>

      <TouchableOpacity style={[styles.button, { backgroundColor: colors.accent }]} onPress={handleSubmit} disabled={loading}>
        {loading ? <ActivityIndicator color="#fff" /> : <Text style={styles.buttonText}>Créer</Text>}
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: { flex: 1, padding: 20 },
  title: { fontSize: 22, fontWeight: 'bold', marginBottom: 20, textAlign: 'center' },
  input: { borderWidth: 1, borderRadius: 8, padding: 10, marginBottom: 15 },
  pickerWrapper: { borderWidth: 1, borderRadius: 8, marginBottom: 15 },
  photoButton: { padding: 10, borderRadius: 8, flex: 1, alignItems: 'center' },
  photoButtonText: { color: '#fff', fontWeight: 'bold' },
  preview: { width: '100%', height: 180, borderRadius: 10, marginBottom: 15 },
  button: { padding: 15, borderRadius: 10, alignItems: 'center', marginTop: 10 },
  buttonText: { color: '#fff', fontWeight: 'bold', fontSize: 16 },
});
