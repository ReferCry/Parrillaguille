namespace Capa_Presentacion
{
    partial class Venta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Venta));
            groupBox4 = new GroupBox();
            rboBoleta = new RadioButton();
            rbdFactura = new RadioButton();
            txtRUC = new TextBox();
            label2 = new Label();
            cboMetodo = new ComboBox();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            button5 = new Button();
            dateTimePicker1 = new DateTimePicker();
            groupBox4.SuspendLayout();
            SuspendLayout();
            //
            // groupBox4
            //
            groupBox4.BackgroundImage = (Image)resources.GetObject("groupBox4.BackgroundImage")!;
            groupBox4.BackgroundImageLayout = ImageLayout.Stretch;
            groupBox4.Controls.Add(dateTimePicker1);
            groupBox4.Controls.Add(rboBoleta);
            groupBox4.Controls.Add(rbdFactura);
            groupBox4.Controls.Add(txtRUC);
            groupBox4.Controls.Add(label2);
            groupBox4.Controls.Add(cboMetodo);
            groupBox4.Controls.Add(textBox5);
            groupBox4.Controls.Add(textBox4);
            groupBox4.Controls.Add(textBox3);
            groupBox4.Controls.Add(textBox2);
            groupBox4.Controls.Add(textBox1);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(label14);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(label10);
            groupBox4.Location = new Point(196, 12);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(409, 426);
            groupBox4.TabIndex = 22;
            groupBox4.TabStop = false;
            groupBox4.Text = "Datos del Cliente";
            //
            // rboBoleta
            //
            rboBoleta.AutoSize = true;
            rboBoleta.Location = new Point(176, 22);
            rboBoleta.Name = "rboBoleta";
            rboBoleta.Size = new Size(58, 19);
            rboBoleta.TabIndex = 22;
            rboBoleta.TabStop = true;
            rboBoleta.Text = "Boleta";
            rboBoleta.TextImageRelation = TextImageRelation.TextBeforeImage;
            rboBoleta.UseVisualStyleBackColor = true;
            rboBoleta.CheckedChanged += rboBoleta_CheckedChanged;
            //
            // rbdFactura
            //
            rbdFactura.AutoSize = true;
            rbdFactura.Location = new Point(34, 22);
            rbdFactura.Name = "rbdFactura";
            rbdFactura.Size = new Size(64, 19);
            rbdFactura.TabIndex = 21;
            rbdFactura.TabStop = true;
            rbdFactura.Text = "Factura";
            rbdFactura.TextImageRelation = TextImageRelation.TextBeforeImage;
            rbdFactura.UseVisualStyleBackColor = true;
            rbdFactura.CheckedChanged += rbdFactura_CheckedChanged;
            //
            // txtRUC
            //
            txtRUC.Location = new Point(83, 54);
            txtRUC.Name = "txtRUC";
            txtRUC.Size = new Size(216, 23);
            txtRUC.TabIndex = 20;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Location = new Point(6, 57);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 19;
            label2.Text = "RUC";
            //
            // cboMetodo
            //
            cboMetodo.FormattingEnabled = true;
            cboMetodo.Location = new Point(116, 315);
            cboMetodo.Name = "cboMetodo";
            cboMetodo.Size = new Size(176, 23);
            cboMetodo.TabIndex = 18;
            //
            // textBox5
            //
            textBox5.Location = new Point(84, 271);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(204, 23);
            textBox5.TabIndex = 12;
            textBox5.ReadOnly = true;
            //
            // textBox4
            //
            textBox4.Location = new Point(83, 229);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(216, 23);
            textBox4.TabIndex = 11;
            textBox4.ReadOnly = true;
            //
            // textBox3
            //
            textBox3.Location = new Point(83, 188);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(216, 23);
            textBox3.TabIndex = 10;
            //
            // textBox2
            //
            textBox2.Location = new Point(83, 141);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(216, 23);
            textBox2.TabIndex = 9;
            //
            // textBox1
            //
            textBox1.Location = new Point(83, 97);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(216, 23);
            textBox1.TabIndex = 8;
            //
            // label15
            //
            label15.AutoSize = true;
            label15.Location = new Point(6, 279);
            label15.Name = "label15";
            label15.Size = new Size(72, 15);
            label15.TabIndex = 7;
            label15.Text = "Monto Total";
            //
            // label14
            //
            label14.AutoSize = true;
            label14.Location = new Point(6, 315);
            label14.Name = "label14";
            label14.Size = new Size(98, 15);
            label14.TabIndex = 6;
            label14.Text = "Método de pago";
            //
            // label13
            //
            label13.AutoSize = true;
            label13.Location = new Point(15, 237);
            label13.Name = "label13";
            label13.Size = new Size(38, 15);
            label13.TabIndex = 5;
            label13.Text = "MESA";
            //
            // label12
            //
            label12.AutoSize = true;
            label12.Location = new Point(6, 144);
            label12.Name = "label12";
            label12.Size = new Size(56, 15);
            label12.TabIndex = 4;
            label12.Text = "Apellidos";
            //
            // label11
            //
            label11.AutoSize = true;
            label11.Location = new Point(15, 188);
            label11.Name = "label11";
            label11.Size = new Size(27, 15);
            label11.TabIndex = 3;
            label11.Text = "DNI";
            //
            // label10
            //
            label10.AutoSize = true;
            label10.Location = new Point(6, 105);
            label10.Name = "label10";
            label10.Size = new Size(56, 15);
            label10.TabIndex = 2;
            label10.Text = "Nombres";
            //
            // button5
            //
            button5.Image = (Image)resources.GetObject("button5.Image")!;
            button5.Location = new Point(690, 21);
            button5.Name = "button5";
            button5.Size = new Size(79, 32);
            button5.TabIndex = 23;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            //
            // dateTimePicker1
            //
            dateTimePicker1.Location = new Point(181, 369);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 23;
            //
            // Venta
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(800, 450);
            Controls.Add(button5);
            Controls.Add(groupBox4);
            Name = "Venta";
            Text = "Registrar Venta";
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox4;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label label15;
        private Label label14;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private ComboBox cboMetodo;
        private TextBox txtRUC;
        private Label label2;
        private RadioButton rbdFactura;
        private RadioButton rboBoleta;
        private Button button5;
        private DateTimePicker dateTimePicker1;
    }
}
