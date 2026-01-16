import React, { useEffect, useState, useContext, useMemo, useRef } from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  ImageBackground,
  TouchableOpacity,
  TextInput,
  Alert,
  Dimensions,
  KeyboardAvoidingView,
  Platform,
  Modal,
  Pressable,
} from 'react-native';
import { Ionicons, MaterialCommunityIcons } from '@expo/vector-icons';
import * as ImagePicker from 'expo-image-picker';
import { LineChart } from 'react-native-chart-kit';
import api from './services/api';
import { AppContext } from './context/AppContext';

// Icônes des paramètres
const ICONS = {
  'pH': 'water-outline',
  'Température': 'thermometer',
  'KH': 'chart-bell-curve',
  'CO₂': 'molecule-co2',
  'Oxygène dissous': 'air-filter',
  'Lumière': 'white-balance-sunny',
  'Nutriments': 'sprout',
  'Nitrites (NO₂)': 'test-tube',
  'Nitrates (NO₃)': 'flask-outline',
  'Phosphates (PO₄)': 'flask-round-bottom-outline',
  'Fer (Fe)': 'beaker',
  'Cuivre (Cu)': 'chemical-weapon',
  'Potassium (K)': 'atom',
  'Ammoniaque (NH₃/NH₄)': 'biohazard',
  'Silicates (SiO₂)': 'grain',
  'Salinité': 'waves',
  'Densité': 'scale-balance',
  'Calcium (Ca)': 'bone',
  'Magnésium (Mg)': 'beaker',
  'Strontium (Sr)': 'alpha-s-box',
  'Iode (I)': 'test-tube',
  'ORP (Redox)': 'current-ac',
  'Turbidité': 'beaker-remove-outline',
  'Humidité': 'water-percent',
  'Humidité du sol': 'flower',
  'EC (Conductivité)': 'flash-outline',
};

// Paramètres selon le type
const PARAMS_BY_TYPE = {
  eau_douce: ['pH', 'Température', 'Nitrites (NO₂)', 'Nitrates (NO₃)', 'Phosphates (PO₄)', 'Fer (Fe)', 'Cuivre (Cu)', 'Potassium (K)', 'KH', 'CO₂'],
  eau_de_mer: [
    'Température', 'Densité', 'pH', 'KH (Alcalinité)', 'Calcium (Ca)',
    'Magnésium (Mg)', 'Potassium (K)', 'Strontium (Sr)', 'Iode (I)', 'Nitrates (NO₃)',
    'Nitrites (NO₂)', 'Ammoniaque (NH₃/NH₄)', 'Phosphates (PO₄)', 'Fer (Fe)',
    'Silicates (SiO₂)', 'Oxygène dissous', 'ORP (Redox)'
  ],
  bassin: ['Température', 'pH', 'Oxygène dissous', 'Turbidité', 'Nitrites (NO₂)', 'Nitrates (NO₃)', 'Phosphates (PO₄)'],
  plante: ['Humidité du sol', 'Température', 'Lumière', 'CO₂', 'pH', 'EC (Conductivité)', 'Nutriments']
};

