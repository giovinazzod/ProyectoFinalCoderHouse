using Microsoft.Extensions.Hosting;
using ProyectoFinalCoderHouse.Controllers.DTOS;
using ProyectoFinalCoderHouse.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=localhost;Initial Catalog=SistemaGestion;Trusted_Connection=True;";


        /// <summary>
        /// Crear producto: Recibe un producto como parámetro, deberá crearlo, puede ser void, pero validar los datos obligatorios.
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public static bool CrearProducto(Producto producto)
        {
            bool alta = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.Connection.Open();
                sqlCommand.CommandText = @"INSERT INTO Producto
                                ([Descripciones],[Costo],[PrecioVenta],[Stock],[IdUsuario]) VALUES
                                (@Descripciones,@Costo,@PrecioVenta,@Stock,@IdUsuario)";

                sqlCommand.Parameters.AddWithValue("@Descripciones", producto.Descripciones);
                sqlCommand.Parameters.AddWithValue("@Costo", producto.Costo);
                sqlCommand.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                sqlCommand.Parameters.AddWithValue("@Stock", producto.Stock);
                sqlCommand.Parameters.AddWithValue("@IdUsuario", producto.IdUsuario);

                sqlCommand.ExecuteNonQuery();

            }
            return alta;
        }

        internal static bool CrearProductoConPostProducto(PostProducto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Producto]" +
                    "([Descripciones],[Costo],[PrecioVenta],[Stock],[IdUsuario]) VALUES " +
                    "(@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario);";

                SqlParameter Descripciones = new SqlParameter("@Descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter Costo = new SqlParameter("@Costo", SqlDbType.VarChar) { Value = producto.Costo };
                SqlParameter PrecioVenta = new SqlParameter("@PrecioVenta", SqlDbType.VarChar) { Value = producto.PrecioVenta };
                SqlParameter Stock = new SqlParameter("@Stock", SqlDbType.VarChar) { Value = producto.Stock };
                SqlParameter IdUsuario = new SqlParameter("@IdUsuario", SqlDbType.VarChar) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(Descripciones);
                    sqlCommand.Parameters.Add(Costo);
                    sqlCommand.Parameters.Add(PrecioVenta);
                    sqlCommand.Parameters.Add(Stock);
                    sqlCommand.Parameters.Add(IdUsuario);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();// Se ejecuta la sentencia SQL y devuelve el nro de filas afectadas

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();
            }

            return resultado;
        }

        /// <summary>
        /// Modificar producto: Recibe un producto como parámetro, debe modificarlo con la nueva información.
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Producto] " +
                                     "SET Descripciones = @descripciones, " +
                                     "Costo = @costo, " +
                                     "PrecioVenta = @precioVenta, " +
                                     "Stock = @stock, " +
                                     "IdUsuario = @idUsuario " +
                                     "WHERE Id = @id";

                SqlParameter id = new SqlParameter("id", SqlDbType.BigInt) { Value = producto.Id };
                SqlParameter descripciones = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costo = new SqlParameter("costo", SqlDbType.VarChar) { Value = producto.Costo };
                SqlParameter precioVenta = new SqlParameter("precioVenta", SqlDbType.VarChar) { Value = producto.PrecioVenta };
                SqlParameter stock = new SqlParameter("stock", SqlDbType.VarChar) { Value = producto.Stock };
                SqlParameter idUsuario = new SqlParameter("idUsuario", SqlDbType.VarChar) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(id);
                    sqlCommand.Parameters.Add(descripciones);
                    sqlCommand.Parameters.Add(costo);
                    sqlCommand.Parameters.Add(precioVenta);
                    sqlCommand.Parameters.Add(stock);
                    sqlCommand.Parameters.Add(idUsuario);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();// Se ejecuta la sentencia SQL y devuelve el nro de filas afectadas

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();

            }

            return resultado;
        }

        /// <summary>
        /// Eliminar producto: Recibe un id de producto a eliminar y debe eliminarlo de la base
        /// de datos (eliminar antes sus productos vendidos también, sino no lo podrá hacer)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool EliminarProducto(int id)
        {
            // Primero chequeamos que no tenga productosVendidos
            bool tieneProductosVendidos = false;
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("select * FROM [SistemaGestion].[dbo].[ProductoVendido] " +
                                                              "WHERE IdProducto = @Id", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            tieneProductosVendidos = true;
                        }
                    }
                    sqlConnection.Close();
                }
            }

            // Si no hay productosVendidos, prosigo y elimino el producto
            if (!tieneProductosVendidos)
            {
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
            }

            return resultado;

        }

        /// <summary>
        /// Traer productos: 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Crear producto
        /// </summary>
        /// <para> Recibe un producto como parámetro, deberá crearlo, puede ser void, pero validar los datos obligatorios</para>
        /// <returns></returns>
        public static bool CrearProducto2(Producto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Producto]" +
                    "(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES" +
                    "(@descripciones, @costo, @precioVenta, @stock, @idUsuario);";

                SqlParameter descripciones = new SqlParameter("descripciones", SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costo = new SqlParameter("costo", SqlDbType.Decimal) { Value = producto.Costo };
                SqlParameter precioVenta = new SqlParameter("precioVenta", SqlDbType.Decimal) { Value = producto.PrecioVenta };
                SqlParameter stock = new SqlParameter("stock", SqlDbType.BigInt) { Value = producto.Stock };
                SqlParameter idUsuario = new SqlParameter("idUsuario", SqlDbType.VarChar) { Value = producto.IdUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripciones.Value);
                    sqlCommand.Parameters.Add(costo);
                    sqlCommand.Parameters.Add(precioVenta);
                    sqlCommand.Parameters.Add(stock);
                    sqlCommand.Parameters.Add(idUsuario);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();// Se ejecuta la sentencia SQL y devuelve el nro de filas afectadas

                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                }
                sqlConnection.Close();

            }

            return resultado;
        }
        /// <summary>
        /// Traer productos
        /// </summary>
        /// <returns></returns>
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
    }
}
