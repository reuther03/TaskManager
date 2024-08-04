import axiosService from '@/services/axiosService'
import tokenService from '@/services/tokenService'
import { reactive } from 'vue'



const getCurrentUser = async () => {
  if (tokenService.getToken()) {
    const state = reactive({
      user: {
        fullName: '',
        email: '',
        profilePicture: '',
        loggedIn: !!tokenService.getToken()
      }
    })
    try {
      const response = await axiosService.get<IUser>('/users-module/Users/current-user')
      state.user.fullName = response.data.value.fullName
      state.user.email = response.data.value.email
      state.user.profilePicture = response.data.value.profilePicture
      console.log('Current user', state.user)
      return state.user
    } catch (e) {
      console.error('Error fetching user', e)
    }
  }
}


export default {
  getCurrentUser,
}
