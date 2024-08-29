public static class Juego
{
    /* ATRIBUTOS */
    static string? Username;
    static int PuntajeActual;
    static int CantidadPreguntasCorrectas;
    static public List<Preguntas>? Preguntas;
    static public List<Respuestas>? Respuestas;

    /* METODOS */
    public static void InicializarJuego()
    // Inicializa todos los atributos privados de la clase que dan inicio el juego: username vacío, puntaje actual y cantidad preguntas correctas a 0.
    {
        Username = null;
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
    }

    public static void CargarPartida(string username, int dificultad, int categoria)
    // Recibe la dificultad y categoría elegidas, invoca a los métodos ObtenerPreguntas y ObtenerRespuestas (en ese orden) y guarda los resultados en los atributos preguntas y respuestas.
    {
        Preguntas = BD.ObtenerPreguntas(dificultad, categoria);
        Respuestas = BD.ObtenerRespuestas(Preguntas);
    }

    public static Preguntas ObtenerProximaPregunta()
    // Retorna, de ser posible, una pregunta al azar de la lista de preguntas.
    {
        Preguntas? preguntaRetornada = null;

        if (Preguntas != null)
        {
            Random rnd = new();
            int randNum = rnd.Next(0, Preguntas.Count());

            preguntaRetornada = Preguntas[randNum];
        }
        return preguntaRetornada;
    }
    public static List<Respuestas> ObtenerProximasRespuestas(int idPregunta)
    {
        List<Respuestas> proximaRespuesta = new List<Respuestas>();

        foreach (Respuestas resp in Respuestas)
        {
            if (resp.IdPregunta == idPregunta)
            {
                proximaRespuesta.Add(resp);
            }
        }

    return proximaRespuesta;

    }


    public static bool VerificarRespuesta(int idPregunta, int idRespuesta, string respuesta)
    // Recibe un id de pregunta y un id de respuesta, y retorna un booleano indicando si la respuesta fue correcta o incorrecta.
    {
        Preguntas.RemoveAt(idPregunta);
        if (Respuestas[idRespuesta].Contenido == respuesta)
        {
            PuntajeActual++;
            CantidadPreguntasCorrectas++;
            return true;
        }
        else
        {
            return false;
        }
    }
}