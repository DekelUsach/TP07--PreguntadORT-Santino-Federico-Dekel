using System.Data.SqlClient;
using Dapper;

public static class BD
{
    /* ATRIBUTOS */
    private static string _connectionString = @"Server=localhost;Database=JJOO;Trusted_Connection=True;";
    public static List<Categorias> ListaCategorias = new List<Categorias>();
    public static List<Dificultades> ListaDificultades = new List<Dificultades>();
    public static List<Preguntas> ListaPreguntas = new List<Preguntas>();

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
        /* 4 POSIBILIDADES:
            -1 -1 cualquier pregunta
            X -1 todas las preguntas de una dificultad sin importar la categoría
            -1  X todas las preguntas de una categoría sin importar dificultad
            X  X todas las preguntas de una cierta dificultad y una cierta categoría
        */

        if (dificultad == -1 && categoria == -1) //todas las preguntas sin importar dificultad
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Preguntas";
                ListaPreguntas = db.Query<Preguntas>(sql).ToList();
            }
            return ListaPreguntas;
        }
        else if (dificultad != -1 && categoria == -1)
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Preguntas WHERE IdDificultad = @pIdDificultad";
                ListaPreguntas = db.Query<Preguntas>(sql, new { pIdDificultad = dificultad }).ToList();
            }
            return ListaPreguntas;

        }
        else if (dificultad == -1 && categoria != -1)
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Preguntas WHERE IdCategoria = @pIdCategoria";
                ListaPreguntas = db.Query<Preguntas>(sql, new { pIdCategoria= categoria }).ToList();
            }
            return ListaPreguntas;
        }
        else
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Preguntas WHERE IdCategoria = @pIdCategoria AND IdDificultad = @pIdDificultad";
                ListaPreguntas = db.Query<Preguntas>(sql, new { pIdCategoria= categoria }).ToList();
            }
            return ListaPreguntas;
        }
    }
    
}