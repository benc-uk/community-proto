<template>
  <div class="is-flex is-flex-wrap-wrap">
    <div v-if="error" class="notification is-danger">{{ error }}</div>

    <div class="card" v-for="(comm, index) of communities" :key="index">
      <div class="card-image">
        <figure class="image is-4by3">
          <img :src="comm.image" alt="Community image" />
        </figure>
      </div>

      <div class="card-content">
        <div class="media">
          <div class="media-content">
            <p class="title is-4">{{ comm.name }}</p>
            <p class="subtitle is-6">{{ comm.members.length }} members</p>
          </div>
        </div>

        <div class="content">
          {{ comm.description }}
        </div>
      </div>
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
</style>
