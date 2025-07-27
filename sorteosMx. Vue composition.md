# SorteosMax - Vue 3 + Vite Project

## 📁 Estructura del Proyecto

```
sorteos-max/
├── package.json
├── vite.config.js
├── index.html
├── src/
│   ├── main.js
│   ├── App.vue
│   ├── assets/
│   │   └── style.css
│   ├── components/
│   │   ├── AppHeader.vue
│   │   ├── WinnersMarquee.vue
│   │   ├── SearchSection.vue
│   │   ├── SorteoCard.vue
│   │   ├── WinnersSidebar.vue
│   │   └── LoginModal.vue
│   └── composables/
│       └── useSorteos.js
```

## 📦 package.json

```json
{
  "name": "sorteos-max",
  "private": true,
  "version": "0.0.0",
  "type": "module",
  "scripts": {
    "dev": "vite",
    "build": "vite build",
    "preview": "vite preview"
  },
  "dependencies": {
    "vue": "^3.4.0"
  },
  "devDependencies": {
    "@vitejs/plugin-vue": "^5.0.0",
    "vite": "^5.0.0"
  }
}
```

## ⚡ vite.config.js

```js
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  plugins: [vue()],
  server: {
    port: 3000,
    open: true
  }
})
```

## 🌐 index.html

```html
<!DOCTYPE html>
<html lang="es">
  <head>
    <meta charset="UTF-8" />
    <link rel="icon" type="image/svg+xml" href="/trophy.svg" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>SorteosMax - Tu Oportunidad de Ganar</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
  </head>
  <body>
    <div id="app"></div>
    <script type="module" src="/src/main.js"></script>
  </body>
</html>
```

## 🎯 src/main.js

```js
import { createApp } from 'vue'
import './assets/style.css'
import App from './App.vue'

createApp(App).mount('#app')
```

## 🎨 src/assets/style.css

