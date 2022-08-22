namespace ProyectoFinalCoderHouse.Repository
{
    public class NombreDelSistemaHandler
    {
        public static string TraerNombreApp()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            string nombreApp = domain.FriendlyName;
            return nombreApp;
        }
    }
}
