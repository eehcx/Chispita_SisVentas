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

namespace capaPresentacion.Modales
{
    public partial class mdProveedor : Form
    {
        public proveedores _proveedor { get; set; }
        public mdProveedor()
        {
            InitializeComponent();
        }

        private void modalProveedor_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true)
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            // Mostrar todos los proveedores
            List<proveedores> listaProveedores = new CN_Proveedores().Listar();

            foreach (proveedores item in listaProveedores)
            {
                dgvData.Rows.Add(new object[] {"",item.IdProveedores,item.documento,item.razonSocial
                });
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

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int iRow = e.RowIndex;
            int iCol = e.ColumnIndex;

            if (iRow >= 0 && iCol > 0)
            {
                _proveedor = new proveedores()
                {
                    IdProveedores = Convert.ToInt32(dgvData.Rows[iRow].Cells["Id"].Value.ToString()),
                    documento = dgvData.Rows[iRow].Cells["documento"].Value.ToString(),
                    razonSocial = dgvData.Rows[iRow].Cells["razonSocial"].Value.ToString()
                };
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
