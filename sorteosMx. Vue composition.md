# Componentes Vue - Detalle del Sorteo

## 📁 Estructura de Archivos Actualizada

```
src/
├── components/
│   ├── AppHeader.vue
│   ├── WinnersMarquee.vue
│   ├── SearchSection.vue
│   ├── SorteoCard.vue (MODIFICADO)
│   ├── WinnerSidebar.vue
│   ├── LoginModal.vue
│   └── sorteo-detail/
│       ├── SorteoDetail.vue (NUEVO)
│       ├── ImageGallery.vue (NUEVO)
│       ├── SorteoInfo.vue (NUEVO)
│       ├── ProgressSection.vue (NUEVO)
│       ├── CountdownTimer.vue (NUEVO)
│       └── TicketDisplay.vue (NUEVO)
├── composables/
│   ├── useSorteos.js (MODIFICADO)
│   └── useSorteoDetail.js (NUEVO)
├── router/
│   └── index.js (NUEVO)
├── views/
│   ├── Home.vue (NUEVO)
│   └── SorteoDetailView.vue (NUEVO)
└── main.js (MODIFICADO)
```

## 🛣️ router/index.js

```js
import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import SorteoDetailView from '../views/SorteoDetailView.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/sorteo/:id',
    name: 'SorteoDetail',
    component: SorteoDetailView,
    props: route => ({
      id: route.params.id,
      // Props precargados desde la navegación
      preloadData: route.query.preload ? JSON.parse(decodeURIComponent(route.query.preload)) : null
    })
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
```

## 🏠 main.js (Modificado)

```js
import { createApp } from 'vue'
import './assets/style.css'
import App from './App.vue'
import router from './router'

createApp(App).use(router).mount('#app')
```

## 📄 views/Home.vue

```vue
<template>
  <div>
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
              @ver-detalle="navigateToDetail"
            />
          </div>
        </main>

        <WinnerSidebar :ganadores="ganadores" />
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
import { useRouter } from 'vue-router'
import AppHeader from '../components/AppHeader.vue'
import SearchSection from '../components/SearchSection.vue'
import SorteoCard from '../components/SorteoCard.vue'
import WinnerSidebar from '../components/WinnerSidebar.vue'
import LoginModal from '../components/LoginModal.vue'
import { useSorteos } from '../composables/useSorteos'

const router = useRouter()

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
  // Navegar al detalle para completar la compra
  navigateToDetail(sorteo)
}

const navigateToDetail = (sorteo) => {
  // Preparar datos para precargar
  const preloadData = {
    nombre: sorteo.nombre,
    descripcion: sorteo.descripcion,
    precio: sorteo.precio,
    multiplicador: sorteo.multiplicador,
    imagen: sorteo.imagen
  }
  
  router.push({
    name: 'SorteoDetail',
    params: { id: sorteo.id },
    query: { 
      preload: encodeURIComponent(JSON.stringify(preloadData))
    }
  })
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

## 📱 views/SorteoDetailView.vue

```vue
<template>
  <div>
    <SorteoDetail 
      :sorteo-id="id"
      :preload-data="preloadData"
      :is-logged-in="isLoggedIn"
      @login-required="handleLoginRequired"
    />
    
    <LoginModal
      v-if="showLogin"
      v-model:celular="loginForm.celular"
      v-model:codigo="loginForm.codigo"
      @login="handleLogin"
      @close="showLogin = false"
      @enviar-codigo="enviarCodigo"
    />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import SorteoDetail from '../components/sorteo-detail/SorteoDetail.vue'
import LoginModal from '../components/LoginModal.vue'

const props = defineProps({
  id: String,
  preloadData: Object
})

// State
const isLoggedIn = ref(false) // En producción, esto vendría de un store global
const showLogin = ref(false)
const loginForm = ref({
  celular: '',
  codigo: ''
})

// Methods
const handleLoginRequired = () => {
  showLogin.value = true
}

const handleLogin = () => {
  isLoggedIn.value = true
  showLogin.value = false
  loginForm.value = { celular: '', codigo: '' }
}

