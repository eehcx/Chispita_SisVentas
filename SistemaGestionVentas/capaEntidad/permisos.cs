using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace capaEntidad
{
    public class permisos
    {
        public int IdPermiso { get; set; }
        public roles oroles { get; set; }
        public string nombre { get; set; }
        public string fechaCreacion { get; set; }
    }
}
