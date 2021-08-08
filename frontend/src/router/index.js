import { createRouter, createWebHistory } from 'vue-router'
import Communities from '../views/Communities.vue'
import Community from '../views/Community.vue'
import Members from '../views/Members.vue'
import Discussion from '../views/Discussion.vue'

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
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
