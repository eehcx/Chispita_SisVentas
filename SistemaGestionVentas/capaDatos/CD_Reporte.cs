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
    public class CD_Reporte
    {
        private static CD_Reporte _instancia = null;

        public CD_Reporte()
        {

        }

        public static CD_Reporte Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new CD_Reporte();
                return _instancia;
            }
        }
        
        public List<reporteCompras> compras(string fechaInicio, string fechaFin, int IdProveedores)
        {
            List<reporteCompras> lista = new List<reporteCompras>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select convert(char(10),c.fechaRegistro,103)[fechaRegistro],c.tipoDocumento,c.numeroDocumento,c.montoTotal,");
                    query.AppendLine("u.nombre[usuarioRegistro],");
                    query.AppendLine("pr.documento[documentoProveedor],pr.razonSocial,");
                    query.AppendLine("p.codigo[codigoProducto],p.nombre[nombreProducto],ca.descripcion[categoria],dc.precioCompra,dc.precioVenta,dc.cantidad,dc.total[totalCompra]");
                    query.AppendLine("from compras c");
                    query.AppendLine("inner join usuarios u on u.IdUsuarios = c.IdUsuario");
                    query.AppendLine("inner join proveedores pr on pr.IdProveedores = c.IdProveedores");
                    query.AppendLine("inner join detalleCompras dc on dc.IdCompras = c.IdCompras");
                    query.AppendLine("inner join productos p on p.IdProductos = dc.IdProductos");
                    query.AppendLine("inner join categorias ca on ca.IdCategorias = p.IdCategorias");
                    query.AppendLine("where convert(date, c.fechaRegistro) between @fechaInicio and @fechaFin");
                    query.AppendLine("and pr.IdProveedores = iif(@IdProveedores=0,pr.IdProveedores,@IdProveedores)");



                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
                    cmd.Parameters.AddWithValue("@IdProveedores", IdProveedores);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new reporteCompras()
                            {
                                fechaRegistro = dr["fechaRegistro"].ToString(),
                                tipoDocumento = dr["tipoDocumento"].ToString(),
                                numeroDocumento = dr["numeroDocumento"].ToString(),
                                montoTotal = dr["montoTotal"].ToString(),
                                usuarioRegistro = dr["usuarioRegistro"].ToString(),
                                documentoProveedor = dr["documentoProveedor"].ToString(),
                                razonSocial = dr["razonSocial"].ToString(),
                                codigoProducto = dr["codigoProducto"].ToString(),
                                nombreProducto = dr["nombreProducto"].ToString(),
                                categoria = dr["categoria"].ToString(),
                                precioCompra = dr["precioCompra"].ToString(),
                                precioVenta = dr["precioVenta"].ToString(),
                                cantidad = dr["cantidad"].ToString(),
                                totalCompra = dr["totalCompra"].ToString()
                            });
                        }
                    }
                }
            }
            catch 
            {
                    lista = new List<reporteCompras>();
            }            
            return lista;
        }



        public List<reporteVentas> ventas(string fechaInicio, string fechaFin)
        {
            List<reporteVentas> lista = new List<reporteVentas>();

            using (SqlConnection oconexion = new SqlConnection(conexion.cadena))
            {
                try 
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("sp_ReporteVenta", oconexion);
                    cmd.Parameters.AddWithValue("fechaInicio", fechaInicio);
                    cmd.Parameters.AddWithValue("fechaFin", fechaFin);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader()) 
                    {
                        while (dr.Read())
                        {
                            lista.Add(new reporteVentas()
                            {
                                fechaRegistro = dr["fechaRegistro"].ToString(),
                                tipoDocumento = dr["tipoDocumento"].ToString(),
                                numeroDocumento = dr["numeroDocumento"].ToString(),
                                montoTotal = dr["montoTotal"].ToString(),
                                usuarioRegistro = dr["usuarioRegistro"].ToString(),
                                codigoProducto = dr["codigoProducto"].ToString(),
                                nombreProducto = dr["nombreProducto"].ToString(),
                                categoria = dr["categoria"].ToString(),
                                precioVenta = dr["precioVenta"].ToString(),
                                cantidad = dr["cantidad"].ToString(),
                                subtotal = dr["subtotal"].ToString()
                            });
                        }
                    }
                }
                catch 
                {
                    lista = new List<reporteVentas>();
                }
            }
            return lista;
        }
        





        
    }                
}
