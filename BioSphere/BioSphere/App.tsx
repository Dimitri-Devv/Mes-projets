

import React from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { LogBox } from "react-native";

import { AppProvider } from "./Ecrans/context/AppContext";
import RegisterScreen from "./Ecrans/RegisterScreen";
import LoginScreen from "./Ecrans/LoginScreen";
import ChangePasswordScreen from "./Ecrans/ChangePasswordScreen";
import ConversationsScreen from "./Ecrans/ConversationScreen";
import AddEcosystemScreen from "./Ecrans/AddEcosystemScreen";
import TabRoutes from "./routes/tabRoutes";
import EcosystemDetailScreen from "./Ecrans/EcosystemDetailScreen";
import CommentsScreen from "./Ecrans/CommentsScreen";
import ChatScreen from "./Ecrans/ChatScreen";
import ProfilPublic from "./Ecrans/ProfilPublic";
import EcosystemPublicScreen from "./Ecrans/EcosystemPublicScreen";
import VerifyEmailScreen from "./Ecrans/VerifyEmailScreen";

// ✅ ignore un warning non bloquant de React Native
LogBox.ignoreLogs(["Text strings must be rendered within a <Text> component"]);

const Stack = createNativeStackNavigator();

export default function App() {
  return (
    <AppProvider>
      <NavigationContainer>
        <Stack.Navigator initialRouteName="Auth">
          <Stack.Screen
            name="Auth"
            component={LoginScreen}
            options={{ headerShown: false }}
          />
          <Stack.Screen
            name="Register"
            component={RegisterScreen}
            options={{ title: "Créer un compte", headerShown: true }}
          />
          <Stack.Screen
            name="Main"
            component={TabRoutes}
            options={{ headerShown: false }}
          />
          <Stack.Screen
            name="AddEcosystem"
            component={AddEcosystemScreen}
            options={{
              title: "Ajouter un écosystème",
              headerBackTitle: "Retour",
            }}
          />
          <Stack.Screen
            name="EcosystemDetail"
            component={EcosystemDetailScreen}
          />
          <Stack.Screen
            name="Comments"
            component={CommentsScreen}
            options={{ title: "Commentaires" }}
          />
          <Stack.Screen
            name="Chat"
            component={ChatScreen}
            options={{ title: "Messages" }}
          />
          <Stack.Screen
            name="ProfilPublic"
            component={ProfilPublic}
            options={{ title: "Profil utilisateur" }}
          />
          <Stack.Screen
            name="EcosystemPublic"
            component={EcosystemPublicScreen}
            options={{ title: "Écosystème public" }}
          />
          <Stack.Screen
            name="Conversations"
            component={ConversationsScreen}
            options={{ title: "Messages privés" }}
          />
          <Stack.Screen
      name="VerifyEmail"
      component={VerifyEmailScreen}
      options={{ headerShown: false }}
/>
<Stack.Screen
  name="ChangePassword"
  component={ChangePasswordScreen}
  options={{ title: "Modifier le mot de passe" }}
/>
        </Stack.Navigator>
      </NavigationContainer>
    </AppProvider>
  );
}