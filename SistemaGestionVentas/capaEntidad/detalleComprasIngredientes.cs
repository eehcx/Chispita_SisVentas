using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class detalleComprasIngredientes
    {
        public int IdDetalleCompraIngredientes { get; set; }
        public int IdCompras { get; set; }
        public int IdIngrediente { get; set; }
        public decimal precioCompra { get; set; }
        public int cantidad { get; set; }
        public decimal total { get; set; }
    }
}