```css
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: 'Inter', -apple-system, BlinkMacSystemFont, sans-serif;
  background: linear-gradient(135deg, #f5f7fa 0%, #c3cfe2 100%);
  min-height: 100vh;
  line-height: 1.6;
  margin: 0;
  padding-top: 140px;
}

/* Header Styles */
.header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  position: fixed;
  top: 0;
  width: 100%;
  z-index: 100;
  transition: all 0.3s ease;
  box-shadow: 0 4px 20px rgba(0,0,0,0.1);
}

.header-main {
  padding: 1.5rem 0;
  transition: all 0.3s ease;
}

.header.scrolled .header-main {
  padding: 0.75rem 0;
}

.header.scrolled {
  backdrop-filter: blur(10px);
  background: linear-gradient(135deg, rgba(102, 126, 234, 0.95) 0%, rgba(118, 75, 162, 0.95) 100%);
}

.header-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 1rem;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
  transition: all 0.3s ease;
}

.header.scrolled .header-container {
  justify-content: flex-start;
}

.logo {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  font-size: 1.8rem;
  font-weight: bold;
  transition: all 0.3s ease;
}

.header.scrolled .logo {
  font-size: 1.3rem;
}

.logo i {
  font-size: 2.2rem;
  color: #ffd700;
  transition: all 0.3s ease;
}

.header.scrolled .logo i {
  font-size: 1.6rem;
}

.nav {
  display: flex;
  gap: 2rem;
  list-style: none;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  transition: all 0.3s ease;
}

.header.scrolled .nav {
  left: auto;
  transform: none;
  margin-left: 3rem;
}

.nav a {
  color: white;
  text-decoration: none;
  transition: color 0.3s ease;
}

.nav a:hover {
  color: #ffd700;
}

.login-btn {
  background: #ffd700;
  color: #764ba2;
  padding: 0.75rem 1.5rem;
  border-radius: 0.5rem;
  text-decoration: none;
  font-weight: bold;
  transition: all 0.3s ease;
  position: absolute;
  right: 0;
  border: none;
  cursor: pointer;
}

.header.scrolled .login-btn {
  padding: 0.5rem 1rem;
  font-size: 0.9rem;
}

.login-btn:hover {
  background: #ffed4e;
  transform: translateY(-2px);
}

/* Marquee Styles */
.winners-marquee {
  background: linear-gradient(135deg, #ffd700 0%, #ffb347 100%);
  color: #2d3748;
  padding: 0.4rem 0;
  overflow: hidden;
  white-space: nowrap;
  border-top: 1px solid rgba(255, 255, 255, 0.2);
}

.marquee-content {
  display: inline-block;
  animation: marqueeWithPause 35s linear infinite;
  font-size: 0.85rem;
  line-height: 1.2;
}

.winner-item {
  display: inline;
  margin-right: 3rem;
}

.winner-medal {
  font-size: 0.9rem;
  margin-right: 0.4rem;
}

.winner-name {
  font-weight: 600;
  color: #1a202c;
}

.winner-prize {
  font-weight: 400;
  color: #2d3748;
  margin-left: 0.4rem;
}

.winner-separator {
  margin: 0 1.2rem;
  color: #4a5568;
  font-size: 1rem;
  font-weight: bold;
}

@keyframes marqueeWithPause {
  0% {
    transform: translateX(100%);
  }
  70% {
    transform: translateX(-100%);
  }
  70.1%, 100% {
    transform: translateX(-100%);
  }
}

/* Main Content */
.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem 1rem;
}

.main-content {
  display: grid;
  grid-template-columns: 2fr 1fr;
  gap: 2rem;
}

.section-title {
  font-size: 2rem;
  font-weight: bold;
  color: #2d3748;
  margin-bottom: 0.5rem;
}

.section-subtitle {
  color: #718096;
  margin-bottom: 2rem;
}

/* Search Section */
.search-section {
  background: white;
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 10px 30px rgba(0,0,0,0.1);
  margin-bottom: 2rem;
}

.search-title {
  font-size: 1.25rem;
  font-weight: 600;
  color: #2d3748;
  margin-bottom: 1rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.search-form {
  display: flex;
  gap: 1rem;
}

.search-input {
  flex: 1;
  padding: 0.75rem 1rem;
  border: 2px solid #e2e8f0;
  border-radius: 0.5rem;
  font-size: 1rem;
  transition: border-color 0.3s ease;
}

.search-input:focus {
  outline: none;
  border-color: #3182ce;
}

.search-btn {
  background: #3182ce;
  color: white;
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 0.5rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.3s ease;
}

.search-btn:hover {
  background: #2c5aa0;
}

/* Sorteos Grid */
.sorteos-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 1.5rem;
}

/* Sorteo Card */
.sorteo-card {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 50%, #f093fb 100%);
  border-radius: 1rem;
  overflow: hidden;
  box-shadow: 0 10px 30px rgba(0,0,0,0.1);
  transition: all 0.3s ease;
  color: white;
  position: relative;
}

.sorteo-card::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: linear-gradient(135deg, #ff6b6b 0%, #ee5a24 50%, #ff9ff3 100%);
  opacity: 0;
  transition: opacity 0.3s ease;
}

.sorteo-card:nth-child(2n)::before {
  background: linear-gradient(135deg, #a55eea 0%, #26de81 50%, #feca57 100%);
}

.sorteo-card:nth-child(3n)::before {
  background: linear-gradient(135deg, #2d3436 0%, #636e72 50%, #74b9ff 100%);
}

.sorteo-card:nth-child(4n)::before {
  background: linear-gradient(135deg, #00b894 0%, #00cec9 50%, #fd79a8 100%);
}

.sorteo-card:hover::before {
  opacity: 1;
}

.sorteo-card > * {
  position: relative;
  z-index: 2;
}

.sorteo-card:hover {
  transform: translateY(-8px);
  box-shadow: 0 25px 50px rgba(0,0,0,0.2);
}

.sorteo-header {
  padding: 1.5rem;
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
}

.sorteo-info h3 {
  font-size: 1.25rem;
  font-weight: bold;
  margin-bottom: 0.5rem;
}

.sorteo-info p {
  color: rgba(255,255,255,0.9);
  font-size: 0.875rem;
}

.opportunity-badge {
  background: linear-gradient(135deg, #00b894 0%, #00a085 100%);
  padding: 0.5rem 1rem;
  border-radius: 2rem;
  font-size: 0.75rem;
  font-weight: bold;
  white-space: nowrap;
  animation: pulse 2s infinite;
}

@keyframes pulse {
  0%, 100% { opacity: 1; }
  50% { opacity: 0.8; }
}

.sorteo-image {
  padding: 0 1.5rem;
}

.sorteo-image img {
  width: 100%;
  height: 10rem;
  object-fit: cover;
  border-radius: 0.5rem;
}

.sorteo-details {
  padding: 1.5rem;
}

.detail-row {
  display: flex;
  justify-content: space-between;
  margin-bottom: 0.75rem;
}

.detail-label {
  color: rgba(255,255,255,0.9);
}

.detail-value {
  font-weight: 600;
}

.progress-bar {
  width: 100%;
  height: 0.5rem;
  background: rgba(255,255,255,0.3);
  border-radius: 0.25rem;
  margin: 1rem 0;
  overflow: hidden;
}

.progress-fill {
  height: 100%;
  background: #ffd700;
  border-radius: 0.25rem;
  transition: width 0.3s ease;
}

.buy-btn {
  width: 100%;
  background: #ffd700;
  color: #0984e3;
  padding: 0.75rem;
  border: none;
  border-radius: 0.5rem;
  font-weight: bold;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.buy-btn:hover {
  background: #ffed4e;
  transform: translateY(-2px);
}

.buy-btn:disabled {
  background: #cbd5e0;
  cursor: not-allowed;
}

/* Sidebar */
.sidebar {
  position: sticky;
  top: 2rem;
}

.winners-section {
  background: white;
  border-radius: 1rem;
  padding: 1.5rem;
  box-shadow: 0 10px 30px rgba(0,0,0,0.1);
}

.winners-title {
  font-size: 1.25rem;
  font-weight: bold;
  color: #2d3748;
  text-align: center;
  margin-bottom: 1.5rem;
}

.winners-list {
  max-height: 24rem;
  overflow-y: auto;
}

.winner-card {
  background: linear-gradient(135deg, #ffeaa7 0%, #fab1a0 100%);
  border-radius: 0.75rem;
  padding: 1rem;
  margin-bottom: 1rem;
  transition: transform 0.3s ease;
}

.winner-card:hover {
  transform: translateX(5px);
}

.winner-content {
  display: flex;
  align-items: center;
  gap: 0.75rem;
}

.winner-avatar {
  width: 3rem;
  height: 3rem;
  border-radius: 50%;
  object-fit: cover;
  border: 2px solid white;
  box-shadow: 0 2px 10px rgba(0,0,0,0.1);
}

.winner-info h4 {
  font-weight: bold;
  color: #2d3748;
  margin-bottom: 0.25rem;
}

.winner-prize {
  font-size: 0.875rem;
  color: #4a5568;
  margin-bottom: 0.25rem;
}

.winner-date {
  font-size: 0.75rem;
  color: #718096;
}

.winner-medal {
  font-size: 1.25rem;
  color: #d4af37;
  margin-left: auto;
}

.view-all-btn {
  width: 100%;
  background: linear-gradient(135deg, #ffd700 0%, #ff8c00 100%);
  color: white;
  padding: 0.75rem;
  border: none;
  border-radius: 0.5rem;
  font-weight: bold;
  cursor: pointer;
  margin-top: 1rem;
  transition: all 0.3s ease;
}

.view-all-btn:hover {
  transform: translateY(-2px);
  box-shadow: 0 5px 15px rgba(255,140,0,0.3);
}

.cta-text {
  text-align: center;
  font-size: 0.875rem;
  color: #718096;
  margin-bottom: 0.75rem;
}

/* Modal */
.modal-overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.modal-content {
  background: white;
  border-radius: 1rem;
  padding: 2rem;
  max-width: 28rem;
  width: 100%;
  margin: 1rem;
  position: relative;
}

.modal-title {
  font-size: 1.5rem;
  font-weight: bold;
  text-align: center;
  margin-bottom: 1.5rem;
}

.form-group {
  margin-bottom: 1rem;
}

.form-label {
  display: block;
  color: #4a5568;
  font-weight: 600;
  margin-bottom: 0.5rem;
}

.form-input {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 2px solid #e2e8f0;
  border-radius: 0.5rem;
  font-size: 1rem;
  transition: border-color 0.3s ease;
}

.form-input:focus {
  outline: none;
  border-color: #3182ce;
}

.btn-primary {
  width: 100%;
  background: #3182ce;
  color: white;
  font-weight: bold;
  padding: 0.75rem;
  border: none;
  border-radius: 0.5rem;
  cursor: pointer;
  transition: background 0.3s ease;
}

.btn-primary:hover {
  background: #2c5aa0;
}

.close-btn {
  position: absolute;
  top: 1rem;
  right: 1rem;
  background: none;
  border: none;
  color: #718096;
  font-size: 1.25rem;
  cursor: pointer;
  transition: color 0.3s ease;
}

.close-btn:hover {
  color: #4a5568;
}

/* Responsive */
@media (max-width: 768px) {
  .main-content {
    grid-template-columns: 1fr;
  }

  .header-container {
    flex-direction: column;
    gap: 1rem;
    justify-content: center !important;
  }

  .header.scrolled .header-container {
    flex-direction: row;
    gap: 0;
  }

  .nav {
    position: static;
    transform: none;
    margin-left: 0;
  }

  .header.scrolled .nav {
    display: none;
  }

  .login-btn {
    position: static;
  }

  .search-form {
    flex-direction: column;
  }

  .sorteos-grid {
    grid-template-columns: 1fr;
  }

  .sorteo-header {
    flex-direction: column;
    gap: 1rem;
  }

  .opportunity-badge {
    align-self: flex-start;
  }

  .marquee-content {
    font-size: 0.8rem;
  }

  .header-main {
    padding: 1rem 0;
  }

  .header.scrolled .header-main {
    padding: 0.6rem 0;
  }

  body {
    padding-top: 130px;
  }
}

/* Custom Scrollbar */
.winners-list::-webkit-scrollbar {
  width: 6px;
}

.winners-list::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 3px;
}

.winners-list::-webkit-scrollbar-thumb {
  background: #c1c1c1;
  border-radius: 3px;
}

.winners-list::-webkit-scrollbar-thumb:hover {
  background: #a8a8a8;
}
```

