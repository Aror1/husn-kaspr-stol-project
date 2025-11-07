<template>
  <footer class="footer container">
    <div class="flex gap-4 justify-center">
      <button class="btn">
        Контактная информация
      </button>
      <button class="btn">
        ООО Комбо
      </button>
    </div>
  </footer>
</template>
<style>
    .container {
      max-width: 1200px;
      margin-left: auto;
      margin-right: auto;
      padding-left: 1rem;
      padding-right: 1rem;
    }

    .footer {
      padding-top: 2rem;
      padding-bottom: 2rem;
    }.btn {
      display: inline-flex;
      align-items: center;
      justify-content: center;
      padding: 0.75rem 1.5rem;
      border: 2px solid #000;
      border-radius: 9999px;
      font-size: 1rem;
      font-weight: 500;
      color: #000; 
      background-color: transparent;
      cursor: pointer;
      transition: background-color 0.2s ease;
    }

    .btn:hover {
      background-color: #e5e7eb; 
    }

    .flex {
      display: flex;
    }

    .gap-4 {
      gap: 1rem;
    }

    .justify-center {
      justify-content: center;
    }
  </style>
