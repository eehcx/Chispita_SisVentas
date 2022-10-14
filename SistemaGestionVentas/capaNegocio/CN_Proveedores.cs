using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CN_Proveedores
    {
        private CD_Proveedores objcd_proveedores = new CD_Proveedores();

        public List<proveedores> Listar()
        {
            return objcd_proveedores.Listar();
        }

        //PROCEDIMIENTOS

        public int Registrar(proveedores obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.documento == "")
            {
                Mensaje += "Es necesario el documento del proveedor\n";
            }
            if (obj.razonSocial == "")
            {
                Mensaje += "Es necesario la Razon Social del proveedor\n";
            }
            if (obj.correo == "")
            {
                Mensaje += "Es necesario el correo del proveedor\n";
            }
            if (obj.telefono == "")
            {
                Mensaje += "Es necesario el telefono del proveedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_proveedores.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(proveedores obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.documento == "")
            {
                Mensaje += "Es necesario el documento del proveedor\n";
            }
            if (obj.razonSocial == "")
            {
                Mensaje += "Es necesario la Razon Social del proveedor\n";
            }
            if (obj.correo == "")
            {
                Mensaje += "Es necesario el correo del proveedor\n";
            }
            if (obj.telefono == "")
            {
                Mensaje += "Es necesario el telefono del proveedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_proveedores.Editar(obj, out Mensaje);
            }

        }

        public bool Eliminar(proveedores obj, out string Mensaje)
        {
            return objcd_proveedores.Eliminar(obj, out Mensaje);
        }
    }
}