## 🏠 src/App.vue

```vue
<template>
  <div id="app">
    <AppHeader 
      :is-logged-in="isLoggedIn"
      :user="user"
      @show-login="showLogin = true"
      @logout="logout"
    />

    <div class="container">
      <div class="main-content">
        <main>
          <div>
            <h1 class="section-title">🎯 Sorteos Activos</h1>
            <p class="section-subtitle">¡Participa ahora y multiplica tus oportunidades de ganar!</p>
          </div>

          <SearchSection 
            v-model="searchQuery"
            :search-results="searchResults"
            @search="searchTickets"
          />

          <div class="sorteos-grid">
            <SorteoCard
              v-for="sorteo in sorteos"
              :key="sorteo.id"
              :sorteo="sorteo"
              :is-logged-in="isLoggedIn"
              @comprar="comprarBoleto"
            />
          </div>
        </main>

        <WinnersSidebar :ganadores="ganadores" />
      </div>
    </div>

    <LoginModal
      v-if="showLogin"
      v-model:celular="loginForm.celular"
      v-model:codigo="loginForm.codigo"
      @login="login"
      @close="showLogin = false"
      @enviar-codigo="enviarCodigo"
    />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import AppHeader from './components/AppHeader.vue'
import SearchSection from './components/SearchSection.vue'
import SorteoCard from './components/SorteoCard.vue'
import WinnersSidebar from './components/WinnersSidebar.vue'
import LoginModal from './components/LoginModal.vue'
import { useSorteos } from './composables/useSorteos'

// State
const isLoggedIn = ref(false)
const showLogin = ref(false)
const user = ref({})
const searchQuery = ref('')
const searchResults = ref([])
const loginForm = ref({
  celular: '',
  codigo: ''
})

// Composables
const { sorteos, ganadores } = useSorteos()

// Methods
const login = () => {
  isLoggedIn.value = true
  user.value = { nombre: 'Juan Pérez', celular: loginForm.value.celular }
  showLogin.value = false
  loginForm.value = { celular: '', codigo: '' }
}

const logout = () => {
  isLoggedIn.value = false
  user.value = {}
}

const enviarCodigo = () => {
  alert('Código enviado por SMS')
}

const comprarBoleto = (sorteo) => {
  if (!isLoggedIn.value) {
    showLogin.value = true
    return
  }
  alert(`¡Compra confirmada! Se te han asignado ${sorteo.multiplicador} oportunidades para ganar ${sorteo.nombre}.`)
}

const searchTickets = () => {
  if (searchQuery.value.trim()) {
    searchResults.value = [
      { id: 1, numero: '001234', sorteo: 'iPhone 15 Pro Max', estado: 'Activo' },
      { id: 2, numero: '001235', sorteo: 'iPhone 15 Pro Max', estado: 'Apartado' }
    ]
  } else {
    searchResults.value = []
  }
}
</script>
```

