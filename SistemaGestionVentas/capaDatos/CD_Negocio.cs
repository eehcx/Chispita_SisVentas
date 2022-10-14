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
    public  class CD_Negocio
    {
        public negocio obtenerDatos()
        {
            negocio obj = new negocio();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    oconexion.Open();

                    string query = "select IdNegocio, nombre, RUC, direccion from negocio where IdNegocio = 1";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj=new negocio()
                            {
                                IdNegocio = int.Parse(dr["IdNegocio"].ToString()),
                                nombre = dr["nombre"].ToString(),
                                RUC = dr["RUC"].ToString(),
                                direccion = dr["direccion"].ToString()
                            };
                        }
                    }
                }
            }
            catch
            {
                obj = new negocio();
            }


            return obj;
        }
        
        public bool GuardarDatos(negocio objeto, out string mensaje)
        {
            bool respuesta = true;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    oconexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.Append("update negocio set nombre = @nombre,");
                    query.Append("RUC = @RUC,");
                    query.Append("direccion = @direccion");
                    query.Append(" where IdNegocio = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@nombre", objeto.nombre);
                    cmd.Parameters.AddWithValue("@RUC", objeto.RUC);
                    cmd.Parameters.AddWithValue("@direccion", objeto.direccion);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        respuesta = false;
                        mensaje = "No se pudo guardar los datos";
                    }


                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }
            return respuesta;
        }

        public byte[] obtenerLogo(out bool obtenido)
        {
            obtenido = true;
            byte[] logoBytes = new byte[0];

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    oconexion.Open();

                    string query = "select logo from negocio where IdNegocio = 1";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            logoBytes = (byte[])dr["logo"];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                logoBytes = new byte[0];
                obtenido = false;
            }
            return logoBytes;
        }

        public bool actualizarLogo(byte[] logo, out string mensaje)
        {
            bool respuesta = true;
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    oconexion.Open();

                    StringBuilder query = new StringBuilder();
                    query.Append("update negocio set logo = @logo");
                    query.Append(" where IdNegocio = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@logo", logo);
                    cmd.CommandType = CommandType.Text;

                    if (cmd.ExecuteNonQuery() < 1)
                    {
                        respuesta = false;
                        mensaje = "No se pudo actualizar el logo";
                    }
                }
            }
            catch
            {
                respuesta = false;
                mensaje = "No se pudo actualizar el logo";
            }

            return respuesta;
            
        }
        
    }
}
            