const enviarCodigo = () => {
  alert('Código enviado por SMS')
}
</script>
```

## 🎯 components/sorteo-detail/SorteoDetail.vue

```vue
<template>
  <div class="sorteo-detail-page">
    <!-- Header -->
    <header class="header">
      <div class="header-container">
        <div class="logo">
          <i class="fas fa-trophy"></i>
          <span>SorteosMax</span>
        </div>
        <button @click="$router.push('/')" class="back-btn">
          <i class="fas fa-arrow-left"></i> Volver a Sorteos
        </button>
      </div>
    </header>

    <!-- Breadcrumb -->
    <div class="breadcrumb">
      <div class="breadcrumb-container">
        <nav class="breadcrumb-nav">
          <router-link to="/">Inicio</router-link>
          <i class="fas fa-chevron-right"></i>
          <router-link to="/">Sorteos</router-link>
          <i class="fas fa-chevron-right"></i>
          <span>{{ sorteo?.nombre || 'Cargando...' }}</span>
        </nav>
      </div>
    </div>

    <!-- Loading State -->
    <div v-if="loading && !preloadData" class="loading-container">
      <div class="loading-spinner">
        <i class="fas fa-spinner fa-spin"></i>
        <p>Cargando detalles del sorteo...</p>
      </div>
    </div>

    <!-- Main Content -->
    <div v-else class="container">
      <div class="detail-layout">
        <!-- Left Column - Images -->
        <ImageGallery 
          :images="sorteo?.imagenes || defaultImages"
          :alt="sorteo?.nombre || preloadData?.nombre"
        />

        <!-- Right Column - Details -->
        <div class="details-section">
          <SorteoInfo 
            :sorteo="sorteo"
            :preload-data="preloadData"
            :loading="loading"
          />

          <ProgressSection 
            :total="sorteo?.total || 1000"
            :vendidos="sorteo?.vendidos || 750"
            :disponibles="sorteo?.disponibles || 250"
            :loading="loading"
          />

          <CountdownTimer 
            :end-date="sorteo?.fecha_fin || '2025-08-15T23:59:59'"
          />

          <!-- Action Section -->
          <div class="action-section">
            <button 
              @click="handleParticipate"
              :disabled="!canParticipate"
              class="participate-btn"
              :class="{ 'loading': participating }"
            >
              <i v-if="!participating" class="fas fa-ticket-alt"></i>
              <i v-else class="fas fa-spinner fa-spin"></i>
              {{ getButtonText }}
            </button>

            <div class="security-badges">
              <div class="security-badge">
                <i class="fas fa-shield-alt"></i>
                <span>Pago Seguro</span>
              </div>
              <div class="security-badge">
                <i class="fas fa-certificate"></i>
                <span>100% Confiable</span>
              </div>
            </div>
          </div>

          <TicketDisplay 
            :ticket="userTicket"
            :sorteo-name="sorteo?.nombre || preloadData?.nombre"
            :opportunities="sorteo?.multiplicador || preloadData?.multiplicador"
          />
        </div>

        <!-- Description Section -->
        <div class="description-section">
          <h2 class="description-title">Descripción del Premio</h2>
          <div class="description-content">
            <div v-if="loading && !preloadData" class="loading-text">
              <div class="skeleton-line"></div>
              <div class="skeleton-line"></div>
              <div class="skeleton-line short"></div>
            </div>
            <div v-else>
              <p>{{ sorteo?.descripcion_completa || getDefaultDescription }}</p>
              
              <h3>Especificaciones incluidas:</h3>
              <ul class="features-list">
                <li v-for="spec in sorteo?.especificaciones || defaultSpecs" :key="spec">
                  {{ spec }}
                </li>
              </ul>

              <h3>Características destacadas:</h3>
              <ul class="features-list">
                <li v-for="feature in sorteo?.caracteristicas || defaultFeatures" :key="feature">
                  {{ feature }}
                </li>
              </ul>

              <p><strong>Nota importante:</strong> El sorteo se realizará de manera totalmente transparente y el ganador será contactado inmediatamente. La entrega se realiza sin costo adicional en toda la República Mexicana.</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import ImageGallery from './ImageGallery.vue'
import SorteoInfo from './SorteoInfo.vue'
import ProgressSection from './ProgressSection.vue'
import CountdownTimer from './CountdownTimer.vue'
import TicketDisplay from './TicketDisplay.vue'
import { useSorteoDetail } from '../../composables/useSorteoDetail'

const props = defineProps({
  sorteoId: String,
  preloadData: Object,
  isLoggedIn: Boolean
})

const emit = defineEmits(['login-required'])
const router = useRouter()

// Composable para manejar el detalle del sorteo
const {
  sorteo,
  loading,
  participating,
  userTicket,
  fetchSorteoDetail,
  participateInSorteo
} = useSorteoDetail(props.sorteoId)

// Computed properties
const canParticipate = computed(() => {
  return props.isLoggedIn && 
         !participating.value && 
         !userTicket.value && 
         (sorteo.value?.disponibles > 0)
})

const getButtonText = computed(() => {
  if (participating.value) return 'Procesando...'
  if (userTicket.value) return '¡Ya estás participando!'
  if (!props.isLoggedIn) return 'Inicia sesión para participar'
  if (sorteo.value?.disponibles === 0) return 'Agotado'
  return '¡Participar en el Sorteo!'
})

const getDefaultDescription = computed(() => {
  const nombre = sorteo.value?.nombre || props.preloadData?.nombre || 'este increíble premio'
  return `Participa por la oportunidad de ganar ${nombre}. Este sorteo representa una oportunidad única de obtener un premio de alta calidad de manera completamente transparente y confiable.`
})

