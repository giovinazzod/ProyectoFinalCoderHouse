namespace ProyectoFinalCoderHouse.Repository
{
    public class NombreDelSistemaHandler
    {
        /// <summary> 
        /// Trae el nombre del sistema
        /// </summary> 
        public static string TraerNombreApp()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            string nombreApp = domain.FriendlyName;
            return nombreApp;
        }
    }
}
