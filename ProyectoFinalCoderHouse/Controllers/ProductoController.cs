using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.ADO.Net;
using ProyectoFinalCoderHouse.Model;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ProductoController : ControllerBase
    {

        [HttpGet(Name = "GetProductos")]
        public static List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }
    }
}
