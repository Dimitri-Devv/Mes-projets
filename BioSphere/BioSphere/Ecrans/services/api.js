import axios from 'axios';
import { Platform } from 'react-native';

// ⚙️ Détection automatique selon le contexte
let API_BASE_URL;

if (Platform.OS === 'android') {
  // Android Emulator utilise 10.0.2.2 pour accéder au localhost
  API_BASE_URL = 'http://10.0.2.2:8081/api';
} else {
  // iOS (simu ou appareil physique via Expo)
  API_BASE_URL = 'http://192.168.1.76:8081/api'; // ⚠️ ton IP locale
}

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: { 'Content-Type': 'application/json' },
  timeout: 10000,
});

export default api;