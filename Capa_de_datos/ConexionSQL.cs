using Microsoft.Data.SqlClient;

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
            _cadenaConexion = @"Server=localhost;Database=ParrillaguilleDB;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }
    }
}