export default function EcosystemDetailScreen({ route, navigation }) {
  const { ecosystem } = route.params;
  const [eco, setEco] = useState(ecosystem);
  const [editing, setEditing] = useState(false);
  const [name, setName] = useState(ecosystem.name);
  const [photoUrl, setPhotoUrl] = useState(ecosystem.photoUrl);
  const [equipment, setEquipment] = useState([]);
  const [newEquip, setNewEquip] = useState({ name: '', brand: '', details: '' });
  const [selectedParam, setSelectedParam] = useState('pH');
  const availableParams = PARAMS_BY_TYPE[eco.type] || ['pH', 'Température', 'Nitrates'];
  const [history, setHistory] = useState([]);
  const [newValue, setNewValue] = useState('');

  // --- Résumé: sélection des paramètres affichés par l'utilisateur ---
  // Default: pH, Température, Densité si possible, sinon 3 premiers
  const defaultSummaryParams = useMemo(() => {
    const preferred = ['pH', 'Température', 'Densité'];
    let paramsToShow = preferred.filter(p => availableParams.includes(p));
    if (paramsToShow.length < 3) {
      paramsToShow = [...new Set([...paramsToShow, ...availableParams])].slice(0,3);
    }
    return paramsToShow;
  }, [availableParams]);
  const [selectedSummaryParams, setSelectedSummaryParams] = useState(
    ecosystem.summaryParams && ecosystem.summaryParams.length > 0
      ? ecosystem.summaryParams
      : defaultSummaryParams
  );
  useEffect(() => {
    if (eco.summaryParams && eco.summaryParams.length > 0) {
      setSelectedSummaryParams(eco.summaryParams);
    }
  }, [eco.summaryParams]);
  // Modal d'édition
  const [summaryModalVisible, setSummaryModalVisible] = useState(false);

  // 🔎 Cache des dernières valeurs par paramètre pour le carrousel
  const [latestByParam, setLatestByParam] = useState({});
  const importantParams = useMemo(() => {
    // On limite à 12 pour éviter un trop grand nombre d'appels
    const base = [
      'pH','Température','Densité','KH','Calcium (Ca)','Magnésium (Mg)',
      'Potassium (K)','Strontium (Sr)','Iode (I)','Nitrates (NO₃)',
      'Nitrites (NO₂)','Ammoniaque (NH₃/NH₄)','Phosphates (PO₄)','Fer (Fe)'
    ];
    // On garde seulement ceux réellement disponibles pour l'écosystème
    const merged = base.filter(p => (PARAMS_BY_TYPE[eco.type] || []).includes(p));
    return merged.slice(0, 12);
  }, [eco.type]);

  // Récupération de la dernière valeur pour chaque paramètre clé
  useEffect(() => {
    let isCancelled = false;
    (async () => {
      const next = {};
      for (const p of importantParams) {
        try {
          const r = await api.get(`/parameters/history/${eco.id}/${encodeURIComponent(p)}`);
          const arr = Array.isArray(r.data) ? r.data : [];
          if (arr.length) {
            const last = arr[arr.length - 1];
            next[p] = { value: last.value, date: last.measuredAt };
          }
        } catch(e) {}
      }
      if (!isCancelled) setLatestByParam(next);
    })();
    return () => { isCancelled = true; };
  }, [eco.id, importantParams]);

  // 🐠 Habitants (API)
  const [inhabitants, setInhabitants] = useState([]);
  const [newInhabitant, setNewInhabitant] = useState('');

  // Suggestions d'espèces selon le type d’écosystème
  const SUGGESTIONS_BY_TYPE = {
    eau_de_mer: [
      // 🟢 Facile
      'Amphiprion ocellaris (Poisson-clown)',
      'Chromis viridis (Demoiselle verte)',
      'Zebrasoma flavescens (Chirurgien jaune)',
      'Lysmata amboinensis (Crevette nettoyeuse)',
      'Pterapogon kauderni (Cardinal de Banggai)',
      'Gobiodon okinawae (Gobie corail jaune)',
      'Ecsenius bicolor (Blennie bicolore)',
      'Halichoeres chrysus (Labre à queue jaune)',
      'Salarias ramosus (Blennie mangeuse d’algues)',
      'Turbo fluctuosus (Escargot Turbo)',
      'Trochus histrio (Escargot Trochus)',
      'Nassarius vibex (Escargot Nassarius)',
      'Mithraculus sculptus (Crabe Mithrax)',
      'Ophiocoma erinaceus (Ophiure à épines)',
      // 🟡 Intermédiaire
      'Synchiropus splendidus (Mandarin)',
      'Centropyge bispinosa (Poisson-ange nain)',
      'Paracanthurus hepatus (Chirurgien bleu)',
      'Nemateleotris magnifica (Gobie de feu)',
      'Gramma loreto (Gramma royal)',
      'Pseudochromis paccagnellae (Dottyback bicolore)',
      'Chelmon rostratus (Poisson papillon cuivre)',
      'Acanthurus leucosternon (Chirurgien à poitrine blanche)',
      'Labroides dimidiatus (Labre nettoyeur)',
      // 🔴 Expert
      'Zanclus cornutus (Idole mauresque)',
      'Pygoplites diacanthus (Poisson-ange royal)',
      'Chaetodon auriga (Poisson-papillon filamenteux)',
      'Acreichthys tomentosus (Poisson lime tacheté)',
      'Hippocampus reidi (Hippocampe)',
      'Rhinomuraena quaesita (Murène ruban bleu)'
    ],

    eau_douce: [
      // 🟢 Facile
      'Rasbora arlequin',
      'Guppy (Poecilia reticulata)',
      'Platy (Xiphophorus maculatus)',
      'Molly (Poecilia sphenops)',
      'Corydoras paleatus',
      'Ancistrus sp. (Laveur de vitre)',
      'Paracheirodon innesi (Néon bleu)',
      'Danio rerio (Zébra)',
      'Otocinclus affinis',
      'Caridina multidentata (Crevette Amano)',
      // 🟡 Intermédiaire
      'Trichogaster lalius (Gourami nain)',
      'Pterophyllum scalare (Scalaire)',
      'Symphysodon aequifasciatus (Discus)',
      'Apistogramma agassizii (Cichlidé nain)',
      'Botia striata (Loche zébrée)',
      'Chromobotia macracanthus (Loche clown)',
      'Carassius auratus (Poisson rouge)',
      'Labeo bicolor',
      // 🔴 Expert
      'Arowana asiatique',
      'Oscar (Astronotus ocellatus)',
      'Pangasius hypophthalmus',
      'Piranha (Pygocentrus nattereri)',
      'Discus sauvage (Symphysodon tarzoo)'
    ],

    terrarium: [
      // 🟢 Facile
      'Eublepharis macularius (Gecko léopard)',
      'Pogona vitticeps (Dragon barbu)',
      'Lampropeltis getula (Serpent roi de Californie)',
      'Python regius (Python royal)',
      'Anolis carolinensis (Anolis vert)',
      'Testudo hermanni (Tortue d’Hermann)',
      'Ceratophrys cranwelli (Grenouille Pacman)',
      'Hemitheconyx caudicinctus (Gecko africain à queue grasse)',
      // 🟡 Intermédiaire
      'Chamaeleo calyptratus (Caméléon du Yémen)',
      'Furcifer pardalis (Caméléon panthère)',
      'Physignathus cocincinus (Dragon d’eau chinois)',
      'Iguana iguana (Iguane vert)',
      'Tiliqua scincoides (Scinque à langue bleue)',
      'Varanus exanthematicus (Varan des savanes)',
      // 🔴 Expert
      'Salvator merianae (Tégu argentin)',
      'Corallus hortulanus (Boa arboricole amazonien)',
      'Heloderma suspectum (Monstre de Gila)',
      'Uromastyx geyri (Uromastyx du Niger)'
    ],

    plante: [
      // 🟢 Facile
      'Anubias barteri',
      'Microsorum pteropus (Fougère de Java)',
      'Taxiphyllum barbieri (Mousse de Java)',
      'Vallisneria spiralis',
      'Sagittaria subulata',
      'Echinodorus amazonicus',
      'Cryptocoryne wendtii',
      // 🟡 Intermédiaire
      'Ludwigia repens',
      'Rotala rotundifolia',
      'Hygrophila polysperma',
      'Alternanthera reineckii',
      'Bacopa caroliniana',
      'Pogostemon helferi',
      // 🔴 Expert
      'Hemianthus callitrichoides (Cuba)',
      'Tonina fluviatilis',
      'Bucephalandra sp.',
      'Glossostigma elatinoides',
      'Eriocaulon cinereum'
    ]
  };

  const [filteredSuggestions, setFilteredSuggestions] = useState([]);
  const [showSuggestions, setShowSuggestions] = useState(false);

  // Charger les habitants depuis l'API
  const loadInhabitants = async () => {
    try {
      const res = await api.get(`/inhabitants/${eco.id}`);
      setInhabitants(res.data);
    } catch (err) {
      console.error("Erreur chargement habitants:", err);
    }
  };

  // Définir un joli titre pour l'écran
  useEffect(() => {
    navigation.setOptions({
      title: 'Détails de l’écosystème',
      headerBackTitle: 'Retour',
    });
  }, [navigation]);
  const addInhabitant = async () => {
    if (!newInhabitant.trim()) return;
    try {
      const res = await api.post('/inhabitants', {
        name: newInhabitant.trim(),
        ecosystem: { id: eco.id },
      });
      setInhabitants(prev => [...prev, res.data]);
      setNewInhabitant('');
    } catch (err) {
      Alert.alert('Erreur', "Impossible d'ajouter cet habitant");
    }
  };
  const removeInhabitant = async (id) => {
    try {
      await api.delete(`/inhabitants/${id}`);
      setInhabitants(prev => prev.filter(i => i.id !== id));
    } catch (err) {
      Alert.alert('Erreur', "Impossible de supprimer cet habitant");
    }
  };

  // Charger les habitants à l'ouverture de la page
  useEffect(() => { loadInhabitants(); }, []);

  // 🌙 Thème global
  const { theme } = useContext(AppContext);
  const isDark = theme === 'dark';
  const colors = {
    bgGradientStart: '#2a9d8f',
    bgGradientEnd: '#73d1c9',
    cardGradientStart: '#ffffff',
    cardGradientEnd: '#f3fdfa',
    chartGradientStart: '#f9fdfd',
    chartGradientEnd: '#e8faf8',
    textPrimary: '#2a2a2a',
    textSecondary: '#555',
    accent: '#2a9d8f',
    greenLight: '#d7f4e9',
    redLight: '#fddede',
    red: '#e63946',
    white: '#fff',
    transparentWhite: 'rgba(255,255,255,0.85)'
  };

  const loadEquipment = async () => {
    try { const r = await api.get(`/equipment/${eco.id}`); setEquipment(r.data); } catch {}
  };
  const loadHistory = async () => {
    try { const r = await api.get(`/parameters/history/${eco.id}/${encodeURIComponent(selectedParam)}`); setHistory(r.data || []); } catch {}
  };

  // Rafraîchit latestByParam pour le carrousel
  const refreshLatestValues = async () => {
    const next = {};
    for (const p of importantParams) {
      try {
        const r = await api.get(`/parameters/history/${eco.id}/${encodeURIComponent(p)}`);
        const arr = Array.isArray(r.data) ? r.data : [];
        if (arr.length) {
          const last = arr[arr.length - 1];
          next[p] = { value: last.value, date: last.measuredAt };
        }
      } catch(e) {}
    }
    setLatestByParam(next);
  };

  useEffect(() => { loadEquipment(); }, []);
  useEffect(() => { loadHistory(); }, [selectedParam]);

  const pickImage = async () => {
    const res = await ImagePicker.launchImageLibraryAsync({ allowsEditing: true, quality: 1 });
    if (!res.canceled) setPhotoUrl(res.assets[0].uri);
  };

  const saveEco = async () => {
    try {
      const body = { name, photoUrl, type: eco.type };
      const r = await api.put(`/ecosystems/${eco.id}`, body);
      setEco(r.data);
      setEditing(false);
      Alert.alert('Modifié', 'Écosystème mis à jour');
    } catch {
      Alert.alert('Erreur', 'Mise à jour impossible');
    }
  };

  const [showEquipInputs, setShowEquipInputs] = useState(false);
  const addEquip = async () => {
    if (!newEquip.name.trim()) return;
    try {
      await api.post('/equipment', { ecosystemId: eco.id, ...newEquip });
      setNewEquip({ name: '', brand: '', details: '' });
      loadEquipment();
      setShowEquipInputs(false);
    } catch { Alert.alert('Erreur', "Ajout matériel impossible"); }
  };

  const delEquip = async (id) => {
    try { await api.delete(`/equipment/${id}`); loadEquipment(); }
    catch { Alert.alert('Erreur', "Suppression impossible"); }
  };

  const addRecord = async () => {
    const v = parseFloat(newValue.replace(',', '.'));
    if (Number.isNaN(v)) return Alert.alert('Erreur', 'Entrez une valeur numérique');
    try {
      await api.post('/parameters/history', { ecosystemId: eco.id, name: selectedParam, value: v });
      setNewValue('');
      loadHistory();
      await refreshLatestValues();
    } catch { Alert.alert('Erreur', "Ajout de mesure impossible"); }
  };

  const handlePointClick = (data) => {
    if (!history[data.index]) return;
    const record = history[data.index];
    Alert.alert(
      "Supprimer la mesure ?",
      `Valeur : ${record.value}\nDate : ${(record.measuredAt || '').slice(0, 10)}`,
      [
        { text: "Annuler", style: "cancel" },
        {
          text: "Supprimer",
          style: "destructive",
          onPress: async () => {
            try {
              await api.delete(`/parameters/history/${record.id}`);
              loadHistory();
              await refreshLatestValues();
            }
            catch { Alert.alert("Erreur", "Impossible de supprimer cette valeur."); }
          }
        }
      ]
    );
  };

  // Compute last update date from history
  const lastUpdateDate = useMemo(() => {
    if (!history.length) return null;
    const dates = history.map(h => new Date(h.measuredAt));
    const maxDate = new Date(Math.max(...dates));
    return maxDate.toLocaleDateString();
  }, [history]);

  // Compute summary stats cards data from latestByParam per selectedSummaryParams
  const summaryParams = useMemo(() => {
    // Use selectedSummaryParams, fallback to defaultSummaryParams
    const paramsToShow = selectedSummaryParams && selectedSummaryParams.length
      ? selectedSummaryParams
      : defaultSummaryParams;
    return paramsToShow.map(param => {
      let value = null;
      if (latestByParam[param]) value = latestByParam[param].value;
      return { param, value };
    });
  }, [selectedSummaryParams, latestByParam, defaultSummaryParams]);

  // Fonction pour obtenir l'unité de mesure d'un paramètre
  const getUnit = (param) => {
    const units = {
      'Température': '°C',
      'pH': '',
      'Densité': 'g/cm³',
      'Calcium (Ca)': 'mg/L',
      'Magnésium (Mg)': 'mg/L',
      'Potassium (K)': 'mg/L',
      'Nitrites (NO₂)': 'mg/L',
      'Nitrates (NO₃)': 'mg/L',
      'Phosphates (PO₄)': 'mg/L',
      'Fer (Fe)': 'mg/L',
      'KH': '°dKH',
      'CO₂': 'mg/L',
      'Oxygène dissous': 'mg/L',
      'Ammoniaque (NH₃/NH₄)': 'mg/L',
    };
    return units[param] || '';
  };

  // Nouvelle analyse rapide avancée selon instructions (multi-paramètres)
  const quickAnalysis = useMemo(() => {
    const analyses = [];

    const idealRanges = {
      eau_douce: {
        'Température': [24, 27],
        'pH': [6.5, 7.8],
        'KH': [3, 10],
        'Nitrates (NO₃)': [0, 25],
        'Nitrites (NO₂)': [0, 0.2],
        'Phosphates (PO₄)': [0, 0.5],
      },
      eau_de_mer: {
        'Température': [24, 27],
        'Densité': [1.022, 1.026],
        'pH': [7.8, 8.4],
        'KH (Alcalinité)': [7, 12],
        'Calcium (Ca)': [380, 450],
        'Magnésium (Mg)': [1200, 1350],
        'Nitrates (NO₃)': [0, 10],
        'Nitrites (NO₂)': [0, 0.1],
        'Phosphates (PO₄)': [0, 0.1],
      },
      bassin: {
        'Température': [15, 25],
        'pH': [6.8, 8.0],
        'Oxygène dissous': [6, 10],
        'Nitrates (NO₃)': [0, 50],
        'Nitrites (NO₂)': [0, 0.3],
      },
    };

    Object.entries(latestByParam).forEach(([param, { value }]) => {
      const range = idealRanges[eco.type]?.[param];
      if (range && value != null) {
        let adjustedValue = value;
        if (param === 'Densité' && value > 10) adjustedValue = value / 1000;
        if (adjustedValue < range[0]) {
          analyses.push(`⚠️ ${param} trop bas (${adjustedValue}${getUnit(param) ? ' ' + getUnit(param) : ''}) — idéal entre ${range[0]} et ${range[1]}${getUnit(param) ? ' ' + getUnit(param) : ''}`);
        } else if (adjustedValue > range[1]) {
          analyses.push(`⚠️ ${param} trop élevé (${adjustedValue}${getUnit(param) ? ' ' + getUnit(param) : ''}) — idéal entre ${range[0]} et ${range[1]}${getUnit(param) ? ' ' + getUnit(param) : ''}`);
        }
      }
    });

    return analyses.length > 0 ? analyses : ['✅ Tous les paramètres sont dans les valeurs idéales.'];
  }, [latestByParam, eco.type]);

  // Helper function for status dot color on stat cards
  function getParamStatusColor(param) {
    const item = latestByParam[param];
    let value = item?.value;
    if (value === undefined || value === null) return '#bbb';
    if (param === 'Densité' && value > 10) value = value / 1000;
    const idealRanges = {
      eau_douce: {
        'Température': [24, 27],
        'pH': [6.5, 7.8],
        'KH': [3, 10],
        'Nitrates (NO₃)': [0, 25],
        'Nitrites (NO₂)': [0, 0.2],
        'Phosphates (PO₄)': [0, 0.5],
      },
      eau_de_mer: {
        'Température': [24, 27],
        'Densité': [1.022, 1.026],
        'pH': [7.8, 8.4],
        'KH (Alcalinité)': [7, 12],
        'Calcium (Ca)': [380, 450],
        'Magnésium (Mg)': [1200, 1350],
        'Nitrates (NO₃)': [0, 10],
        'Nitrites (NO₂)': [0, 0.1],
        'Phosphates (PO₄)': [0, 0.1],
      },
      bassin: {
        'Température': [15, 25],
        'pH': [6.8, 8.0],
        'Oxygène dissous': [6, 10],
        'Nitrates (NO₃)': [0, 50],
        'Nitrites (NO₂)': [0, 0.3],
      },
    };
    const range = idealRanges[eco.type]?.[param];
    if (range) {
      if (value < range[0] || value > range[1]) {
        return '#e63946'; // rouge
      } else {
        return '#2ecc71'; // vert
      }
    }
    if (param === selectedParam && quickAnalysis.some(line => line.includes('fluctuantes')))
      return 'orange';
    return '#bbb';
  }

  return (
    <KeyboardAvoidingView
      style={{ flex: 1, backgroundColor: colors.bgGradientEnd }}
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      keyboardVerticalOffset={100}
    >
      <View style={styles.flex1}>

        <ScrollView
          style={styles.flex1}
          contentContainerStyle={styles.scrollContent}
          keyboardShouldPersistTaps="handled"
        >
          {/* Image banner */}
          <View style={styles.imageBannerContainer}>
            {photoUrl ? (
              <ImageBackground
                source={{ uri: photoUrl }}
                style={styles.imageBanner}
                imageStyle={styles.imageBannerImageModern}
                resizeMode="cover"
              >
                {/* Bouton modifier en haut à droite de l'image */}
                <TouchableOpacity
                  style={{
                    position: 'absolute',
                    top: 12,
                    right: 12,
                    backgroundColor: 'rgba(255,255,255,0.8)',
                    borderRadius: 20,
                    padding: 6,
                    zIndex: 10,
                  }}
                  onPress={() => (editing ? saveEco() : setEditing(true))}
                  activeOpacity={0.8}
                >
                  <Ionicons
                    name={editing ? 'checkmark' : 'create-outline'}
                    size={22}
                    color={colors.accent}
                  />
                </TouchableOpacity>
                <View style={styles.ecoImageOverlay}>
                  {editing ? (
                    <TextInput
                      style={[
                        styles.ecoImageText,
                        {
                          backgroundColor: 'rgba(255,255,255,0.7)',
                          color: '#2a9d8f',
                          borderRadius: 8,
                          paddingHorizontal: 8,
                        },
                      ]}
                      value={name}
                      onChangeText={setName}
                      placeholder="Nom de l'écosystème"
                      placeholderTextColor="#2a9d8f"
                    />
                  ) : (
                    <Text style={styles.ecoImageText}>{eco.name}</Text>
                  )}
                </View>
                {editing && (
                  <TouchableOpacity style={styles.changePhotoBtn} onPress={pickImage} activeOpacity={0.8}>
                    <Ionicons name="image-outline" size={20} color={colors.white} />
                    <Text style={styles.changePhotoBtnText}>Changer la photo</Text>
                  </TouchableOpacity>
                )}
              </ImageBackground>
            ) : (
              <View style={[styles.imageBanner, styles.imageBannerPlaceholder, styles.imageBannerImageModern]}>
                {/* Bouton modifier en haut à droite de l'image placeholder */}
                <TouchableOpacity
                  style={{
                    position: 'absolute',
                    top: 12,
                    right: 12,
                    backgroundColor: 'rgba(255,255,255,0.8)',
                    borderRadius: 20,
                    padding: 6,
                    zIndex: 10,
                  }}
                  onPress={() => (editing ? saveEco() : setEditing(true))}
                  activeOpacity={0.8}
                >
                  <Ionicons
                    name={editing ? 'checkmark' : 'create-outline'}
                    size={22}
                    color={colors.accent}
                  />
                </TouchableOpacity>
                {/* Nom éditable ou non sur le placeholder également */}
                <View style={styles.ecoImageOverlay}>
                  {editing ? (
                    <TextInput
                      style={[
                        styles.ecoImageText,
                        {
                          backgroundColor: 'rgba(255,255,255,0.7)',
                          color: '#2a9d8f',
                          borderRadius: 8,
                          paddingHorizontal: 8,
                        },
                      ]}
                      value={name}
                      onChangeText={setName}
                      placeholder="Nom de l'écosystème"
                      placeholderTextColor="#2a9d8f"
                    />
                  ) : (
                    <Text style={styles.ecoImageText}>{eco.name}</Text>
                  )}
                </View>
                <Ionicons name="image-outline" size={64} color={colors.accent} />
                {editing && (
                  <TouchableOpacity style={styles.changePhotoBtn} onPress={pickImage} activeOpacity={0.8}>
                    <Ionicons name="image-outline" size={20} color={colors.white} />
                    <Text style={styles.changePhotoBtnText}>Ajouter une photo</Text>
                  </TouchableOpacity>
                )}
              </View>
            )}
          </View>

          {/* Summary stats cards + bouton de choix */}
          <View style={[styles.cardSection, { marginBottom: 24 }]}>
            <View style={[styles.summaryContainer, { alignItems: 'flex-start' }]}>
              {/* Ligne horizontale avec titre et bouton à droite */}
              <View style={{ flexDirection: 'row', alignItems: 'center', width: '100%', marginBottom: 12 }}>
                <Text style={{ fontWeight: '900', fontSize: 20, color: colors.accent, flex: 1 }}>Résumé rapide</Text>
                <TouchableOpacity
                  style={styles.chooseParamsBtn}
                  onPress={() => setSummaryModalVisible(true)}
                  activeOpacity={0.8}
                >
                  <Ionicons name="settings-outline" size={18} color={colors.accent} style={{ marginRight: 3 }} />
                  <Text style={styles.chooseParamsBtnText}>Paramètres</Text>
                </TouchableOpacity>
              </View>
              <View style={{ flexDirection: 'row', flex: 1 }}>
                {summaryParams.map(({ param, value }) => (
                  <View key={param} style={styles.statCardModern}>
                    {/* Status indicator dot */}
                    <View style={[styles.statusDot, { backgroundColor: getParamStatusColor(param) }]} />
                    <View style={styles.statIconContainerModern}>
                      <MaterialCommunityIcons name={ICONS[param] || 'flask-outline'} size={32} color={colors.accent} />
                    </View>
                    <Text style={styles.statNameModern}>{param}</Text>
                    <Text style={styles.statValueModern} numberOfLines={1}>
                      {value !== null ? `${value} ${getUnit(param)}` : '-'}
                    </Text>
                  </View>
                ))}
              </View>
            </View>
          </View>

          {/* Modal de sélection des paramètres du résumé */}
          <Modal
            visible={summaryModalVisible}
            animationType="slide"
            transparent
            onRequestClose={() => setSummaryModalVisible(false)}
          >
            <Pressable
              style={styles.modalBackdrop}
              onPress={() => setSummaryModalVisible(false)}
            />
            <View style={styles.modalContent}>
              <Text style={styles.modalTitle}>Choisir jusqu'à 3 paramètres à afficher</Text>
              <ScrollView style={{ maxHeight: 320, marginBottom: 12 }}>
                {availableParams.map(p => {
                  const checked = selectedSummaryParams.includes(p);
                  const disabled = !checked && selectedSummaryParams.length >= 3;
                  return (
                    <TouchableOpacity
                      key={`choose-${p}`}
                      style={[
                        styles.modalParamRow,
                        checked && styles.modalParamRowChecked,
                        disabled && styles.modalParamRowDisabled,
                      ]}
                      onPress={() => {
                        if (checked) {
                          setSelectedSummaryParams(prev => prev.filter(x => x !== p));
                        } else if (selectedSummaryParams.length < 3) {
                          setSelectedSummaryParams(prev => [...prev, p]);
                        }
                      }}
                      activeOpacity={0.8}
                      disabled={disabled}
                    >
                      <MaterialCommunityIcons
                        name={ICONS[p] || 'flask-outline'}
                        size={22}
                        color={checked ? colors.accent : '#bbb'}
                        style={{ marginRight: 8 }}
                      />
                      <Text style={[styles.modalParamText, checked && { color: colors.accent }]}>{p}</Text>
                      <View style={[styles.checkbox, checked && styles.checkboxChecked]}>
                        {checked && <Ionicons name="checkmark" size={16} color="#fff" />}
                      </View>
                    </TouchableOpacity>
                  );
                })}
              </ScrollView>
              <TouchableOpacity
                style={[styles.modalSaveBtn, { backgroundColor: colors.accent }]}
                onPress={async () => {
                  try {
                    await api.put(`/ecosystems/${eco.id}/summary-params`, selectedSummaryParams);
                    setEco(prev => ({ ...prev, summaryParams: selectedSummaryParams }));
                    setSummaryModalVisible(false);
                    Alert.alert('Succès', 'Résumé rapide mis à jour !');
                  } catch (err) {
                    console.error('Erreur maj params:', err);
                    Alert.alert('Erreur', 'Impossible de sauvegarder les paramètres.');
                  }
                }}
                activeOpacity={0.8}
              >
                <Text style={styles.modalSaveBtnText}>Enregistrer</Text>
              </TouchableOpacity>
              <TouchableOpacity
                style={styles.modalCloseBtn}
                onPress={() => setSummaryModalVisible(false)}
                activeOpacity={0.8}
              >
                <Text style={styles.modalCloseBtnText}>Fermer</Text>
              </TouchableOpacity>
            </View>
          </Modal>

          {/* Paramètres suivis — chips de sélection */}

          {/* Quick analysis */}
          <View style={styles.analysisContainer}>
            <Text style={styles.analysisTitle}>Analyse rapide</Text>
            {quickAnalysis.length === 0 ? (
              <Text style={styles.analysisText}>✅ Tous les paramètres sont dans les valeurs idéales.</Text>
            ) : (
              quickAnalysis.map((txt, i) => (
                <Text key={i} style={styles.analysisText}>• {txt}</Text>
              ))
            )}
          </View>

          {/* Graph section */}
          <View style={styles.chartCard}>
            <Text style={styles.chartTitle}>Historique — {selectedParam}</Text>
            <LineChart
              data={{
                labels: history.length ? history.map(h => (h.measuredAt || '').slice(5, 10)) : [''],
                datasets: [{ data: history.length ? history.map(h => h.value || 0) : [0] }],
              }}
              width={Dimensions.get('window').width - 60}
              height={220}
              fromZero
              yAxisInterval={1}
              onDataPointClick={handlePointClick}
              chartConfig={{
                backgroundGradientFrom: colors.chartGradientStart,
                backgroundGradientTo: colors.chartGradientEnd,
                color: () => colors.accent,
                labelColor: () => colors.accent,
                propsForLabels: {
                  fontSize: 10,
                  fontWeight: '700',
                },
                propsForDots: {
                  r: history.length ? '5' : '0',
                  strokeWidth: '2',
                  stroke: colors.accent,
                },
              }}
              style={{ borderRadius: 20, marginHorizontal: 'auto' }}
            />

            {/* Récapitulatif des dernières mesures */}
            <View style={styles.latestWrap}>
              <Text style={styles.latestTitle}>📋 Dernières mesures enregistrées</Text>
              <ScrollView horizontal showsHorizontalScrollIndicator={false} contentContainerStyle={styles.latestRow}>
                {(importantParams.length ? importantParams : availableParams).map(p => {
                  const item = latestByParam[p];
                  const active = selectedParam === p;
                  return (
                    <TouchableOpacity
                      key={`latest-${p}`}
                      style={[styles.latestCard, active && styles.latestCardActive]}
                      activeOpacity={0.8}
                      onPress={() => setSelectedParam(p)}
                    >
                      <Text style={[styles.latestParam, active && { color: '#fff' }]} numberOfLines={1}>{p}</Text>
                      <Text style={[styles.latestValue, active && { color: '#fff' }]}>{item?.value ?? '—'}</Text>
                      <Text style={[styles.latestDate, active && { color: '#e0f7f3' }]}>{item?.date ? String(item.date).slice(5,10) : '—'}</Text>
                    </TouchableOpacity>
                  );
                })}
              </ScrollView>
            </View>

            <View style={styles.addMeasureRow}>
              <TextInput
                style={[styles.input, { color: colors.textPrimary, borderColor: colors.textSecondary }]}
                keyboardType="decimal-pad"
                value={newValue}
                onChangeText={setNewValue}
                placeholder={`Ajouter une valeur (${selectedParam})`}
                placeholderTextColor={colors.textSecondary}
              />
              <TouchableOpacity onPress={addRecord} style={styles.addBtn} activeOpacity={0.7}>
                <Ionicons name="add-circle" size={36} color={colors.accent} />
              </TouchableOpacity>
            </View>
          </View>

          {/* Equipment section */}
          <View style={styles.cardSection}>
            <View style={styles.equipmentSection}>
              <View style={styles.sectionHeaderRow}>
                <Text style={styles.equipmentTitle}>Matériel</Text>
                <View style={styles.sectionUnderline} />
              </View>
              {equipment.length === 0 ? (
                <Text style={[styles.emptyChart, { color: colors.textSecondary }]}>Aucun matériel ajouté.</Text>
              ) : (
                equipment.map(it => (
                  <View key={it.id} style={styles.equipCard}>
                    <View style={styles.equipInfo}>
                      <MaterialCommunityIcons name="tools" size={24} color={colors.accent} style={{ marginRight: 10 }} />
                      <View style={{ flexShrink: 1 }}>
                        <Text style={styles.equipName} numberOfLines={1}>{it.name}</Text>
                        {(it.brand || it.details) && (
                          <Text style={styles.equipDetails} numberOfLines={1}>
                            {it.brand ? it.brand : ''}{it.brand && it.details ? ' — ' : ''}{it.details ? it.details : ''}
                          </Text>
                        )}
                      </View>
                    </View>
                    <TouchableOpacity
                      onPress={() => delEquip(it.id)}
                      style={styles.equipDeleteBtn}
                      activeOpacity={0.7}
                    >
                      <Ionicons name="trash-outline" size={20} color={colors.red} />
                    </TouchableOpacity>
                  </View>
                ))
              )}

              <View style={styles.addEquipContainer}>
                <TouchableOpacity
                  style={styles.addEquipBtn}
                  onPress={() => {
                    if (!showEquipInputs) setShowEquipInputs(true);
                    else if (newEquip.name.trim()) addEquip();
                    else Alert.alert('Erreur', 'Veuillez saisir le nom du matériel');
                  }}
                  activeOpacity={0.8}
                >
                  <Ionicons name={showEquipInputs ? 'checkmark' : 'add'} size={24} color={colors.accent} />
                  <Text style={styles.addEquipBtnText}>
                    {showEquipInputs ? 'Valider le matériel' : 'Ajouter du matériel'}
                  </Text>
                </TouchableOpacity>

                {showEquipInputs && (
                  <View style={styles.addEquipInputs}>
                    <TextInput
                      style={[styles.inputEquip, { color: colors.textPrimary, borderColor: colors.textSecondary }]}
                      placeholder="Nom du matériel"
                      value={newEquip.name}
                      onChangeText={t => setNewEquip({ ...newEquip, name: t })}
                      placeholderTextColor={colors.textSecondary}
                    />
                    <TextInput
                      style={[styles.inputEquip, { color: colors.textPrimary, borderColor: colors.textSecondary }]}
                      placeholder="Marque"
                      value={newEquip.brand}
                      onChangeText={t => setNewEquip({ ...newEquip, brand: t })}
                      placeholderTextColor={colors.textSecondary}
                    />
                    <TextInput
                      style={[styles.inputEquip, { color: colors.textPrimary, borderColor: colors.textSecondary }]}
                      placeholder="Détails"
                      value={newEquip.details}
                      onChangeText={t => setNewEquip({ ...newEquip, details: t })}
                      placeholderTextColor={colors.textSecondary}
                    />
                    <TouchableOpacity
                      style={styles.cancelEquipBtn}
                      onPress={() => {
                        setNewEquip({ name: '', brand: '', details: '' });
                        setShowEquipInputs(false);
                      }}
                      activeOpacity={0.8}
                    >
                      <Text style={styles.cancelEquipBtnText}>Annuler</Text>
                    </TouchableOpacity>
                  </View>
                )}
              </View>
            </View>
          </View>

          {/* Habitants */}
          <View style={styles.cardSection}>
            <View style={styles.inhabitantsSection}>
              <View style={styles.sectionHeaderRow}>
                <Text style={styles.inhabitantsTitle}>Habitants</Text>
                <View style={styles.sectionUnderline} />
              </View>

              {inhabitants.length === 0 ? (
                <Text style={[styles.emptyChart, { color: '#2a9d8f' }]}>Aucun habitant ajouté.</Text>
              ) : (
                inhabitants.map(h => (
                  <View key={h.id} style={styles.habCard}>
                    <View style={styles.habInfo}>
                      <MaterialCommunityIcons name="fish" size={22} color="#2a9d8f" style={{ marginRight: 8 }} />
                      <Text style={styles.habName} numberOfLines={1}>{h.name}</Text>
                    </View>
                    <TouchableOpacity onPress={() => removeInhabitant(h.id)} style={styles.habDeleteBtn} activeOpacity={0.8}>
                      <Ionicons name="trash-outline" size={18} color="#e63946" />
                    </TouchableOpacity>
                  </View>
                ))
              )}

              <View style={styles.addInhabitantRow}>
                <TextInput
                  style={[styles.input, { flex: 1, borderColor: '#cde7e2' }]}
                  placeholder="Nom de l'habitant (ex : Rasbora Arlequin)"
                  placeholderTextColor="#7aa9a3"
                  value={newInhabitant}
                  onChangeText={(text) => {
                    setNewInhabitant(text);
                    const list = SUGGESTIONS_BY_TYPE[eco.type] || [];
                    const filtered = list.filter((s) => s.toLowerCase().includes(text.toLowerCase()));
                    setFilteredSuggestions(filtered);
                    setShowSuggestions(filtered.length > 0);
                  }}
                />
                <TouchableOpacity onPress={addInhabitant} style={styles.addBtn} activeOpacity={0.8}>
                  <Ionicons name="add-circle" size={36} color="#2a9d8f" />
                </TouchableOpacity>
              </View>
              {showSuggestions && (
                <View style={styles.suggestionsBox}>
                  {filteredSuggestions.map((s, idx) => (
                    <TouchableOpacity
                      key={idx}
                      style={styles.suggestionItem}
                      onPress={() => {
                        setNewInhabitant(s);
                        setShowSuggestions(false);
                      }}
                    >
                      <Text style={styles.suggestionText}>{s}</Text>
                    </TouchableOpacity>
                  ))}
                </View>
              )}
            </View>
          </View>
        </ScrollView>
      </View>
    </KeyboardAvoidingView>
  );
}


