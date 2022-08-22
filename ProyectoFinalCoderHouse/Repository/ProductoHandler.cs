using ProyectoFinalCoderHouse.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=localhost;Initial Catalog=SistemaGestion;Trusted_Connection=True;";

        public static List<Producto> GetProductos()
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me aseguro que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToDouble(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);

                                resultados.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }

        public static List<Producto> TraerProductos(int idUsuario)
        {
            List<Producto> resultados = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto " +
                                                              "WHERE IdUsuario = @IdUsuario", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me aseguro que haya filas
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto.Costo = Convert.ToDouble(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);

                                resultados.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }

        internal static bool EliminarProducto(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Producto WHERE Id = @Id";

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
    }
}
