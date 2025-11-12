<!-- src/components/ContactModal.vue -->
<script setup lang="ts">
defineProps<{
  isOpen: boolean;
}>();

const emit = defineEmits<{
  close: [];
}>();
</script>

<template>
  <Transition name="modal">
    <div v-if="isOpen" class="fixed inset-0 bg-black/30 backdrop-blur-sm z-50 flex items-center justify-center p-4" @click="emit('close')">
      <div class="bg-white rounded-2xl max-w-md w-full overflow-hidden" @click.stop>
        <div class="p-6 border-b border-gray-200 flex items-center justify-between">
          <h2 class="text-xl font-bold">Контактная информация</h2>
          <button @click="emit('close')" class="p-2 hover:bg-gray-100 rounded-full">
            <svg class="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>
        </div>
        <div class="p-6">
          <p class="mb-2"><strong>Телефон:</strong> +7 (XXX) XXX-XX-XX</p>
          <p class="mb-2"><strong>Email:</strong> info@normtakoi.ru</p>
          <p class="mb-2"><strong>Адрес:</strong> г. Москва, ул. Примерная, д. 10</p>
          <p class="mb-2"><strong>Режим работы:</strong> ежедневно с 10:00 до 22:00</p>
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
</style>