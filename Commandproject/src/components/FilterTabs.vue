<script setup lang="ts">
defineProps<{
  activeCategory: string;
}>();

const emit = defineEmits<{
  updateCategory: [category: string];
}>();
</script>

<template>
  <section class="container mx-auto px-4 py-4">
    <div class="flex gap-6 border-b border-gray-200">
      <button
        :class="['pb-3 px-2 font-medium transition-colors',
                 activeCategory === 'напитков' ? 'text-gray-900 border-b-2 border-gray-900' : 'text-gray-500 hover:text-gray-700']"
        @click="emit('updateCategory', 'напитков')"
      >
        напитков
      </button>
      <button
        :class="['pb-3 px-2 font-medium transition-colors',
                 activeCategory === 'от кофе' ? 'text-gray-900 border-b-2 border-gray-900' : 'text-gray-500 hover:text-gray-700']"
        @click="emit('updateCategory', 'от кофе')"
      >
        от кофе
      </button>
      <button
        :class="['pb-3 px-2 font-medium transition-colors',
                 activeCategory === 'сладкого' ? 'text-gray-900 border-b-2 border-gray-900' : 'text-gray-500 hover:text-gray-700']"
        @click="emit('updateCategory', 'сладкого')"
      >
        сладкого
      </button>
    </div>
  </section>
</template>
