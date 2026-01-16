import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL;

export const getFilms = async () => {
  const response = await axios.get(`${API_URL}/films`);
  return response.data;
};

export const getFilmsAffiche = async () => {
  const response = await axios.get(`${API_URL}/films/short`);
  return response.data;
}
export const getFilmById = async (id) => {
  const response = await axios.get(`${API_URL}/films/${id}`);
  return response.data;
};

export const createFilm = async (filmData) => {
  const response = await axios.post(`${API_URL}/films`, filmData);
  return response.data;
};

export const updateFilm = async (id, filmData) => {
  const response = await axios.put(`${API_URL}/films/${id}`, filmData);
  return response.data;
};

export const deleteFilm = async (id) => {
  const response = await axios.delete(`${API_URL}/films/${id}`);
  return response.data;
};
