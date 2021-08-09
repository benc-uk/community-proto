<template>
  <div v-if="error">
    {{ error }}
  </div>
  <button @click="login" class="button is-dark is-large m-4"><img src="../assets/ms-tiny-logo.png" /> &nbsp; Sign-in using Microsoft Account</button>
</template>

<script>
import auth from '../services/auth'

export default {
  name: 'Login',
  emits: ['loginComplete'],

  data: function () {
    return {
      error: null,
    }
  },

  methods: {
    async login() {
      try {
        await auth.login([`api://${process.env.VUE_APP_CLIENT_ID}/Communities.ReadWrite`])
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
