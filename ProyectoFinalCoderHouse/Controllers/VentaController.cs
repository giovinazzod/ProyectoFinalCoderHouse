using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Model;
using ProyectoFinalCoderHouse.Repository;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class VentaController : ControllerBase
    {
        [HttpGet(Name = "GetVentas")]
        public List<Venta> GetVentas()
        {
            return VentaHandler.GetVentas();
        }

        [HttpGet("{idUsuario}")]
        public List<Venta> GetVentasByIdUsuario(int idUsuario)
        {
            return VentaHandler.TraerVentas(idUsuario);
        }

    }
}
