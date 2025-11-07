<!-- AppHeader.vue -->
<script setup lang="ts">
import { ref } from 'vue';

defineProps<{
  cartCount: number;
}>();

const emit = defineEmits<{
  openCart: [];
}>();

const isMenuOpen = ref(false);

const toggleMenu = () => {
  isMenuOpen.value = !isMenuOpen.value;
};

const menuItems = [
  { label: 'Меню', href: '#' },
  { label: 'Меню', href: '#' },
  { label: 'Меню', href: '#' },
  { label: 'Меню', href: '#' },

];
</script>

<template>
  <header class="header">
    <div class="logo">NormtaKoiRest</div>

    <div class="actions">
      <!-- Кнопка меню -->
      <div class="menu-wrapper" @click.stop>
        <button class="menu-btn" @click="toggleMenu">☰</button>
        <div v-if="isMenuOpen" class="dropdown">
          <a v-for="item in menuItems" :key="item.label" :href="item.href" class="dropdown-item"
            @click="isMenuOpen = false">
            {{ item.label }}
          </a>
        </div>
      </div>

      <button class="cart-btn" @click="emit('openCart')">
        🛒
        <span v-if="cartCount > 0" class="badge">{{ cartCount }}</span>
      </button>
    </div>
  </header>
</template>

<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  max-width: 1200px;
  margin: 0 auto;
  padding: 16px 16px;
  background: white;
}

.logo {
  font-size: 20px;
  font-weight: bold;
}

.actions {
  display: flex;
  gap: 16px;
  position: relative;
}

.menu-btn {
  width: 40px;
  height: 40px;
  border: none;
  background: #f3f4f6;
  border-radius: 8px;
  font-size: 20px;
  cursor: pointer;
}

.menu-btn:hover {
  background: #e5e7eb;
}

.menu-wrapper {
  position: relative;
}

.dropdown {
  position: absolute;
  top: 100%;
  right: 0;
  margin-top: 8px;
  width: 160px;
  background: white;
  border: 1px solid #ddd;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  padding: 8px 0;
  z-index: 50;
}

.dropdown-item {
  display: block;
  padding: 10px 16px;
  text-decoration: none;
  color: #333;
  font-size: 14px;
}

.dropdown-item:hover {
  background: #f9fafb;
}

.cart-btn {
  position: relative;
  width: 40px;
  height: 40px;
  border: none;
  background: #f3f4f6;
  border-radius: 50%;
  font-size: 18px;
  cursor: pointer;
}

.cart-btn:hover {
  background: #e5e7eb;
}

.badge {
  position: absolute;
  top: -4px;
  right: -4px;
  background: #f97316;
  color: white;
  width: 20px;
  height: 20px;
  border-radius: 50%;
  font-size: 12px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
}
</style>