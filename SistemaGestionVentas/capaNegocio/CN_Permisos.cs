using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CN_Permisos
    {
        private CD_Permisos objcd_permisos = new CD_Permisos();

        public List<permisos> Listar(int IdUsuarios)
        {
            return objcd_permisos.Listar(IdUsuarios);
        }
    }
}
