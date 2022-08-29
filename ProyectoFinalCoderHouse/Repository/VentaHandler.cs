using ProyectoFinalCoderHouse.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public class VentaHandler
    {
        IConfiguration configuration;
        public VentaHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<RelacionProductosVenta> TraerVentas()
        {
            string query = "SELECT * FROM Venta";

            List<RelacionProductosVenta> listaRelacionProductosVentas = new List<RelacionProductosVenta>();

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                RelacionProductosVenta ventasYProductos = new RelacionProductosVenta();

                                Venta venta = new Venta();
                                venta.Id = Convert.ToInt64(dataReader["Id"]);
                                venta.Comentarios = dataReader["Comentarios"].ToString();

                                ventasYProductos.Venta = venta;
                                ventasYProductos.ListaProducto = ProductoVendidoHandler.TraerPVPorIdV(venta.Id, configuration);

                                listaRelacionProductosVentas.Add(ventasYProductos);
                            }
                        }
                    }
                }
            }
            return listaRelacionProductosVentas;

        }

        public bool CargarVenta(List<Producto> listaProductos, int idVendedor)
        {     
            int idVentaCargada = 0;

            string query = "INSERT INTO Venta VALUES (@Comentarios); SELECT CAST(scope_identity() AS int)";

            try
            {
                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        command.Parameters.Add(new SqlParameter("@Comentarios", SqlDbType.VarChar, 255)).Value = "";

                        idVentaCargada = (int)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            if (idVentaCargada != 0)
            {
                List<VentaRealizada> ventaRealizadas = PrdChk(listaProductos, idVendedor, idVentaCargada);

                foreach (var item in ventaRealizadas)
                {
                    if (item.Stock < item.StockDeProducto)
                    {
                        return false;
                    }
                }
                
                ProductoVendidoHandler productoVendidoHandler = new ProductoVendidoHandler();
                productoVendidoHandler.AgregarPV(ventaRealizadas, configuration);
                
                ProductoHandler productoHandler = new ProductoHandler(configuration);
                foreach (var item in ventaRealizadas)
                {
                    int nuevoStock = item.Stock - item.StockDeProducto;
                    productoHandler.ActProductoStck(item.IdProducto, nuevoStock);
                }
            }

            return true;
        }
        private List<VentaRealizada> PrdChk(List<Producto> listaProductos, int idVendedor, int idNuevaVenta)
        {

            List<VentaRealizada> ventasRealizadas = new List<VentaRealizada>();

            bool enStock = false;
            foreach (var itemProducto in listaProductos)
            {
                foreach (var itemVenta in ventasRealizadas)
                {
                    if (itemVenta.IdProducto == itemProducto.Id)
                    {
                        itemVenta.StockDeProducto++;
                        enStock = true;
                    }
                }
                if (enStock is not true)
                {
                    VentaRealizada prdVen = new VentaRealizada();
                    prdVen.IdProducto = itemProducto.Id;
                    prdVen.IdVendedor = idVendedor;
                    prdVen.StockDeProducto = 1;
                    prdVen.IdVenta = idNuevaVenta;
                    prdVen.Stock = itemProducto.Stock;

                    ventasRealizadas.Add(prdVen);
                }
                enStock = false;
            }
            return ventasRealizadas; 
        }

        public bool EliminarVenta(int idv)
        {

            string query = "DELETE FROM Venta WHERE id = @idVenta";

            try
            {
                List<ProductoVendido> productosVendidos = ProductoVendidoHandler.TraerPVPorId(idv, configuration);
                foreach (var item in productosVendidos)
                {
                    ProductoHandler.ActStkVendido(item.IdProducto, item.Stock, configuration);
                }

                ProductoVendidoHandler.BorrarPrdPorIdV(idv, configuration);

                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        command.Parameters.Add(new SqlParameter("@idVenta", SqlDbType.BigInt)).Value = idv;
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
