<script setup lang="ts">
import { reactive } from 'vue'
import tokenService from '@/services/tokenService'
import axiosService from '@/services/axiosService'

const state = reactive({
  user: {
    loggedIn: tokenService.getToken()
  }
})

const list = reactive([
  { icon: 'mdi-home', text: 'Home' },
  { icon: 'mdi-account', text: 'Account' },
  { icon: 'mdi-settings', text: 'Settings' },
  { icon: 'mdi-logout', text: 'Logout' }
])


</script>

<template>
<!--    <v-card v-if="state.user.loggedIn"-->
<!--      class="mx-auto"-->
<!--      width="256"-->
<!--    >-->
<!--      <v-layout>-->
<!--        <v-navigation-drawer absolute permanent>-->
<!--          <v-list>-->
<!--            <v-list-item-->
<!--              prepend-avatar="https://cdn.vuetifyjs.com/images/john.png"-->
<!--              subtitle="john@google.com"-->
<!--              title="John Leider"-->
<!--            >-->
<!--              <template v-slot:append>-->
<!--                <v-btn-->
<!--                  icon="mdi-menu-down"-->
<!--                  size="small"-->
<!--                  variant="text"-->
<!--                ></v-btn>-->
<!--              </template>-->
<!--            </v-list-item>-->
<!--          </v-list>-->

<!--          <v-divider></v-divider>-->

<!--          <v-list-->
<!--            :lines="false"-->
<!--            density="compact"-->
<!--            nav-->
<!--          >-->
<!--            <v-list-item-->
<!--              v-for="(item, i) in list"-->
<!--              :key="i"-->
<!--              :value="item"-->
<!--              color="primary"-->
<!--            >-->
<!--              <template v-slot:prepend>-->
<!--                <v-icon :icon="item.icon"></v-icon>-->
<!--              </template>-->

<!--              <v-list-item-title v-text="item.text"></v-list-item-title>-->
<!--            </v-list-item>-->
<!--          </v-list>-->
<!--        </v-navigation-drawer>-->

<!--        <v-main style="height: 354px;"></v-main>-->
<!--      </v-layout>-->
<!--    </v-card>-->
</template>

<style scoped></style>
