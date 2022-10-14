using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class detalleVentas
    {
		public int IdDetalleVentas { get; set; }
		public ventas oventas { get; set; }
		public productos oproductos { get; set; }
		public decimal precioVenta { get; set; }
		public int cantidad { get; set; }
		public decimal subtotal { get; set; }
		public string fechaRegistro { get; set; }
	}
}
