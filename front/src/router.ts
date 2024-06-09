import { createWebHistory, createRouter } from 'vue-router';

import Main from './components/Main.vue';
import Login from './components/Login.vue';

const routes = [
  { path: '/main', name: 'main', component: Main },
  { path: '/', name: 'login', component: Login },
];

export default createRouter({
  history: createWebHistory(),
  routes,
});