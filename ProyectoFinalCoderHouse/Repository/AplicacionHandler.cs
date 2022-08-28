namespace ProyectoFinalCoderHouse.Repository
{
    public class AplicacionHandler
    {
        IConfiguration configuration;
        public AplicacionHandler(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }

        public string TraerNombre()
        {
            return configuration.GetSection("NameApp").Value;
        }




    }
}
