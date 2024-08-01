<script setup lang="ts">
import { RouterLink } from 'vue-router'
import logo from '@/assets/img/logo.png'
import { reactive } from 'vue'
import tokenService from '@/services/tokenService'
import router from '@/router'

const state = reactive({
  user: {
    loggedIn: tokenService.getToken()
  }
})
const handleLogout = () => {
  tokenService.removeToken()
  state.user.loggedIn = false
  router.push('/')
}

</script>

<template>
  <nav class="bg-blue-700 border-b border-blue-900">
    <div class="mx-auto max-w-7xl px-2 sm:px-6 lg:px-8">
      <div class="flex h-20 items-center justify-between">
        <div class="flex flex-1 items-center justify-center md:items-stretch md:justify-start">
          <!-- Logo -->
          <RouterLink class="flex flex-shrink-0 items-center mr-4" to="/">
            <img class="h-10 w-auto" :src="logo" alt="Task Manager" />
            <span class="hidden md:block text-white text-2xl font-bold ml-2">Task Manager</span>
          </RouterLink>
          <div class="md:ml-auto">
            <div class="flex space-x-2">
              <div class="flex space-x-2" v-if="state.user.loggedIn">
                <RouterLink to="/" @click="handleLogout" class="text-white bg-blue-900 hover:bg-gray-900 hover:text-white rounded-md px-3 py-2">Logout</RouterLink>
              </div>
              <div v-else class="flex space-x-2">
                <RouterLink to="/login" class="text-white bg-blue-900 hover:bg-gray-900 hover:text-white rounded-md px-3 py-2">Login</RouterLink>
                <RouterLink to="/register" class="text-white bg-blue-900 hover:bg-gray-900 hover:text-white rounded-md px-3 py-2">Register</RouterLink>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </nav>
</template>

<style scoped></style>
