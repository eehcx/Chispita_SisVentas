using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class ventas
    {
		public int IdVenta { get; set; }
		public usuarios ousuarios { get; set; }
		public string tipoDocumento { get; set; }
		public string numeroDocumento { get; set; }
		public decimal montoPago { get; set; }
		public decimal montoCambio { get; set; }
		public List<detalleVentas> odetalleVentas { get; set; }
		public decimal montoTotal { get; set; }
		public string fechaRegistro { get; set; }
	}
}
