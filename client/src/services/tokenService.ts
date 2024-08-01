const tokenService = {
  getToken() {
    return !!localStorage.getItem('token');
},
  removeToken(){
    localStorage.removeItem('token');
  },
}

export default tokenService;
