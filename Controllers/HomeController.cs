using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07__PreguntadORT_Santino_Federico_Dekel.Models;

namespace TP07__PreguntadORT_Santino_Federico_Dekel.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ConfigurarJuego()
    {

        Juego.InicializarJuego();
        ViewBag.Categorias = BD.ObtenerCategorias();
        ViewBag.Dificultad = BD.ObtenerDificultades();
        return View();
    }
    [HttpGet]
    public IActionResult Rueda()
    {
        return View();
    }

    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        Juego.CargarPartida(username, dificultad, categoria);

        if (Juego.Preguntas != null)
        {
            return View("Jugar");
        }
        else
        {
            return View("ConfigurarJuego");
        }

    }
    [HttpPost]
    [HttpPost]
    public IActionResult GuardarCategoria([FromBody] CategoriaRequest request)
    {
        // Convertir el valor recibido de string a int
        int categoriaSeleccionada;
        bool conversionExitosa = int.TryParse(request.Categoria, out categoriaSeleccionada);

        // Si la conversión falla, enviamos un error al cliente
        if (!conversionExitosa)
        {
            return BadRequest(new { success = false, message = "Categoría inválida." });
        }

        // Aquí puedes manejar la lógica con la categoría seleccionada
        // Por ejemplo, podrías guardarla en la sesión o usarla para otra lógica del juego
        // Juego.CargarPartida(username, dificultad, categoriaSeleccionada);

        // Retornar una respuesta en formato JSON al cliente
        return Json(new { success = true, categoria = categoriaSeleccionada });
    }




    public IActionResult Jugar()
    {
        Preguntas pregunta = Juego.ObtenerProximaPregunta();

        if (pregunta != null)
        {
            ViewBag.pregunta = pregunta;
            return View("Juego");
        }
        else
        {
            return View("Fin");
        }
    }

    public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta, string respuesta)
    {
        bool resultado = Juego.VerificarRespuesta(idPregunta, idRespuesta, respuesta);
        ViewBag.resultado = resultado;
        if (!resultado)
        {
            ViewBag.respuestaCorrecta = Juego.Respuestas[idRespuesta];
        }
        return View("Respuesta");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
