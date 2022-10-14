using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CN_Usuarios
    {
        private CD_Usuarios objcd_usuarios = new CD_Usuarios();

        public List<usuarios> Listar()
        {
            return objcd_usuarios.Listar();
        }

        public int Registrar(usuarios obj,out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }
            if (obj.nombre == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }
            if (obj.apellidos == "")
            {
                Mensaje += "Son necesarios los apellidos del usuario\n";
            }
            if (obj.contrasena == "")
            {
                Mensaje += "Es necesaria la contraseña del usuario\n";
            }

            if(Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_usuarios.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(usuarios obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.documento == "")
            {
                Mensaje += "Es necesario el documento del usuario\n";
            }
            if (obj.nombre == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }
            if (obj.apellidos == "")
            {
                Mensaje += "Son necesarios los apellidos del usuario\n";
            }
            if (obj.contrasena == "")
            {
                Mensaje += "Es necesaria la contraseña del usuario\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_usuarios.Editar(obj, out Mensaje);
            }
            
        }

        public bool Eliminar(usuarios obj, out string Mensaje)
        {
            return objcd_usuarios.Eliminar(obj, out Mensaje);
        }
    }
}
