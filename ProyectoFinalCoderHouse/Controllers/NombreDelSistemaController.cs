using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Repository;

namespace ProyectoFinalCoderHouse.Controllers
{
    public class NombreDelSistemaController : ControllerBase
    {
        [HttpGet]
        [Route("[controller]")]

        public string TraerNombreDelSistema()
        {
            return NombreDelSistemaHandler.TraerNombreApp();
        }
    }
}
