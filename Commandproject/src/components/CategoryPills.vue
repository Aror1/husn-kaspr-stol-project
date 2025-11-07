<!-- CategoryPills.vue -->
<script setup lang="ts">
import type { Category } from './types';
const emit = defineEmits<{ selectCategory: [name: string] }>();
defineProps<{ categories: Category[] }>();
</script>

<template>
  <section class="container mx-auto px-4 py-6">
    <div class="flex gap-3 overflow-x-auto pb-2">
      <button
        v-for="category in categories"
        :key="category.name"
        class="flex items-center gap-2 px-6 py-3 rounded-full border-2 border-orange-500 bg-orange-50 hover:bg-orange-100 transition-colors whitespace-nowrap"
        @click="emit('selectCategory', category.name)"
      >
        <span class="text-xl">{{ category.icon }}</span>
        <span class="font-medium text-gray-900">{{ category.label }}</span>
      </button>
    </div>
  </section>
</template>