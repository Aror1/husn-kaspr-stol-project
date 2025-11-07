<script setup lang="ts">
import type { Product } from './types';
import ProductCard from './ProductCard.vue';

defineProps<{
  title: string;
  products: Product[];
}>();

const emit = defineEmits<{
  addToCart: [product: Product];
}>();
</script>

<template>
  <section class="container mx-auto px-4 py-8">
    <h2 class="text-2xl font-bold mb-6">{{ title }}</h2>
    <div class="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-4">
      <ProductCard
        v-for="product in products"
        :key="product.id"
        :product="product"
        @add-to-cart="emit('addToCart', $event)"
      />
    </div>
  </section>
</template>
