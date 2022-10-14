using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CN_Compras
    {
        private CD_Compras objcd_compra = new CD_Compras();

        public int obtenerCorrelativo()
        {
            return objcd_compra.obtenerCorrelativo();
        }

        public bool Registrar(compras obj, DataTable detalleCompra, out string Mensaje)
        {
            return objcd_compra.Registrar(obj,detalleCompra, out Mensaje);
        }

        public compras obtenerCompras(string numero)
        {
            compras oCompras = objcd_compra.obtenerCompras(numero);

            if (oCompras.IdCompras != 0)
            {
                List<detalleCompras> oDetalleCompra = objcd_compra.obtenerDetalleCompra(oCompras.IdCompras);

                oCompras.odetalleCompras = oDetalleCompra;
            }
            return oCompras;
        }
    }
}
