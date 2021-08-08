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
        <a class="navbar-item">
          <router-link to="/communities" exact> <i class="fas fa-layer-group"></i> Communities </router-link>
        </a>
        <a class="navbar-item">
          <router-link to="/members" exact><i class="fas fa-users"></i> Members</router-link>
        </a>
        <a class="navbar-item">
          <router-link to="/login" exact><i class="fas fa-sign-in-alt"></i> Login</router-link>
        </a>
      </div>
    </div>
  </nav>

  <section class="container">
    <router-view />
  </section>
</template>

<style scoped>
.navbar-menu a {
  color: #222 !important;
}
</style>
