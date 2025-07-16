import axios from 'axios';

const axiosClient = axios.create({
    baseURL: 'https://localhost:7026/api',
    headers: {
        'Content-Type': 'application/json',
    }
});

axiosClient.interceptors.request.use((config) => {
    const token = localStorage.getItem('JwtToken')
    
    if (token) {
        config.headers.Authorization = `Bearer ${token}`
    }

    return config;
});

export default axiosClient;