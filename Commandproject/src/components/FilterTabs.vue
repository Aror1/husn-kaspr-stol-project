<script setup lang="ts">
defineProps<{
  activeCategory: string;
}>();

const emit = defineEmits<{
  updateCategory: [category: string];
}>();
</script>

<template>
  <section class="filter-tabs-section">
    <div class="filter-tabs-container">
      <button
        class="filter-tab"
        :class="{ 'filter-tab--active': activeCategory === 'напитки' }"
        @click="emit('updateCategory', 'напитки')"
      >
        напитки
      </button>
      <button
        class="filter-tab"
        :class="{ 'filter-tab--active': activeCategory === 'к кофе' }"
        @click="emit('updateCategory', 'к кофе')"
      >
        к кофе
      </button>
      <button
        class="filter-tab"
        :class="{ 'filter-tab--active': activeCategory === 'сладкое' }"
        @click="emit('updateCategory', 'сладкое')"
      >
        сладкое
      </button>
    </div>
  </section>
</template>

<style scoped>
.filter-tabs-section {
  max-width: 1200px;
  margin-left: auto;
  margin-right: auto;
  padding-left: 16px;
  padding-right: 16px;
  padding-top: 16px;
  padding-bottom: 16px;
}

.filter-tabs-container {
  display: flex;
  gap: 24px;
  border-bottom: 1px solid #e5e7eb;
  padding-bottom: 4px;
}

.filter-tab {
  padding: 10px 5px;
  font-size: 16px;
  font-weight: 500;
  color: #6b7280;
  background: none;
  border: 1px solid transparent;
  border-radius: 10px;
  transition: all 0.2s ease;
}

.filter-tab:hover {
  color: #374151;
  border-color: #d1d5db;
}

.filter-tab--active {
  color: #111827;
  border-color: #111827;
  border-radius: 20px;
}
</style>