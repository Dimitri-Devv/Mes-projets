import React, { useContext, useEffect, useState } from 'react';
import { View, Text, StyleSheet, TextInput, TouchableOpacity, FlatList, KeyboardAvoidingView, Platform, SafeAreaView, Keyboard } from 'react-native';
import { AppContext } from './context/AppContext';
import api from './services/api';

export default function ChatScreen({ route }) {
  const { toUser } = route.params;
  const { user: currentUser, theme } = useContext(AppContext);
  const isDark = theme === 'dark';

  const [messages, setMessages] = useState([]);
  const [text, setText] = useState('');
  const [loading, setLoading] = useState(false);

  const colors = {
    bg: isDark ? '#0f1113' : '#fff',
    text: isDark ? '#fff' : '#222',
    card: isDark ? '#171a1d' : '#f7f7f9',
    border: isDark ? '#2b2f34' : '#e5e5ea',
    muted: isDark ? '#a7a7a7' : '#666',
    accent: '#2a9d8f',
  };

  // 🔹 Charger la conversation au montage
  useEffect(() => {
    const fetchConversation = async () => {
      try {
        const res = await api.get(`/community/messages/${currentUser.id}/${toUser.id}`);
        const sorted = res.data.sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt));
        setMessages(sorted);
      } catch (err) {
        console.error('Erreur récupération messages :', err);
      }
    };
    fetchConversation();
  }, [toUser]);

  // 🔹 Envoyer un message
  const send = async () => {
    if (!text.trim()) return;

    const tempMsg = {
      id: Date.now(),
      text: text.trim(),
      sender: { id: currentUser.id },
      receiver: { id: toUser.id },
      mine: true,
      createdAt: new Date().toISOString(),
    };

    setMessages(prev => [tempMsg, ...prev]);
    setText('');

    try {
      const res = await api.post('/community/messages/send', {
        senderId: currentUser.id,
        receiverId: toUser.id,
        text: text.trim(),
      });

      // 🔹 Remplace le message temporaire par celui renvoyé par le backend
      setMessages(prev => [res.data, ...prev.filter(m => m.id !== tempMsg.id)]);
    } catch (err) {
      console.error('Erreur envoi message :', err);
    }
  };

  return (
    <SafeAreaView style={{ flex: 1 }}>
      <KeyboardAvoidingView
        style={[styles.container, { backgroundColor: colors.bg }]}
        behavior={Platform.OS === "ios" ? "padding" : "height"}
        keyboardVerticalOffset={120}
        onTouchStart={() => Keyboard.dismiss()}
      >
        <Text style={{ color: colors.text, textAlign:'center', marginVertical:10 }}>
          Conversation avec {toUser.username || toUser.email}
        </Text>

        <FlatList
          data={messages}
          keyExtractor={m => String(m.id)}
          inverted
          contentContainerStyle={{ padding: 16 }}
          keyboardShouldPersistTaps="handled"
          renderItem={({item}) => (
            <View style={[
              styles.bubble,
              {
                alignSelf: item.sender?.id === currentUser.id ? 'flex-end' : 'flex-start',
                backgroundColor: item.sender?.id === currentUser.id ? colors.accent : colors.card
              }
            ]}>
              <Text style={{ color: item.sender?.id === currentUser.id ? '#fff' : colors.text }}>
                {item.text}
              </Text>
            </View>
          )}
        />

        <View style={[styles.row, { borderTopColor: colors.border }]}>
          <TextInput
            value={text}
            onChangeText={setText}
            placeholder="Écrire un message…"
            placeholderTextColor={colors.muted}
            style={[styles.input, { color: colors.text }]}
          />
          <TouchableOpacity style={[styles.send, { backgroundColor: colors.accent }]} onPress={send}>
            <Text style={{ color:'#fff', fontWeight:'700' }}>Envoyer</Text>
          </TouchableOpacity>
        </View>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container:{ flex:1 },
  bubble:{ padding:10, borderRadius:12, marginBottom:10, maxWidth:'80%' },
  row:{ flexDirection:'row', alignItems:'center', padding:10, borderTopWidth:1 },
  input:{ flex:1, padding:10 },
  send:{ paddingVertical:10, paddingHorizontal:14, borderRadius:10, marginLeft:8 }
});