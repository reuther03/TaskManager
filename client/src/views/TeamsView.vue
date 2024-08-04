<script setup lang="ts">
import { onMounted, reactive } from 'vue'
import tokenService from '@/services/tokenService'
import axiosService from '@/services/axiosService'

const state = reactive({
  user: {
    loggedIn: !!tokenService.getToken()
  },
  teams:[] as ITeam[]
})


if (state.user.loggedIn) {
  onMounted(async () => {
    try {
      const result = await axiosService.get<ITeamResult>('/managements-module/Managements/teams',{
        headers: {
          Authorization: `Bearer ${tokenService.getToken()}`,
        },
      });
      if (result.data.isSuccess && result.data.value.items) {
        if (result.data.isSuccess) {
          state.teams = result.data.value.items.map(item => ({
            id: item.id,
            name: item.name
          }));
      }
    }
    } catch (e) {
      console.error('Error fetching user', e)
    }
  })
}
</script>

<template>
  <div >
    <header class="p-4">
      <h1 class="text-xl">Your teams</h1>
    </header>
    <div>
      <v-row dense>
        <v-col
          v-for="(team, i) in state.teams"
          :key="i"
          cols="12"
          md="4"
        >
          <v-card
            variant="outlined"
            class="mx-auto"
            color="surface-variant"
            max-width="300"
            :title="team.name"
          >
            <template v-slot:actions>

              <RouterLink :to="`/teams/${team.id[i]}`">
                <v-btn text="View"></v-btn>
              </RouterLink>
            </template>
          </v-card>
        </v-col>
      </v-row>
    </div>
  </div>
</template>

<style scoped>

</style>
