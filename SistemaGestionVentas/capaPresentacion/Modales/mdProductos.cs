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
    public partial class mdProductos : Form
    {
        public productos _producto;
        
        public mdProductos()
        {
            InitializeComponent();
        }

        private void modalProductos_Load(object sender, EventArgs e)
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

            List<productos> lista = new CN_Productos().Listar();

            foreach (productos item in lista)
            {
                dgvData.Rows.Add(new object[] {
                    item.IdProductos,
                    item.codigo,
                    item.nombre,
                    item.precioCompra,
                    item.precioVenta,
                    item.stock,
                    item.ocategorias.IdCategorias,
                    item.ocategorias.descripcion
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
            int iColumn = e.ColumnIndex;
            if (iRow >= 0 && iColumn > 0)
            {
                _producto = new productos()
                {
                    IdProductos = Convert.ToInt32(dgvData.Rows[iRow].Cells["IdProducto"].Value.ToString()),
                    codigo = dgvData.Rows[iRow].Cells["codigo"].Value.ToString(),
                    nombre = dgvData.Rows[iRow].Cells["nombre"].Value.ToString(),
                    precioCompra = Convert.ToDecimal(dgvData.Rows[iRow].Cells["precioCompra"].Value.ToString()),
                    precioVenta = Convert.ToDecimal(dgvData.Rows[iRow].Cells["precioVenta"].Value.ToString()),
                    stock = Convert.ToInt32(dgvData.Rows[iRow].Cells["stock"].Value.ToString()),
                };
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
