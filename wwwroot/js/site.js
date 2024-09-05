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
const ruleta = document.querySelector("#ruleta");
ruleta.addEventListener("click",girar);
dinero = 50;
function girar(){
    
    	let rand = Math.random()*7200; 
        calcular(rand);
}


function calcular(rand){
    valor = rand / 360;
    valor = (valor - parseInt(valor.toString().split(".")[0])) * 360;
    ruleta.style.transform = "rotate("+rand+"deg)";
    setTimeout(()=>{
    switch(true){
    	case valor > 0 && valor <= 45:
    	    alert("Has ganado y te has llevado 500 ");
    	    break;
    	case valor > 45 && valor <= 90:
    	    alert("Has ganado 20 USD");
    	    break;
        case valor > 90 && valor <= 135:
            alert("has sumado 5 puntos");
            break; 
        case valor > 135 && valor <= 180:
            alert("has sumado 50 puntos");
            break;  
        case valor > 180 && valor <= 225:
            alert("has sumado 100 puntos");
            break;
        case valor > 225 && valor <= 270:
            alert("No has sumado puntos");
            break;
        case valor > 270 && valor <= 315:
            alert("has sumado 70 puntos");
            break;
        case valor > 315 && valor <= 360:
            alert("Has sumado 10 puntos");
            break;
    }},6000);
}