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
    public partial class frmProveedores : Form
    {
        public frmProveedores()
        {
            InitializeComponent();
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            cboEstados.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstados.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            cboEstados.DisplayMember = "Texto";
            cboEstados.ValueMember = "Valor";
            cboEstados.SelectedIndex = 0;


            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            // Mostrar todos los usuarios
            List<proveedores> listaProveedores = new CN_Proveedores().Listar();

            foreach (proveedores item in listaProveedores)
            {
                dgvData.Rows.Add(new object[] {"",item.IdProveedores,item.documento,item.razonSocial,item.correo,item.telefono,
                    item.estado == true ? 1 : 0,
                    item.estado == true ? "Activo" : "Inactivo"
                });
            }
        }

        /*
        PROCEDIMIENTOS 
        */

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            proveedores obj = new proveedores()
            {
                IdProveedores = Convert.ToInt32(txtId.Text),
                documento = txtIngresarDocumento.Text,
                razonSocial = txtIngresarRazonSocial.Text,
                correo = txtIngresarCorreo.Text,
                telefono = txtIngresarTelefono.Text,
                estado = Convert.ToInt32(((OpcionCombo)cboEstados.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdProveedores == 0)
            {
                int IdGenerado = new CN_Proveedores().Registrar(obj, out mensaje);

                if (IdGenerado != 0)
                {
                    // Registro - DataGridView
                    dgvData.Rows.Add(new object[] {"",IdGenerado,txtIngresarDocumento.Text,txtIngresarRazonSocial.Text,txtIngresarCorreo.Text,txtIngresarTelefono.Text,
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
                bool resultado = new CN_Proveedores().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["Documento"].Value = txtIngresarDocumento.Text;
                    row.Cells["razonSocial"].Value = txtIngresarRazonSocial.Text;
                    row.Cells["correo"].Value = txtIngresarCorreo.Text;
                    row.Cells["telefono"].Value = txtIngresarTelefono.Text;
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
        private void limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtIngresarDocumento.Text = "";
            txtIngresarRazonSocial.Text = "";
            txtIngresarCorreo.Text = "";
            txtIngresarTelefono.Text = "";
            cboEstados.SelectedIndex = 0;

            txtIngresarDocumento.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea Eliminar el Proveedor?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = String.Empty;
                    proveedores obj = new proveedores()
                    {
                        IdProveedores = Convert.ToInt32(txtId.Text)
                    };

                    bool respuesta = new CN_Proveedores().Eliminar(obj, out mensaje);

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

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.icons8_checkmark_48.Width;
                var h = Properties.Resources.icons8_checkmark_48.Height;

                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.icons8_checkmark_48, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();

                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtIngresarDocumento.Text = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtIngresarRazonSocial.Text = dgvData.Rows[indice].Cells["razonSocial"].Value.ToString();
                    txtIngresarCorreo.Text = dgvData.Rows[indice].Cells["correo"].Value.ToString();
                    txtIngresarTelefono.Text = dgvData.Rows[indice].Cells["telefono"].Value.ToString();

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

        
    }
}
