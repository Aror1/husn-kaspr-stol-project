import { createRouter, createWebHistory } from 'vue-router';
import App from '../App.vue';
import CheckoutPage from '../components/CheckoutPage.vue';

const routes = [
  { path: '/', component: App },
  { path: '/checkout', component: CheckoutPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;