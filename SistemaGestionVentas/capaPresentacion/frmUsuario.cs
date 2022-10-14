using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using capaPresentacion.Utilidades;
using capaEntidad;
using capaNegocio;

namespace capaPresentacion
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {

            cboEstados.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstados.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            cboEstados.DisplayMember = "Texto";
            cboEstados.ValueMember = "Valor";
            cboEstados.SelectedIndex = 0; 
            


            List<roles> listaRoles = new CN_Roles().Listar();

            foreach(roles item in listaRoles){
                cboRoles.Items.Add(new OpcionCombo() { Valor = item.IdRoles, Texto = item.descripcion });
            }
            cboRoles.DisplayMember = "Texto";
            cboRoles.ValueMember = "Valor";
            cboRoles.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name!= "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            // Mostrar todos los usuarios
            List<usuarios> listaUsarios = new CN_Usuarios().Listar();

            foreach (usuarios item in listaUsarios)
            {
                dgvData.Rows.Add(new object[] {"",item.IdUsuarios,item.documento,item.nombre,item.apellidos,item.contrasena,
                    item.oroles.IdRoles,
                    item.oroles.descripcion,
                    item.estado == true ? 1 : 0,
                    item.estado == true ? "Activo" : "Inactivo"
                });
            }
        }

        /*
        PROCEDIMIENTOS 
        */

        /*Botón - Guardar usuario*/
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            usuarios objusuarios = new usuarios()
            {
                IdUsuarios = Convert.ToInt32(txtId.Text),
                documento = txtIngresarDocumento.Text,
                nombre = txtIngresarNombre.Text,
                apellidos = txtIngresarApellidos.Text,
                contrasena = txtIngresarContrasena.Text,
                oroles = new roles() { IdRoles = Convert.ToInt32(((OpcionCombo)cboRoles.SelectedItem).Valor) },
                estado = Convert.ToInt32(((OpcionCombo)cboEstados.SelectedItem).Valor) == 1 ? true:false 
            };

            if (objusuarios.IdUsuarios == 0)
            {
                int IdUsuarioGenerado = new CN_Usuarios().Registrar(objusuarios, out mensaje);

                if (IdUsuarioGenerado != 0)
                {
                    // Registro - DataGridView
                    dgvData.Rows.Add(new object[] {"",IdUsuarioGenerado,txtIngresarDocumento.Text,txtIngresarNombre.Text,txtIngresarApellidos.Text,txtIngresarContrasena.Text,
                ((OpcionCombo)cboRoles.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboRoles.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cboEstados.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboEstados.SelectedItem).Texto.ToString()
                });

                    limpiar(); //Implementar metodo
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_Usuarios().Editar(objusuarios, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Documento"].Value = txtIngresarDocumento.Text;
                    row.Cells["Nombre"].Value = txtIngresarNombre.Text;
                    row.Cells["Apellidos"].Value = txtIngresarApellidos.Text;
                    row.Cells["Contrasena"].Value = txtIngresarContrasena.Text;
                    row.Cells["IdRol"].Value = ((OpcionCombo)cboRoles.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cboRoles.SelectedItem).Texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstados.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstados.SelectedItem).Texto.ToString();
                    
                    limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }          
        }

        /*Limpiar - Cada vez que se Ingrese un usuario*/
        private void limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtIngresarDocumento.Text= "";
            txtIngresarNombre.Text = "";
            txtIngresarApellidos.Text = "";
            txtIngresarContrasena.Text = "";
            txtConfirmarContrasena.Text = "";
            cboRoles.SelectedIndex=0;
            cboEstados.SelectedIndex = 0;

            txtIngresarDocumento.Focus();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex <0)
                return;
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.icons8_checkmark_48.Width;
                var h = Properties.Resources.icons8_checkmark_48.Height;

                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.icons8_checkmark_48, new Rectangle(x,y,w,h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if(indice >= 0)
                {
                    txtIndice.Text=indice.ToString();

                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtIngresarDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtIngresarNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtIngresarApellidos.Text = dgvData.Rows[indice].Cells["Apellidos"].Value.ToString();
                    txtIngresarContrasena.Text = dgvData.Rows[indice].Cells["Contrasena"].Value.ToString();
                    txtConfirmarContrasena.Text = dgvData.Rows[indice].Cells["Contrasena"].Value.ToString();

                    foreach(OpcionCombo oc in cboRoles.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indice_combo = cboRoles.Items.IndexOf(oc);
                            cboRoles.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in cboEstados.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboEstados.Items.IndexOf(oc);
                            cboEstados.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea Eliminar el Usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = String.Empty;
                    usuarios objusuarios = new usuarios()
                    {
                        IdUsuarios = Convert.ToInt32(txtId.Text)
                    };

                    bool respuesta = new CN_Usuarios().Eliminar(objusuarios, out mensaje);

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columaFiltro = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            txtBusqueda.Focus();

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtIngresarDocumento.Text = "";
            txtIngresarNombre.Text = "";
            txtIngresarApellidos.Text = "";
            txtIngresarContrasena.Text = "";
            txtConfirmarContrasena.Text = "";
            cboRoles.SelectedIndex = 0;
            cboEstados.SelectedIndex = 0;
        }
    }
}
