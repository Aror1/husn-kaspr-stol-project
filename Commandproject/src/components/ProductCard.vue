<script setup lang="ts">
import type { Product } from './types';

defineProps<{
  product: Product;
}>();

const emit = defineEmits<{
  addToCart: [product: Product];
}>();
</script>

<template>
  <div
    class="bg-white rounded-2xl border border-gray-200 overflow-hidden hover:shadow-lg transition-shadow cursor-pointer"
  >
    <div class="aspect-square bg-gray-100 relative overflow-hidden">
      <img :src="product.image" :alt="product.name" class="w-full h-full object-cover">
    </div>
    <div class="p-4">
      <h3 class="text-sm font-medium text-gray-900 mb-2 line-clamp-2">
        {{ product.name }}
      </h3>
      <div class="flex items-center justify-between">
        <span class="text-lg font-bold">{{ product.price }}₽</span>
        <button
          @click="emit('addToCart', product)"
          class="px-4 py-2 bg-orange-500 hover:bg-orange-600 text-white rounded-lg text-sm font-medium transition-colors"
        >
          +
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.line-clamp-2 {
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}
</style>
