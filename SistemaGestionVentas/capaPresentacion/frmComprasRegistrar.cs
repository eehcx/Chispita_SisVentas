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
    public partial class frmComprasRegistrar : Form
    {
        private usuarios _usuarios;
        public frmComprasRegistrar(usuarios oUsuarios = null)
        {
            _usuarios = oUsuarios;
            InitializeComponent();
        }

        private void btnBuscarProveedores_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProveedor.Text = modal._proveedor.IdProveedores.ToString();
                    txtDocumentoProveedor.Text = modal._proveedor.documento;
                    txtRazonSocial.Text = modal._proveedor.razonSocial;
                }
                else
                {
                    txtDocumentoProveedor.Select();
                }
            }
        }

        private void btnBuscarProductos_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProductos())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._producto.IdProductos.ToString();
                    txtCodigoProducto.Text = modal._producto.codigo;
                    txtNombreProducto.Text = modal._producto.nombre;
                    txtPrecioCompra.Select();
                }
                else
                {
                    txtCodigoProducto.Select();
                }
            }
        }

        private void frmComprasRegistrar_Load(object sender, EventArgs e)
        {
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
            cboTipoDocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
            cboTipoDocumento.DisplayMember = "Texto";
            cboTipoDocumento.ValueMember = "Valor";
            cboTipoDocumento.SelectedIndex = 0;
            
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void txtCodigoProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                productos oProducto = new CN_Productos().Listar().Where(p => p.codigo == txtCodigoProducto.Text && p.estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtCodigoProducto.BackColor = Color.Honeydew;
                    txtIdProducto.Text = oProducto.IdProductos.ToString();
                    txtNombreProducto.Text = oProducto.nombre;
                    txtPrecioCompra.Select();
                }
                else
                {
                    txtCodigoProducto.BackColor = Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtNombreProducto.Text = "";
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal precioCompra = 0;
            decimal precioVenta = 0;
            bool producto_existe = false;
            
            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtPrecioCompra.Text, out precioCompra))
            {
                MessageBox.Show("Precio de Compra - Formato de moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecioCompra.Select();
                txtPrecioCompra.Text = "";
                return;
            }

            if (!decimal.TryParse(txtPrecioVenta.Text, out precioVenta))
            {
                MessageBox.Show("Precio de venta - Formato de moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecioVenta.Select();
                txtPrecioVenta.Text = "";
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
                dgvData.Rows.Add(new object[]{
                    txtIdProducto.Text,
                    txtNombreProducto.Text,
                    precioCompra.ToString("0.00"),
                    precioVenta.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (txtCantidad.Value * precioCompra).ToString("0.00")                
                });
                calcularTotal();
                limpiarProductos();
                txtCodigoProducto.Select();
            }
        }

        private void limpiarProductos()
        {
            txtIdProducto.Text = "0";
            txtCodigoProducto.Text = "";
            txtCodigoProducto.BackColor = Color.White;
            txtNombreProducto.Text = "";
            txtPrecioCompra.Text = "0.00";
            txtPrecioVenta.Text = "0.00";
            txtCantidad.Value = 1;
            txtCodigoProducto.Select();
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
            if (e.ColumnIndex == 6)
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
                    dgvData.Rows.RemoveAt(indice);
                    calcularTotal();
                }
            }
        }

        private void txtPrecioCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecioCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
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

        private void txtDocumentoProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                proveedores oProveedor = new CN_Proveedores().Listar().Where(p => p.documento == txtDocumentoProveedor.Text && p.estado == true).FirstOrDefault();

                if (oProveedor != null)
                {
                    txtDocumentoProveedor.BackColor = Color.Honeydew;
                    txtIdProveedor.Text = oProveedor.IdProveedores.ToString();
                    txtRazonSocial.Text = oProveedor.razonSocial;
                }
                else
                {
                    txtDocumentoProveedor.BackColor = Color.MistyRose;
                    txtIdProveedor.Text = "0";
                    txtRazonSocial.Text = "";
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdProveedor.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDocumentoProveedor.Select();
                return;
            }

            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe agregar productos a la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCodigoProducto.Select();
                return;
            }


            DataTable detalle_compra = new DataTable();

            detalle_compra.Columns.Add("IdProducto", typeof(int));
            detalle_compra.Columns.Add("precioCompra", typeof(decimal));
            detalle_compra.Columns.Add("precioVenta", typeof(decimal));
            detalle_compra.Columns.Add("cantidad", typeof(int));
            detalle_compra.Columns.Add("montoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                detalle_compra.Rows.Add(new object[] { row.Cells["IdProducto"].Value.ToString(), row.Cells["precioCompra"].Value.ToString(), row.Cells["precioVenta"].Value.ToString(), row.Cells["cantidad"].Value.ToString(), row.Cells["subTotal"].Value.ToString() });
            }
            int IdCorrelativo = new CN_Compras().obtenerCorrelativo();
            string numero_Documento = string.Format("{0:00000}", IdCorrelativo);

            compras oCompra = new compras()
            {
                ousuario = new usuarios() { IdUsuarios = _usuarios.IdUsuarios },
                oproveedores = new proveedores() { IdProveedores = Convert.ToInt32(txtIdProveedor.Text) },
                tipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem).Texto,
                numeroDocumento = numero_Documento.ToString(),
                montoTotal = Convert.ToDecimal(txtTotalPagar.Text),
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Compras().Registrar(oCompra, detalle_compra, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de compra generada: \n" + numero_Documento + "\n\n¿Desea copiar al" +
                    " portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    Clipboard.SetText(numero_Documento);
                }

                txtIdProveedor.Text = "0";
                txtDocumentoProveedor.Text = "";
                txtRazonSocial.Text = ""; 
                dgvData.Rows.Clear();
                calcularTotal();
            }
            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
               
        }
    }
}
