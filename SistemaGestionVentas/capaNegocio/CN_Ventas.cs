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
    public class CN_Ventas
    {
        private CD_Ventas objcd_venta = new CD_Ventas();

        public bool restarStock(int IdProductos, int cantidad)
        {
            return objcd_venta.restarStock(IdProductos, cantidad);
        }

        public bool sumarStock(int IdProductos, int cantidad)
        {
            return objcd_venta.sumarStock(IdProductos, cantidad);
        }


        public int obtenerCorrelativo()
        {
            return objcd_venta.obtenerCorrelativo();
        }

        public bool Registrar(ventas obj, DataTable detalleVenta, out string Mensaje)
        {
            return objcd_venta.Registrar(obj, detalleVenta, out Mensaje);
        }

        public ventas obtenerVentas(string numero)
        {
            ventas oVentas = objcd_venta.obtenerVentas(numero);

            if (oVentas.IdVenta != 0)
            {
                List<detalleVentas> oDetalleVenta = objcd_venta.obtenerDetalleVentas(oVentas.IdVenta);

                oVentas.odetalleVentas = oDetalleVenta;
            }
            return oVentas;
        }
    }
}
