using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class usuarios
    {
        public readonly string apellido;

        public int IdUsuarios { get; set; }
        public string documento { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string contrasena { get; set; }
        public bool estado { get; set; }
        public roles oroles { get; set; }
        public string fechaCreacion { get; set; }
    }
}
