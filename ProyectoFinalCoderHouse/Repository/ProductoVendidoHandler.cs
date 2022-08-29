using ProyectoFinalCoderHouse.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public class ProductoVendidoHandler
    {
        public static List<Producto> TraerPVPorIdV(long idVenta, IConfiguration configuration)
        {
            string query = "SELECT * from Producto as P INNER JOIN ProductoVendido as PV ON PV.IdProducto = P.Id " +
                "WHERE PV.IdVenta = @idVenta";

            List<Producto> productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add(new SqlParameter("@idVenta", SqlDbType.BigInt)).Value = idVenta;

                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt64(dataReader["Id"]);
                                producto.Descripciones = dataReader["Stock"].ToString();
                                producto.Costo = Convert.ToDecimal(dataReader["IdProducto"]);
                                producto.PrecioVenta = Convert.ToDecimal(dataReader["IdVenta"]);
                                producto.Stock = Convert.ToInt32(dataReader["IdVenta"]);
                                producto.IdUsuario = Convert.ToInt64(dataReader["IdVenta"]);
                                productos.Add(producto);
                            }
                        }
                    }
                }
            }

            return productos;
        }

        public List<Producto> TraerProductosVendidos(int idUsuario, IConfiguration configuration)
        {

            string query = "SELECT * from Producto as P INNER JOIN ProductoVendido as PV on PV.IdProducto = P.id" +
                " where P.idUsuario = @idUsuario;";

            List<Producto> _productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.BigInt)).Value = idUsuario;


                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();

                                producto.Id = Convert.ToInt64(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                producto.Costo = Convert.ToDecimal(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToDecimal(dataReader["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt64(dataReader["IdUsuario"]);
                                _productos.Add(producto);
                            }
                        }
                    }
                }
            }

            return _productos;
        }

        public void AgregarPV(List<VentaRealizada> ventaRealizadas, IConfiguration configuration)
        {
            string query = "INSERT INTO ProductoVendido VALUES (@Stock, @IdProducto, @IdVenta);";

            try
            {
                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        foreach (var item in ventaRealizadas)
                        {
                            command.Parameters.Clear();
                            command.Parameters.Add(new SqlParameter("@Stock", SqlDbType.VarChar, 255)).Value = item.StockDeProducto;
                            command.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Money)).Value = item.IdProducto;
                            command.Parameters.Add(new SqlParameter("@IdVenta", SqlDbType.Money)).Value = item.IdVenta;
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static List<ProductoVendido> TraerPVPorId(long idVenta, IConfiguration configuration)
        {
            string query = "SELECT * FROM ProductoVendido as PV WHERE PV.IdVenta = @idVenta";

            List<ProductoVendido> _productosVendidos = new List<ProductoVendido>();

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@idVenta", SqlDbType.BigInt)).Value = idVenta;

                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();

                                productoVendido.Id = Convert.ToInt64(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt64(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt64(dataReader["IdVenta"]);
                                _productosVendidos.Add(productoVendido);
                            }
                        }
                    }
                }
            }

            return _productosVendidos;
        }

        public static bool BorrarPrdPorIdV(int idVenta, IConfiguration configuration)
        {
            string query = "DELETE FROM ProductoVendido WHERE IdVenta = @idVenta;";

            try
            {
                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.Add(new SqlParameter("@idVenta", SqlDbType.BigInt)).Value = idVenta;
                        command.ExecuteNonQuery();
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
