import axios from "axios";
const API_URL = import.meta.env.VITE_API_URL + "/reviews";

export const getReviewsForFilm = async (filmId) => {
  try {
    const response = await axios.get(`${API_URL}/film/${filmId}`);
    return response.data;
  } catch (error) {
    console.error("Erreur lors du chargement des critiques :", error);
    throw error;
  }
};

export const createReview = async (filmId, userId, data, token) => {
  try {
    const response = await axios.post(
      `${API_URL}/film/${filmId}/user/${userId}`,
      data,
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la création de la critique :", error);
    throw error;
  }
};

export const updateReview = async (reviewId, userId, data, token) => {
  try {
    const response = await axios.put(
      `${API_URL}/${reviewId}/user/${userId}`,
      data,
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );
    return response.data;
  } catch (error) {
    console.error("Erreur lors de la modification :", error);
    throw error;
  }
};

export const deleteReview = async (reviewId, userId, token) => {
  try {
    await axios.delete(`${API_URL}/${reviewId}/user/${userId}`, {
      headers: { Authorization: `Bearer ${token}` },
    });

    return true;
  } catch (error) {
    console.error("Erreur suppression critique :", error);
    throw error;
  }
};

export const toggleReviewLike = async (reviewId, userId, liked, token) => {
  try {
    const response = await axios.put(
      `${API_URL}/likes/toggle/${reviewId}`,
      { userId, liked },
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );

    return response.data;
  } catch (error) {
    console.error("Erreur like critique :", error);
    throw error;
  }
};

export const getReviewUserLikeStatus = async (reviewId, userId, token) => {
  try {
    const response = await axios.get(
      `${API_URL}/likes/status/${reviewId}/user/${userId}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    return response.data;
  } catch (error) {
    console.error("Erreur like critique :", error);
    return null;
  }
};

export const getTopReviews = async () => {
  try {
    const response = await axios.get(`${API_URL}/top`);
    return response.data;
  } catch (error) {
    console.error("Erreur récupération top critiques :", error);
    throw error;
  }
};

export const getReviewById = async (userId) => {
  const response = await axios.get(`${API_URL}/user/${userId}`);
  return response.data;
};


