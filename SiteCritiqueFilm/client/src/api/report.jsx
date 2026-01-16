import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL + "/reports";

export const createReport = async (reporterId, reportedId, message) => {
    const res = await axios.post(`${API_URL}/create`, null, {
        params: { reporterId, reportedId, message }
    });
    return res.data;
};

export const getAllReports = async () => {
    const res = await axios.get(API_URL);
    return res.data;
};

export const processReport = async (reportId, adminId, warning = false) => {
    const res = await axios.post(`${API_URL}/${reportId}/process`, null, {
        params: { adminId, warning }
    });
    return res.data;
};