// Default data for fallbacks
const defaultImages = [
  'https://images.unsplash.com/photo-1592286062464-b91080b98d6f?w=600&h=600&fit=crop',
  'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=600&h=600&fit=crop',
  'https://images.unsplash.com/photo-1556656793-08538906a9f8?w=600&h=600&fit=crop',
  'https://images.unsplash.com/photo-1549298916-b41d501d3772?w=600&h=600&fit=crop'
]

const defaultSpecs = [
  'Producto original con garantía oficial',
  'Caja sellada con todos los accesorios',
  'Documentación y herramientas incluidas',
  'Envío gratuito a toda la República Mexicana'
]

const defaultFeatures = [
  'Calidad premium garantizada',
  'Entrega verificada y segura',
  'Soporte técnico incluido',
  'Satisfacción 100% garantizada'
]

// Methods
const handleParticipate = async () => {
  if (!props.isLoggedIn) {
    emit('login-required')
    return
  }

  try {
    await participateInSorteo()
  } catch (error) {
    console.error('Error al participar:', error)
    // Aquí podrías mostrar una notificación de error
  }
}

// Fetch sorteo details on mount
fetchSorteoDetail()
</script>

<style scoped>
.sorteo-detail-page {
  min-height: 100vh;
  background: #f5f5f5;
  padding-top: 100px;
}

/* Header styles */
.header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  position: fixed;
  top: 0;
  width: 100%;
  z-index: 100;
  padding: 1rem 0;
  box-shadow: 0 2px 10px rgba(0,0,0,0.1);
}

.header-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 1rem;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.logo {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  font-size: 1.5rem;
  font-weight: bold;
}

.logo i {
  color: #ffd700;
  font-size: 1.8rem;
}

.back-btn {
  background: rgba(255,255,255,0.2);
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 0.5rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.back-btn:hover {
  background: rgba(255,255,255,0.3);
}

/* Breadcrumb styles */
.breadcrumb {
  background: white;
  padding: 1rem 0;
  border-bottom: 1px solid #e0e0e0;
}

.breadcrumb-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 1rem;
}

.breadcrumb-nav {
  display: flex;
  gap: 0.5rem;
  align-items: center;
  font-size: 0.875rem;
  color: #666;
}

.breadcrumb-nav a {
  color: #3182ce;
  text-decoration: none;
}

.breadcrumb-nav a:hover {
  text-decoration: underline;
}

/* Loading states */
.loading-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 400px;
}

.loading-spinner {
  text-align: center;
  color: #718096;
}

.loading-spinner i {
  font-size: 2rem;
  margin-bottom: 1rem;
}

.skeleton-line {
  height: 1rem;
  background: linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%);
  border-radius: 0.25rem;
  margin-bottom: 0.5rem;
  animation: skeleton-loading 1.5s infinite;
}

.skeleton-line.short {
  width: 60%;
}

@keyframes skeleton-loading {
  0% { background-position: -200px 0; }
  100% { background-position: calc(200px + 100%) 0; }
}

/* Main layout */
.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 2rem 1rem;
}

.detail-layout {
  display: grid;
  grid-template-columns: 1fr 400px;
  gap: 3rem;
  background: white;
  border-radius: 1rem;
  overflow: hidden;
  box-shadow: 0 4px 20px rgba(0,0,0,0.1);
}

.details-section {
  padding: 2rem;
  background: #fafafa;
  display: flex;
  flex-direction: column;
}

.action-section {
  margin-top: auto;
}

