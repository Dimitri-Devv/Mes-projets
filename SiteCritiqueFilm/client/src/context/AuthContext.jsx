import { createContext, useContext, useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";
import axios from "axios";

import { getUserById } from "../api/user";

const AuthContext = createContext();

// eslint-disable-next-line react-refresh/only-export-components
export const useAuth = () => {
  return useContext(AuthContext);
};
export const AuthProvider = ({ children }) => {
  const [token, setToken] = useState(() => localStorage.getItem("token"));
  const [user, setUser] = useState(null);
  const [isAdmin, setIsAdmin] = useState(false);
  const [userId, setUserId] = useState(null);

  const fetchUser = async (id) => {
    try {
      const response = await getUserById(id);
      const userData = response.data ? response.data : response;
      setUser(userData);
      setIsAdmin(userData.role === "ADMIN");
    } catch (err) {
      console.error("Erreur récupération utilisateur :", err);
      logout();
    }
  };

  useEffect(() => {
    const token = localStorage.getItem("token");
    const expiry = localStorage.getItem("tokenExpiry");
    if (expiry && Date.now() > Number(expiry)) {
      logout();
    }
    if (token) {
      setToken(token);
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      try {
        const userDecoded = jwtDecode(token);
        setUserId(userDecoded.id);
        fetchUser(userDecoded.id);
      } catch (e) {
        console.warn("Erreur décodage JWT :", e);
        logout();
      }
    }
  }, []);

  useEffect(() => {
    const interval = setInterval(() => {
      const expiry = localStorage.getItem("tokenExpiry");
      if (expiry && Date.now() > Number(expiry)) {
        console.log("Token expiré automatiquement");
        logout();
      }
    }, 60_000);
    return () => clearInterval(interval);
  }, []);

  const login = (token) => {
    const expiry = Date.now() + 24 * 60 * 60 * 1000;
    localStorage.setItem("token", token);
    localStorage.setItem("tokenExpiry", expiry.toString());
    setToken(token);
    axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    try {
      const userDecoded = jwtDecode(token);
      setUserId(userDecoded.id);
      fetchUser(userDecoded.id);
    } catch (e) {
      void e;
      setUser(null);
      setUserId(null);
      setIsAdmin(false);
    }
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("tokenExpiry");
    setToken(null);
    setUser(null);
    setUserId(null);
    setIsAdmin(false);
    delete axios.defaults.headers.common["Authorization"];
  };

  return (
    <AuthContext.Provider
      value={{ token, user, isAdmin, userId, login, logout }}
    >
      {children}
    </AuthContext.Provider>
  );
};
