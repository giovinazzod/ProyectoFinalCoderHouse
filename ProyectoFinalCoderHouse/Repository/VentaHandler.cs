using ProyectoFinalCoderHouse.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public static class VentaHandler
    {
        public const string ConnectionString = "Server=localhost;Initial Catalog=SistemaGestion;Trusted_Connection=True;";

        /// <summary>
        /// Trae todas las ventas registradas en la tabla [SistemaGestion].[dbo].[Venta]
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Trae todas las ventas registradas en la tabla [SistemaGestion].[dbo].[Venta]
        /// Este método recibe un IdUsuario y solo trae las ventas de ese usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public static List<Venta> GetVentasByIdUsuario(int idUsuario)
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

        /// <summary>
        /// Eliminar venta
        /// <para>Recibe un Id y elimina la venta relacionada</para>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool EliminarVenta(int id)
        {
            bool resultado = false;

            // Primero debemos eliminar de la tabla [ProductoVendido] a los que correspondan a la venta a borrar
            ProductoVendidoHandler.EliminarProductoVendidoByIdVenta(id);


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

        /// <summary>
        /// Cargar Venta: 
        /// Recibe una lista de productos y el número de IdUsuario de quien la efectuó,
        /// primero cargar una nueva Venta en la base de datos, luego debe cargar los productos recibidos en la base de
        /// ProductosVendidos uno por uno por un lado, y descontar el stock en la base de productos por el otro.
        /// </summary>
        /// <param name="venta"></param>
        /// <returns></returns>
        public static void CargarVenta(List<Producto> productos, int IdUsuario)
        {
            Venta venta = new Venta();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Connection.Open();

                // Realizamos el insert en la tabla [SistemaGestion].[dbo].[Venta]
                sqlCommand.CommandText = @"INSERT INTO Venta
                                ([Comentarios],[IdUsuario])
                                VALUES
                                (@Comentarios,@IdUsuario)";

                sqlCommand.Parameters.AddWithValue("@Comentarios", "");
                sqlCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                sqlCommand.ExecuteNonQuery();
                venta.Id = GetId.Get(sqlCommand); // Obtenemos el ID de la venta para luego hacer el insert en la tabla [Producto]
                venta.IdUsuario = IdUsuario;

                foreach (Producto producto in productos)
                {
                    // Realizamos el insert en la tabla [SistemaGestion].[dbo].[ProductoVendido]
                    sqlCommand.CommandText = @"INSERT INTO ProductoVendido
                                ([Stock],[IdProducto],[IdVenta])
                                VALUES
                                (@Stock,@IdProducto,@IdVenta)";

                    sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                    sqlCommand.Parameters.AddWithValue("@IdProducto", producto.Id);
                    sqlCommand.Parameters.AddWithValue("@IdVenta", venta.Id);

                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();

                    // Actualizamos el Stock del producto Vendido en la tabla [SistemaGestion].[dbo].[Producto]
                    sqlCommand.CommandText = @" UPDATE Producto
                                                SET 
                                                Stock = Stock - @Stock
                                                WHERE id = @IdProducto";

                    sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                    sqlCommand.Parameters.AddWithValue("@IdProducto", producto.Id);

                    sqlCommand.ExecuteNonQuery();
                    sqlCommand.Parameters.Clear();
                }
            }
        }
    }
}
