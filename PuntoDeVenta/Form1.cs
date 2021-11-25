using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PuntoDeVenta
{
    public partial class Form1 : Form
    {

        char x;
        String ca="", pago="";
        private Timer ti;
        String usuario;
        public Form1(String usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            ti = new Timer();
            ti.Tick += new EventHandler(eventoTimer);
            ti.Enabled = true;
            Screen screen = Screen.PrimaryScreen;

            int height = screen.Bounds.Width;

            int width = screen.Bounds.Height;

            //this.Size = new Size(height, width);

            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            

            this.pictureBox1.Location = new Point(Convert.ToInt32((height * 0.795)), Convert.ToInt32(width * 0.1));

            this.label1.Location = new Point(Convert.ToInt32(height * 0.82), Convert.ToInt32(width * 0.4));
            this.label2.Location = new Point(Convert.ToInt32(height * 0.82), Convert.ToInt32(width * 0.6));
            this.label3.Location = new Point(Convert.ToInt32(height * 0.82), Convert.ToInt32(width * 0.8));
            this.label4.Location = new Point(Convert.ToInt32(height * 0.82), Convert.ToInt32(width * 0.01));
            this.label5.Location = new Point(Convert.ToInt32(height * 0.9), Convert.ToInt32(width * 0.01));
            this.label6.Location = new Point(Convert.ToInt32(height * 0.82), Convert.ToInt32(width * 0.95));
            this.label8.Location = new Point(Convert.ToInt32(height * 0.82), Convert.ToInt32(width * 0.90));

            this.textBox1.Location = new Point(Convert.ToInt32(height * 0.82), Convert.ToInt32(width * 0.45));
            this.textBox2.Location = new Point(Convert.ToInt32(height * 0.82), Convert.ToInt32(width * 0.65));
            this.textBox3.Location = new Point(Convert.ToInt32(height * 0.82), Convert.ToInt32(width * 0.85));

            dataGridView1.Size = new Size(Convert.ToInt32(height-(height*0.3)), width-30);

            dataGridView1.Columns[0].Width = Convert.ToInt32(height - (height * 0.3)) / 4;
            dataGridView1.Columns[1].Width = Convert.ToInt32(height - (height * 0.3)) / 4;
            dataGridView1.Columns[2].Width = Convert.ToInt32(height - (height * 0.3)) / 4;
            dataGridView1.Columns[3].Width = Convert.ToInt32(height - (height * 0.3)) / 4;
            //dataGridView1.Rows.Size

            //dataGridView1.Size(height - (height * 0.70), width);
            //dataGridView1.Location
        }

        private void eventoTimer(object ob, EventArgs evt)
        {
            DateTime x = DateTime.Now;
            label4.Text = x.ToString("hh:mm:ss");
            label5.Text = x.ToString("dd:MM:yyyy");
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        //8:33
        public void ingresarPago(String pago)
        {
            double pag;

            pag = Convert.ToDouble(pago);

            if (Convert.ToDouble(label1) >= pag)
            {
                double cambio = (Convert.ToDouble(label1)) - pag;
                label3.Text = Convert.ToString(cambio);
                label1.Text = "";
                label2.Text = "";
            }
            else
            {
                MessageBox.Show("PAGO INSUFICIENTE");
            }
        }

        public void ejecutarConsulta(String codigo)
        {
            String x = "", img = "";
            int z = 0;
            int r = 0;
            Boolean yn = false;
            string cadenaConexion = "server=127.0.0.1; user=root; database=punto_de_venta; SSL Mode=None;";

            MySqlConnection servidor = new MySqlConnection(cadenaConexion);
            try
            {
                servidor.Open();
                string cons = "SELECT nombre_producto, precio FROM productos WHERE id_producto =" + codigo + ";";
                MySqlCommand consulta = new MySqlCommand(cons, servidor);
                MySqlDataReader resultado = consulta.ExecuteReader();
                if (resultado.HasRows)
                {
                    resultado.Read();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (codigo.Equals(row.Cells[0].Value))
                        { 
                            yn = true;
                            z = r;  
                        }
                        r++;
                    }
                    
                    if(yn == true)
                    {
                        dataGridView1.Rows[z].Cells[2].Value = Convert.ToInt32(dataGridView1.Rows[z].Cells[2].Value.ToString())+1;
                        total();
                    }
                    else
                    {
                        int n = dataGridView1.Rows.Add();

                        dataGridView1.Rows[n].Cells[0].Value = codigo;
                        dataGridView1.Rows[n].Cells[1].Value = resultado.GetString(0);
                        dataGridView1.Rows[n].Cells[3].Value = resultado.GetString(1);
                        int count = 0;

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[0].Value.ToString().Contains(codigo))
                            {
                                count++;
                            }
                        }

                        dataGridView1.Rows[n].Cells[2].Value = count;
                        count = 0;
                        //textBox1.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) + Convert.ToDouble(resultado.GetString(1)));

                        total();

                        ca = "";
                        textBox3.Text = "";
                    }

                }
                else
                {
                    MessageBox.Show("Producto NO encontrado");

                    ca = "";
                    textBox3.Text = "";
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(), "e", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("X");

                ca = "";
                textBox3.Text = "";

            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar != (char)Keys.Escape)&& (!(((Convert.ToString(e.KeyChar)).Equals(Convert.ToString(Keys.E))) || (Convert.ToString(e.KeyChar)).Equals("e"))))
            {
                ca = ca + e.KeyChar;
                //textBox3.Text = ca;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                ejecutarConsulta(ca);
            }

            if ((Convert.ToString(e.KeyChar).Equals("C")) || (Convert.ToString(e.KeyChar).Equals("c")))
            {
                Reportes r = new Reportes();
                r.Show();
            }

            if(e.KeyChar == (char)Keys.Space)
            {
                
                textBox2.Text = ca;

                
                ca = "";
                double pagx = 0;
                String texto = textBox1.Text;
                if (!String.IsNullOrEmpty(texto))
                {
                    pagx = Convert.ToDouble(textBox2.Text) - Convert.ToDouble(texto);
                    if (pagx >= 0)
                    {
                        textBox3.Text = Convert.ToString(pagx);

                        if (MessageBox.Show("¿Realizar venta?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                           
                            string cadenaConexion = "server=127.0.0.1; user=root; database=punto_de_venta; SSL Mode=None;";
                            MySqlConnection servidor = new MySqlConnection(cadenaConexion);
                            String queryventasdetalle = "INSERT INTO ventas_detalle (id_producto, cantidad, precio_producto) VALUES (@id_producto, @cantidad, @precio_producto)";
                            String queryventas = "INSERT INTO ventas (fechaventa, horaventa, operadorVenta) VALUES (@fechaventa, @horaventa, @operadorVenta)";
                            servidor.Open();
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                          
                                MySqlCommand command1 = new MySqlCommand(queryventas, servidor);
                                command1.Parameters.AddWithValue("@fechaventa", DateTime.UtcNow.ToString("yyyy-MM-dd"));
                                command1.Parameters.AddWithValue("@horaventa", label4.Text);
                                command1.Parameters.AddWithValue("@operadorVenta", this.usuario);
                                command1.ExecuteNonQuery();

                                MySqlCommand command = new MySqlCommand(queryventasdetalle, servidor);
                                command.Parameters.AddWithValue("@id_producto", row.Cells[0].Value);
                                command.Parameters.AddWithValue("@cantidad", row.Cells[2].Value);
                                command.Parameters.AddWithValue("@precio_producto", row.Cells[3].Value);
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Venta realizada");
                            limpiarPantalla();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Pago insuficiente");
                    }
                }
                else
                {
                    
                }
            }
            
            if(e.KeyChar == (char)Keys.Escape)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count-1);

                total();
                
            }
            if((Convert.ToString(e.KeyChar).Equals("D")) || (Convert.ToString(e.KeyChar).Equals("d")))
            {
                deposito d = new deposito();
                d.Show();
                
            }
        }

        public void limpiarPantalla()
        {
            dataGridView1.Rows.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        public void total()
        {
            textBox1.Text = "0";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                textBox1.Text = Convert.ToString(Convert.ToDouble(textBox1.Text) + Convert.ToDouble(row.Cells["Precio"].Value.ToString()) * Convert.ToDouble(row.Cells["Cantidad"].Value.ToString()));

            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            
            
        }

    }
}
