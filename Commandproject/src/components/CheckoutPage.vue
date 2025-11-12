<template>
  <div class="page">
    <div class="container">
      <h1 class="page-title">Оформление заказа</h1>

      <form class="form" @submit.prevent="submitOrder">
        <div class="form-group">
          <label class="form-label">Имя</label>
          <input
            v-model="formData.name"
            type="text"
            class="form-input"
            placeholder="Иван"
          />
        </div>

        <div class="form-group">
          <label class="form-label">Телефон</label>
          <input
            v-model="formData.phone"
            type="tel"
            class="form-input"
            placeholder="+7 (999) 123-45-67"
          />
        </div>

        <div class="form-group">
          <label class="form-label">Адрес доставки</label>
          <textarea
            v-model="formData.address"
            class="form-textarea"
            rows="3"
            placeholder="г. Москва, ул. Ленина, д. 10, кв. 5"
          ></textarea>
        </div>

        <div class="form-actions">
          <button type="button" class="btn btn-outline" @click="$router.back()">
            Назад
          </button>
          <button type="submit" class="btn btn-primary">
            Подтвердить заказ
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const formData = ref({
  name: '',
  phone: '',
  address: '',
});

const submitOrder = () => {
  console.log('Заказ отправлен:', formData.value);
  alert('Заказ оформлен! Спасибо!');
  router.push('/');
};
</script>

<style scoped>
.page {
  min-height: 100vh;
  background-color: #f9fafb;
  padding-top: 32px;
  padding-bottom: 32px;
}

.container {
  width: 100%;
  max-width: 768px;
  margin-left: auto;
  margin-right: auto;
  padding-left: 16px;
  padding-right: 16px;
}

.page-title {
  font-size: 24px;
  font-weight: bold;
  margin-bottom: 24px;
  color: #1f2937;
}

.form {
  background: white;
  border-radius: 16px;
  padding: 24px;
  box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
  border: 1px solid #e5e7eb;
}

.form-group {
  margin-bottom: 16px;
}

.form-label {
  display: block;
  font-size: 14px;
  color: #374151;
  margin-bottom: 8px;
  font-weight: 500;
}

.form-input,
.form-textarea {
  width: 100%;
  padding: 12px 16px;
  border: 1px solid #d1d5db;
  border-radius: 8px;
  font-size: 16px;
  color: #111827;
  outline: none;
  transition: border-color 0.2s, box-shadow 0.2s;
}

.form-input:focus,
.form-textarea:focus {
  border-color: #f97316;
  box-shadow: 0 0 0 3px rgba(249, 115, 22, 0.2);
}

.form-textarea {
  resize: vertical;
  min-height: 80px;
}

.form-actions {
  display: flex;
  gap: 12px;
  margin-top: 24px;
}

.btn {
  padding: 12px 24px;
  font-size: 16px;
  font-weight: 600;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.2s, color 0.2s;
}

.btn-outline {
  border: 1px solid #d1d5db;
  background: white;
  color: #374151;
}

.btn-outline:hover {
  background: #f3f4f6;
}

.btn-primary {
  border: none;
  background: #f97316;
  color: white;
  flex: 1;
}

.btn-primary:hover {
  background: #ea580c;
}
</style>