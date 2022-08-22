using ProyectoFinalCoderHouse.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=localhost;Initial Catalog=SistemaGestion;Trusted_Connection=True;";

        public static List<ProductoVendido> GetProductosVendidos()
        {
            List<ProductoVendido> resultados = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ProductoVendido", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                resultados.Add(productoVendido);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }

        public static List<ProductoVendido> TraerProductosVendidos(int idUsuario)
        {
            List<ProductoVendido> resultados = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ProductoVendido pv " +
                                                              "INNER JOIN Producto p ON p.Id = pv.IdProducto " +
                                                              "INNER JOIN Usuario u ON u.Id = p.IdUsuario " +
                                                              "WHERE u.Id = @IdUsuario", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                resultados.Add(productoVendido);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }

        internal static bool EliminarProductoVendido(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM ProductoVendido WHERE Id = @Id";

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
