using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CN_Roles
    {
        private CD_Roles objcd_roles = new CD_Roles();

        public List<roles> Listar()
        {
            return objcd_roles.Listar();
        }
    }
}
