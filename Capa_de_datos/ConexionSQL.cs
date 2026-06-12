using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Capa_de_datos
{
    public class ConexionSQL
    {
        private readonly string _cadenaConexion;

        public ConexionSQL(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public ConexionSQL()
        {
            _cadenaConexion = ObtenerConnectionString();
        }

        private static string ObtenerConnectionString()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

                IConfiguration config = builder.Build();
                string? cadena = config.GetConnectionString("DefaultConnection");

                if (!string.IsNullOrWhiteSpace(cadena))
                    return cadena;
            }
            catch { }

            return @"Server=localhost;Database=ParrillaguilleDB;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}
