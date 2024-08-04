<script setup lang="ts">
import tokenService from '@/services/tokenService'
import { onMounted, reactive } from 'vue'
import CurrentUserService from '@/services/CurrentUserService'
import axiosService from '@/services/axiosService'


const state = reactive({
  user: {
    fullName: '',
    email: '',
    profilePicture: '',
    loggedIn: !!tokenService.getToken()
  },
  teams: {
    ids: [] as string[],
    names: [] as string[]
  }
})

if (state.user.loggedIn) {
  onMounted(async () => {
    try {
      const currentUser = CurrentUserService.getCurrentUser();
      currentUser.then((user) => {
        state.user.fullName = user?.fullName ?? '';
        state.user.email = user?.email ?? '';
        state.user.profilePicture = user?.profilePicture ?? '';
      }).then(async () => {
        const result = await axiosService.get<ITeamResult>('/managements-module/Managements/teams', {
          headers: {
            Authorization: `Bearer ${tokenService.getToken()}`
          }
        });
        if (result.data.isSuccess && result.data.value.items) {
          result.data.value.items.forEach((item) => {
            state.teams.ids.push(item.id);
            state.teams.names.push(item.name);
          });
          console.log('Teams', state.teams);
        } else {
          console.error('Error fetching teams', result.data.message, result.data.statusCode);
        }
      });
    } catch (e) {
      console.error('Error fetching user', e);
    }
  });
}

</script>

<template>
  <v-menu
    transition="slide-y-transition"
  >
    <template v-slot:activator="{ props }">
      <v-btn
        color="primary"
        v-bind="props"
      >
        Teams
      </v-btn>
    </template>
    <v-list>
      <v-list-item
        v-for="(team, i) in state.teams.names"
        :key="i"
      >
        <v-list-item-title>{{ team }}</v-list-item-title>
      </v-list-item>
    </v-list>
  </v-menu>
</template>

<style scoped>

</style>
