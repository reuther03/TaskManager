<script setup lang="ts">
import axiosService from '@/services/axiosService'
import { onMounted, reactive } from 'vue'
import tokenService from '@/services/tokenService'
import CurrentUserService from '@/services/CurrentUserService'

const state = reactive({
  user: {
    fullName: '',
    email: '',
    profilePicture: '',
    loggedIn: !!tokenService.getToken()
  }
})

if (state.user.loggedIn) {
  onMounted(async () => {
    const currentUser = CurrentUserService.getCurrentUser()
    currentUser.then((user) => {
      state.user.fullName = user?.fullName ?? ''
      state.user.email = user?.email ?? ''
      state.user.profilePicture = user?.profilePicture ?? ''
    })
  })
}
</script>

<template>
</template>

<style scoped>

</style>
