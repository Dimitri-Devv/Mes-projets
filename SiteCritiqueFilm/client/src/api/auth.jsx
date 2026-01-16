import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL;

export const loginApi = async (email, password) => {
  const response = await axios.post(`${API_URL}/auth/login`, {
    email,
    password,
  });
  return response.data;
};

export const registerApi = async (email, password, username) => {
  const response = await axios.post(`${API_URL}/auth/register`, {
    username,
    email,
    password,
  });
  return response.data;
};

export const verifyEmailApi = async (email, code) => {
  const response = await axios.post(`${API_URL}/auth/verify`, {
    email,
    code,
  });
  return response.data;
};
