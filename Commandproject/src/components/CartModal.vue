<script setup lang="ts">
import type { CartItem } from './types';
import { computed } from 'vue';

const props = defineProps<{
  isOpen: boolean;
  items: CartItem[];
}>();

const emit = defineEmits<{
  close: [];
  addItem: [id: number];
  removeItem: [id: number];
}>();

const cartTotal = computed(() => {
  return props.items.reduce((sum, item) => sum + (item.price * item.quantity), 0);
});

const cartCount = computed(() => {
  return props.items.reduce((sum, item) => sum + item.quantity, 0);
});
</script>

<template>
  <Transition name="modal">
    <div v-if="isOpen" class="fixed inset-0 bg-black bg-opacity-50 z-50 flex items-center justify-center p-4" @click="emit('close')">
      <div class="bg-white rounded-2xl max-w-md w-full max-h-[90vh] overflow-hidden" @click.stop>
        <div class="p-6 border-b border-gray-200 flex items-center justify-between">
          <h2 class="text-xl font-bold">Корзина</h2>
          <button @click="emit('close')" class="p-2 hover:bg-gray-100 rounded-full">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>
        </div>

        <div class="p-6 overflow-y-auto max-h-[60vh]">
          <div v-if="items.length === 0" class="text-center py-8 text-gray-500">
            Корзина пуста
          </div>
          <div v-else class="space-y-4">
            <div v-for="item in items" :key="item.id" class="flex items-start gap-4 bg-gray-50 p-4 rounded-xl">
              <div class="w-16 h-16 bg-orange-200 rounded-lg flex-shrink-0" />
              <div class="flex-1">
                <h3 class="text-sm font-medium text-gray-900 mb-2">{{ item.name }}</h3>
                <div class="flex items-center justify-between">
                  <div class="flex items-center gap-2">
                    <button @click="emit('removeItem', item.id)" class="w-8 h-8 rounded-lg border border-gray-300 hover:bg-gray-100 flex items-center justify-center">
                      -
                    </button>
                    <span class="text-sm font-medium">{{ item.quantity }} шт</span>
                    <button @click="emit('addItem', item.id)" class="w-8 h-8 rounded-lg border border-gray-300 hover:bg-gray-100 flex items-center justify-center">
                      +
                    </button>
                  </div>
                  <span class="text-lg font-bold">{{ item.price * item.quantity }}₽</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div v-if="items.length > 0" class="p-6 border-t border-gray-200">
          <div class="flex items-center justify-between mb-4">
            <div class="space-y-1">
              <div class="text-sm text-gray-600">Всего: {{ cartCount }} товара</div>
              <div class="text-sm text-gray-600">Доставка: до заведения 267₽</div>
            </div>
            <div class="text-right">
              <div class="text-2xl font-bold">{{ cartTotal }}₽</div>
            </div>
          </div>
          <button class="w-full py-4 bg-orange-500 hover:bg-orange-600 text-white rounded-xl font-medium transition-colors">
            Перейти к оформлению
          </button>
        </div>
      </div>
    </div>
  </Transition>
</template>

<style scoped>
.modal-enter-active,
.modal-leave-active {
  transition: opacity 0.3s ease;
}

.modal-enter-from,
.modal-leave-to {
  opacity: 0;
}

.modal-enter-active .bg-white,
.modal-leave-active .bg-white {
  transition: transform 0.3s ease;
}

.modal-enter-from .bg-white,
.modal-leave-to .bg-white {
  transform: scale(0.9);
}
</style>
