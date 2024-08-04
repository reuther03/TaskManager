<script setup lang="ts">
import { RouterLink } from 'vue-router'
import logo from '@/assets/img/logo.png'
import { onMounted, reactive } from 'vue'
import tokenService from '@/services/tokenService'
import router from '@/router'
import axiosService from '@/services/axiosService'

const items = reactive([
  { title: 'Account', action: 'account' },
  { title: 'Logout', action: 'logout' },
  { title: 'Teams', action: 'teams'}
])

const state = reactive({
  user: {
    fullName: '',
    email: '',
    profilePicture: '',
    loggedIn: !!tokenService.getToken()
  }
})

const handleMenuItemClick = (action: string) => {
  if (action === 'account') {
    router.push('/account')
  } else if (action === 'logout') {
    handleLogout()
  } else if (action === 'teams') {
    router.push('/teams')
  }
}

const handleLogout = () => {
  tokenService.removeToken()
  state.user.loggedIn = false
  router.push('/')
}


if (state.user.loggedIn) {
  onMounted(async () => {
    try {
      const response = await axiosService.get<IUser>('/users-module/Users/current-user')
      state.user.fullName = response.data.value.fullName
      state.user.email = response.data.value.email
      state.user.profilePicture = response.data.value.profilePicture
    } catch (e) {
      console.error('Error fetching user', e)
    }
  })
}

</script>

<template>
  <nav class="bg-blue-700 border-b border-blue-900">
    <div class="mx-auto max-w-10xl px-2 sm:px-6 lg:px-2">
      <div class="flex h-20 items-center justify-between">
        <div class="flex flex-1 items-center justify-center md:items-stretch md:justify-start">
          <!-- Logo -->
          <RouterLink class="flex flex-shrink-0 items-center mr-4" to="/">
            <img class="h-10 w-auto" :src="logo" alt="Task Manager" />
            <span class="hidden md:block text-white text-2xl font-bold ml-2">Task Manager</span>
          </RouterLink>
          <div class="justify-center ma-2">
          </div>

          <div class="md:ml-auto">
            <div class="flex space-x-2">
              <div class="flex space-x-2" v-if="state.user.loggedIn">
                <div class="d-flex justify-space-around">

                  <v-menu
                    transition="scale-transition"
                  >
                    <template v-slot:activator="{ props } ">
                      <v-btn color="blue-darken-4" v-bind="props" icon="" class="profile-picture-btn">
                        <img class="user" :src="state.user.profilePicture" alt="profile picture">
                      </v-btn>
                    </template>

                    <v-list>
                      <v-list-item
                        class="hover:bg-blue-500 hover:text-white hover:shadow-lg cursor-pointer"
                        @click="handleMenuItemClick(item.action)"
                        v-for="(item, i) in items"
                        :key="i"
                      >
                        <v-list-item-title>{{ item.title }}</v-list-item-title>
                      </v-list-item>
                    </v-list>
                  </v-menu>
                </div>
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

<style scoped>
.profile-picture-btn {
  width: 50px;
  height: 50px;
  border-radius: 50%;
  overflow: hidden;
  padding: 0;
  margin: 0;
}

.user {
  display: inline-block;
  width: 50px;
  height: 50px;
  margin-top: -1px;
  border-radius: 50%;
  object-fit: cover;
}
</style>
