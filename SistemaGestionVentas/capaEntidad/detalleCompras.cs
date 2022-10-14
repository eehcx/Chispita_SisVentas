using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class detalleCompras
    {
		public int IdDetalleCompras { get; set; }
		public productos oproductos { get; set; }
		public decimal precioCompra { get; set; }
		public decimal precioVenta { get; set; }
		public int cantidad { get; set; }
		public decimal total { get; set; }
		public string fechaRegistro { get; set; }
	}
}
