document.addEventListener("DOMContentLoaded", () => {
    const container = document.getElementById("bars-container");
    const numBars = 30; // más barras = fondo más denso
    const colors = ["#00ffaa", "#00aaff", "#ff00ff", "#ff0055"];

    for (let i = 0; i < numBars; i++) {
        const bar = document.createElement("div");
        bar.classList.add("bar");

        // Ancho aleatorio entre 14px y 26px
        const width = Math.floor(Math.random() * 12) + 14;

        // Altura inicial aleatoria entre 30% y 90%
        const height = Math.floor(Math.random() * 20) + 30;

        // Duración y delay aleatorios
        const duration = (Math.random() * 2.5 + 1.5).toFixed(2);
        const delay = (Math.random() * 1.5).toFixed(2);

        // Color aleatorio
        const color = colors[Math.floor(Math.random() * colors.length)];

        bar.style.width = `${width}px`;
        bar.style.height = `${height}%`;
        bar.style.background = `linear-gradient(180deg, ${color}, #000)`;
        bar.style.animationDuration = `${duration}s`;
        bar.style.animationDelay = `${delay}s`;

        container.appendChild(bar);
    }
});
