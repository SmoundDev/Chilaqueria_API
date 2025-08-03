<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sistema de Turnos</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: #000;
            overflow: hidden;
            height: 100vh;
        }

        .container {
            position: relative;
            width: 100%;
            height: 100vh;
            display: flex;
            flex-direction: column;
        }

        .main-area {
            flex: 1;
            display: grid;
            grid-template-columns: 1fr 2fr;
            position: relative;
            background: #000;
        }

        /* Sección del turno actual */
        .turno-actual-section {
            background: linear-gradient(135deg, rgba(102, 126, 234, 0.15) 0%, rgba(118, 75, 162, 0.15) 100%);
            backdrop-filter: blur(20px);
            border-right: 2px solid rgba(255,255,255,0.1);
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            padding: 40px;
            position: relative;
        }

        .turno-label {
            font-size: 2rem;
            color: rgba(255,255,255,0.8);
            margin-bottom: 20px;
            text-transform: uppercase;
            letter-spacing: 4px;
            font-weight: 300;
        }

        .turno-numero {
            font-size: 8rem;
            font-weight: 900;
            color: #fff;
            text-shadow: 0 0 50px rgba(102, 126, 234, 0.8);
            animation: pulse 2s infinite;
            margin-bottom: 30px;
        }

        .ventanilla {
            font-size: 2rem;
            color: rgba(255,255,255,0.9);
            font-weight: 500;
            padding: 15px 30px;
            background: rgba(255,255,255,0.1);
            backdrop-filter: blur(10px);
            border-radius: 15px;
            border: 1px solid rgba(255,255,255,0.2);
        }

        @keyframes pulse {
            0%, 100% { 
                transform: scale(1); 
                text-shadow: 0 0 50px rgba(102, 126, 234, 0.8);
            }
            50% { 
                transform: scale(1.02); 
                text-shadow: 0 0 80px rgba(102, 126, 234, 1);
            }
        }

        /* Sección de video/anuncios */
        .video-section {
            position: relative;
            overflow: hidden;
        }

        .video-background {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            object-fit: cover;
            z-index: 1;
        }

        .video-overlay {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: linear-gradient(45deg, rgba(0,0,0,0.2) 0%, rgba(0,0,0,0.1) 100%);
            z-index: 2;
        }

        /* Franja inferior */
        .bottom-strip {
            height: 120px;
            background: linear-gradient(90deg, rgba(255,255,255,0.15) 0%, rgba(255,255,255,0.1) 100%);
            backdrop-filter: blur(25px);
            border-top: 2px solid rgba(255,255,255,0.2);
            display: flex;
            align-items: center;
            padding: 0 40px;
            position: relative;
            z-index: 10;
        }

        .turnos-siguientes {
            display: flex;
            gap: 30px;
            flex: 1;
            justify-content: space-around;
        }

        .turno-card {
            flex: 1;
            max-width: 300px;
            background: rgba(255,255,255,0.1);
            backdrop-filter: blur(15px);
            border: 1px solid rgba(255,255,255,0.3);
            border-radius: 15px;
            padding: 20px 30px;
            text-align: center;
            transition: all 0.3s ease;
            position: relative;
            overflow: hidden;
        }

        .turno-card::before {
            content: '';
            position: absolute;
            top: 0;
            left: -100%;
            width: 100%;
            height: 100%;
            background: linear-gradient(90deg, transparent, rgba(255,255,255,0.2), transparent);
            transition: left 0.5s;
        }

        .turno-card:hover::before {
            left: 100%;
        }

        .turno-card:hover {
            transform: translateY(-3px);
            box-shadow: 0 15px 30px rgba(0,0,0,0.3);
            border-color: rgba(255,255,255,0.5);
        }

        .turno-card .card-label {
            font-size: 1rem;
            color: rgba(255,255,255,0.7);
            margin-bottom: 8px;
            text-transform: uppercase;
            letter-spacing: 1px;
            font-weight: 300;
        }

        .turno-card .numero {
            font-size: 2.5rem;
            font-weight: 700;
            color: #fff;
            text-shadow: 0 0 20px rgba(102, 126, 234, 0.6);
        }

        .turno-card .estado {
            font-size: 0.9rem;
            color: rgba(255,255,255,0.6);
            margin-top: 5px;
            font-style: italic;
        }

        /* Elementos flotantes */
        .status-indicator {
            position: absolute;
            top: 20px;
            right: 20px;
            display: flex;
            align-items: center;
            gap: 10px;
            background: rgba(0,255,0,0.15);
            padding: 12px 24px;
            border-radius: 25px;
            backdrop-filter: blur(15px);
            border: 1px solid rgba(0,255,0,0.3);
            z-index: 15;
        }

        .status-dot {
            width: 12px;
            height: 12px;
            background: #00ff00;
            border-radius: 50%;
            animation: blink 1.5s infinite;
            box-shadow: 0 0 15px #00ff00;
        }

        @keyframes blink {
            0%, 50% { opacity: 1; }
            51%, 100% { opacity: 0.3; }
        }

        .time-display {
            position: absolute;
            bottom: 140px;
            right: 20px;
            font-size: 1.4rem;
            color: rgba(255,255,255,0.9);
            background: rgba(0,0,0,0.6);
            padding: 15px 25px;
            border-radius: 15px;
            backdrop-filter: blur(15px);
            border: 1px solid rgba(255,255,255,0.2);
            z-index: 15;
            font-weight: 500;
        }

        .comercial-info {
            position: absolute;
            bottom: 140px;
            left: 40px;
            background: rgba(0,0,0,0.7);
            padding: 20px 30px;
            border-radius: 15px;
            backdrop-filter: blur(15px);
            max-width: 450px;
            border: 1px solid rgba(255,255,255,0.1);
            z-index: 15;
            color: rgba(255,255,255,0.9);
            line-height: 1.6;
        }

        .video-controls {
            position: absolute;
            top: 20px;
            left: 20px;
            display: flex;
            gap: 10px;
            z-index: 15;
        }

        .control-btn {
            background: rgba(255,255,255,0.15);
            border: 1px solid rgba(255,255,255,0.2);
            color: white;
            padding: 12px;
            border-radius: 50%;
            cursor: pointer;
            backdrop-filter: blur(15px);
            transition: all 0.3s ease;
            width: 45px;
            height: 45px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 1.2rem;
        }

        .control-btn:hover {
            background: rgba(255,255,255,0.25);
            transform: scale(1.1);
            box-shadow: 0 5px 15px rgba(0,0,0,0.3);
        }

        .notification {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background: linear-gradient(45deg, #667eea, #764ba2);
            color: white;
            padding: 30px 60px;
            border-radius: 20px;
            font-size: 2rem;
            font-weight: bold;
            z-index: 1000;
            box-shadow: 0 25px 50px rgba(0,0,0,0.4);
            border: 2px solid rgba(255,255,255,0.3);
            backdrop-filter: blur(20px);
        }

        /* Responsive */
        @media (max-width: 768px) {
            .main-area {
                grid-template-columns: 1fr;
                grid-template-rows: 2fr 1fr;
            }
            
            .turno-numero {
                font-size: 6rem;
            }
            
            .bottom-strip {
                height: 100px;
                padding: 0 20px;
            }
            
            .turnos-siguientes {
                gap: 15px;
            }
            
            .turno-card {
                padding: 15px 20px;
            }
            
            .turno-card .numero {
                font-size: 2rem;
            }
        }
    </style>
</head>
<body>
    <div class="container">
        <div class="main-area">
            <!-- Sección del turno actual (1/3 de pantalla) -->
            <div class="turno-actual-section">
                <div class="turno-label">Turno Actual</div>
                <div class="turno-numero" id="turnoActual">A-047</div>
                <div class="ventanilla">Ventanilla 3</div>
            </div>
            
            <!-- Sección de video/anuncios (2/3 de pantalla) -->
            <div class="video-section">
                <video class="video-background" autoplay muted loop id="backgroundVideo">
                    <source src="https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4" type="video/mp4">
                </video>
                <div class="video-overlay"></div>
                
                <div class="video-controls">
                    <button class="control-btn" onclick="toggleVideo()" title="Play/Pause">▶</button>
                    <button class="control-btn" onclick="changeVideo()" title="Cambiar Video">⟲</button>
                </div>
                
                <div class="comercial-info">
                    <strong>🎯 Ofertas Especiales</strong><br>
                    Consulta con nuestros asesores sobre los nuevos productos y servicios disponibles
                </div>
                
                <div class="time-display" id="timeDisplay">
                    00:00:00
                </div>
            </div>
        </div>
        
        <!-- Franja inferior con próximos turnos -->
        <div class="bottom-strip">
            <div class="turnos-siguientes">
                <div class="turno-card">
                    <div class="card-label">Siguiente</div>
                    <div class="numero" id="turnoSiguiente">A-048</div>
                    <div class="estado">Preparando...</div>
                </div>
                <div class="turno-card">
                    <div class="card-label">En Cola</div>
                    <div class="numero" id="turnoEnCola">A-049</div>
                    <div class="estado">Esperando</div>
                </div>
                <div class="turno-card">
                    <div class="card-label">Posterior</div>
                    <div class="numero" id="turnoPosterior">A-050</div>
                    <div class="estado">En fila</div>
                </div>
            </div>
        </div>
        
        <div class="status-indicator">
            <div class="status-dot"></div>
            <span>Sistema Activo</span>
        </div>
    </div>

    <script>
        // Lista de videos de demostración
        const videos = [
            'https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4',
            'https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4',
            'https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/Sintel.mp4'
        ];
        
        let currentVideoIndex = 0;
        let turnoCounter = 47;
        let isPlaying = true;
        
        // Actualizar reloj
        function updateTime() {
            const now = new Date();
            const timeString = now.toLocaleTimeString('es-ES', { 
                hour12: false,
                hour: '2-digit',
                minute: '2-digit',
                second: '2-digit'
            });
            document.getElementById('timeDisplay').textContent = timeString;
        }
        
        // Simular avance de turnos
        function avanzarTurno() {
            turnoCounter++;
            const turnoActual = document.getElementById('turnoActual');
            const turnoSiguiente = document.getElementById('turnoSiguiente');
            const turnoEnCola = document.getElementById('turnoEnCola');
            const turnoPosterior = document.getElementById('turnoPosterior');
            
            // Actualizar turnos con animación
            turnoActual.style.transform = 'scale(1.1)';
            turnoActual.style.transition = 'all 0.5s ease';
            
            setTimeout(() => {
                turnoActual.textContent = `A-${turnoCounter.toString().padStart(3, '0')}`;
                turnoSiguiente.textContent = `A-${(turnoCounter + 1).toString().padStart(3, '0')}`;
                turnoEnCola.textContent = `A-${(turnoCounter + 2).toString().padStart(3, '0')}`;
                turnoPosterior.textContent = `A-${(turnoCounter + 3).toString().padStart(3, '0')}`;
                
                turnoActual.style.transform = 'scale(1)';
            }, 300);
        }
        
        // Controles de video
        function toggleVideo() {
            const video = document.getElementById('backgroundVideo');
            const btn = document.querySelector('.control-btn');
            
            if (isPlaying) {
                video.pause();
                btn.textContent = '⏸';
                isPlaying = false;
            } else {
                video.play();
                btn.textContent = '▶';
                isPlaying = true;
            }
        }
        
        function changeVideo() {
            currentVideoIndex = (currentVideoIndex + 1) % videos.length;
            const video = document.getElementById('backgroundVideo');
            video.src = videos[currentVideoIndex];
            video.load();
            if (isPlaying) {
                video.play();
            }
        }
        
        // Efectos de notificación
        function mostrarNotificacion() {
            const notification = document.createElement('div');
            notification.className = 'notification';
            notification.style.animation = 'fadeInOut 4s ease-in-out';
            
            notification.textContent = `¡Turno A-${turnoCounter.toString().padStart(3, '0')} por favor!`;
            document.body.appendChild(notification);
            
            setTimeout(() => {
                if (document.body.contains(notification)) {
                    document.body.removeChild(notification);
                }
            }, 4000);
        }
        
        // Añadir animación CSS
        const style = document.createElement('style');
        style.textContent = `
            @keyframes fadeInOut {
                0% { opacity: 0; transform: translate(-50%, -50%) scale(0.3) rotate(-10deg); }
                15% { opacity: 1; transform: translate(-50%, -50%) scale(1.1) rotate(2deg); }
                25% { opacity: 1; transform: translate(-50%, -50%) scale(1) rotate(0deg); }
                75% { opacity: 1; transform: translate(-50%, -50%) scale(1) rotate(0deg); }
                90% { opacity: 1; transform: translate(-50%, -50%) scale(1.05) rotate(-1deg); }
                100% { opacity: 0; transform: translate(-50%, -50%) scale(0.3) rotate(5deg); }
            }
        `;
        document.head.appendChild(style);
        
        // Inicializar
        updateTime();
        setInterval(updateTime, 1000);
        setInterval(avanzarTurno, 10000); // Cambiar turno cada 10 segundos
        setInterval(mostrarNotificacion, 10000); // Mostrar notificación con cada turno
        
        // Reproducir video automáticamente
        document.getElementById('backgroundVideo').play().catch(e => {
            console.log('Autoplay prevented:', e);
        });
        
        // Cambiar video automáticamente cada 30 segundos
        setInterval(changeVideo, 30000);
    </script>
</body>
</html>