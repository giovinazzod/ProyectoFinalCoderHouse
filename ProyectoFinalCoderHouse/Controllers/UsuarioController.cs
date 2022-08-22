using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Controllers.DTOS;
using ProyectoFinalCoderHouse.Model;
using ProyectoFinalCoderHouse.Repository;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();
        }


        //[HttpGet("{nombreUsuario}/{contraseña}")] // Recibimos los parámetros por path
        //public bool Login(string nombreUsuario, string contraseña)
        //{
        //    return UsuarioHandler.Login(nombreUsuario, contraseña);
        //}

        [HttpGet("{nombreUsuario}/{contraseña}")] // Recibimos los parámetros por path
        public Usuario IniciarSesion(string nombreUsuario, string contraseña)
        {
            return UsuarioHandler.IniciarSesion(nombreUsuario, contraseña);
        }

        [HttpGet("{nombreUsuario}")]
        public Usuario TraerUsuario(string nombreUsuario)
        {
            return UsuarioHandler.TraerUsuario(nombreUsuario);
        }

        //[HttpGet("{id}")]
        //public List<Usuario> TraerUsuario(int id)
        //{
        //    return UsuarioHandler.TraerUsuario(id);
        //}

        [HttpDelete] // Recibimos los parámetros por Body
        public bool EliminarUsuario([FromBody] int id)
        {
            try
            {
                return UsuarioHandler.EliminarUsuario(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut]   //  UPDATE
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.ModificarNombreDeUsuario(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.NombreUsuario
            });
        }

        [HttpPost]  //  INSERT
        public bool CrearUsuario([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.CrearUsuario(new Usuario
                {
                    Apellido = usuario.Apellido,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail,
                    Nombre = usuario.Nombre,
                    NombreUsuario = usuario.NombreUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