## 🧩 Componentes Vue

### src/components/AppHeader.vue

```vue
<template>
  <header class="header" :class="{ scrolled: isScrolled }" ref="headerRef">
    <div class="header-main">
      <div class="header-container">
        <div class="logo">
          <i class="fas fa-trophy"></i>
          <span>SorteosMax</span>
        </div>
        <nav>
          <ul class="nav">
            <li><a href="#sorteos">Sorteos</a></li>
            <li><a href="#boletos">Mis Boletos</a></li>
            <li><a href="#ganadores">Ganadores</a></li>
          </ul>
        </nav>
        <button 
          v-if="!isLoggedIn" 
          @click="$emit('show-login')"
          class="login-btn"
        >
          <i class="fas fa-sign-in-alt"></i> Iniciar Sesión
        </button>
        <div v-else class="user-menu">
          <span class="user-name">¡Hola, {{ user.nombre }}!</span>
          <button @click="$emit('logout')" class="login-btn">
            <i class="fas fa-sign-out-alt"></i>
          </button>
        </div>
      </div>
    </div>
    
    <WinnersMarquee />
  </header>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import WinnersMarquee from './WinnersMarquee.vue'

defineProps({
  isLoggedIn: Boolean,
  user: Object
})

defineEmits(['show-login', 'logout'])

const isScrolled = ref(false)
const headerRef = ref(null)

const handleScroll = () => {
  isScrolled.value = window.scrollY > 50
}

onMounted(() => {
  window.addEventListener('scroll', handleScroll)
})

onUnmounted(() => {
  window.removeEventListener('scroll', handleScroll)
})
</script>
```

