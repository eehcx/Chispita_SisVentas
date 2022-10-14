using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objcd_categorias = new CD_Categoria();

        public List<categorias>Listar()
        {
            return objcd_categorias.Listar();
        }

        public int Registrar(categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.descripcion == "")
            {
                Mensaje += "Es necesario la descripcion de la  categoria\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_categorias.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(categorias obj, out string Mensaje)
        {
            Mensaje = string.Empty;


            if (obj.descripcion == "")
            {
                Mensaje += "Es necesario la descripcion de la  categoria\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_categorias.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(categorias obj, out string Mensaje)
        {
            return objcd_categorias.Eliminar(obj, out Mensaje);
        }
    }
}
