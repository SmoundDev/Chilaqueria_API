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
      <span class="progress-title">