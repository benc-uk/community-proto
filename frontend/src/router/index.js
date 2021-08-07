import { createRouter, createWebHistory } from 'vue-router'
import Communities from '../views/Communities.vue'
import Members from '../views/Members.vue'

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
    path: '/members',
    name: 'Members',
    component: Members,
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
