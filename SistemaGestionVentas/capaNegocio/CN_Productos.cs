using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CN_Productos
    {
        private CD_Productos objcd_productos = new CD_Productos();

        public List<productos> Listar()
        {
            return objcd_productos.Listar();
        }

        public int Registrar(productos obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.codigo == "")
            {
                Mensaje += "Es necesario el codigo del producto\n";
            }
            if (obj.nombre == "")
            {
                Mensaje += "Es necesario el nombre del producto\n";
            }
            
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_productos.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(productos obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.codigo == "")
            {
                Mensaje += "Es necesario el codigo del producto\n";
            }
            if (obj.nombre == "")
            {
                Mensaje += "Es necesario el nombre del producto\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_productos.Editar(obj, out Mensaje);
            }

        }

        public bool Eliminar(productos obj, out string Mensaje)
        {
            return objcd_productos.Eliminar(obj, out Mensaje);
        }
    }
}
