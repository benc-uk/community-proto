<template>
  <div class="is-flex is-flex-wrap-wrap">
    <div v-if="error" class="notification is-danger">{{ error }}</div>

    <progress v-if="!communities && !error" class="progress is-large is-info" max="100">-</progress>

    <div class="card" v-for="(community, index) of communities" :key="index">
      <router-link :to="{ name: 'Community', params: { id: community.id } }">
        <div class="card-image">
          <figure class="image is-4by3">
            <img :src="community.image" alt="Community image" />
          </figure>
        </div>
      </router-link>

      <router-link :to="{ name: 'Community', params: { id: community.id } }">
        <div class="card-content">
          <div class="media">
            <div class="media-content">
              <p class="title is-4">{{ community.name }}</p>
              <p class="subtitle is-6">{{ community.members.length }} members - {{ community.members.length }} discussions</p>
            </div>
          </div>

          <div class="content">
            {{ community.description }}
          </div>
        </div>
      </router-link>
    </div>
  </div>
</template>

<script>
import apiMixin from '../mixins/apiMixin.js'

export default {
  // Adds functions to call the API
  mixins: [apiMixin],

  data: function () {
    return {
      communities: null,
      error: null,
    }
  },

  mounted() {
    this.getCommunities()
  },

  methods: {
    getCommunities: async function () {
      try {
        this.communities = await this.apiGetAllCommunities()
      } catch (err) {
        this.error = err
      }
    },
  },
}
</script>

<style scoped>
.card {
  width: 20rem;
  margin: 1rem;
}
.notification {
  margin: 1rem;
}
.progress {
  margin: 3rem;
}
</style>
