using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class compras
    {
		public int IdCompras { get; set; }
		public usuarios ousuario { get; set; }
		public proveedores oproveedores { get; set; }
		public string tipoDocumento { get; set; }
		public string numeroDocumento { get; set; }
		public decimal montoTotal { get; set; }
		public List<detalleCompras> odetalleCompras { get; set; }
		public string fechaRegistro { get; set; }
	}
}
