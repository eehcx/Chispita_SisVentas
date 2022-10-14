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
using capaPresentacion.Modales;

namespace capaPresentacion
{
    public partial class frmVentasRegistrar : Form
    {
        private usuarios _usuarios;
        public frmVentasRegistrar(usuarios oUsuarios = null) 
        {
            _usuarios = oUsuarios;
            InitializeComponent();
        }

        private void btnBuscarProductos_Click_1(object sender, EventArgs e)
        {
            using (var modal = new mdProductos())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._producto.IdProductos.ToString();
                    txtCodProducto.Text = modal._producto.codigo;
                    txtProducto.Text = modal._producto.nombre;
                    txtPrecioVenta.Text = modal._producto.precioVenta.ToString();
                    txtStock.Text = modal._producto.stock.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCodProducto.Select();
                }
            }
        }

        private void frmVentasRegistrar_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cboTipoDocumento.DisplayMember = "Texto";
            cboTipoDocumento.ValueMember = "Valor";
            cboTipoDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdProducto.Text = "";

            txtPagaCon.Text = "";
            txtCambio.Text = "";
            txtTotalPagar.Text = "0.00";
        }

        private void txtCodProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                productos oProducto = new CN_Productos().Listar().Where(p => p.codigo == txtCodProducto.Text && p.estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtCodProducto.BackColor = Color.Honeydew;
                    txtIdProducto.Text = oProducto.IdProductos.ToString();
                    txtProducto.Text = oProducto.nombre;
                    txtPrecioVenta.Text = oProducto.precioVenta.ToString();
                    txtStock.Text = oProducto.stock.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCodProducto.BackColor = Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtProducto.Text = "";
                    txtPrecioVenta.Text = "";
                    txtStock.Text = "";
                    txtCantidad.Value = 1;
                    txtCodProducto.Select();
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            bool producto_existe = false;

            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (int.Parse(txtCantidad.Text) > int.Parse(txtStock.Text))
            {
                MessageBox.Show("La cantidad no puede ser mayor al stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                limpiarProductos();
                txtCodProducto.Select();
                return;
            }

            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    producto_existe = true;
                    break;
                }
            }


            if (!producto_existe)
            {

                string mensaje = string.Empty;
                bool respuesta = new CN_Ventas().restarStock(
                    Convert.ToInt32(txtIdProducto.Text),
                    Convert.ToInt32(txtCantidad.Text)
                );
                if (respuesta)
                {
                    dgvData.Rows.Add(new object[]{
                        txtIdProducto.Text,
                        txtProducto.Text,
                        txtPrecioVenta.Text,
                        txtCantidad.Value.ToString(),
                        (txtCantidad.Value * Convert.ToDecimal(txtPrecioVenta.Text)).ToString("0.00")
                    });
                    calcularTotal();
                    limpiarProductos();
                    txtCodProducto.Select();
                }
                
            }
        }


        private void limpiarProductos()
        {
            txtIdProducto.Text = "0";
            txtCodProducto.Text = "";
            txtCodProducto.BackColor = Color.White;
            txtProducto.Text = "";
            txtPrecioVenta.Text = "0.00";
            txtCantidad.Value = 1;
            txtStock.Text = "";
            txtCodProducto.Select();
        }

        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    total += Convert.ToDecimal(row.Cells["subTotal"].Value.ToString());
                }
            }
            txtTotalPagar.Text = total.ToString("0.00");
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.icons8_remove_48.Width;
                var h = Properties.Resources.icons8_remove_48.Height;

                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.icons8_remove_48, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    bool respuesta = new CN_Ventas().sumarStock(
                        Convert.ToInt32(dgvData.Rows[indice].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvData.Rows[indice].Cells["Cantidad"].Value.ToString())
                    );

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(indice);
                        calcularTotal();
                    }
                    
                }
            }
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecioVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void txtPagaCon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPagaCon.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                else
                {
                    if (char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ".")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        private void calcularCambio()
        {
            if (txtTotalPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagaCon;
            decimal Total = Convert.ToDecimal(txtTotalPagar.Text);

            if (txtPagaCon.Text.Trim() == "")
            {
                txtPagaCon.Text = "0";
            }

            if (decimal.TryParse(txtPagaCon.Text.Trim(), out pagaCon))
            {
                if (pagaCon < Total)
                {
                    txtCambio.Text = "0.00";
                }
                else
                {
                    txtCambio.Text = "";
                    decimal cambio = pagaCon - Total;
                    txtCambio.Text = cambio.ToString("0.00");
                }
            }

        }

        private void txtPagaCon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularCambio();
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe agregar productos a la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodProducto.Select();
                return;
            }

            DataTable detalle_venta = new DataTable();

            detalle_venta.Columns.Add("IdProducto", typeof(int));
            detalle_venta.Columns.Add("precioVenta", typeof(decimal));
            detalle_venta.Columns.Add("cantidad", typeof(int));
            detalle_venta.Columns.Add("subtotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                detalle_venta.Rows.Add(
                    Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()),
                    Convert.ToDecimal(row.Cells["PrecioVenta"].Value.ToString()),
                    Convert.ToInt32(row.Cells["Cantidad"].Value.ToString()),
                    Convert.ToDecimal(row.Cells["subTotal"].Value.ToString())
                );
            }
            int IdCorrelativo = new CN_Ventas().obtenerCorrelativo();
            string numero_Documento = string.Format("{0:00000}", IdCorrelativo);
            calcularCambio();


            ventas oVentas = new ventas()
            {
                ousuarios = new usuarios() { IdUsuarios = _usuarios.IdUsuarios},
                tipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                numeroDocumento = numero_Documento,
                montoPago = Convert.ToDecimal(txtPagaCon.Text),
                montoCambio = Convert.ToDecimal(txtCambio.Text),
                montoTotal = Convert.ToDecimal(txtTotalPagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Ventas().Registrar(oVentas, detalle_venta, out mensaje);
            
            if (respuesta)
            {
                var result = MessageBox.Show("Numero de venta generada: \n" + numero_Documento + "\n\n¿Desea copiar al" +
                    " portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numero_Documento);
                }


                dgvData.Rows.Clear();
                calcularTotal();
                txtPagaCon.Text = "0.00";
                txtCambio.Text = "0.00";
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
