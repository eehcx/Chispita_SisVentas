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
    public class CD_Productos
    {
        /*Consultar - Productos*/
        public List<productos> Listar()
        {
            List<productos> lista = new List<productos>();

            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    //Seleccionar los datos que vamos a traer 
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select IdProductos,codigo,nombre,c.Idcategorias,c.descripcion");
                    query.AppendLine("[descripcion],stock,precioCompra,precioVenta,p.estado from productos p");
                    query.AppendLine("inner join categorias c  on c.IdCategorias = p.IdCategorias");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras se ejecute productos
                        while (dr.Read())
                        {
                            lista.Add(new productos()
                            {
                                IdProductos = Convert.ToInt32(dr["IdProductos"]),
                                codigo = dr["codigo"].ToString(),
                                nombre = dr["nombre"].ToString(),
                                ocategorias = new categorias() { IdCategorias = Convert.ToInt32(dr["IdCategorias"]), descripcion = dr["descripcion"].ToString() },
                                stock = Convert.ToInt32(dr["stock"].ToString()),
                                precioCompra = Convert.ToDecimal(dr["precioCompra"].ToString()),
                                precioVenta = Convert.ToDecimal(dr["precioVenta"].ToString()),
                                estado = Convert.ToBoolean(dr["estado"]),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<productos>();
                }
            }
            return lista;
        }

        /*Consultar - Registrar productos*/
        public int Registrar(productos obj, out string Mensaje)
        {
            int resultado = 0;
            Mensaje = String.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);
                    cmd.Parameters.AddWithValue("codigo", obj.codigo);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("IdCategorias", obj.ocategorias.IdCategorias);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToInt32(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
                Mensaje = ex.Message;
            }

            return resultado;
        }

        /*Consultar - Editar productos*/
        public bool Editar(productos obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProductos", obj.IdProductos);
                    cmd.Parameters.AddWithValue("codigo", obj.codigo);
                    cmd.Parameters.AddWithValue("nombre", obj.nombre);
                    cmd.Parameters.AddWithValue("IdCategorias", obj.ocategorias.IdCategorias);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }

        /*Consultar - Eliminar productos*/
        public bool Eliminar(productos obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = String.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProductos", obj.IdProductos);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
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
