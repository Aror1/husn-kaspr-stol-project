<script setup lang="ts">
import type { CartItem } from './types';
import { ref, computed } from 'vue';
import { useRouter } from 'vue-router';


const router = useRouter();

const goToCheckout = () => {
  emit('close');
  router.push('/checkout');
};
const props = defineProps<{
  isOpen: boolean;
  items: CartItem[];
}>();

const isCheckout = ref(false);
const emit = defineEmits<{
  close: [];
  addItem: [id: number];
  removeItem: [id: number];
}>();

const cartTotal = computed(() => {
  return props.items.reduce((sum, item) => sum + item.price * item.quantity, 0);
});

const cartCount = computed(() => {
  return props.items.reduce((sum, item) => sum + item.quantity, 0);
});
</script>

<template>
  <Transition name="modal">
    <div v-if="isOpen" class="overlay" @click="emit('close')">
      <div class="modal" @click.stop>
        <div class="modal-header">
          <h2 class="modal-title">Корзина</h2>
          <button class="close-btn" @click="emit('close')">×</button>
        </div>
        <div class="modal-body">
          <div v-if="items.length === 0" class="empty-text">Корзина пуста</div>
          <div v-else class="cart-items">
            <div v-for="item in items" :key="item.id" class="cart-item">
              <div class="item-thumb"></div>
              <div class="item-info">
                <h3 class="item-name">{{ item.name }}</h3>
                <div class="item-controls">
                  <button class="qty-btn" @click="emit('removeItem', item.id)">-</button>
                  <span class="qty-value">{{ item.quantity }} шт</span>
                  <button class="qty-btn" @click="emit('addItem', item.id)">+</button>
                  <span class="item-total">{{ item.price * item.quantity }}₽</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div v-if="isCheckout" class="p-6">
          <h3 class="text-lg font-bold mb-4">Оформление заказа</h3>
          <div class="space-y-3">
            <input type="text" placeholder="Имя" class="w-full px-3 py-2 border rounded-lg">
            <input type="tel" placeholder="Телефон" class="w-full px-3 py-2 border rounded-lg">
            <textarea placeholder="Адрес" class="w-full px-3 py-2 border rounded-lg" rows="2"></textarea>
          </div>
          <div class="mt-4 flex gap-2">
            <button class="flex-1 py-2 border rounded-lg" @click="isCheckout = false">Назад</button>
            <button class="flex-1 py-2 bg-orange-500 text-white rounded-lg" @click="submitOrder">Заказать</button>
          </div>
        </div>

        <div v-if="items.length > 0" class="modal-footer">
          <div class="totals">
            <div class="total-row">Всего: {{ cartCount }} товара</div>
            <div class="total-row">Доставка: до заведения 267₽</div>
          </div>
          <div class="total-price">{{ cartTotal }}₽</div>
          <button v-if="!isCheckout"
            class="w-full py-4 bg-orange-500 hover:bg-orange-600 text-white rounded-xl font-medium transition-colors"
            @click="isCheckout = true">
            Перейти к оформлению
          </button>
        </div>
      </div>
    </div>
  </Transition>
</template>

<style scoped>
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.4);
  backdrop-filter: blur(6px);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 50;
  padding: 16px;
}

/* === Модальное окно === */
.modal {
  background: white;
  border-radius: 16px;
  width: 100%;
  max-width: 400px;
  max-height: 90vh;
  overflow: hidden;
  box-shadow: 0 8px 32px rgba(0, 0, 0, 0.2);
}

/* === Шапка === */
.modal-header {
  padding: 24px;
  border-bottom: 1px solid #e5e7eb;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.modal-title {
  font-size: 20px;
  font-weight: bold;
  color: #1f2937;
}

.close-btn {
  width: 32px;
  height: 32px;
  border: none;
  background: none;
  font-size: 24px;
  color: #6b7280;
  cursor: pointer;
}

.close-btn:hover {
  color: #1f2937;
}

/* === Тело === */
.modal-body {
  padding: 24px;
  max-height: calc(90vh - 200px);
  overflow-y: auto;
}

.empty-text {
  text-align: center;
  color: #9ca3af;
  font-size: 16px;
  padding: 32px 0;
}

.cart-items {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.cart-item {
  display: flex;
  gap: 16px;
  background: #f9fafb;
  padding: 16px;
  border-radius: 12px;
}

.item-thumb {
  width: 64px;
  height: 64px;
  background: #fed7aa;
  border-radius: 8px;
  flex-shrink: 0;
}

.item-info {
  flex: 1;
}

.item-name {
  font-size: 14px;
  font-weight: 600;
  color: #1f2937;
  margin-bottom: 12px;
}

.item-controls {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
}

.qty-btn {
  width: 32px;
  height: 32px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  background: white;
  font-size: 16px;
  cursor: pointer;
}

.qty-btn:hover {
  background: #f3f4f6;
}

.qty-value {
  font-size: 14px;
  font-weight: 600;
  color: #374151;
}

.item-total {
  font-size: 18px;
  font-weight: bold;
  color: #1f2937;
  white-space: nowrap;
}

/* === Футер === */
.modal-footer {
  padding: 24px;
  border-top: 1px solid #e5e7eb;
}

.totals {
  display: flex;
  justify-content: space-between;
  margin-bottom: 16px;
  font-size: 14px;
  color: #4b5563;
}

.total-row {
  margin-bottom: 4px;
}

.total-price {
  font-size: 24px;
  font-weight: bold;
  color: #1f2937;
  text-align: right;
  margin-bottom: 16px;
}

.checkout-btn {
  width: 100%;
  padding: 16px;
  background: #f97316;
  color: white;
  border: none;
  border-radius: 12px;
  font-size: 16px;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s;
}

.checkout-btn:hover {
  background: #ea580c;
}

/* === Анимации === */
.modal-enter-active,
.modal-leave-active {
  transition: opacity 0.3s ease;
}

.modal-enter-from,
.modal-leave-to {
  opacity: 0;
}

.modal-enter-active .modal,
.modal-leave-active .modal {
  transition: transform 0.3s ease;
}

.modal-enter-from .modal,
.modal-leave-to .modal {
  transform: scale(0.9);
}
</style>