using Capa_Entidad;
using Capa_de_datos;
using Capa_Logica;

namespace Capa_Presentacion
{
    public partial class SalonPrincipal : Form
    {
        private readonly MesaService _mesaService;
        private readonly string _tipoAtencion;

        // Constructor por defecto (requerido por Designer)
        public SalonPrincipal() : this("Salon") { }

        // Constructor parametrizado
        public SalonPrincipal(string tipoAtencion)
        {
            InitializeComponent();
            _tipoAtencion = tipoAtencion;

            var conexion = new ConexionSQL();
            _mesaService = new MesaService(conexion);

            if (tipoAtencion == "Delivery")
            {
                this.Text = "Delivery - Seleccionar Pedido";
                button1.Text = "Pedido 1";
                button2.Text = "Pedido 2";
                button15.Text = "Pedido 3";
                button14.Text = "Pedido 4";
                button13.Text = "Pedido 5";
                button8.Text = "Pedido 6";
                button9.Text = "Pedido 7";
                button10.Text = "Pedido 8";
                button11.Text = "Pedido 9";
                button12.Text = "Pedido 10";
                button7.Text = "Pedido 11";
                button6.Text = "Pedido 12";
                button3.Text = "Pedido 13";
                button5.Text = "Pedido 14";
                button4.Text = "Pedido 15";
                button20.Text = "Pedido 16";
                button19.Text = "Pedido 17";
                button18.Text = "Pedido 18";
                button17.Text = "Pedido 19";
                button16.Text = "Pedido 20";
            }
            else
            {
                this.Text = "Salón Principal - Seleccione una Mesa";
            }
        }

        private void AbrirPedidoMesa(int numeroMesa)
        {
            var pedidoForm = new Pedio2(_tipoAtencion, numeroMesa);
            pedidoForm.Show();
            this.Hide();
        }

        // ── Mapeo de botones a números de mesa ──
        private void button1_Click(object sender, EventArgs e) => AbrirPedidoMesa(1);
        private void button2_Click(object sender, EventArgs e) => AbrirPedidoMesa(2);
        private void button15_Click(object sender, EventArgs e) => AbrirPedidoMesa(3);
        private void button14_Click(object sender, EventArgs e) => AbrirPedidoMesa(4);
        private void button13_Click(object sender, EventArgs e) => AbrirPedidoMesa(5);
        private void button8_Click(object sender, EventArgs e) => AbrirPedidoMesa(6);
        private void button9_Click(object sender, EventArgs e) => AbrirPedidoMesa(7);
        private void button10_Click(object sender, EventArgs e) => AbrirPedidoMesa(8);
        private void button11_Click(object sender, EventArgs e) => AbrirPedidoMesa(9);
        private void button12_Click(object sender, EventArgs e) => AbrirPedidoMesa(10);
        private void button7_Click(object sender, EventArgs e) => AbrirPedidoMesa(11);
        private void button6_Click(object sender, EventArgs e) => AbrirPedidoMesa(12);
        private void button3_Click(object sender, EventArgs e) => AbrirPedidoMesa(13);
        private void button5_Click(object sender, EventArgs e) => AbrirPedidoMesa(14);
        private void button4_Click(object sender, EventArgs e) => AbrirPedidoMesa(15);
        private void button20_Click(object sender, EventArgs e) => AbrirPedidoMesa(16);
        private void button19_Click(object sender, EventArgs e) => AbrirPedidoMesa(17);
        private void button18_Click(object sender, EventArgs e) => AbrirPedidoMesa(18);
        private void button17_Click(object sender, EventArgs e) => AbrirPedidoMesa(19);
        private void button16_Click(object sender, EventArgs e) => AbrirPedidoMesa(20);

        private void button21_Click(object sender, EventArgs e) // Volver al menú principal
        {
            var frm = new Form1();
            frm.Show();
            this.Close();
        }
    }
}
