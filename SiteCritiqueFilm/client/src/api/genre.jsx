import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL;

// 🔹 Récupérer tous les genres
export const getGenres = async () => {
  const response = await axios.get(`${API_URL}/genres`);
  return response.data;
};

// 🔹 Récupérer un genre par ID
export const getGenreById = async (id) => {
  const response = await axios.get(`${API_URL}/genres/${id}`);
  return response.data;
};

// 🔹 Créer un nouveau genre
export const createGenre = async (genreData) => {
  const response = await axios.post(`${API_URL}/genres`, genreData);
  return response.data;
};

// 🔹 Mettre à jour un genre
export const updateGenre = async (genreData) => {
  const response = await axios.put(`${API_URL}/genres`, genreData);
  return response.data;
};

// 🔹 Supprimer un genre
export const deleteGenre = async (id) => {
  const response = await axios.delete(`${API_URL}/genres/${id}`);
  return response.data;
};