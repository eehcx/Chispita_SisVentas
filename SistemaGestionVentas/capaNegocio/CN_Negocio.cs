using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using capaDatos;
using capaEntidad;

namespace capaNegocio
{
    public class CN_Negocio
    {
        private CD_Negocio objcd_negocio = new CD_Negocio();

        public negocio obtenerDatos()
        {
            return objcd_negocio.obtenerDatos();
        }

        public bool GuardarDatos(negocio obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj.nombre == "")
            {
                mensaje = "El nombre no puede estar vacio";
            }
            if (obj.RUC == "")
            {
                mensaje = "El RUC no puede estar vacio";
            }
            if (obj.direccion == "")
            {
                mensaje = "La direccion no puede estar vacia";
            }
            if (mensaje != String.Empty)
            {
                return false;
            }
            else
            {
                return objcd_negocio.GuardarDatos(obj, out mensaje);
            }
        }
        

        public byte[] obtenerLogo(out bool obtenido)
        {
            return objcd_negocio.obtenerLogo(out obtenido);
        }

        public bool ActualizarLogo(byte[] image, out string mensaje)
        {
            return objcd_negocio.actualizarLogo(image, out mensaje);
        }




    }
}
