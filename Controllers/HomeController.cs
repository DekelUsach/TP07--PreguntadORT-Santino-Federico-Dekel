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
        Juego.CategoriaUnica = false;
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
    public IActionResult Rueda(int categoria)
    {
        if (categoria == -1)
        {
            Random rnd1 = new Random();
            categoria = rnd1.Next(1, 8);
        }
        Juego.idCategoria = categoria;
        Preguntas pregunta = Juego.ObtenerProximaPreguntaCategoria(categoria);
        List<Respuestas> respuestas = Juego.ObtenerProximasRespuestas(pregunta.IdPregunta);
        Random rnd = new Random();

        List<Respuestas> respDesord = respuestas.OrderBy(x => rnd.Next()).ToList();
        List<Usuarios> UsuariosTop25 = BD.ObtenerUsers();


        if (Juego.cantVidas != 0)
        {
            ViewBag.Usuarios = UsuariosTop25;
            ViewBag.pregunta = pregunta;
            ViewBag.respuestas = respDesord;
            ViewBag.Categorias = BD.ObtenerCategorias();
            ViewBag.Username = Juego.Username;
            ViewBag.PuntajeActual = Juego.PuntajeActual;
            ViewBag.vida = Juego.cantVidas;

            return View("Juego");
        }
        else
        {
            return View("Fin");
        }
    }

    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {

        // Cargar el juego con la categoría y dificultad seleccionadas
        Juego.CargarPartida(username, dificultad, categoria);

        if (Juego.Preguntas != null && categoria == -1)
        {
            ViewBag.Username = Juego.Username;
            ViewBag.PuntajeActual = Juego.PuntajeActual;
            ViewBag.vida = Juego.cantVidas;
            return View("Rueda");
        }
        else if (Juego.Preguntas != null)
        {
            Juego.CategoriaUnica = true;
            return RedirectToAction("Jugar");
        }
        else
        {
            return View("ConfigurarJuego");
        }
    }

    public IActionResult Respuesta()
    {
        return View("Respuesta");
    }

    public IActionResult Continuar()
    {
        List<Usuarios> UsuariosTop25 = BD.ObtenerUsers();
        ViewBag.Usuarios = UsuariosTop25;

        if (Juego.Preguntas != null && Juego.cantVidas != 0)
        {
            if (Juego.CategoriaUnica)
            {
                return RedirectToAction("Jugar");
            }
            else
            {
                ViewBag.Username = Juego.Username;
                ViewBag.PuntajeActual = Juego.PuntajeActual;
                ViewBag.vida = Juego.cantVidas;
                return View("Rueda");
            }
        }
        else
        {
            return View("Fin");
        }

    }

    public IActionResult Jugar()
    {
        Preguntas pregunta = Juego.ObtenerProximaPregunta();
        List<Respuestas> respuestas = Juego.ObtenerProximasRespuestas(pregunta.IdPregunta);
        Random rnd = new Random();
        List<Respuestas> respDesord = respuestas.OrderBy(x => rnd.Next()).ToList();
        List<Usuarios> UsuariosTop25 = BD.ObtenerUsers();

        if (Juego.cantVidas > 0)
        {

            ViewBag.Usuarios = UsuariosTop25;
            ViewBag.Categorias = BD.ObtenerCategorias();
            ViewBag.pregunta = pregunta;
            ViewBag.respuestas = respDesord;
            ViewBag.Username = Juego.Username;
            ViewBag.PuntajeActual = Juego.PuntajeActual;
            ViewBag.vida = Juego.cantVidas;
            return View("Juego");
        }
        else
        {
            return View("Fin");
        }
    }

    public IActionResult VerificarRespuestaController(int idPregunta, int idRespuesta, string respuesta)
    {
        bool resultado = Juego.VerificarRespuesta(idRespuesta, respuesta);
        ViewBag.resultado = resultado;
        List<Respuestas> respProvisoria = Juego.Respuestas;
        int espacioR = 0;
        int contadorR = 0;
        foreach (Respuestas res in Juego.Respuestas)
        {
            if (res.IdRespuesta == idRespuesta)
            {
                espacioR = contadorR;
            }

            contadorR++;
        }
        /*
        //Solucionar este error
        int contador = 0;
        int espacio = 0;
        foreach(Respuestas res in respProvisoria)
        {
            if(res.IdPregunta != idPregunta){
                respProvisoria.RemoveAt(espacio);

            }
            if(res.IdPregunta == idPregunta){
                espacio =contador;
            }
            contador++;
        }

if (!resultado)
{
    ViewBag.respuestaCorrecta = respProvisoria[espacio];
}*/
        Thread.Sleep(300);
        return View("Respuesta");
    }

    public IActionResult Ranking()
    {
        List<Usuarios> UsuariosTop25 = BD.ObtenerUsers();

        ViewBag.Usuarios = UsuariosTop25;

        return View();
    }

    public IActionResult Fin()
    {
        List<Usuarios> UsuariosTop25 = BD.ObtenerUsers();
        ViewBag.Usuarios = UsuariosTop25;

        return View();
    }

    public IActionResult MandarUser(bool guarda)
    {
        List<Usuarios> UsuariosTop25 = BD.ObtenerUsers();
        ViewBag.Usuarios = UsuariosTop25;
        if (guarda)
        {
            BD.MandarUser(Juego.Username, Juego.PuntajeActual);
        }
        return View("Index");
    }


    public IActionResult Creditos()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
