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
    public partial class frmComprasDetalle : Form
    {
        public frmComprasDetalle()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           
            compras oCompras = new CN_Compras().obtenerCompras(txtBusqueda.Text);

            if (oCompras.IdCompras != 0)
            {
                txtNumeroDocumento.Text = oCompras.numeroDocumento;

                txtFecha.Text = oCompras.fechaRegistro;
                txtTipoDocumento.Text = oCompras.tipoDocumento;
                txtUsuarios.Text = oCompras.ousuario.nombre;
                txtDocumentoProveedor.Text = oCompras.oproveedores.documento;
                txtRazonSocial.Text = oCompras.oproveedores.razonSocial;

                dgvData.Rows.Clear();
                foreach (detalleCompras dc in oCompras.odetalleCompras)
                {
                    dgvData.Rows.Add(new object[] { dc.oproductos.nombre, dc.precioCompra, dc.cantidad, dc.total });
                }
                txtMontoTotal.Text = oCompras.montoTotal.ToString("0.00");
            }
            
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            txtNumeroDocumento.Text = "";
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuarios.Text = "";
            txtDocumentoProveedor.Text = "";
            txtRazonSocial.Text = "";

            dgvData.Rows.Clear();
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

            string Texto_Html = Properties.Resources.PlantillaCompra.ToString();
            negocio odatos = new CN_Negocio().obtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", odatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Text);
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtNumeroDocumento.Text);

            Texto_Html = Texto_Html.Replace("@docproveedor", txtDocumentoProveedor.Text);
            Texto_Html = Texto_Html.Replace("@nombreproveedor", txtRazonSocial.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuarios.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["precioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["subTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtMontoTotal.Text);

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = String.Format("Compra_{0}.pdf", txtNumeroDocumento.Text);
            saveFile.Filter = "Pdf Files|*.pdf";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4,25,25,25,25);
                    
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

        private void frmComprasDetalle_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }
    }
}
