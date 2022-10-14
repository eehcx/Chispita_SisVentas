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
    public partial class frmProductos : Form
    {
        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            cboEstados.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstados.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Inactivo" });
            cboEstados.DisplayMember = "Texto";
            cboEstados.ValueMember = "Valor";
            cboEstados.SelectedIndex = 0;


            List<categorias> lista = new CN_Categoria().Listar();

            foreach (categorias item in lista)
            {
                cboCategoria.Items.Add(new OpcionCombo() { Valor = item.IdCategorias, Texto = item.descripcion });
            }
            cboCategoria.DisplayMember = "Texto";
            cboCategoria.ValueMember = "Valor";
            cboCategoria.SelectedIndex = 0;



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
            List<productos> listaProductos = new CN_Productos().Listar();

            foreach (productos item in listaProductos)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdProductos,
                    item.codigo,
                    item.nombre,
                    item.ocategorias.IdCategorias,
                    item.ocategorias.descripcion,
                    item.stock,
                    item.precioCompra,
                    item.precioVenta,
                    item.estado == true ? 1 : 0,
                    item.estado == true ? "Activo" : "Inactivo"
                });
            }
            
        }
        
        /*
        PROCEDIMIENTOS 
        */

        /*Botón - Guardar productos*/

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            productos obj = new productos()
            {
                IdProductos = Convert.ToInt32(txtId.Text),
                codigo = txtIngresarCodigo.Text,
                nombre = txtIngresarNombre.Text,
                ocategorias = new categorias() { IdCategorias = Convert.ToInt32(((OpcionCombo)cboCategoria.SelectedItem).Valor) },
                estado = Convert.ToInt32(((OpcionCombo)cboEstados.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdProductos == 0)
            {
                int IdGenerado = new CN_Productos().Registrar(obj, out mensaje);

                if (IdGenerado != 0)
                {
                    // Registro - DataGridView
                    dgvData.Rows.Add(new object[] {"",IdGenerado,txtIngresarCodigo.Text,txtIngresarNombre.Text,
                        ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString(),
                        "0",
                        "0.00",
                        "0.00",
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
                bool resultado = new CN_Productos().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["codigo"].Value = txtIngresarCodigo.Text;
                    row.Cells["Nombre"].Value = txtIngresarNombre.Text;
                    row.Cells["IdCategoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Valor.ToString();
                    row.Cells["Categoria"].Value = ((OpcionCombo)cboCategoria.SelectedItem).Texto.ToString();
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

        /*Limpiar - Cada vez que se Ingrese un productos*/

        private void limpiar()
        {
            txtId.Text = "0";
            txtIngresarCodigo.Text = string.Empty;
            txtIngresarNombre.Text = string.Empty;
            cboCategoria.SelectedIndex = 0;
            cboEstados.SelectedIndex = 0;
            txtIndice.Text = string.Empty;
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
                    txtIngresarCodigo.Text = dgvData.Rows[indice].Cells["codigo"].Value.ToString();
                    txtIngresarNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();

                    foreach (OpcionCombo oc in cboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            int indice_combo = cboCategoria.Items.IndexOf(oc);
                            cboCategoria.SelectedIndex = indice_combo;
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea Eliminar el producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = String.Empty;
                    productos obj = new productos()
                    {
                        IdProductos = Convert.ToInt32(txtId.Text)
                    };

                    bool respuesta = new CN_Productos().Eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        limpiar();
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

        private void btnDescargarExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
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
                            row.Cells["codigo"].Value.ToString(),
                            row.Cells["Nombre"].Value.ToString(),
                            row.Cells["Categoria"].Value.ToString(),
                            row.Cells["stock"].Value.ToString(),
                            row.Cells["precioCompra"].Value.ToString(),
                            row.Cells["precioVenta"].Value.ToString(),
                            row.Cells["Estado"].Value.ToString()
                            
                        });
                    }
                }

                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = string.Format("ReporteProductos_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmss"));
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

        private void cboBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
