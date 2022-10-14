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
using ClosedXML.Excel;

namespace capaPresentacion
{
    public partial class frmReporteVentas : Form
    {
        public frmReporteVentas()
        {
            InitializeComponent();
        }

        private void frmReporteVentas_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn item in dgvData.Columns)
            {
                cboBusqueda.Items.Add(new OpcionCombo() { Valor = item.Name, Texto = item.HeaderText });
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;
        }

        private void btnBuscarResultado_Click(object sender, EventArgs e)
        {

            DateTime dt1 = Convert.ToDateTime(dtpFechaInicio.Value.ToString("dd/MM/yyyy"));
            DateTime dt2 = Convert.ToDateTime(dtpFechaFin.Value.ToString("dd/MM/yyyy"));
            List<reporteVentas> lista = new CN_Reporte().ventas(dt1.ToString("yyyy-MM-dd"), dt2.ToString("yyyy-MM-dd"));

            dgvData.Rows.Clear();

            foreach (reporteVentas rc in lista)
            {
                dgvData.Rows.Add(new object[]{
                    rc.fechaRegistro,
                    rc.tipoDocumento,
                    rc.numeroDocumento,
                    rc.montoTotal,
                    rc.usuarioRegistro,
                    rc.codigoProducto,
                    rc.nombreProducto,
                    rc.categoria,
                    rc.precioVenta,
                    rc.cantidad,
                    rc.subtotal
                    });
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn columna in dgvData.Columns)
                {
                    if (columna.HeaderText != "" && columna.Visible)
                    {
                        dt.Columns.Add(columna.HeaderText, typeof(string));
                    }
                }

                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible)
                    {
                        dt.Rows.Add(new object[] {
                            row.Cells["FechaRegistro"].Value.ToString(),
                            row.Cells["tipoDocumento"].Value.ToString(),
                            row.Cells["numeroDocumento"].Value.ToString(),
                            row.Cells["montoTotal"].Value.ToString(),
                            row.Cells["usuarioRegistro"].Value.ToString(),
                            row.Cells["codigoProducto"].Value.ToString(),
                            row.Cells["nombreProducto"].Value.ToString(),
                            row.Cells["Categoria"].Value.ToString(),
                            row.Cells["precioCompra"].Value.ToString(),
                            row.Cells["precioVenta"].Value.ToString(),
                            row.Cells["cantidad"].Value.ToString(),
                            row.Cells["totalCompra"].Value.ToString()
                        });
                    }
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteVentas_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss"));
                saveFile.Filter = "Excel Files |*.xlsx";

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(saveFile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