### src/components/WinnersMarquee.vue

```vue
<template>
  <div class="winners-marquee">
    <div class="marquee-content">
      <span 
        v-for="(winner, index) in winners" 
        :key="index"
        class="winner-item"
      >
        <span class="winner-medal">🏅</span>
        <span class="winner-name">{{ winner.name }}</span>
        <span class="winner-prize">{{ winner.prize }}</span>
      </span>
      <span 
        v-for="(separator, index) in separators" 
        :key="`sep-${index}`"
        class="winner-separator"
      >•</span>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'

const winners = [
  { name: 'Jack Sparrow', prize: 'iPhone 15 Pro Max' },
  { name: 'David Jones', prize: 'iPad Air' },
  { name: 'Elizabeth Swan', prize: 'MacBook Air M3' },
  { name: 'Will Turner', prize: 'PlayStation 5' },
  { name: 'Capitán Barbossa', prize: 'Apple Watch Series 9' },
  { name: 'Tia Dalma', prize: 'AirPods Pro' },
  { name: 'Bootstrap Bill', prize: 'Smart TV 65"' },
  { name: 'Calypso Marina', prize: 'Nintendo Switch OLED' }
]

const separators = computed(() => new Array(winners.length - 1).fill('•'))
</script>
```

### src/components/SorteoCard.vue

