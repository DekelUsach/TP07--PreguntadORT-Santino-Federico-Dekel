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
    
    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        Juego.CargarPartida(username, dificultad, categoria);

        if (Juego.Preguntas != null)
        {
            return View("Jugar");   
        }
        else{
            return View("ConfigurarJuego");
        }
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
