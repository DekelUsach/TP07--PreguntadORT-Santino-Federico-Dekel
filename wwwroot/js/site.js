const anime = require('animejs');
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


const difInput = document.querySelector('.dif_input')

