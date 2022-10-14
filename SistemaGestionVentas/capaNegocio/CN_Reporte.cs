using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objcd_reporte = new CD_Reporte();

        public List<reporteCompras> compras(string fechaInicio, string fechaFin, int IdProveedores)
        {
            return objcd_reporte.compras(fechaInicio, fechaFin, IdProveedores);
        }

        public List<reporteVentas> ventas(string fechaInicio, string fechaFin)
        {
            return objcd_reporte.ventas(fechaInicio, fechaFin);
        }
    }
}
