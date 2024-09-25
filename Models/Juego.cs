public static class Juego
{
    /* ATRIBUTOS */
    public static bool CategoriaUnica = false;
    public static string? Username;
    public static int PuntajeActual;
    public static int CantidadPreguntasCorrectas;
    public static int idCategoria;
    public static int idDificultad;
    public static int cantVidas = 1;
    public static List<Preguntas>? Preguntas;
    public static List<Respuestas>? Respuestas;
    
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
        Username = username;
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

    public static Preguntas ObtenerProximaPreguntaCategoria(int categoria)
    {
        Preguntas? preguntaRetornada = null;
        List<Preguntas> PreguntasCategoria = new List<Preguntas>();
        if(Preguntas != null)
        {
            foreach(Preguntas pregunta in Preguntas)
            {
                if(pregunta.IdCategoria == categoria)
                {
                    PreguntasCategoria.Add(pregunta);
                }
                
            }
                Random rnd = new();
                int randNum = rnd.Next(0, PreguntasCategoria.Count()-1);
                preguntaRetornada = PreguntasCategoria[randNum];
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


    public static bool VerificarRespuesta(int idRespuesta, string respuesta)
    // Recibe un id de pregunta y un id de respuesta, y retorna un booleano indicando si la respuesta fue correcta o incorrecta.
    {
        bool resultado = true;
        bool RespuestaIDencontrado = false;
        int contadorR = 0;
        do 
        {
            if (Respuestas[contadorR].IdRespuesta == idRespuesta)
            {
                RespuestaIDencontrado = true;

                bool PreguntaIDencontrada = true;
                int contadorP = 0;
                do
                {
                    if (Preguntas[contadorP].IdPregunta == Respuestas[contadorR].IdPregunta)
                    {
                        PreguntaIDencontrada = false;
                        Preguntas.RemoveAt(contadorP);
                    }
                    contadorP++;
                } while(PreguntaIDencontrada);

                if (Respuestas[contadorR].Correcta)
                {
                    PuntajeActual += 100;
                    CantidadPreguntasCorrectas++;
                    resultado = true;
                }
                else
                {
                    cantVidas--;
                    PuntajeActual -= 40;
                    resultado = false;
                }
            }
            contadorR++;
        } while(!RespuestaIDencontrado);
        return resultado;
    }
}