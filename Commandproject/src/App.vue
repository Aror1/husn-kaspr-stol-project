<script setup lang="ts">
import { ref, computed } from 'vue';
import type { CartItem, Product, Category } from './components/types';
import AppHeader from './components/AppHeader.vue';
import HeroSection from './components/HeroSection.vue';
import CategoryPills from './components/CategoryPills.vue';
import FilterTabs from './components/FilterTabs.vue';
import ProductGrid from './components/ProductGrid.vue';
import CartModal from './components/CartModal.vue';
import AppFooter from './components/AppFooter.vue';
import ContactModal from './components/ContactModal.vue';
import CheckoutPage from './components/CheckoutPage.vue';

// Cart state
const cartOpen = ref(false);
const cartItems = ref<CartItem[]>([
]);
const contactOpen = ref(false);
const activeCategory = ref('напитки');
const activeMainCategory = ref('Завтраки');
const activeSubCategory = ref('напитки');



// Products data
const lemonades: Product[] = [
  { id: 1, name: 'Михалъч клубничный', price: 123, image: 'https://cookingwithcurls.com/wp-content/uploads/2015/06/A-Blue-Lemonade-is-the-perfect-cocktail-to-cool-you-off-this-summer-cookingwithcurls.com_.jpg' },
  { id: 2, name: 'Михалъч клубничный', price: 123, image: 'https://cookingwithcurls.com/wp-content/uploads/2015/06/A-Blue-Lemonade-is-the-perfect-cocktail-to-cool-you-off-this-summer-cookingwithcurls.com_.jpg' },
  { id: 3, name: 'Михалъч клубничный', price: 123, image: 'https://cookingwithcurls.com/wp-content/uploads/2015/06/A-Blue-Lemonade-is-the-perfect-cocktail-to-cool-you-off-this-summer-cookingwithcurls.com_.jpg' },
  { id: 4, name: 'Михалъч клубничный', price: 123, image: 'https://cookingwithcurls.com/wp-content/uploads/2015/06/A-Blue-Lemonade-is-the-perfect-cocktail-to-cool-you-off-this-summer-cookingwithcurls.com_.jpg' },
  { id: 5, name: 'Михалъч клубничный', price: 123, image: 'https://cookingwithcurls.com/wp-content/uploads/2015/06/A-Blue-Lemonade-is-the-perfect-cocktail-to-cool-you-off-this-summer-cookingwithcurls.com_.jpg' },
  { id: 6, name: 'Михалъч клубничный', price: 123, image: 'https://cookingwithcurls.com/wp-content/uploads/2015/06/A-Blue-Lemonade-is-the-perfect-cocktail-to-cool-you-off-this-summer-cookingwithcurls.com_.jpg' },
  { id: 7, name: 'Михалъч клубничный', price: 123, image: 'https://cookingwithcurls.com/wp-content/uploads/2015/06/A-Blue-Lemonade-is-the-perfect-cocktail-to-cool-you-off-this-summer-cookingwithcurls.com_.jpg' }
];

const pizzas: Product[] = [
  { id: 8, name: 'Пицца маргарита', price: 123, image: 'https://slicelife.imgix.net/33440/photos/original/Untitled_(750_x_550_px).jpg?auto=compress&auto=format' },
  { id: 9, name: 'Пицца маргарита', price: 123, image: 'https://slicelife.imgix.net/33440/photos/original/Untitled_(750_x_550_px).jpg?auto=compress&auto=format' },
  { id: 10, name: 'Пицца маргарита', price: 123, image: 'https://slicelife.imgix.net/33440/photos/original/Untitled_(750_x_550_px).jpg?auto=compress&auto=format' },
  { id: 11, name: 'Пицца маргарита', price: 123, image: 'https://slicelife.imgix.net/33440/photos/original/Untitled_(750_x_550_px).jpg?auto=compress&auto=format' },
  { id: 12, name: 'Пицца маргарита', price: 123, image: 'https://slicelife.imgix.net/33440/photos/original/Untitled_(750_x_550_px).jpg?auto=compress&auto=format' }
];

const categories: Category[] = [
  { name: 'Завтраки', icon: '🍳', label: 'Завтраки' },
  { name: 'Комплексы', icon: '🍽️', label: 'Комплексы' },
  { name: 'Закуски', icon: '🍤', label: 'Закуски' },
  { name: 'Десерты', icon: '🍰', label: 'Десерты' }
];

const categoryProducts: Record<string, Product[]> = {
  'напитков': lemonades,
  'от кофе': pizzas,
  'сладкого': [], // заглушка
};

const categoryData: Record<string, Record<string, Product[]>> = {
  'Завтраки': {
    'напитки': lemonades,
    'к кофе': pizzas,
    'сладкое': [],
  },
  'Комплексы': {
    'напитки': lemonades,
    'к кофе': pizzas,
    'сладкое': [],
  },
  'Закуски': {
    'напитки': [],
    'к кофе': pizzas,
    'сладкое': [],
  },
  'Десерты': {
    'напитки': lemonades,
    'к кофе': [],
    'сладкое': lemonades,
  }
};



const currentProducts = computed(() => {
  return categoryData[activeMainCategory.value]?.[activeSubCategory.value] || [];
});

const addToCart = (product: Product) => {
  const existingItem = cartItems.value.find(item => item.id === product.id);
  if (existingItem) {
    existingItem.quantity++;
  } else {
    cartItems.value.push({
      id: product.id,
      name: product.name,
      price: product.price,
      quantity: 1
    });
  }
};

const removeFromCart = (id: number) => {
  const index = cartItems.value.findIndex(item => item.id === id);
  if (index > -1) {
    if (cartItems.value[index].quantity > 1) {
      cartItems.value[index].quantity--;
    } else {
      cartItems.value.splice(index, 1);
    }
  }
};

const addItemToCart = (id: number) => {
  const item = cartItems.value.find(item => item.id === id);
  if (item) {
    item.quantity++;
  }
};

const cartCount = () => {
  return cartItems.value.reduce((sum, item) => sum + item.quantity, 0);
};

const handleCategorySelect = (categoryName: string) => {
  const mapping: Record<string, string> = {
    'Завтраки': 'напитков',
    'Комплексы': 'от кофе',
    'Закуски': 'сладкого',
    'Десерты': 'сладкого',
  };
  activeCategory.value = mapping[categoryName] || 'напитки';
};
</script>

<template>
  <div class="min-h-screen bg-white">
    <AppHeader :cart-count="cartCount()" @open-cart="cartOpen = true" />

    <HeroSection />

    <CategoryPills :categories="categories" @select-category="activeMainCategory = $event" />
    <FilterTabs :active-category="activeSubCategory" @update-category="activeSubCategory = $event" />
    <ProductGrid :title="activeSubCategory" :products="currentProducts" @add-to-cart="addToCart" />

    <AppFooter @open-contact="contactOpen = true" />
    <ContactModal :is-open="contactOpen" @close="contactOpen = false" />

    <CartModal :is-open="cartOpen" :items="cartItems" @close="cartOpen = false" @add-item="addItemToCart"
      @remove-item="removeFromCart" />
  </div>
</template>
