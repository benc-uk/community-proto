<template>
  <nav class="navbar is-info has-shadow" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <a class="navbar-item" href="/">
        <img src="./assets/community.svg" width="66" height="66" />
        Home
      </a>

      <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
      </a>
    </div>

    <div id="navbarBasicExample" class="navbar-menu">
      <div class="navbar-start">
        <router-link class="navbar-item" to="/communities" exact> <i class="fas fa-layer-group"></i> &nbsp; Communities </router-link>
        <router-link class="navbar-item" to="/members" exact><i class="fas fa-users"></i> &nbsp; Members</router-link>
      </div>
    </div>

    <div class="navbar-end">
      <a v-if="user" class="navbar-item" @click="logout"><i class="fas fa-sign-out-alt"></i> &nbsp; Logout {{ user.name }}</a>
    </div>
  </nav>

  <section class="container">
    <router-view @loginComplete="loginComplete" />
  </section>
</template>

<script>
import auth from './services/auth'

export default {
  name: 'App',

  data: function () {
    return {
      user: {},
    }
  },

  async created() {
    // Basic setup of MSAL helper with client id
    if (process.env.VUE_APP_CLIENT_ID) {
      auth.configure(process.env.VUE_APP_CLIENT_ID)
      console.log(`Configured ${auth.isConfigured()}`)

      // Restore any cached or saved local user
      this.user = auth.user()
      if (!this.user) {
        this.$router.push('/login')
      }
    } else {
      console.log('VUE_APP_CLIENT_ID is not set, login will not function')
    }
  },

  methods: {
    async loginComplete() {
      this.user = auth.user()
    },

    logout() {
      auth.clearLocal()
      this.$router.go('/')
    },
  },
}
</script>
