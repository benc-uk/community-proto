<template>
  <nav class="navbar is-light" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
      <a class="navbar-item" href="/">
        <img src="./assets/community.svg" width="66" height="66" />
        Community Prototype
      </a>

      <a role="button" class="navbar-burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
        <span aria-hidden="true"></span>
      </a>
    </div>

    <div id="navbarBasicExample" class="navbar-menu">
      <div class="navbar-start">
        <a class="navbar-item"> <router-link to="/communities" exact>Communities</router-link> </a>
        <a class="navbar-item"> <router-link to="/members" exact>Members</router-link> </a>
      </div>
    </div>
  </nav>

  <section class="container">
    <router-view />
  </section>
</template>

<style></style>
