
// const html = document.querySelector("#tema");
// const htmlColorMode = document.querySelector(".site-theme");
// html.setAttribute("data-bs-theme", localStorage.getItem("tema"));

// document.getElementById("themeSelector").src = localStorage.getItem("img");

// htmlColorMode.addEventListener("click", () => {
//     if (html.getAttribute("data-bs-theme") === "dark") {
//         localStorage.setItem("tema", "light");
//         localStorage.setItem("img", "../Images/sol.png");
//         htmlColorMode.id = "boton-cambiar-tema-light";
//     } else {
//         localStorage.setItem("tema", "dark");
//         localStorage.setItem("img", "../Images/luna.png");
//         htmlColorMode.id = "boton-cambiar-tema-dark";
//     }
//     html.setAttribute("data-bs-theme", localStorage.getItem("tema"));
//     document.getElementById("themeSelector").src = localStorage.getItem("img");
// });



 const correcta = document.querySelector(".res_option_True");
 console.log( correcta );
 const incorrecta = document.querySelectorAll(".res_option_False");
 correcta.addEventListener("click", () => 
 {
     const idPregunta = document.querySelector(".res_input_True");
     correcta.classList = "bienRespondido";
     const idPreguntaF = idPregunta.dataset.pregunta;
     const idRespuesta = idPregunta.value;
     console.log(idPreguntaF)
     console.log(idRespuesta);
 })
 incorrecta.addEventListener("click", () => {
     incorrecta.classList = "incorrecta";s


 })
