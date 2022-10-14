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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace capaPresentacion
{
    public partial class frmVentasDetalle : Form
    {
        public frmVentasDetalle()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ventas oVentas = new CN_Ventas().obtenerVentas(txtBusqueda.Text);

            if (oVentas.IdVenta != 0)
            {
                txtNumeroDocumento.Text = oVentas.numeroDocumento;

                txtFecha.Text = oVentas.fechaRegistro;
                txtTipoDocumento.Text = oVentas.tipoDocumento;
                txtUsuarios.Text = oVentas.ousuarios.nombre;
                
                dgvData.Rows.Clear();
                foreach (detalleVentas dv in oVentas.odetalleVentas)
                {
                    dgvData.Rows.Add(new object[] { dv.oproductos.nombre, dv.precioVenta, dv.cantidad, dv.subtotal });
                }
                txtMontoTotal.Text = oVentas.montoTotal.ToString("0.00");
                txtMontoPago.Text = oVentas.montoPago.ToString("0.00");
                txtMontoCambio.Text = oVentas.montoCambio.ToString("0.00");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNumeroDocumento.Text = "";
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuarios.Text = "";

            dgvData.Rows.Clear();
            txtMontoCambio.Text = "";
            txtMontoPago.Text = "";
            txtMontoTotal.Text = "";
            txtBusqueda.Text = "";
            txtBusqueda.Select();
        }

        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtBusqueda.Select();
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaVenta.ToString();
            negocio odatos = new CN_Negocio().obtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", odatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Text);
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtNumeroDocumento.Text);

            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuarios.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["precioVenta"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["subTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtMontoTotal.Text);
            Texto_Html = Texto_Html.Replace("@pagocon", txtMontoPago.Text);
            Texto_Html = Texto_Html.Replace("@cambio", txtMontoCambio.Text);

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = String.Format("Venta_{0}.pdf", txtNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files|*.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().obtenerLogo(out obtenido);

                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void frmVentasDetalle_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }
    }
}
