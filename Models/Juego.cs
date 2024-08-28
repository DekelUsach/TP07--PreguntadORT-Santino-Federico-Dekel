public static class Juego
{
    /* ATRIBUTOS */
    static string? Username;
    static int PuntajeActual;
    static int CantidadPreguntasCorrectas;
    static List<Preguntas>? Preguntas;
    static List<Respuestas>? Respuestas;

    /* METODOS */
    public static void InicializarJuego()
    {
        Username = null;
        PuntajeActual = 0;
        CantidadPreguntasCorrectas = 0;
    }
    public static void CargarPartida(string username, int dificultad, int categoria)
    {
        Preguntas = BD.ObtenerPreguntas(dificultad, categoria);
        Respuestas = BD.ObtenerRespuestas(Preguntas);
    }
}