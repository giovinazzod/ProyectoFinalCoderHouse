using ProyectoFinalCoderHouse.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public static class VentaHandler
    {
        public const string ConnectionString = "Server=localhost;Initial Catalog=SistemaGestion;Trusted_Connection=True;";

        public static List<Venta> GetVentas()
        {
            List<Venta> resultados = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();

                                resultados.Add(venta);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }

        public static List<Venta> TraerVentas(int idUsuario)
        {
            List<Venta> resultados = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta v " +
                                                              "INNER JOIN ProductoVendido pv ON v.Id = pv.IdVenta " +
                                                              "INNER JOIN Producto p ON p.Id = pv.IdProducto " +
                                                              "INNER JOIN Usuario u ON u.Id = p.IdUsuario " +
                                                              "WHERE u.Id = @idUsuario", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();

                                venta.Id = Convert.ToInt32(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();

                                resultados.Add(venta);
                            }
                        }
                    }
                    sqlConnection.Close();
                }

                return resultados;
            }
        }

        public static bool EliminarVenta(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Venta WHERE Id = @Id";

                SqlParameter sqlParameter = new SqlParameter("Id", SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numOfRows = sqlCommand.ExecuteNonQuery();
                    if (numOfRows > 0)
                    {

                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }

            return resultado;
        }

        /*Cargar Venta: 
         * Recibe una lista de productos y el número de IdUsuario de quien la efectuó, 
         * primero cargar una nueva Venta en la base de datos, luego debe cargar los productos recibidos en la base de 
         * ProductosVendidos uno por uno por un lado, y descontar el stock en la base de productos por el otro.*/
        public static bool CargarVenta(Venta venta)
        {
            bool resultado = false;

            // Desarrollar

            return resultado;
        }
    }
}