.participate-btn {
  width: 100%;
  background: linear-gradient(135deg, #ff6b6b 0%, #ee5a24 100%);
  color: white;
  padding: 1rem 2rem;
  border: none;
  border-radius: 1rem;
  font-size: 1.2rem;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s ease;
  margin-bottom: 1rem;
  box-shadow: 0 4px 15px rgba(255, 107, 107, 0.3);
}

.participate-btn:hover:not(:disabled) {
  transform: translateY(-2px);
  box-shadow: 0 8px 25px rgba(255, 107, 107, 0.4);
}

.participate-btn:disabled {
  background: #cbd5e0;
  cursor: not-allowed;
  transform: none;
  box-shadow: none;
}

.participate-btn.loading {
  background: #4299e1;
}

.security-badges {
  display: flex;
  gap: 1rem;
  margin-top: 1rem;
  flex-wrap: wrap;
}

.security-badge {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  background: #f7fafc;
  padding: 0.5rem 1rem;
  border-radius: 0.5rem;
  font-size: 0.875rem;
  color: #4a5568;
}

.security-badge i {
  color: #48bb78;
}

/* Description section */
.description-section {
  grid-column: 1 / -1;
  padding: 2rem;
  border-top: 1px solid #e2e8f0;
  background: white;
}

.description-title {
  font-size: 1.5rem;
  font-weight: bold;
  color: #2d3748;
  margin-bottom: 1rem;
}

.description-content {
  color: #4a5568;
  line-height: 1.8;
}

.description-content h3 {
  margin: 1.5rem 0 1rem 0;
  color: #2d3748;
}

.features-list {
  list-style: none;
  margin: 1rem 0;
}

.features-list li {
  padding: 0.5rem 0;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.features-list li::before {
  content: '✓';
  color: #48bb78;
  font-weight: bold;
  width: 20px;
}

/* Responsive */
@media (max-width: 768px) {
  .detail-layout {
    grid-template-columns: 1fr;
    gap: 0;
  }

  .details-section {
    background: white;
  }

  .sorteo-detail-page {
    padding-top: 80px;
  }
}
</style>
```

## 🖼️ components/sorteo-detail/ImageGallery.vue

```vue
<template>
  <div class="image-section">
    <div class="main-image-container">
      <img 
        :src="currentImage" 
        :alt="alt" 
        class="main-image"
        @error="handleImageError"
      >
      <div class="image-indicator">
        <i class="fas fa-images"></i> {{ currentIndex + 1 }} / {{ images.length }}
      </div>
    </div>

    <div class="thumbnail-gallery">
      <div 
        v-for="(image, index) in images" 
        :key="index"
        class="thumbnail"
        :class="{ active: index === currentIndex }"
        @click="selectImage(index)"
      >
        <img :src="image" :alt="`${alt} - vista ${index + 1}`">
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'

const props = defineProps({
  images: {
    type: Array,
    default: () => []
  },
  alt: String
})

const currentIndex = ref(0)
const currentImage = computed(() => props.images[currentIndex.value] || props.images[0])

const selectImage = (index) => {
  currentIndex.value = index
}

const handleImageError = (event) => {
  // Fallback to a default image if the current one fails to load
  event.target.src = 'https://via.placeholder.com/600x600?text=Imagen+no+disponible'
}
</script>

<style scoped>
.image-section {
  padding: 2rem;
}

.main-image-container {
  position: relative;
  margin-bottom: 1rem;
  border-radius: 1rem;
  overflow: hidden;
  aspect-ratio: 1;
  background: #f8f9fa;
}

.main-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
  transition: transform 0.3s ease;
}

.main-image:hover {
  transform: scale(1.05);
}

.image-indicator {
  position: absolute;
  bottom: 1rem;
  right: 1rem;
  background: rgba(0,0,0,0.7);
  color: white;
  padding: 0.5rem 1rem;
  border-radius: 2rem;
  font-size: 0.875rem;
}

.thumbnail-gallery {
  display: flex;
  gap: 0.5rem;
  overflow-x: auto;
  padding: 0.5rem 0;
}

.thumbnail {
  width: 80px;
  height: 80px;
  border-radius: 0.5rem;
  overflow: hidden;
  border: 2px solid transparent;
  cursor: pointer;
  transition: all 0.3s ease;
  flex-shrink: 0;
}

.thumbnail.active {
  border-color: #3182ce;
  transform: scale(1.05);
}

.thumbnail img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.thumbnail:hover {
  border-color: #90cdf4;
}
</style>
```

## ℹ️ components/sorteo-detail/SorteoInfo.vue

```vue
<template>
  <div>
    <div class="title-section">
      <h1 class="sorteo-title">
        {{ displayTitle }}
      </h1>
      <p class="sorteo-subtitle">
        {{ displaySubtitle }}
      </p>
    </div>

    <div class="price-section">
      <div class="price-main">
        <span v-if="loading && !preloadData" class="skeleton-text">$000</span>
        <span v-else>${{ displayPrice }}</span>
      </div>
      <div class="opportunity-info">
        <div class="opportunity-badge">
          🎯 1 boleto = {{ displayMultiplicador }} oportunidades de ganar
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'

const props = defineProps({
  sorteo: Object,
  preloadData: Object,
  loading: Boolean
})

const displayTitle = computed(() => {
  return props.sorteo?.nombre || props.preloadData?.nombre || 'Cargando sorteo...'
})

const displaySubtitle = computed(() => {
  return props.sorteo?.descripcion || props.preloadData?.descripcion || 'Obteniendo información...'
})

const displayPrice = computed(() => {
  return props.sorteo?.precio || props.preloadData?.precio || 0
})

const displayMultiplicador = computed(() => {
  return props.sorteo?.multiplicador || props.preloadData?.multiplicador || 1
})
</script>

<style scoped>
.title-section {
  margin-bottom: 1.5rem;
}

.sorteo-title {
  font-size: 1.8rem;
  font-weight: bold;
  color: #2d3748;
  margin-bottom: 0.5rem;
}

.sorteo-subtitle {
  color: #718096;
  font-size: 1rem;
}

.price-section {
  background: white;
  padding: 1.5rem;
  border-radius: 1rem;
  margin-bottom: 1.5rem;
  box-shadow: 0 2px 10px rgba(0,0,0,0.05);
}

.price-main {
  font-size: 2.5rem;
  font-weight: bold;
  color: #2d3748;
  margin-bottom: 0.5rem;
}

.opportunity-info {
  background: linear-gradient(135deg, #00b894 0%, #00a085 100%);
  color: white;
  padding: 1rem;
  border-radius: 0.75rem;
  text-align: center;
  font-weight: 600;
  margin-bottom: 1rem;
  animation: pulse 2s infinite;
}

@keyframes pulse {
  0%, 100% { transform: scale(1); }
  50% { transform: scale(1.02); }
}

.opportunity-badge {
  font-size: 1.1rem;
}

.skeleton-text {
  background: linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%);
  background-size: 200px 100%;
  background-repeat: no-repeat;
  border-radius: 4px;
  display: inline-block;
  line-height: 1;
  width: 100px;
  animation: skeleton-loading 1.5s infinite;
}

@keyframes skeleton-loading {
  0% { background-position: -200px 0; }
  100% { background-position: calc(200px + 100%) 0; }
}
</style>
```

## 📊 components/sorteo-detail/ProgressSection.vue

```vue
<template>
  <div class="progress-section">
    <div class="progress-header">
      <span class="progress-title">Progreso del Sorteo</span>
      <span class="progress-alert" :class="alertClass">{{ alertText }}</span>
    </div>
    <div class="progress-bar">
      <div 
        class="progress-fill" 
        :style="{ width: `${progressPercentage}%` }"
      ></div>
    </div>
    <div class="progress-stats">
      <span>{{ vendidos }} boletos vendidos</span>
      <span>{{ disponibles }} disponibles de {{ total }}</span>
    </div>
  </div>
</template>

<script setup>
import { computed } from 'vue'

const props = defineProps({
  total: {
    type: Number,
    default: 1000
  },
  vendidos: {
    type: Number,
    default: 0
  },
  disponibles: {
    type: Number,
    default: 1000
  },
  loading: Boolean
})

const progressPercentage = computed(() => {
  if (props.loading) return 0
  return (props.vendidos / props.total) * 100
})

const alertClass = computed(() => {
  const percentage = progressPercentage.value
  if (percentage >= 90) return 'critical'
  if (percentage >= 75) return 'warning'
  return 'normal'
})

const alertText = computed(() => {
  const percentage = progressPercentage.value
  if (percentage >= 95) return '¡Últimos boletos!'
  if (percentage >= 90) return '¡Quedan muy pocos!'
  if (percentage >= 75) return '¡Quedan pocos!'
  return 'Disponibles'
})
</script>

<style scoped>
.progress-section {
  background: white;
  padding: 1.5rem;
  border-radius: 1rem;
  margin-bottom: 1.5rem;
  box-shadow: 0 2px 10px rgba(0,0,0,0.05);
}

.progress-header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 1rem;
  align-items: center;
}

