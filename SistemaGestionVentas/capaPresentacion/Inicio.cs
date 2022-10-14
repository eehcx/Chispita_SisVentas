using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using capaEntidad;
using capaNegocio;
using FontAwesome.Sharp;

namespace capaPresentacion
{
    public partial class Inicio : Form
    {
        private static usuarios usuarioActual;
        private static IconMenuItem menuActivo = null;
        private static Form formularioActivo=null;


        public Inicio(usuarios objusuarios)
        {
            usuarioActual = objusuarios;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<permisos> listaPermisos = new CN_Permisos().Listar(usuarioActual.IdUsuarios);

            foreach (IconMenuItem iconmenu in menu.Items)
            {
                bool encontrado =listaPermisos.Any(m => m.nombre == iconmenu.Name);

                if (encontrado == false)
                {
                    iconmenu.Visible = false;
                }
            }

            lblUsuario.Text = usuarioActual.nombre;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //iconMenuItem1
        }

        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (menuActivo !=null)
            {
                menuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Gainsboro;
            
            menuActivo = menu;

            if (formularioActivo !=null)
            {
                formularioActivo.Close();
            }
            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock= DockStyle.Fill;
            formulario.BackColor = Color.Gainsboro; //Color.FromArgb(206, 94, 101)

            contenedor.Controls.Add(formulario);
            formulario.Show();

        }

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuario());

        }

        private void subCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmCategorias());
        }

        private void subProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmProductos());
        }
        
        private void subNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuMantenedor, new frmNegocio());
        }

        private void subVentaRegistrar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuVentas, new frmVentasRegistrar(usuarioActual));
        }

        private void subVentaDetalle_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuVentas, new frmVentasDetalle());
        }

        private void subCompraRegistrar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCompras, new frmComprasRegistrar(usuarioActual));
        }

        private void subCompraDetalle_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuCompras, new frmComprasDetalle());
        }

        private void menuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        private void reporteVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuReportes, new frmReporteVentas());
        }

        private void reporteComprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuReportes, new frmReporteCompras());
        }

        private void menuTexto_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
