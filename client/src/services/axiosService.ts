import axios from 'axios'

const axiosService = axios.create({
  baseURL: 'http://localhost:5000/',
});

axiosService.interceptors.request.use(
  config => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

export default axiosService;
