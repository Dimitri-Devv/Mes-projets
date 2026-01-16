import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL + "/comments";

export const getCommentsForReview = async (reviewId) => {
    try {
        const res = await axios.get(`${API_URL}/review/${reviewId}`);
        return res.data;
    } catch (error) {
        console.error("Error fetching comments for review:", error);
        throw error;
    }
};

export const addComment = async (reviewId, userId, data, token) => {
    try {
        const res = await axios.post(
            `${API_URL}/review/${reviewId}/user/${userId?.id ?? userId}`,
            data,
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            }
        );
        return res.data;
    } catch (error) {
        console.error("Error adding comment:", error);
        throw error;
    }
};

export const updateComment = async (commentId, userId, data, token) => {
    try {
        const res = await axios.put(
            `${API_URL}/${commentId}/user/${userId?.id ?? userId}`,
            data,
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            }
        );
        return res.data;
    } catch (error) {
        console.error("Error updating comment:", error);
        throw error;
    }
};

export const deleteComment = async (commentId, userId, isAdmin = false, token) => {
    try {
        const params = new URLSearchParams({ isAdmin });
        const url = `${API_URL}/${commentId}/user/${userId?.id ?? userId}?${params.toString()}`;
        await axios.delete(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });
    } catch (error) {
        console.error("Error deleting comment:", error);
        throw error;
    }
};