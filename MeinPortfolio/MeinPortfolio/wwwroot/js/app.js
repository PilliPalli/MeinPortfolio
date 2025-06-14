// Theme management
function applyTheme(theme, colors) {
    const root = document.documentElement;

    root.style.setProperty('--terminal-bg', colors.bg);
    root.style.setProperty('--terminal-text', colors.text);
    root.style.setProperty('--terminal-border', colors.border);

    root.style.setProperty('--terminal-header-bg', colors.bg);
    root.style.setProperty('--terminal-header-text', colors.text);
    root.style.setProperty('--body-bg', colors.bg);
}


async function downloadCv() {
    try {
        const response = await fetch('cv/cv.pdf', { cache: 'no-store' });
        if (!response.ok) throw new Error(`Fehler beim Abrufen der Datei: ${response.status}`);

        const blob = await response.blob();
        const url = window.URL.createObjectURL(blob);

        const a = document.createElement("a");
        a.href = url;
        a.download = "My_CV.pdf"; 
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);
        window.URL.revokeObjectURL(url);
    } catch (error) {
        console.error("CV-Download fehlgeschlagen:", error);
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
window.initTerminal = async function () {
    try {
        await loadJsPDF();
        window.terminalReady = true;
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

window.scrollTerminalToBottom = function () {
    const el = document.getElementById('terminal-body');
    if (el) {
        el.scrollTop = el.scrollHeight;
    }
};


