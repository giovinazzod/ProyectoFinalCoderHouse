using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Controllers.DTO;
using ProyectoFinalCoderHouse.Model;
using ProyectoFinalCoderHouse.ADO.Net;

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


        [HttpDelete (Name = "EliminarUsuario")]
        public void EliminarUsuario([FromBody] int Id)
        {
            UsuarioHandler.EliminarUsuario(Id);
            Console.WriteLine("Usuario eliminado");
        }

        [HttpPut]
        public void ModificarUsuario([FromBody] PutUsuario usuario)
        {
            Console.WriteLine("Usuario actualizado");
        }

        [HttpPost]
        public void CrearUsuario([FromBody] PostUsuario usuario)
        {
            Console.WriteLine("Usuario creado");
        }
    }
}
