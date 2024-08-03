import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from '@/router'
import Toast from 'vue-toastification'
import 'vue-toastification/dist/index.css'

import 'vuetify/styles'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { createVuetify } from 'vuetify'

const app = createApp(App)

app.use(router)
app.use(Toast, {
  transition: 'Vue-Toastification__bounce',
  maxToasts: 20,
  newestOnTop: true
})

const vuetify = createVuetify({
  components,
  directives,
  ssr: true
})
app.use(vuetify)

app.mount('#app')
