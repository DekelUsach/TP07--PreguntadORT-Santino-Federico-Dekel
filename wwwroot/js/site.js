const html = document.querySelector("#tema");
const htmlColorMode = document.querySelector(".site-theme");
html.setAttribute("data-bs-theme", localStorage.getItem("tema"));

document.getElementById("themeSelector").src = localStorage.getItem("img");

htmlColorMode.addEventListener("click", () => {
    if (html.getAttribute("data-bs-theme") === "dark") {
        localStorage.setItem("tema", "light");
        localStorage.setItem("img", "../Images/sol.png");
        htmlColorMode.id = "boton-cambiar-tema-light";
    } else {
        localStorage.setItem("tema", "dark");
        localStorage.setItem("img", "../Images/luna.png");
        htmlColorMode.id = "boton-cambiar-tema-dark";
    }
    html.setAttribute("data-bs-theme", localStorage.getItem("tema"));
    document.getElementById("themeSelector").src = localStorage.getItem("img");
});

//wheel
let selectedCategory;

function spinWheel() {
    const wheel = document.querySelector('.wheel');
    const result = document.getElementById('result');
   
    const degrees = Math.floor(Math.random() * 360) + 720;
    wheel.style.transform = `rotate(${degrees}deg)`;
   
    setTimeout(() => {
        const normalizedDegrees = degrees % 360;
        const sectionDegrees = 360 / 4;
        const selectedIndex = Math.floor((normalizedDegrees + sectionDegrees / 2) / sectionDegrees) % 4;
        const categories = ['Azul Cielo', 'Azul Acero', 'Azul Dodger', 'Azul Celeste'];
        selectedCategory = categories[selectedIndex];
       
        result.textContent = `Categoría seleccionada: ${selectedCategory}`;
        console.log("Categoría seleccionada:", selectedCategory);
    }, 1600);
}