.progress-title {
  font-weight: 600;
  color: #2d3748;
}

.progress-alert {
  font-weight: 600;
  font-size: 0.875rem;
}

.progress-alert.normal {
  color: #48bb78;
}

.progress-alert.warning {
  color: #ed8936;
}

.progress-alert.critical {
  color: #e53e3e;
  animation: blink 1s infinite;
}

@keyframes blink {
  0%, 50% { opacity: 1; }
  51%, 100% { opacity: 0.5; }
}

.progress-bar {
  width: 100%;
  height: 1rem;
  background: #e2e8f0;
  border-radius: 0.5rem;
  overflow: hidden;
  margin-bottom: 0.5rem;
}

.progress-fill {
  height: 100%;
  background: linear-gradient(135deg, #ffd700 0%, #ff8c00 100%);
  border-radius: 0.5rem;
  transition: width 0.5s ease;
  position: relative;
}

.progress-fill::after {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: linear-gradient(90deg, transparent, rgba(255,255,255,0.3), transparent);
  animation: shimmer 2s infinite;
}

@keyframes shimmer {
  0% { transform: translateX(-100%); }
  100% { transform: translateX(100%); }
}

.progress-stats {
  display: flex;
  justify-content: space-between;
  font-size: 0.875rem;
  color: #718096;
}
</style>
```

## ⏰ components/sorteo-detail/CountdownTimer.vue

```vue
<template>
  <div class="countdown-section">
    <div class="countdown-title">
      <i class="fas fa-clock"></i> Tiempo restante para participar
    </div>
    <div class="countdown-display">
      <div class="countdown-item">
        <span class="countdown-number">{{ days }}</span>
        <span class="countdown-label">Días</span>
      </div>
      <div class="countdown-item">
        <span class="countdown-number">{{ hours }}</span>
        <span class="countdown-label">Horas</span>
      </div>
      <div class="countdown-item">
        <span class="countdown-number">{{ minutes }}</span>
        <span class="countdown-label">Min</span>
      </div>
      <div class="countdown-item">
        <span class="countdown-number">{{ seconds }}</span>
        <span class="countdown-label">Seg</span>
      </div>
    </div>
    <div v-if="timeRemaining <= 0" class="expired-message">
      ⏰ Este sorteo ha terminado
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'

const props = defineProps({
  endDate: {
    type: String,
    required: true
  }
})

const timeRemaining = ref(0)
let intervalId = null

const days = computed(() => Math.floor(timeRemaining.value / (1000 * 60 * 60 * 24)).toString().padStart(2, '0'))
const hours = computed(() => Math.floor((timeRemaining.value % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60)).toString().padStart(2, '0'))
const minutes = computed(() => Math.floor((timeRemaining.value % (1000 * 60 * 60)) / (1000 * 60)).toString().padStart(2, '0'))
const seconds = computed(() => Math.floor((timeRemaining.value % (1000 * 60)) / 1000).toString().padStart(2, '0'))

const updateCountdown = () => {
  const targetDate = new Date(props.endDate).getTime()
  const now = new Date().getTime()
  timeRemaining.value = Math.max(0, targetDate - now)
}

onMounted(() => {
  updateCountdown()
  intervalId = setInterval(updateCountdown, 1000)
})

onUnmounted(() => {
  if (intervalId) {
    clearInterval(intervalId)
  }
})
</script>

<style scoped>
.countdown-section {
  background: white;
  padding: 1.5rem;
  border-radius: 1rem;
  margin-bottom: 1.5rem;
  box-shadow: 0 2px 10px rgba(0,0,0,0.05);
  text-align: center;
}

.countdown-title {
  font-size: 1.1rem;
  font-weight: 600;
  color: #2d3748;
  margin-bottom: 1rem;
}

.countdown-display {
  display: flex;
  justify-content: center;
  gap: 1rem;
  margin-bottom: 1rem;
}

.countdown-item {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 1rem;
  border-radius: 0.75rem;
  min-width: 60px;
  text-align: center;
  transition: transform 0.3s ease;
}

.countdown-item:hover {
  transform: scale(1.05);
}

.countdown-number {
  font-size: 1.5rem;
  font-weight: bold;
  display: block;
}

.countdown-label {
  font-size: 0.75rem;
  opacity: 0.9;
}

.expired-message {
  color: #e53e3e;
  font-weight: 600;
  font-size: 1.1rem;
}

@media (max-width: 768px) {
  .countdown-display {
    gap: 0.5rem;
  }

  .countdown-item {
    padding: 0.75rem 0.5rem;
    min-width: 50px;
  }

  .countdown-number {
    font-size: 1.2rem;
  }
}
</style>
```

## 🎫 components/sorteo-detail/TicketDisplay.vue

```vue
<template>
  <div class="ticket-section" :class="{ 'has-ticket': !!ticket }">
    <div v-if="!ticket" class="ticket-placeholder">
      <i class="fas fa-ticket-alt ticket-icon"></i>
      <p>Tu boleto aparecerá aquí después de la compra</p>
    </div>
    
    <div v-else class="ticket-display">
      <div class="ticket-number">#{{ ticket.numero }}</div>
      <div class="ticket-info">Tu boleto para {{ sorteoName }}</div>
      <div class="ticket-opportunities">
        🎯 {{ opportunities }} oportunidades incluidas
      </div>
      <div class="ticket-date">
        Comprado el {{ formatDate(ticket.fecha) }}
      </div>
    </div>
    
    <p v-if="ticket" class="confirmation-message">
      <i class="fas fa-check-circle"></i> ¡Participación confirmada!
    </p>
  </div>
</template>

<script setup>
defineProps({
  ticket: Object,
  sorteoName: String,
  opportunities: Number
})

const formatDate = (dateString) => {
  if (!dateString) return ''
  const date = new Date(dateString)
  return date.toLocaleDateString('es-ES', {
    day: 'numeric',
    month: 'long',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  })
}
</script>

<style scoped>
.ticket-section {
  background: white;
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 2px 10px rgba(0,0,0,0.05);
  text-align: center;
  border: 2px dashed #e2e8f0;
  margin-top: 1rem;
  transition: all 0.3s ease;
}

.ticket-section.has-ticket {
  border-color: #48bb78;
  background: linear-gradient(135deg, #f0fff4 0%, #c6f6d5 100%);
  transform: scale(1.02);
}

.ticket-placeholder {
  color: #a0aec0;
}

.ticket-icon {
  font-size: 2rem;
  margin-bottom: 1rem;
  opacity: 0.3;
}

.ticket-placeholder p {
  font-style: italic;
  margin: 0;
}

.ticket-display {
  background: linear-gradient(135deg, #ffd700 0%, #ffb347 100%);
  color: #2d3748;
  padding: 1.5rem;
  border-radius: 1rem;
  box-shadow: 0 4px 15px rgba(255, 215, 0, 0.3);
  margin-bottom: 1rem;
  transform: rotateX(0deg);
  transition: transform 0.3s ease;
}

.ticket-display:hover {
  transform: rotateX(5deg) rotateY(5deg);
}

.ticket-number {
  font-size: 2rem;
  font-weight: bold;
  margin-bottom: 0.5rem;
  text-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.ticket-info {
  font-size: 0.875rem;
  opacity: 0.8;
  margin-bottom: 0.5rem;
}

.ticket-opportunities {
  background: rgba(255,255,255,0.2);
  padding: 0.5rem 1rem;
  border-radius: 0.5rem;
  margin: 1rem 0 0.5rem 0;
  font-weight: 600;
}

.ticket-date {
  font-size: 0.75rem;
  opacity: 0.7;
}

.confirmation-message {
  color: #48bb78;
  font-weight: 600;
  margin: 1rem 0 0 0;
  animation: fadeInUp 0.5s ease;
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}
</style>
```

## 🎯 composables/useSorteoDetail.js

```js
import { ref, reactive } from 'vue'

export function useSorteoDetail(sorteoId) {
  const sorteo = ref(null)
  const loading = ref(false)
  const participating = ref(false)
  const userTicket = ref(null)
  const error = ref(null)

  // Simular API call para obtener detalles del sorteo
  const fetchSorteoDetail = async () => {
    if (!sorteoId) return

    loading.value = true
    error.value = null

    try {
      // Simular delay de API
      await new Promise(resolve => setTimeout(resolve, 1500))
      
      // Datos simulados - en producción vendría de la API
      const sorteoData = {
        id: sorteoId,
        nombre: 'iPhone 15 Pro Max',
        descripcion: 'Último modelo de Apple con 256GB - Color Titanio Natural',
        descripcion_completa: 'Participa por la oportunidad de ganar el nuevo iPhone 15 Pro Max en su elegante color Titanio Natural con 256GB de almacenamiento. Este dispositivo representa lo último en tecnología móvil de Apple.',
        precio: 150,
        multiplicador: 60,
        total: 1000,
        vendidos: 750,
        disponibles: 250,
        fecha_fin: '2025-08-15T23:59:59',
        imagenes: [
          'https://images.unsplash.com/photo-1592286062464-b91080b98d6f?w=600&h=600&fit=crop',
          'https://images.unsplash.com/photo-1511707171634-5f897ff02aa9?w=600&h=600&fit=crop',
          'https://images.unsplash.com/photo-1556656793-08538906a9f8?w=600&h=600&fit=crop',
          'https://images.unsplash.com/photo-1549298916-b41d501d3772?w=600&h=600&fit=crop'
        ],
        especificaciones: [
          'iPhone 15 Pro Max 256GB - Titanio Natural',
          'Cable USB-C a USB-C trenzado (1 metro)',
          'Documentación y herramientas',
          'Caja original sellada',
          'Garantía oficial de Apple por 1 año'
        ],
        caracteristicas: [
          'Sistema de cámaras Pro con teleobjetivo 5x',
          'Chip A17 Pro con GPU de 6 núcleos',
          'Pantalla Super Retina XDR de 6.7 pulgadas',
          'Diseño resistente con Titanio grado 5',
          'Action Button personalizable',
          'USB-C con velocidades USB 3'
        ]
      }

      sorteo.value = sorteoData
    } catch (err) {
      error.value = 'Error al cargar los detalles del sorteo'
      console.error('Error fetching sorteo details:', err)
    } finally {
      loading.value = false
    }
  }

  // Simular participación en sorteo
  const participateInSorteo = async () => {
    if (participating.value || userTicket.value) return

    participating.value = true

    try {
      // Simular proceso de compra
      await new Promise(resolve => setTimeout(resolve, 2000))
      
      // Generar ticket simulado
      const ticket = {
        numero: Math.floor(Math.random() * 900000) + 100000,
        sorteo_id: sorteoId,
        fecha: new Date().toISOString(),
        estado: 'activo',
        oportunidades: sorteo.value?.multiplicador || 60
      }

      userTicket.value = ticket
      
      // Actualizar disponibilidad del sorteo
      if (sorteo.value) {
        sorteo.value.disponibles -= 1
        sorteo.value.vendidos += 1
      }

    } catch (err) {
      error.value = 'Error al procesar la participación'
      console.error('Error participating in sorteo:', err)
      throw err
    } finally {
      participating.value = false
    }
  }

  // Verificar si el usuario ya tiene un ticket
  const checkUserTicket = async () => {
    // En producción, verificar en la API si el usuario ya participó
    // Por ahora, no hacer nada
  }

  return {
    sorteo,
    loading,
    participating,
    userTicket,
    error,
    fetchSorteoDetail,
    participateInSorteo,
    checkUserTicket
  }
}
```

## 🔄 components/SorteoCard.vue (Modificado)

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
    
    <div class="sorteo-image" @click="$emit('ver-detalle', sorteo)">
      <img :src="sorteo.imagen" :alt="sorteo.nombre">
      <div class="image-overlay">
        <i class="fas fa-eye"></i>
        <span>Ver detalles</span>
      </div>
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

      <div class="card-actions">
        <button 
          @click="$emit('ver-detalle', sorteo)"
          class="detail-btn"
        >
          <i class="fas fa-info-circle"></i> Ver Detalles
        </button>
        
        <button 
          @click="$emit('comprar', sorteo)"
          :disabled="!isLoggedIn || sorteo.disponibles === 0"
          class="buy-btn"
        >
          <i class="fas fa-ticket-alt"></i>
          {{ sorteo.disponibles === 0 ? 'Agotado' : 'Participar' }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
defineProps({
  sorteo: Object,
  isLoggedIn: Boolean
})

defineEmits(['comprar', 'ver-detalle'])

const formatDate = (dateString) => {
  const date = new Date(dateString)
  return date.toLocaleDateString('es-ES', { 
    day: 'numeric', 
    month: 'short', 
    year: 'numeric' 
  })
}
</script>

<style scoped>
/* Estilos existentes más estos nuevos */
.sorteo-image {
  position: relative;
  cursor: pointer;
  overflow: hidden;
}

.image-overlay {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0,0,0,0.7);
  color: white;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  opacity: 0;
  transition: opacity 0.3s ease;
}

.sorteo-image:hover .image-overlay {
  opacity: 1;
}

.image-overlay i {
  font-size: 2rem;
  margin-bottom: 0.5rem;
}

.card-actions {
  display: flex;
  gap: 0.5rem;
}

.detail-btn {
  flex: 1;
  background: #4299e1;
  color: white;
  padding: 0.75rem;
  border: none;
  border-radius: 0.5rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
}

.detail-btn:hover {
  background: #3182ce;
  transform: translateY(-1px);
}

.buy-btn {
  flex: 2;
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

.buy-btn:hover:not(:disabled) {
  background: #ffed4e;
  transform: translateY(-2px);
}

.buy-btn:disabled {
  background: #cbd5e0;
  cursor: not-allowed;
}
</style>
```

## 📦 Dependencias Adicionales para package.json

```json
{
  "dependencies": {
    "vue": "^3.4.0",
    "vue-router": "^4.2.0"
  },
  "devDependencies": {
    "@vitejs/plugin-vue": "^5.0.0",
    "vite": "^5.0.0"
  }
}
```

## 🔄 Instrucciones de Instalación y Uso

```bash
# Instalar Vue Router
npm install vue-router@4

# Estructura de archivos a crear:
# 1. Crear carpeta src/router/
# 2. Crear carpeta src/views/
# 3. Crear carpeta src/components/sorteo-detail/
# 4. Copiar todos los componentes mostrados arriba
# 5. Modificar main.js para incluir el router

# Ejecutar
npm run dev
```

## 🎯 Funcionalidades Implementadas

### ✅ **Navegación Inteligente:**
- **Precarga de datos** - Nombre, descripción, precio e imagen se muestran inmediatamente
- **Consulta en tiempo real** - Los datos actualizados llegan desde la API
- **Fallbacks elegantes** - Skeletons y placeholders mientras carga

### ✅ **Componentes Modulares:**
- **SorteoDetail** - Componente principal que orquesta todo
- **ImageGallery** - Galería de imágenes interactiva
- **SorteoInfo** - Información básica con precarga
- **ProgressSection** - Barra de progreso en tiempo real
- **CountdownTimer** - Contador regresivo funcional
- **TicketDisplay** - Visualización del boleto generado

### ✅ **UX Optimizada:**
- **Estados de carga** - Indicators visuales para cada sección
- **Responsive design** - Perfecto en todos los dispositivos
- **Navegación fluida** - Router con parámetros y query strings
- **Validaciones** - Botones deshabilitados según estado

### ✅ **Integración Completa:**
- **Modificación de SorteoCard** - Botón "Ver Detalles" agregado
- **Router configurado** - Rutas parametrizadas
- **Composable de detalle** - Lógica reutilizable
- **Props y emits** - Comunicación entre componentes

¿Te gusta cómo quedó la integración? ¿Hay algún componente que quieras que ajuste o alguna funcionalidad adicional?