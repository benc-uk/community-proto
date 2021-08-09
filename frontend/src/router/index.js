import { createRouter, createWebHistory } from 'vue-router'
import Communities from '../views/Communities.vue'
import Community from '../views/Community.vue'
import Members from '../views/Members.vue'
import Discussion from '../views/Discussion.vue'
import Login from '../views/Login.vue'

import auth from '../services/auth'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Communities,
  },
  {
    path: '/communities',
    name: 'Communities',
    component: Communities,
  },
  {
    path: '/community/:id',
    name: 'Community',
    component: Community,
  },
  {
    path: '/discussion/:id',
    name: 'Discussion',
    component: Discussion,
  },
  {
    path: '/members',
    name: 'Members',
    component: Members,
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

router.beforeEach((to, from, next) => {
  if (to.name !== 'Login' && !auth.user()) next({ name: 'Login' })
  else next()
})

export default router
