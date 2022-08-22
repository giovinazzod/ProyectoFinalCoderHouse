using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Model;
using ProyectoFinalCoderHouse.Repository;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProductos")]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }

        [HttpGet("{idUsuario}")]
        public List<Producto> GetProductosByIdUsuario(int idUsuario)
        {
            return ProductoHandler.TraerProductos(idUsuario);
        }
    }
}
