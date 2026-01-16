import * as React from 'react';
import { TouchableOpacity, Text, Image } from 'react-native';
import { Ionicons } from '@expo/vector-icons';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { useContext } from 'react';
import { AppContext } from '../Ecrans/context/AppContext';

import EcosystemesScreen from '../Ecrans/EcosystemesScreen';
import CommunauteScreen from '../Ecrans/CommunauteScreen';
import ProfilScreen from '../Ecrans/ProfilScreen';
import ParametresScreen from '../Ecrans/ParametresScreen';

const Tab = createBottomTabNavigator();

export default function TabRoutes({ route, navigation }) {
  const { user } = useContext(AppContext);

  // 🔐 Redirection si aucun utilisateur connecté
  if (!user) {
    navigation.replace("Auth");
    return null;
  }

  return (
    <Tab.Navigator
      initialRouteName="Écosystèmes"
      screenOptions={({ route }) => ({
        headerShown: true,
        headerTitleAlign: 'center',
        tabBarIcon: ({ color, size }) => {
          let iconName;
          if (route.name === 'Écosystèmes') iconName = 'leaf';
          else if (route.name === 'Communauté') iconName = 'people';
          else if (route.name === 'Profil') iconName = 'person';
          else if (route.name === 'Paramètres') iconName = 'settings';
          return <Ionicons name={iconName} size={size} color={color} />;
        },
        tabBarActiveTintColor: '#2a9d8f',
        tabBarInactiveTintColor: 'gray',
        tabBarStyle: {
          backgroundColor: '#fff',
          borderTopWidth: 0,
          elevation: 0,
          height: 60,
        },
      })}
    >
      {/* --- ÉCOSYSTÈMES --- */}
      <Tab.Screen
        name="Écosystèmes"
        options={{
          // ✅ Logo dans le header
          headerTitle: () => (
            <Image
              source={require('../assets/logo.png')}
              style={{ width: 180, height: 60, resizeMode: 'contain' }}
            />
          ),
          headerRight: () => (
            <TouchableOpacity
              style={{ marginRight: 15 }}
              onPress={() => navigation.navigate('AddEcosystem')}
            >
              <Text style={{ fontSize: 28, color: '#2a9d8f' }}>＋</Text>
            </TouchableOpacity>
          ),
        }}
      >
        {props => <EcosystemesScreen {...props} />}
      </Tab.Screen>

    

      {/* --- PROFIL --- */}
      <Tab.Screen
        name="Profil"
        options={{ headerTitle: 'Mon profil' }}
      >
        {props => <ProfilScreen {...props} />}
      </Tab.Screen>

 {/* --- Communaute --- */}
      <Tab.Screen name="Communauté">
        {props => <CommunauteScreen {...props} />}
      </Tab.Screen>


      {/* --- PARAMÈTRES --- */}
      <Tab.Screen
        name="Paramètres"
        component={ParametresScreen}
        options={{ headerTitle: 'Paramètres' }}
      />

        
    </Tab.Navigator>
  );
}
