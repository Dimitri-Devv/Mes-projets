import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL;

export const getActors = async () => {
    const res = await axios.get(`${API_URL}/actors`);
    return res.data;
};

export const createActor = async (actorData) => {
    const res = await axios.post(`${API_URL}/actors`, actorData);
    return res.data;
};

export const updateActor = async (actorId, actorData) => {
    const res = await axios.put(`${API_URL}/actors/${actorId}`, actorData);
    return res.data;
};

export const deleteActor = async (actorId) => {
    const res = await axios.delete(`${API_URL}/actors/${actorId}`);
    return res.data;
};

export const getActorById = async (actorId) => {
    const res = await axios.get(`${API_URL}/actors/${actorId}`);
    return res.data;
};

export const getFilmsByActor = async (actorId) => {
    const res = await axios.get(`${API_URL}/actors/${actorId}/films`);
    return res.data;
};

export const getActorsByFilm = async (filmId) => {
    const res = await axios.get(`${API_URL}/actors/film/${filmId}`);
    return res.data;
};

export const addFilmToActor = async (actorId, filmId) => {
    const res = await axios.post(`${API_URL}/actors/${actorId}/films/${filmId}`);
    return res.data;
};

export const removeFilmFromActor = async (actorId, filmId) => {
    const res = await axios.delete(`${API_URL}/actors/${actorId}/films/${filmId}`);
    return res.data;
};