```vue
<template>
  <div class="sorteo-card">
    <div class="sorteo-header">
      <div class="sorteo-info">
        <h3>{{ sorteo.nombre }}</h3>
        <p>{{ sorteo.descripcion }}</p>
      </div>
      <div class="opportunity-badge">
        1 boleto = {{ sorteo.multiplicador }} oportunidades
      </div>
    </div>
    
    <div class="sorteo-image">
      <img :src="sorteo.imagen" :alt="sorteo.nombre">
    </div>

    <div class="sorteo-details">
      <div class="detail-row">
        <span class="detail-label">Precio del boleto:</span>
        <span class="detail-value">${{ sorteo.precio }}</span>
      </div>
      <div class="detail-row">
        <span class="detail-label">Boletos disponibles:</span>
        <span class="detail-value">{{ sorteo.disponibles }}/{{ sorteo.total }}</span>
      </div>
      <div class="detail-row">
        <span class="detail-label">Termina:</span>
        <span class="detail-value">{{ formatDate(sorteo.fecha_fin) }}</span>
      </div>

      <div class="progress-bar">
        <div 
          class="progress-fill" 
          :style="{ width: `${(sorteo.vendidos/sorteo.total)*100}%` }"
        ></div>
      </div>

      <button 
        @click="$emit('comprar', sorteo)"
        :disabled="!isLoggedIn || sorteo.disponibles === 0"
        class="buy-btn"
      >
        <i class="fas fa-ticket-alt"></i>
        {{ sorteo.disponibles === 0 ? 'Agotado' : 'Comprar Boleto' }}
      </button>
    </div>
  </div>
</template>

<script setup>
defineProps({
  sorteo: Object,
  isLoggedIn: Boolean
})

defineEmits(['comprar'])

const formatDate = (dateString) => {
  const date = new Date(dateString)
  return date.toLocaleDateString('es-ES', { 
    day: 'numeric', 
    month: 'short', 
    year: 'numeric' 
  })
}
</script>
```

### src/components/SearchSection.vue

```vue
<template>
  <section class="search-section">
    <h3 class="search-title">
      <i class="fas fa-search" style="color: #3182ce;"></i>
      Buscar mis Boletos
    </h3>
    <div class="search-form">
      <input 
        :value="modelValue"
        @input="$emit('update:modelValue', $event.target.value)"
        type="text" 
        placeholder="Número de boleto, celular o orden..."
        class="search-input"
      >
      <button @click="$emit('search')" class="search-btn">
        <i class="fas fa-search"></i> Buscar
      </button>
    </div>
    
    <div v-if="searchResults.length > 0" class="search-results">
      <div 
        v-for="ticket in searchResults" 
        :key="ticket.id" 
        class="search-result-item"
      >
        <div class="result-info">
          <span class="ticket-number">Boleto #{{ ticket.numero }}</span>
          <span class="ticket-sorteo">{{ ticket.sorteo }}</span>
        </div>
        <span :class="getStatusClass(ticket.estado)" class="ticket-status">
          {{ ticket.estado }}
        </span>
      </div>
    </div>
  </section>
</template>

<script setup>
defineProps({
  modelValue: String,
  searchResults: Array
})

defineEmits(['update:modelValue', 'search'])

const getStatusClass = (estado) => {
  const classes = {
    'Activo': 'status-active',
    'Apartado': 'status-reserved',
    'Ganador': 'status-winner',
    'Perdedor': 'status-loser',
    'Cancelado': 'status-cancelled'
  }
  return classes[estado] || 'status-default'
}
</script>

<style scoped>
.search-results {
  margin-top: 1rem;
}

.search-result-item {
  background: #f7fafc;
  padding: 1rem;
  border-radius: 0.5rem;
  margin-bottom: 0.5rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.result-info {
  display: flex;
  flex-direction: column;
}

.ticket-number {
  font-weight: 600;
  color: #2d3748;
}

.ticket-sorteo {
  font-size: 0.875rem;
  color: #718096;
}

.ticket-status {
  padding: 0.25rem 0.75rem;
  border-radius: 9999px;
  font-size: 0.75rem;
  font-weight: 600;
}

.status-active {
  background: #c6f6d5;
  color: #22543d;
}

.status-reserved {
  background: #fefcbf;
  color: #744210;
}

.status-winner {
  background: #bee3f8;
  color: #1a365d;
}

.status-loser {
  background: #e2e8f0;
  color: #4a5568;
}

.status-cancelled {
  background: #fed7d7;
  color: #742a2a;
}
</style>
```

