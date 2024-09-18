using System.Data.SqlClient;
using Dapper;

public static class BD
{
    /* ATRIBUTOS */
    private static string _connectionString = @"Server=localhost\SQLEXPRESS;Database=TriviaDB;Trusted_Connection=True;";
    public static List<Categorias> ListaCategorias = new List<Categorias>();
    public static List<Dificultades> ListaDificultades = new List<Dificultades>();

    /* METODOS */
    public static List<Categorias> ObtenerCategorias()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Categorias";
            ListaCategorias = db.Query<Categorias>(sql).ToList();
        }
        return ListaCategorias;
    } 

    public static List<Dificultades> ObtenerDificultades()
    {
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Dificultades";
            ListaDificultades = db.Query<Dificultades>(sql).ToList();
        }
        return ListaDificultades;
    }

    public static List<Preguntas> ObtenerPreguntas(int dificultad, int categoria)
    {
        List<Preguntas> ListaPreguntas = new List<Preguntas>();
        /* 4 POSIBILIDADES:
            -1 -1 cualquier pregunta
            X -1 todas las preguntas de una dificultad sin importar la categoría
            -1  X todas las preguntas de una categoría sin importar dificultad
            X  X todas las preguntas de una cierta dificultad y una cierta categoría
        */
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            if (dificultad == -1 && categoria == -1) //todas las preguntas sin importar dificultad
            {
                string sql = "SELECT * FROM Preguntas";
                ListaPreguntas = db.Query<Preguntas>(sql).ToList();
                return ListaPreguntas;
            }
            else if (dificultad != -1 && categoria == -1)
            { 
                string sql = "SELECT * FROM Preguntas WHERE IdDificultad = @pIdDificultad";
                ListaPreguntas = db.Query<Preguntas>(sql, new { pIdDificultad = dificultad }).ToList();
                return ListaPreguntas;
            }
            else if (dificultad == -1 && categoria != -1)
            {
                string sql = "SELECT * FROM Preguntas WHERE IdCategoria = @pIdCategoria";
                ListaPreguntas = db.Query<Preguntas>(sql, new { pIdCategoria= categoria }).ToList();
                return ListaPreguntas;
            }
            else
            {
                string sql = "SELECT * FROM Preguntas WHERE IdCategoria = @pIdCategoria AND IdDificultad = @pIdDificultad";
                ListaPreguntas = db.Query<Preguntas>(sql, new { pIdCategoria = categoria, pIdDificultad = dificultad}).ToList();
                return ListaPreguntas;
            }
        }
    }
    public static List<Respuestas> ObtenerRespuestas(List<Preguntas> ListaPreguntas)
    {
        List<Respuestas> ListaRespuestas = new List<Respuestas>();
        List<Respuestas> RespuestasProvisionales = new List<Respuestas>();
        using (SqlConnection db = new SqlConnection(_connectionString))
        {
            foreach(Preguntas pregunta in ListaPreguntas)
            {
                string sql = "SELECT * FROM Respuestas WHERE IdPregunta = @pIdPregunta";
                RespuestasProvisionales = db.Query<Respuestas>(sql, new { pIdPregunta = pregunta.IdPregunta }).ToList();
                ListaRespuestas.AddRange(RespuestasProvisionales);
            }
        }
        return ListaRespuestas;
    }
}