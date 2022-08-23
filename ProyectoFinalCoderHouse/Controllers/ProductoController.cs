using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Controllers.DTOS;
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

        [HttpPost]  //  INSERT
        public bool CrearProducto([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.CrearProducto(new Producto
                {
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpPut]   //  UPDATE
        public bool ModificarProducto([FromBody] PutProducto producto)
        {
            try
            {
                return ProductoHandler.ModificarProducto(new Producto
                {
                    Id = producto.Id,
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        [HttpDelete]
        public bool EliminarProducto(int id)
        {
            return ProductoHandler.EliminarProducto(id);
        }
    }
}
