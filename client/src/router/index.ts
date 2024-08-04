import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
import AccountView from '@/views/AccountView.vue'
import TeamsView from '@/views/TeamsView.vue'
import TeamView from '@/views/TeamsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Home',
      component: HomeView,
      meta: {
        showNavbar: true
      }
    },
    {
      path: '/login',
      name: 'Login',
      component: LoginView,
      meta: {
        showNavbar: false
      }
    },
    {
      path: '/register',
      name: 'Register',
      component: RegisterView,
      meta: {
        showNavbar: false
      }
    },
    {
      path: '/account',
      name: 'Account',
      component: AccountView,
      meta: {
        showNavbar: true
      }
    },
    {
      path: '/teams',
      name: 'Teams',
      component: TeamsView,
      meta: {
        showNavbar: true
      }
    },
  ]
})

export default router
