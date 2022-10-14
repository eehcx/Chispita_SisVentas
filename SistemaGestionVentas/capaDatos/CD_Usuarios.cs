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
    public class CD_Usuarios
    {
        /*Consultar - Usuarios*/
        public List<usuarios> Listar()
        {
            List<usuarios> lista = new List<usuarios>();

            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    //Seleccionar los datos que vamos a traer 
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select u.IdUsuarios,u.documento,u.nombre,u.apellidos,u.contrasena,u.estado,r.IdRoles,r.descripcion from usuarios u");
                    query.AppendLine("inner join roles r on r.IdRoles = u.IdRoles");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras se ejecute usuarios
                        while (dr.Read())
                        {
                            lista.Add(new usuarios()
                            {
                                IdUsuarios = Convert.ToInt32(dr["IdUsuarios"]),
                                documento = dr["documento"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                apellidos = dr["apellidos"].ToString(),
                                contrasena = dr["contrasena"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"]),
                                oroles = new roles() { IdRoles = Convert.ToInt32(dr["IdRoles"]), descripcion = dr["descripcion"].ToString() }
                            }) ;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<usuarios>();
                }
            }
            return lista;

        }

        /*Consultar - Registrar usuarios*/
        public int Registrar(usuarios obj,out string Mensaje)
        {
            int IdUsuarioGenerado = 0;
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_registrarUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("contrasena", obj.contrasena);
                    cmd.Parameters.AddWithValue("IdRoles", obj.oroles.IdRoles);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("IdUsuariosResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    IdUsuarioGenerado = Convert.ToInt32(cmd.Parameters["IdUsuariosResultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IdUsuarioGenerado = 0;
                Mensaje = ex.Message;
            }

            return IdUsuarioGenerado;
        }

        /*Consultar - Editar usuarios*/
        public bool Editar(usuarios obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuarios", obj.IdUsuarios);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("apellidos", obj.apellidos);
                    cmd.Parameters.AddWithValue("contrasena", obj.contrasena);
                    cmd.Parameters.AddWithValue("IdRoles", obj.oroles.IdRoles);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }

        /*Consultar - Eliminar usuarios*/
        public bool Eliminar(usuarios obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarUsuarios", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuarios", obj.IdUsuarios);
                    cmd.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Respuesta"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }
    }
}
