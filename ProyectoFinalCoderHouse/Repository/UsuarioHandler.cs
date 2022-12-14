using ProyectoFinalCoderHouse.Model;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public static class UsuarioHandler
    {
        public const string ConnectionString = "Server=localhost;Initial Catalog=SistemaGestion;Trusted_Connection=True;";

        /// <summary>
        /// Inicio de sesión v1
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        public static bool Login(string nombreUsuario, string contraseña)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario " +
                                                              "WHERE nombreUsuario = @nombreUsuario " +
                                                              "AND contraseña = @contraseña", sqlConnection))
                {
                    //Add parameters to SQL Query
                    sqlCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    sqlCommand.Parameters.AddWithValue("@contraseña", contraseña);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                resultado = true;
                            }
                        }
                        else
                        {
                            resultado = false;
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultado;

        }

        /// <summary>
        /// Inicio de sesión
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <param name="contraseña"></param>
        /// <returns></returns>
        public static Usuario IniciarSesion(string nombreUsuario, string contraseña)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario " +
                                                              "WHERE nombreUsuario = @nombreUsuario " +
                                                              "AND contraseña = @contraseña", sqlConnection))
                {
                    //Add parameters to SQL Query
                    sqlCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                    sqlCommand.Parameters.AddWithValue("@contraseña", contraseña);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Mail = dataReader["mail"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return usuario;

        }

        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> resultados = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();

                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["NombreUsuario"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Mail = dataReader["mail"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();

                                resultados.Add(usuario);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }

        public static List<Usuario> TraerUsuario(int id)
        {
            List<Usuario> resultados = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario " +
                                                              "WHERE Id = @Id", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Id", id);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();

                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["NombreUsuario"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Mail = dataReader["mail"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();

                                resultados.Add(usuario);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultados;
        }

        public static Usuario TraerUsuario(string nombreUsuario)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario " +
                                                              "WHERE NombreUsuario = @nombreUsuario", sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);

                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Mail = dataReader["mail"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return usuario;
        }

        public static bool EliminarUsuario(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                // Comparamos el id con el que nos estan pasando por parámetro (@id)
                string queryDelete = "DELETE FROM Usuario WHERE Id = @Id";

                // Le pasamos al sqlParameter el nombre que va a tener ese parámetro en la query y el tipo de dato
                SqlParameter sqlParameter = new SqlParameter("Id", SqlDbType.BigInt);

                // Le asignamos el valor al sqlParameter
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
        /// Modificar usuario: Recibe como parámetro todos los datos del objeto usuario y se
        /// deberá modificar el mismo con los datos nuevos (No crear uno nuevo)
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Usuario] " +
                                     "SET Nombre = @nombre, " +
                                     "Apellido = @apellido, " +
                                     "NombreUsuario = @nombreUsuario, " +
                                     "Contraseña = @contraseña, " +
                                     "Mail = @mail " +
                                     "WHERE Id = @id";

                SqlParameter id = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Id };
                SqlParameter nombre = new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellido = new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuario = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseña = new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mail = new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(id);
                    sqlCommand.Parameters.Add(nombre);
                    sqlCommand.Parameters.Add(apellido);
                    sqlCommand.Parameters.Add(nombreUsuario);
                    sqlCommand.Parameters.Add(contraseña);
                    sqlCommand.Parameters.Add(mail);

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

        public static bool ModificarNombreDeUsuario(Usuario usuario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "UPDATE [SistemaGestion].[dbo].[Usuario] " +
                    "SET NombreUsuario = @nombreUsuario " +
                    "WHERE Id = @id;";

                SqlParameter id = new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Id };
                SqlParameter nombreUsuario = new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(id);
                    sqlCommand.Parameters.Add(nombreUsuario);

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

        public static bool CrearUsuario(Usuario usuario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO [SistemaGestion].[dbo].[Usuario]" +
                    "(Nombre, Apellido, NombreUsuario, Contraseña, Mail) VALUES" +
                    "(@nombreParameter, @apellidoParameter, @nombreUsuarioParameter, @contraseñaParameter, @mailParameter);";

                SqlParameter nombreParameter = new SqlParameter("nombreParameter", SqlDbType.VarChar) { Value = usuario.Nombre };
                SqlParameter apellidoParameter = new SqlParameter("apellidoParameter", SqlDbType.VarChar) { Value = usuario.Apellido };
                SqlParameter nombreUsuarioParameter = new SqlParameter("nombreUsuarioParameter", SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                SqlParameter contraseñaParameter = new SqlParameter("contraseñaParameter", SqlDbType.VarChar) { Value = usuario.Contraseña };
                SqlParameter mailParameter = new SqlParameter("mailParameter", SqlDbType.VarChar) { Value = usuario.Mail };

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(nombreParameter);
                    sqlCommand.Parameters.Add(apellidoParameter);
                    sqlCommand.Parameters.Add(nombreUsuarioParameter);
                    sqlCommand.Parameters.Add(contraseñaParameter);
                    sqlCommand.Parameters.Add(mailParameter);

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
    }
}
