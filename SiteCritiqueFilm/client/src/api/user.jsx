import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL;

export const getUsers = async () => {
  const response = await axios.get(`${API_URL}/admin/users`);
  return response.data;
};

export const getUserById = async (userId) => {
  const response = await axios.get(`${API_URL}/users/${userId}`);
  return response.data;
};

export const updateUser = async (userData) => {
  const response = await axios.put(`${API_URL}/users/me`, userData);
  return response.data;
};

export const deleteUser = async () => {
  const response = await axios.delete(`${API_URL}/users/me`);
  return response.data;
};

export const getFavorites = async (userId) => {
  const res = await axios.get(`${API_URL}/users/${userId}/favorites`);
  return res.data;
};

export const addFavorite = async (filmId) => {
  const res = await axios.post(`${API_URL}/users/me/favorites/${filmId}`);
  return res.data;
};

export const removeFavorite = async (filmId) => {
  const res = await axios.delete(`${API_URL}/users/me/favorites/${filmId}`);
  return res.data;
};

export const getRecommended = async () => {
  const res = await axios.get(`${API_URL}/users/me/recommended`);
  return res.data;
};

export const adminUpdateUser = async (userId, data) => {
    const res = await axios.put(`${API_URL}/admin/users/${userId}`, data);
    return res.data;
};

export const adminDeleteUser = async (userId) => {
    const res = await axios.delete(`${API_URL}/admin/users/${userId}`);
    return res.data;
};
