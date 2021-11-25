using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVenta
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();


        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EJECUTAR CONSULTA PARA VALIDAR USUARIOS

            string cadenaConexion = "server=127.0.0.1; user=root; database=punto_de_venta; SSL Mode=None;";

            MySqlConnection servidor = new MySqlConnection(cadenaConexion);
            try
            {
                servidor.Open();

                string cons = "SELECT id_usuario, password FROM usuarios WHERE id_usuario = '" + textBox1.Text + "';";


                MySqlCommand consulta = new MySqlCommand(cons, servidor);

                MySqlDataReader resultado = consulta.ExecuteReader();

                if (resultado.Read())
                {
                    if (((resultado.GetString(0)).Equals(textBox1.Text) && (resultado.GetString(1)).Equals(textBox2.Text)))
                    {
                        Form1 vf = new Form1(textBox1.Text);
                        vf.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Error, usuario o contraseña incorrectos.");

                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                }
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(e.ToString(), "e", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("Error x");



            }

            
        }
    }
}