// --- Styles supplémentaires pour la fonctionnalité de choix des paramètres ---
const modalShadow = {
  shadowColor: '#000',
  shadowOpacity: 0.18,
  shadowRadius: 16,
  shadowOffset: { width: 0, height: 8 },
  elevation: 16,
};

const styles = StyleSheet.create({
  flex1: { flex: 1 },
  headerGradient: {
    paddingTop: Platform.OS === 'ios' ? 54 : 36,
    paddingBottom: 24,
    paddingHorizontal: 20,
    backgroundColor: 'transparent',
    shadowColor: '#000',
    shadowOpacity: 0.13,
    shadowRadius: 12,
    elevation: 8,
    // Use linear-gradient background via expo-linear-gradient or fallback
    backgroundImage: 'linear-gradient(90deg, #2a9d8f 0%, #73d1c9 100%)',
    // fallback for react-native: use backgroundColor as backup
    backgroundColor: '#2a9d8f',
  },
  headerContent: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    backgroundColor: 'transparent',
  },
  headerBackBtn: {
    padding: 6,
    borderRadius: 30,
    backgroundColor: 'rgba(255,255,255,0.18)',
  },
  headerEditBtn: {
    padding: 6,
    borderRadius: 30,
    backgroundColor: 'rgba(255,255,255,0.18)',
  },
  headerTexts: {
    flex: 1,
    marginHorizontal: 14,
  },
  headerTitle: {
    color: '#fff',
    fontWeight: '900',
    fontSize: 28,
    textTransform: 'capitalize',
    letterSpacing: 0.7,
    textShadowColor: 'rgba(0,0,0,0.13)',
    textShadowOffset: { width: 0, height: 2 },
    textShadowRadius: 6,
  },
  headerSubtitle: {
    color: '#d6f0ed',
    fontWeight: '700',
    fontSize: 15,
    marginTop: 3,
    textTransform: 'capitalize',
    letterSpacing: 0.2,
  },
  headerUpdate: {
    color: '#bde4dd',
    fontSize: 12,
    marginTop: 2,
    fontWeight: '600',
  },
  scrollContent: {
    paddingBottom: 40,
    backgroundColor: 'transparent',
  },
  imageBannerContainer: {
    marginHorizontal: 20,
    marginTop: 10,
    marginBottom: 16,
    shadowColor: '#000',
    shadowOpacity: 0.16,
    shadowRadius: 14,
    shadowOffset: { width: 0, height: 6 },
    elevation: 10,
    borderRadius: 26,
    overflow: 'hidden',
  },
  imageBanner: {
    width: '100%',
    height: 220,
    justifyContent: 'flex-end',
    borderRadius: 26,
    overflow: 'hidden',
  },
  imageBannerImageModern: {
    borderRadius: 26,
  },
  imageBannerPlaceholder: {
    backgroundColor: '#e0f0eb',
    justifyContent: 'center',
    alignItems: 'center',
  },
  ecoImageOverlay: {
    position: 'absolute',
    left: 0,
    right: 0,
    bottom: 0,
    height: 60,
    backgroundColor: 'rgba(42,157,143,0.44)',
    borderBottomLeftRadius: 26,
    borderBottomRightRadius: 26,
    justifyContent: 'center',
    alignItems: 'center',
    paddingBottom: 4,
    paddingTop: 8,
  },
  ecoImageText: {
    color: '#fff',
    fontSize: 22,
    fontWeight: '900',
    textShadowColor: 'rgba(0,0,0,0.15)',
    textShadowOffset: { width: 0, height: 1 },
    textShadowRadius: 6,
    letterSpacing: 0.4,
    backgroundColor: 'rgba(0,0,0,0.08)',
    borderRadius: 8,
    paddingHorizontal: 12,
    paddingVertical: 2,
    overflow: 'hidden',
  },
  changePhotoBtn: {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: [{ translateX: -85 }, { translateY: -20 }],
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: 'rgba(42,157,143,0.85)',
    paddingVertical: 10,
    paddingHorizontal: 18,
    borderRadius: 24,
    shadowColor: '#1f776d',
    shadowOpacity: 0.14,
    shadowRadius: 7,
    elevation: 3,
  },
  changePhotoBtnText: {
    color: '#fff',
    fontWeight: '700',
    marginLeft: 7,
    fontSize: 15,
    letterSpacing: 0.1,
  },
  editEcoNameContainer: {
    marginHorizontal: 32,
    marginTop: -12,
    marginBottom: 12,
    alignItems: 'center',
  },
  editEcoNameInput: {
    borderWidth: 1,
    borderColor: '#bde4dd',
    borderRadius: 18,
    backgroundColor: '#f3fdfa',
    fontWeight: '800',
    fontSize: 20,
    color: '#2a9d8f',
    paddingHorizontal: 18,
    paddingVertical: 10,
    width: '100%',
    textAlign: 'center',
    elevation: 2,
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.08,
    shadowRadius: 4,
  },
  summaryContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginHorizontal: 14,
    marginBottom: 24,
    gap: 10,
    flexWrap: 'wrap',
  },
  statCardModern: {
  flex: 1,
  backgroundColor: '#ffffff',
  marginHorizontal: 6,
  borderRadius: 22,
  paddingVertical: 6,   // 🔽 encore plus compact
  paddingHorizontal: 10,
  alignItems: 'center',
  shadowColor: '#000',
  shadowOpacity: 0.08,
  shadowRadius: 8,
  elevation: 6,
  minHeight: 70,         // 🔽 plus bas
  justifyContent: 'center',
},
  statIconContainerModern: {
    marginBottom: 10,
    backgroundColor: '#e0f0eb',
    borderRadius: 18,
    padding: 2,
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.12,
    shadowRadius: 6,
    elevation: 3,
  },
  statNameModern: {
    fontWeight: '800',
    fontSize: 8,
    color: '#2a9d8f',
    marginBottom: 3,
    textAlign: 'center',
    letterSpacing: 0.06,
    maxWidth: 70,
    flexShrink: 1,
    width: 70,
    numberOfLines: 1,
    ellipsizeMode: 'tail',
    // Empêche les retours à la ligne
    // textAlign: 'center', // déjà présent
  },
  statValueModern: {
    fontSize: 10,
    fontWeight: '900',
    color: '#2a9d8f',
    marginTop: 4,
    textAlign: 'center',
    maxWidth: 70,
    includeFontPadding: false,
    lineHeight: 20,
  },
  analysisContainer: {
    marginHorizontal: 20,
    marginBottom: 16,
    backgroundColor: 'rgba(255,255,255,0.9)',
    padding: 16,
    borderRadius: 20,
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.12,
    shadowRadius: 8,
    elevation: 6,
  },
  analysisTitle: {
    fontWeight: '800',
    fontSize: 18,
    color: '#2a9d8f',
    marginBottom: 8,
  },
  analysisText: {
    fontSize: 12,
    color: '#2a9d8f',
    marginBottom: 4,
  },
  chartCard: {
    marginHorizontal: 20,
    marginBottom: 20,
    backgroundColor: '#e8faf8',
    borderRadius: 24,
    paddingVertical: 20,
    paddingHorizontal: 16,
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
    overflow: 'hidden',
  },
  chartTitle: {
    fontWeight: '800',
    fontSize: 18,
    color: '#2a9d8f',
    marginBottom: 12,
    textAlign: 'center',
  },
  chartStyle: {
    borderRadius: 24,
  },
  emptyChart: {
    textAlign: 'center',
    marginVertical: 20,
    fontSize: 16,
  },
  addMeasureRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 16,
  },
  input: {
    flex: 1,
    borderWidth: 1,
    borderRadius: 12,
    paddingVertical: 10,
    paddingHorizontal: 14,
    backgroundColor: '#fff',
    fontSize: 12,
    fontWeight: '600',
  },
  addBtn: {
    marginLeft: 12,
  },
  equipmentSection: {
    marginHorizontal: 20,
    marginBottom: 40,
  },
  equipmentTitle: {
    fontWeight: '900',
    fontSize: 18,
    color: '#2a9d8f',
    marginBottom: 4,
  },
  equipCard: {
    backgroundColor: '#fff',
    borderRadius: 16,
    paddingVertical: 14,
    paddingHorizontal: 16,
    marginBottom: 12,
    flexDirection: 'row',
    alignItems: 'center',
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.1,
    shadowRadius: 6,
    elevation: 4,
    justifyContent: 'space-between',
  },
  equipInfo: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
    marginRight: 12,
  },
  equipName: {
    fontWeight: '700',
    fontSize: 12,
    color: '#2a2a2a',
  },
  equipDetails: {
    fontSize: 12,
    color: '#555',
    marginTop: 2,
  },
  equipDeleteBtn: {
    backgroundColor: 'rgba(230, 57, 70, 0.12)',
    padding: 8,
    borderRadius: 20,
  },
  addEquipContainer: {
    marginTop: 10,
  },
  addEquipBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#d7f4e9',
    paddingVertical: 14,
    paddingHorizontal: 20,
    borderRadius: 30,
    justifyContent: 'center',
    marginBottom: 12,
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.15,
    shadowRadius: 8,
    elevation: 6,
  },
  addEquipBtnText: {
    color: '#2a9d8f',
    fontWeight: '800',
    fontSize: 12,
    marginLeft: 8,
  },
  addEquipInputs: {
    backgroundColor: '#fff',
    borderRadius: 16,
    padding: 12,
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.1,
    shadowRadius: 6,
    elevation: 4,
  },
  inputEquip: {
    borderWidth: 1,
    borderColor: '#ccc',
    borderRadius: 12,
    paddingVertical: 10,
    paddingHorizontal: 14,
    marginBottom: 10,
    fontSize: 12,
    fontWeight: '600',
    backgroundColor: '#fff',
  },
  paramChipsRow: {
    paddingHorizontal: 20,
    paddingBottom: 8,
    gap: 8,
  },
  paramChip: {
    flexDirection: 'row',
    alignItems: 'center',
    borderWidth: 1,
    borderColor: '#2a9d8f',
    paddingVertical: 8,
    paddingHorizontal: 12,
    borderRadius: 18,
    marginRight: 8,
    backgroundColor: '#eef9f7',
  },
  paramChipActive: {
    backgroundColor: '#2a9d8f',
    borderColor: '#2a9d8f',
  },
  paramChipText: {
    color: '#2a9d8f',
    fontWeight: '700',
  },
  latestWrap: {
    marginTop: 12,
    marginBottom: 8,
  },
  latestTitle: {
    textAlign: 'left',
    marginLeft: 4,
    marginBottom: 6,
    color: '#2a9d8f',
    fontWeight: '800',
    fontSize: 14,
  },
  latestRow: {
    paddingVertical: 6,
    paddingHorizontal: 4,
    gap: 8,
  },
  latestCard: {
    minWidth: 90,
    paddingVertical: 10,
    paddingHorizontal: 12,
    backgroundColor: '#fff',
    borderRadius: 14,
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.1,
    shadowRadius: 6,
    elevation: 4,
    alignItems: 'center',
  },
  latestCardActive: {
    backgroundColor: '#2a9d8f',
    shadowColor: '#1f776d',
    transform: [{ scale: 1.05 }],
  },
  latestParam: {
    fontSize: 10,
    color: '#2a9d8f',
    fontWeight: '800',
    marginBottom: 4,
  },
  latestValue: {
    fontSize: 10,
    fontWeight: '900',
    color: '#2a9d8f',
    marginBottom: 2,
  },
  latestDate: {
    fontSize: 10,
    color: '#7aa9a3',
  },
  sectionHeaderRow: {
    marginBottom: 6,
  },
  sectionUnderline: {
    marginTop: 4,
    width: 64,
    height: 4,
    borderRadius: 4,
    backgroundColor: '#2a9d8f',
  },
  inhabitantsSection: {
    marginHorizontal: 20,
    marginBottom: 40,
  },
  inhabitantsTitle: {
    fontWeight: '900',
    fontSize: 18,
    color: '#2a9d8f',
  },
  habCard: {
    backgroundColor: '#fff',
    borderRadius: 16,
    paddingVertical: 12,
    paddingHorizontal: 14,
    marginBottom: 10,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.1,
    shadowRadius: 6,
    elevation: 4,
  },
  habInfo: {
    flexDirection: 'row',
    alignItems: 'center',
    flex: 1,
    marginRight: 10,
  },
  habName: {
    fontSize: 12,
    fontWeight: '700',
    color: '#2a2a2a',
  },
  habDeleteBtn: {
    backgroundColor: 'rgba(230, 57, 70, 0.12)',
    padding: 8,
    borderRadius: 18,
  },
  addInhabitantRow: {
    flexDirection: 'row',
    alignItems: 'center',
    marginTop: 8,
  },
  chooseParamsBtn: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#e0f0eb',
    borderRadius: 16,
    paddingHorizontal: 13,
    paddingVertical: 7,
    marginLeft: 8,
    ...modalShadow,
  },
  chooseParamsBtnText: {
    color: '#2a9d8f',
    fontWeight: '700',
    fontSize: 14,
  },
  modalBackdrop: {
    position: 'absolute',
    left: 0, right: 0, top: 0, bottom: 0,
    backgroundColor: 'rgba(0,0,0,0.18)',
  },
  modalContent: {
    position: 'absolute',
    left: 25, right: 25, top: '26%',
    backgroundColor: '#fff',
    borderRadius: 24,
    padding: 20,
    ...modalShadow,
    zIndex: 10,
  },
  modalTitle: {
    fontWeight: '900',
    fontSize: 14,
    color: '#2a9d8f',
    marginBottom: 14,
    textAlign: 'center',
  },
  modalParamRow: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingVertical: 10,
    paddingHorizontal: 6,
    borderRadius: 12,
    marginBottom: 4,
    backgroundColor: '#f3fdfa',
  },
  modalParamRowChecked: {
    backgroundColor: '#d7f4e9',
  },
  modalParamRowDisabled: {
    opacity: 0.55,
  },
  modalParamText: {
    flex: 1,
    fontSize: 12,
    color: '#2a2a2a',
    fontWeight: '700',
  },
  checkbox: {
    width: 22,
    height: 22,
    borderRadius: 7,
    borderWidth: 2,
    borderColor: '#bde4dd',
    backgroundColor: '#fff',
    alignItems: 'center',
    justifyContent: 'center',
    marginLeft: 10,
  },
  checkboxChecked: {
    backgroundColor: '#2a9d8f',
    borderColor: '#2a9d8f',
  },
  modalCloseBtn: {
    backgroundColor: '#2a9d8f',
    borderRadius: 16,
    paddingVertical: 12,
    marginTop: 8,
    alignItems: 'center',
  },
  modalCloseBtnText: {
    color: '#fff',
    fontWeight: '800',
    fontSize: 16,
    letterSpacing: 0.2,
  },
    cardSection: {
    marginHorizontal: 20,
    marginBottom: 20,
    backgroundColor: '#e8faf8',
    borderRadius: 24,
    paddingVertical: 20,
    paddingHorizontal: 16,
    shadowColor: '#2a9d8f',
    shadowOpacity: 0.15,
    shadowRadius: 10,
    elevation: 8,
  },
  cancelEquipBtn: {
    marginTop: 8,
    alignSelf: 'center',
    paddingVertical: 10,
    paddingHorizontal: 20,
    backgroundColor: '#fddede',
    borderRadius: 20,
    shadowColor: '#e63946',
    shadowOpacity: 0.12,
    shadowRadius: 6,
    elevation: 3,
  },
  cancelEquipBtnText: {
    color: '#e63946',
    fontWeight: '700',
    fontSize: 12,
  },
  statusDot: {
    position: 'absolute',
    top: 6,
    right: 6,
    width: 10,
    height: 10,
    borderRadius: 5,
  },
  modalSaveBtn: {
    borderRadius: 10,
    paddingVertical: 10,
    alignItems: 'center',
    marginBottom: 10,
  },
  modalSaveBtnText: {
    color: '#fff',
    fontWeight: '700',
    fontSize: 16,
  },
  suggestionsBox: {
    backgroundColor: '#fff',
    borderRadius: 8,
    marginTop: 4,
    paddingVertical: 4,
    shadowColor: '#000',
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 4,
  },
  suggestionItem: {
    paddingVertical: 8,
    paddingHorizontal: 12,
  },
  suggestionText: {
    color: '#2a9d8f',
    fontSize: 12,
    fontWeight: '600',
  },
});

  
  

  
  