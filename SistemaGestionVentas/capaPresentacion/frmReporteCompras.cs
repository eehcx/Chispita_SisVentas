using System;
using System.Collections.Generic;
using System.Windows.Forms;

using capaPresentacion.Utilidades;
using capaEntidad;
using capaNegocio;
using System.Data;
using ClosedXML.Excel;

namespace capaPresentacion
{
    public partial class frmReporteCompras : Form
    {
        public List<reporteCompras> CD_Reporte { get; private set; }

        public frmReporteCompras()
        {
            InitializeComponent();
        }

        private void frmReporteCompras_Load(object sender, EventArgs e)
        {

            List<proveedores> listp  = new CN_Proveedores().Listar();

            cboProveedor.Items.Add(new OpcionCombo { Valor = 0, Texto = "Todos" });
            foreach (proveedores item in listp)
            {
                cboProveedor.Items.Add(new OpcionCombo() { Valor = item.IdProveedores, Texto = item.razonSocial });
            }
            cboProveedor.DisplayMember = "Texto";
            cboProveedor.ValueMember = "Valor";
            cboProveedor.SelectedIndex = 0;
            
            

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
            int IdProveedores = Convert.ToInt32(((OpcionCombo)cboProveedor.SelectedItem).Valor.ToString());
            
            DateTime dt1 = Convert.ToDateTime(dtpFechaInicio.Value.ToString("dd/MM/yyyy"));
            DateTime dt2 = Convert.ToDateTime(dtpFechaFin.Value.ToString("dd/MM/yyyy"));
            List<reporteCompras> lista = new CN_Reporte().compras(dt1.ToString("yyyy-MM-dd"), dt2.ToString("yyyy-MM-dd"), IdProveedores);

            dgvData.Rows.Clear();

            foreach (reporteCompras rc in lista)
            {
                dgvData.Rows.Add(new object[]{
                    rc.fechaRegistro,
                    rc.tipoDocumento,
                    rc.numeroDocumento,
                    rc.montoTotal,
                    rc.usuarioRegistro,
                    rc.documentoProveedor,
                    rc.razonSocial,
                    rc.codigoProducto,
                    rc.nombreProducto,
                    rc.categoria,
                    rc.precioCompra,
                    rc.precioVenta,
                    rc.cantidad,
                    rc.totalCompra
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
                            row.Cells["documentoProveedor"].Value.ToString(),
                            row.Cells["razonSocial"].Value.ToString(),
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
                saveFile.FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss"));
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
