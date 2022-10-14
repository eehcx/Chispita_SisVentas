using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using capaEntidad;

namespace capaDatos
{
    public class CD_Roles
    {
        public List<roles> Listar()
        {
            List<roles> lista = new List<roles>();

            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdRoles,descripcion from roles");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras se ejecute usuarios
                        while (dr.Read())
                        {
                            lista.Add(new roles()
                            {
                                IdRoles = Convert.ToInt32(dr["IdRoles"]),
                                descripcion = dr["descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<roles>();
                }
            }
            return lista;

        }
    }
}
