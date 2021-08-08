<template>
  <div class="is-flex is-flex-wrap-wrap">
    <div v-if="error" class="notification is-danger">{{ error }}</div>

    <progress v-if="!members && !error" class="progress is-large is-info" max="100">60%</progress>

    <div class="card" v-for="(member, index) of members" :key="index">
      <div class="card-content">
        <div class="media">
          <div class="media-left">
            <figure class="image is-128x128">
              <img :src="member.avatar" alt="Placeholder image" />
            </figure>
          </div>
          <div class="media-content">
            <p class="title is-4">{{ member.name }}</p>
          </div>
        </div>

        <div class="content">
          {{ member.about }}
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
      members: null,
      error: null,
    }
  },

  mounted() {
    this.getMembers()
  },

  methods: {
    getMembers: async function () {
      try {
        this.members = await this.apiGetAllMembers()
      } catch (err) {
        this.error = err
      }
    },
  },
}
</script>

<style scoped>
.card {
  width: 25rem;
  margin: 1rem;
}
.notification {
  margin: 1rem;
}
.progress {
  margin: 3rem;
}
</style>
