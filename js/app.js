// Theme management
function applyTheme(theme) {
    const root = document.documentElement;
    
    switch (theme) {
        case 'Dark':
            root.style.setProperty('--terminal-bg', '#121212');
            root.style.setProperty('--terminal-text', '#33ff33');
            root.style.setProperty('--terminal-border', '#33ff33');
            root.style.setProperty('--terminal-header-bg', '#121212');
            root.style.setProperty('--terminal-header-text', '#33ff33');
            root.style.setProperty('--body-bg', '#121212');
            break;
        case 'Light':
            root.style.setProperty('--terminal-bg', '#f0f0f0');
            root.style.setProperty('--terminal-text', '#121212');
            root.style.setProperty('--terminal-border', '#121212');
            root.style.setProperty('--terminal-header-bg', '#e0e0e0');
            root.style.setProperty('--terminal-header-text', '#121212');
            root.style.setProperty('--body-bg', '#f0f0f0');
            break;
        case 'Hacker':
            root.style.setProperty('--terminal-bg', '#000000');
            root.style.setProperty('--terminal-text', '#00ff00');
            root.style.setProperty('--terminal-border', '#00ff00');
            root.style.setProperty('--terminal-header-bg', '#000000');
            root.style.setProperty('--terminal-header-text', '#00ff00');
            root.style.setProperty('--body-bg', '#000000');
            break;
        case 'Retro':
            root.style.setProperty('--terminal-bg', '#2b2b2b');
            root.style.setProperty('--terminal-text', '#ff8c00');
            root.style.setProperty('--terminal-border', '#ff8c00');
            root.style.setProperty('--terminal-header-bg', '#2b2b2b');
            root.style.setProperty('--terminal-header-text', '#ff8c00');
            root.style.setProperty('--body-bg', '#2b2b2b');
            break;
        case 'Cyberpunk':
            root.style.setProperty('--terminal-bg', '#272932');
            root.style.setProperty('--terminal-text', '#CB1DCD');
            root.style.setProperty('--terminal-border', '#37EBF3');
            root.style.setProperty('--terminal-header-bg', '#272932');
            root.style.setProperty('--terminal-header-text', '#00D0DB');
            root.style.setProperty('--body-bg', '#272932');
            break;
    }
}

async function downloadResume() {
    try {
        const response = await fetch('resumes/resume.pdf', { cache: 'no-store' });
        if (!response.ok) throw new Error(`Fehler beim Abrufen der Datei: ${response.status}`);

        const blob = await response.blob();
        const url = window.URL.createObjectURL(blob);

        const a = document.createElement("a");
        a.href = url;
        a.download = "Mein_Resume.pdf"; // Erzwingt den Download
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        window.URL.revokeObjectURL(url);
    } catch (error) {
        console.error("Resume-Download fehlgeschlagen:", error);
    }
}



function loadJsPDF() {
    return new Promise((resolve, reject) => {
        if (window.jspdf) {
            resolve();
            return;
        }
        
        const script = document.createElement('script');
        script.src = 'https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js';
        script.onload = () => resolve();
        script.onerror = () => reject(new Error('Failed to load jsPDF'));
        document.head.appendChild(script);
    });
}

// Initialize
window.initTerminal = async function() {
    try {
        await loadJsPDF();
        console.log('Terminal initialized');
    } catch (error) {
        console.error('Error initializing terminal:', error);
    }
};

// Text animation
window.animateText = function(text, elementId, speed = 50) {
    return new Promise(resolve => {
        const element = document.getElementById(elementId);
        if (!element) {
            resolve();
            return;
        }
        
        element.textContent = '';
        let i = 0;
        
        const interval = setInterval(() => {
            if (i < text.length) {
                element.textContent += text.charAt(i);
                i++;
            } else {
                clearInterval(interval);
                resolve();
            }
        }, speed);
    });
};