### src/components/WinnersSidebar.vue

```vue
<template>
  <aside class="sidebar">
    <div class="winners-section">
      <h3 class="winners-title">
        <i class="fas fa-crown" style="color: #ffd700;"></i> 🏆 Ganadores Recientes
      </h3>
      
      <div class="winners-list">
        <div 
          v-for="ganador in ganadores" 
          :key="ganador.id" 
          class="winner-card"
        >
          <div class="winner-content">
            <img 
              :src="ganador.foto" 
              :alt="ganador.nombre" 
              class="winner-avatar"
            >
            <div class="winner-info">
              <h4>{{ ganador.nombre }}</h4>
              <p class="winner-prize">{{ ganador.premio }}</p>
              <p class="winner-date">{{ formatDate(ganador.fecha) }}</p>
            </div>
            <i class="fas fa-medal winner-medal"></i>
          </div>
        </div>
      </div>

      <div class="cta-section">
        <p class="cta-text">¡Tú podrías ser el próximo!</p>
        <button class="view-all-btn">
          Ver todos los ganadores
        </button>
      </div>
    </div>
  </aside>
</template>

<script setup>
defineProps({
  ganadores: Array
})

const formatDate = (dateString) => {
  const date = new Date(dateString)
  return date.toLocaleDateString('es-ES', { 
    day: 'numeric', 
    month: 'short', 
    year: 'numeric' 
  })
}
</script>
```

### src/components/LoginModal.vue

```vue
<template>
  <div class="modal-overlay" @click="$emit('close')">
    <div class="modal-content" @click.stop>
      <button @click="$emit('close')" class="close-btn">
        <i class="fas fa-times"></i>
      </button>
      
      <h3 class="modal-title">Iniciar Sesión</h3>
      
      <form @submit.prevent="$emit('login')">
        <div class="form-group">
          <label class="form-label">Número de Celular</label>
          <input 
            :value="celular"
            @input="$emit('update:celular', $event.target.value)"
            type="tel" 
            placeholder="Ej: 555-123-4567"
            class="form-input"
            required
          >
        </div>
        
        <div class="form-group">
          <label class="form-label">Código de Verificación</label>
          <div class="code-input-group">
            <input 
              :value="codigo"
              @input="$emit('update:codigo', $event.target.value)"
              type="text" 
              placeholder="123456"
              class="form-input"
              maxlength="6"
              style="flex: 1; margin-right: 0.5rem;"
            >
            <button 
              type="button" 
              @click="$emit('enviar-codigo')" 
              class="send-code-btn"
            >
              Enviar
            </button>
          </div>
        </div>
        
        <button type="submit" class="btn-primary">
          Iniciar Sesión
        </button>
      </form>
    </div>
  </div>
</template>

<script setup>
defineProps({
  celular: String,
  codigo: String
})

defineEmits(['close', 'login', 'update:celular', 'update:codigo', 'enviar-codigo'])
</script>

<style scoped>
.code-input-group {
  display: flex;
  gap: 0.5rem;
}

.send-code-btn {
  background: #718096;
  color: white;
  padding: 0.75rem 1rem;
  border: none;
  border-radius: 0.5rem;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.3s ease;
  white-space: nowrap;
}

.send-code-btn:hover {
  background: #4a5568;
}
</style>
```

### src/composables/useSorteos.js

