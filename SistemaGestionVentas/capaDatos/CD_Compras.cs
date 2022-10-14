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
    public class CD_Compras
    {
        public int obtenerCorrelativo()
        {
            int IdCorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    //Seleccionar los datos que vamos a traer 
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from compras");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    IdCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    IdCorrelativo = 0;
                }
            }
            return IdCorrelativo;
        }

        public bool Registrar(compras obj, DataTable detalleCompra, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = String.Empty;
            
            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCompra", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.ousuario.IdUsuarios);
                    cmd.Parameters.AddWithValue("IdProveedores",obj.oproveedores.IdProveedores);
                    cmd.Parameters.AddWithValue("tipoDocumento",obj.tipoDocumento);
                    cmd.Parameters.AddWithValue("numeroDocumento",obj.numeroDocumento);
                    cmd.Parameters.AddWithValue("montoTotal",obj.montoTotal);
                    cmd.Parameters.AddWithValue("detalleCompra",detalleCompra);
                    cmd.Parameters.Add("resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["resultado"].Value);
                    Mensaje = cmd.Parameters["mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    Respuesta = false;
                    Mensaje = ex.Message;
                }
            }
            return Respuesta;
        }
          
        public compras obtenerCompras(string numero)
        {
            compras obj = new compras();
            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    //Seleccionar los datos que vamos a traer 
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.IdCompras,");
                    query.AppendLine("u.nombre,u.apellidos,");
                    query.AppendLine("pr.documento,pr.razonSocial,");
                    query.AppendLine("c.tipoDocumento, c.numeroDocumento,c.montoTotal,convert(char(10),c.fechaRegistro,103)[fechaRegistro]");
                    query.AppendLine("from compras c");
                    query.AppendLine("inner join usuarios u on u.IdUsuarios = c.IdUsuario");
                    query.AppendLine("inner join proveedores pr on pr.IdProveedores = c.IdProveedores");
                    query.AppendLine("where c.numeroDocumento = @numero");

                    
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new compras()
                            {
                                IdCompras = Convert.ToInt32(dr["IdCompras"]),
                                ousuario = new usuarios()
                                {
                                    nombre = dr["nombre"].ToString(),
                                    apellidos = dr["apellidos"].ToString()
                                },
                                oproveedores = new proveedores()
                                {
                                    documento = dr["documento"].ToString(),
                                    razonSocial = dr["razonSocial"].ToString()
                                },
                                tipoDocumento = dr["tipoDocumento"].ToString(),
                                numeroDocumento = dr["numeroDocumento"].ToString(),
                                montoTotal = Convert.ToDecimal(dr["montoTotal"].ToString()),
                                fechaRegistro = dr["fechaRegistro"].ToString()
                            };  
                        };
                        
                    }
                }
                catch (Exception ex)
                {
                    obj = new compras();
                }
            }

            return obj;
        }

        public List<detalleCompras> obtenerDetalleCompra(int IdCompras)
        {
            List<detalleCompras> oLista = new List<detalleCompras>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    
                    query.AppendLine("select p.nombre,dc.precioCompra,dc.cantidad,dc.total from detalleCompras dc");
                    query.AppendLine("inner join productos p on p.IdProductos = dc.IdProductos");
                    query.AppendLine("where dc.IdCompras=@IdCompras");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IdCompras", IdCompras);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr=cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new detalleCompras()
                            {
                                oproductos = new productos(){nombre = dr["nombre"].ToString()},
                                precioCompra = Convert.ToDecimal(dr["precioCompra"].ToString()),
                                cantidad = Convert.ToInt32(dr["cantidad"].ToString()),
                                total = Convert.ToDecimal(dr["total"].ToString())
                            });
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                oLista = new List<detalleCompras>();
            }
            return oLista;
        }










    }
}
