using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Model;
using ProyectoFinalCoderHouse.Repository;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet(Name = "GetProductosVendidos")]
        public List<ProductoVendido> GetProductosVendidos()
        {
            return ProductoVendidoHandler.GetProductosVendidos();
        }

        [HttpGet ("{idUsuario}")]
        public List<ProductoVendido> GetProductosVendidosByIdUsuario(int idUsuario)
        {
            return ProductoVendidoHandler.TraerProductosVendidos(idUsuario);
        }

    }
}
