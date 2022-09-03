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
            return VentaHandler.GetVentasByIdUsuario(idUsuario);
        }

        [HttpPost] // INSERT
        public void PostVenta(List<Producto> productos, int idUsuario)
        {
            VentaHandler.CargarVenta(productos, idUsuario);
        }

        [HttpDelete("{idVenta}")]

        public bool EliminarVenta(int idVenta)
        {
            try
            {
                return VentaHandler.EliminarVenta(idVenta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
