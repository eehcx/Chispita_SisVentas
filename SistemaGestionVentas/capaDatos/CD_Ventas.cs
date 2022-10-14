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
    public class CD_Ventas
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
                    query.AppendLine("select count(*) + 1 from ventas");
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

        public bool restarStock(int IdProductos, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    //Seleccionar los datos que vamos a traer 
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update productos set stock = stock - @cantidad where IdProductos = @IdProductos");
                    
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@IdProductos", IdProductos);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true: false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool sumarStock(int IdProductos, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    //Seleccionar los datos que vamos a traer 
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update productos set stock = stock + @cantidad where IdProductos = @IdProductos");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@IdProductos", IdProductos);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool Registrar(ventas obj, DataTable detalleVentas, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = String.Empty;

            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarVentas", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuarios", obj.ousuarios.IdUsuarios);
                    cmd.Parameters.AddWithValue("tipoDocumento", obj.tipoDocumento);
                    cmd.Parameters.AddWithValue("numeroDocumento", obj.numeroDocumento);
                    cmd.Parameters.AddWithValue("montoPago", obj.montoPago);
                    cmd.Parameters.AddWithValue("montoCambio", obj.montoCambio);
                    cmd.Parameters.AddWithValue("montoTotal", obj.montoTotal);
                    cmd.Parameters.AddWithValue("detalleVenta", detalleVentas);
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


        public ventas obtenerVentas(string numero)
        {
            ventas obj = new ventas();
            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try
                {
                    //Seleccionar los datos que vamos a traer 
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.IdVenta,u.nombre,");
                    query.AppendLine("v.tipoDocumento,v.numeroDocumento,v.montoPago,v.montoCambio,v.montoTotal,");
                    query.AppendLine("convert(char(10),v.fechaRegistro,103)[fechaRegistro]");
                    query.AppendLine("from ventas v");
                    query.AppendLine("inner join usuarios u on u.IdUsuarios = v.IdUsuarios");
                    query.AppendLine(" where v.numeroDocumento = @numero");


                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new ventas()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                ousuarios = new usuarios()
                                {
                                    nombre = dr["nombre"].ToString()
                                },
                                tipoDocumento = dr["tipoDocumento"].ToString(),
                                numeroDocumento = dr["numeroDocumento"].ToString(),
                                montoPago = Convert.ToDecimal(dr["montoPago"].ToString()),
                                montoCambio = Convert.ToDecimal(dr["montoCambio"].ToString()),
                                montoTotal = Convert.ToDecimal(dr["montoTotal"].ToString()),
                                fechaRegistro = dr["fechaRegistro"].ToString()
                            };                        
                        };

                    }
                }
                catch
                {
                    obj = new ventas();
                }
            }

            return obj;
        }

        public List<detalleVentas> obtenerDetalleVentas(int IdVenta)
        {
            List<detalleVentas> oLista = new List<detalleVentas>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select p.nombre,dv.precioVenta,dv.cantidad,dv.subtotal from detalleVentas dv");
                    query.AppendLine("inner join productos p on p.IdProductos = dv.IdProductos");
                    query.AppendLine(" where dv.IdVentas = @IdVenta");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IdVenta", IdVenta);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new detalleVentas()
                            {
                                oproductos = new productos() { nombre = dr["nombre"].ToString() },
                                precioVenta = Convert.ToDecimal(dr["precioVenta"].ToString()),
                                cantidad = Convert.ToInt32(dr["cantidad"].ToString()),
                                subtotal = Convert.ToDecimal(dr["subtotal"].ToString())
                            });
                        }
                    }

                }
            }
            catch
            {
                oLista = new List<detalleVentas>();
            }
            return oLista;
        }










    }
}
