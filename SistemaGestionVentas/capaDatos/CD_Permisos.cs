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
    public class CD_Permisos
    {
        public List<permisos> Listar(int IdUsuarios)
        {
            List<permisos> lista = new List<permisos>();

            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    StringBuilder query= new StringBuilder();
                    query.AppendLine("select p.IdRoles,p.nombre from permisos p");
                    query.AppendLine("inner join roles r on r.IdRoles = p.IdRoles");
                    query.AppendLine("inner join usuarios u on u.IdRoles = r.IdRoles");
                    query.AppendLine("where u.IdUsuarios = @IdUsuarios");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IdUsuarios",IdUsuarios);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras se ejecute usuarios
                        while (dr.Read())
                        {
                            lista.Add(new permisos()
                            {
                                oroles = new roles() { IdRoles = Convert.ToInt32(dr["IdRoles"]) },
                                nombre = dr["nombre"].ToString(),
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<permisos>();
                }
            }
            return lista;

        }
    }
}
