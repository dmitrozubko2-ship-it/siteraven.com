// Летючі ворони на фоні
function createFlyingRavens() {
    const container = document.body;
    const raven = document.createElement('div');
    raven.className = 'flying-raven';
    raven.textContent = '🐦';
    
    const top = Math.random() * window.innerHeight;
    const duration = 10 + Math.random() * 10; // 10-20 сек
    
    raven.style.top = top + 'px';
    raven.style.animationDuration = duration + 's';
    
    container.appendChild(raven);
    
    // Видалити елемент після завершення анімації
    setTimeout(() => raven.remove(), duration * 1000);
}

// Створювати летючих воронів кожні 5-10 секунд
function startFlyingRavens() {
    setInterval(() => {
        createFlyingRavens();
    }, 5000 + Math.random() * 5000);
}

// Звук при наведенні (дуже гарячий ефект)
function initHoverSounds() {
    const buttons = document.querySelectorAll('button, a, .card, .nav-link');
    
    buttons.forEach(element => {
        element.addEventListener('mouseenter', () => {
            // Можна додати звук через Web Audio API
            // Поки що просто взяли звук онлайн
            try {
                const audio = new Audio('data:audio/wav;base64,UklGRiYAAABXQVZFZm10IBAAAAABAAEAQB8AAAB9AAACABAAZGF0YQIAAAAAAA==');
                audio.volume = 0.1;
                audio.play().catch(() => {});
            } catch(e) {
                // Звук не працює, що ж
            }
        });
    });
}

// Плавна поява блоків при скролі
function initScrollReveal() {
    const revealElements = document.querySelectorAll('.achievement-card, .member-card, .rule-section, .review-card, .news-card');
    
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -100px 0px'
    };
    
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('active');
                observer.unobserve(entry.target);
            }
        });
    }, observerOptions);
    
    revealElements.forEach(element => {
        element.classList.add('reveal');
        observer.observe(element);
    });
}

// Динамічна тема (темна/світла)
function initThemeToggle() {
    const htmlElement = document.documentElement;
    
    // Перевірити локальне сховище для збереженої теми
    const savedTheme = localStorage.getItem('theme') || 'dark';
    applyTheme(savedTheme);
    
    // Додати кнопку для перемикання теми до navMenuToolContent
    const navMenu = document.querySelector('.nav-scrollable');
    if (navMenu) {
        const themeToggle = document.createElement('div');
        themeToggle.className = 'theme-toggle';
        themeToggle.innerHTML = '<span class="theme-toggle-icon">🌙</span><span>Тема</span>';
        themeToggle.style.margin = '1rem 1.5rem';
        
        themeToggle.addEventListener('click', () => {
            const currentTheme = localStorage.getItem('theme') || 'dark';
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            applyTheme(newTheme);
            localStorage.setItem('theme', newTheme);
            themeToggle.querySelector('.theme-toggle-icon').textContent = newTheme === 'dark' ? '🌙' : '☀️';
        });
        
        navMenu.appendChild(themeToggle);
    }
}

function applyTheme(theme) {
    const root = document.documentElement;
    
    if (theme === 'light') {
        root.style.setProperty('--primary-color', '#f5f5f5');
        root.style.setProperty('--secondary-color', '#ffffff');
        root.style.setProperty('--light-color', '#2c2c54');
    } else {
        root.style.setProperty('--primary-color', '#2c2c54');
        root.style.setProperty('--secondary-color', '#0f3460');
        root.style.setProperty('--light-color', '#e8e8e8');
    }
}

// Прелоадер
function showPreloader() {
    const preloader = document.createElement('div');
    preloader.className = 'preloader';
    preloader.innerHTML = `
        <div class="preloader-content">
            <div class="preloader-icon">🐦</div>
            <div class="preloader-text">Завантаження сім'ї...</div>
        </div>
    `;
    document.body.appendChild(preloader);
}

// Кастомний курсор
function initCustomCursor() {
    // CSS вже містить кастомний курсор
    // Цей файл потрібний лише для додаткових ефектів
}

// Ініціалізувати все при завантаженні сторінки
document.addEventListener('DOMContentLoaded', () => {
    showPreloader();
    // Видалено: startFlyingRavens(); - анімовані ворони було видалено для покращення продуктивності
    initHoverSounds();
    initScrollReveal();
    initThemeToggle();
    initCustomCursor();
});

// Також запустити при навігації у Blazor
window.addEventListener('load', () => {
    initScrollReveal();
});