```js
import { ref } from 'vue'

export function useSorteos() {
  const sorteos = ref([
    {
      id: 1,
      nombre: 'iPhone 15 Pro Max',
      descripcion: 'Último modelo de Apple con 256GB',
      precio: 150,
      multiplicador: 60,
      total: 1000,
      vendidos: 750,
      disponibles: 250,
      fecha_fin: '2025-08-15',
      imagen: 'https://images.unsplash.com/photo-1592286062464-b91080b98d6f?w=400&h=300&fit=crop'
    },
    {
      id: 2,
      nombre: 'MacBook Air M3',
      descripcion: 'Nueva MacBook Air con chip M3',
      precio: 200,
      multiplicador: 50,
      total: 800,
      vendidos: 420,
      disponibles: 380,
      fecha_fin: '2025-08-20',
      imagen: 'https://images.unsplash.com/photo-1541807084-5c52b6b3adef?w=400&h=300&fit=crop'
    },
    {
      id: 3,
      nombre: 'PlayStation 5',
      descripcion: 'Consola PS5 con 2 controles',
      precio: 100,
      multiplicador: 80,
      total: 1200,
      vendidos: 950,
      disponibles: 250,
      fecha_fin: '2025-08-10',
      imagen: 'https://images.unsplash.com/photo-1606813907291-d86efa9b94db?w=400&h=300&fit=crop'
    },
    {
      id: 4,
      nombre: 'Smart TV 65"',
      descripcion: 'Samsung QLED 4K 65 pulgadas',
      precio: 120,
      multiplicador: 75,
      total: 900,
      vendidos: 600,
      disponibles: 300,
      fecha_fin: '2025-08-25',
      imagen: 'https://images.unsplash.com/photo-1593359677879-a4bb92f829d1?w=400&h=300&fit=crop'
    }
  ])

  const ganadores = ref([
    {
      id: 1,
      nombre: 'María González',
      premio: 'iPhone 14 Pro',
      fecha: '2025-07-20',
      foto: 'https://images.unsplash.com/photo-1494790108755-2616b612b830?w=100&h=100&fit=crop&crop=face'
    },
    {
      id: 2,
      nombre: 'Carlos Rodríguez',
      premio: 'MacBook Pro',
      fecha: '2025-07-18',
      foto: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=100&h=100&fit=crop&crop=face'
    },
    {
      id: 3,
      nombre: 'Ana López',
      premio: 'iPad Air',
      fecha: '2025-07-15',
      foto: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=100&h=100&fit=crop&crop=face'
    },
    {
      id: 4,
      nombre: 'Pedro Martínez',
      premio: 'AirPods Pro',
      fecha: '2025-07-12',
      foto: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=100&h=100&fit=crop&crop=face'
    },
    {
      id: 5,
      nombre: 'Laura Silva',
      premio: 'Apple Watch',
      fecha: '2025-07-10',
      foto: 'https://images.unsplash.com/photo-1544725176-7c40e5a71c5e?w=100&h=100&fit=crop&crop=face'
    }
  ])

  return {
    sorteos,
    ganadores
  }
}
```

## 🚀 Comandos para Inicializar el Proyecto

```bash
# 1. Crear directorio del proyecto
mkdir sorteos-max
cd sorteos-max

# 2. Inicializar package.json
npm init -y

# 3. Instalar dependencias
npm install vue@^3.4.0
npm install -D @vitejs/plugin-vue@^5.0.0 vite@^5.0.0

# 4. Crear estructura de archivos
mkdir src src/components src/composables src/assets

# 5. Copiar todos los archivos mostrados arriba

# 6. Ejecutar en modo desarrollo
npm run dev

# 7. Construir para producción
npm run build
```

## 📋 Características del Proyecto Vue

### ✨ **Arquitectura Moderna:**
- **Vue 3 Composition API** - Sintaxis moderna y reactiva
- **Vite** - Build tool ultrarrápido y HMR instantáneo
- **Componentes modulares** - Código organizado y reutilizable
- **Composables** - Lógica reutilizable (useSorteos)

### 🎯 **Funcionalidades Implementadas:**
- **Header dinámico** - Se transforma con scroll
- **Marquesina integrada** - Con pausa perfecta
- **Tarjetas de sorteos** - Gradientes únicos y hover effects
- **Sidebar de ganadores** - Responsive y elegante
- **Modal de login** - Funcional con v-model
- **Búsqueda de boletos** - Con estados visuales
- **Responsive design** - Perfecto en móviles

### 🛠️ **Mejores Prácticas:**
- **Props tipadas** - Mejor developer experience
- **Emits definidos** - Comunicación clara entre componentes
- **CSS scoped** - Estilos encapsulados donde es necesario
- **Composables** - Lógica separada y reutilizable
- **Single File Components** - Estructura organizada

¡El proyecto está completamente listo para desarrollo! ¿Te gustaría que ajuste algún componente específico o que agregue alguna funcionalidad adicional?