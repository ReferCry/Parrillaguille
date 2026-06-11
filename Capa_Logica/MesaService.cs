using Capa_Entidad;
using Capa_de_datos;

namespace Capa_Logica
{
    public class MesaService
    {
        private readonly MesaRepository _repo;

        public MesaService(ConexionSQL conexion)
        {
            _repo = new MesaRepository(conexion);
        }

        public List<Mesa> ObtenerTodas() => _repo.ObtenerTodas();

        public void OcuparMesa(int idMesa) => _repo.ActualizarEstado(idMesa, "Ocupada");

        public void LiberarMesa(int idMesa) => _repo.ActualizarEstado(idMesa, "Libre");

        public void ReservarMesa(int idMesa) => _repo.ActualizarEstado(idMesa, "Reservada");
    }
}
