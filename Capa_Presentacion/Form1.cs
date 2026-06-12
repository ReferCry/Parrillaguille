namespace Capa_Presentacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "Parrillaguille - Sistema de Ventas";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SalonPrincipal frm = new SalonPrincipal("Salon");
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SalonPrincipal frm = new SalonPrincipal("Delivery");
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dashboard = new Dashboard();
            dashboard.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Menu frm = new Menu();
            frm.Show();
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
