using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using capaNegocio;
using capaEntidad;

namespace capaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        /*Cerrar - Aplicación*/
        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        /*Ingresar - Al Menú principal*/
        private void btn_ingresar_Click(object sender, EventArgs e)
        {

            List<usuarios> TEST = new CN_Usuarios().Listar();

            usuarios oUsuarios = new CN_Usuarios().Listar().Where(u => u.documento == txtdocumento.Text && u.contrasena == txtcontrasena.Text).FirstOrDefault();

            //Validación de controles
            if (txtdocumento.Text == "")
            {
                MessageBox.Show("Falta Ingresar el Usuario", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdocumento.Focus();
                return;
            }
            if (txtcontrasena.Text == "")
            {
                MessageBox.Show("Falta Ingresar una Contraseña", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcontrasena.Focus();
                return;
            }

            //Validación de usuarios
            if (oUsuarios != null)
            {
                Inicio form = new Inicio(oUsuarios);

                form.Show();
                this.Hide(); //Hide()

                form.FormClosing += frm_clossing;

            }
            else
            {
                MessageBox.Show("No se encontro el usuarios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        /**/
        private void frm_clossing(object sender, FormClosingEventArgs e)
        {
            txtdocumento.Text = "";
            txtcontrasena.Text = "";
            this.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
