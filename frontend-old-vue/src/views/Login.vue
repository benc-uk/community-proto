<template>
  <div v-if="error">
    {{ error }}
  </div>
  <button @click="login" class="button is-dark is-large m-4"><img src="../assets/ms-tiny-logo.png" /> &nbsp; Sign-in using Microsoft Account</button>
</template>

<script>
import auth from '../services/auth'
import apiMixin from '../mixins/apiMixin.js'

export default {
  name: 'Login',
  emits: ['loginComplete'],
  // Adds functions to call the API
  mixins: [apiMixin],

  data: function () {
    return {
      error: null,
    }
  },

  methods: {
    async login() {
      try {
        await auth.login([`api://${process.env.VUE_APP_CLIENT_ID}/Communities.ReadWrite`])

        // Register user, note it's safe to call this each time
        await this.apiAddUser(auth.user().username, auth.user().name)

        this.$emit('loginComplete')
        this.$router.replace({ path: '/' })
      } catch (err) {
        this.error = err
      }
    },
  },
}
</script>

<style></style>
