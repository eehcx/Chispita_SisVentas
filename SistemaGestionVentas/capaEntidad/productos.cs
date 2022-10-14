using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class productos
    {
		public int IdProductos { get; set; }
		public categorias ocategorias { get; set; }
		public string codigo { get; set; }
		public string nombre { get; set; }
		public decimal precioCompra { get; set; }
		public decimal precioVenta { get; set; }
		public bool estado { get; set; }
		public string fechaRegistro { get; set; }
		public int stock { get; set; }
	}
}
