<script setup lang="ts">
import { reactive } from 'vue'
import router from '@/router'
import { useToast } from 'vue-toastification'
import axiosService from '@/services/axiosService'
import tokenService from '@/services/tokenService'
const toast = useToast()

const form = reactive({
  email: '',
  password: ''
})

const handleSubmit = async () => {
  const user = {
    email: form.email,
    password: form.password
  }
  try {
    const result = await axiosService.post<ILoginResult>('/users-module/Users/login', user)

    if (result.data.isSuccess) {
      tokenService.setToken(result.data.value.token)
      toast.success('Logged in successfully')
    } else {
      console.error('Error logging in', result.data.message)
      toast.error(result.data.message)
      return new Error(result.data.message)
    }

    await router.push('/')
  } catch (e) {
    console.error('Error', e)
  }
}
</script>

<template>
  <section class="min-h-screen bg-blue-50">
    <div class="container m-auto max-w-2xl py-24">
      <div class="bg-white px-6 py-8 mb-4 shadow-md rounded-md border m-4 md:m-0">
        <form @submit.prevent="handleSubmit">
          <h2 class="text-3xl text-center font-semibold mb-6">Login</h2>

          <div class="mb-4">
            <label for="contact_email" class="block text-gray-700 font-bold mb-2">Email</label>
            <input v-model="form.email" type="email" id="contact_email" name="contact_email" class="border rounded w-full py-2 px-3" placeholder="Email address" required />
          </div>
          <div class="mb-4">
            <label for="Password" class="block text-gray-700 font-bold mb-2">Password</label>
            <input v-model="form.password" type="password" id="password" name="password" class="border rounded w-full py-2 px-3" placeholder="Password" required />
          </div>
          <div>
            <button class="bg-blue-700 hover:bg-blue-800 text-white font-bold py-2 px-4 rounded-full w-full focus:outline-none focus:shadow-outline" type="submit">Login</button>
          </div>
        </form>
        <div class="flex mt-5 justify-center">
          <router-link to="/register" class="text-blue-700 hover:text-blue-950">Create an account</router-link>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped></style>
