import axios from "axios";
import { message } from "antd";

// get the base URL from environment variables or use a default
const BASE_URL = process.env.REACT_APP_BASE_URL || "http://localhost:8080";
const api = axios.create({
    baseURL: BASE_URL,
    headers: {
        "Content-Type": "application/json",
    },
});

// Add a request interceptor to include the token in headers
api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem("token");
        if (token) {
            config.headers["Authorization"] = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    },
);

// Add a response interceptor to handle errors globally
api.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        if (error.response?.status === 401) {
            localStorage.removeItem("token");
            message.error("Session expired. Please login again.");
            window.location.href = "/login";
        }
        return Promise.reject(error);
    },
);

export default api;