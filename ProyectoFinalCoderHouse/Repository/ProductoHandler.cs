using ProyectoFinalCoderHouse.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public class ProductoHandler
    {
        IConfiguration configuration;
        public ProductoHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<Producto> ListaDeProductos()
        {
            List<Producto> listaDeProductos = new List<Producto>();
            string query = "SELECT * FROM Producto";

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt64(reader["Id"]);
                                producto.Descripciones = reader["Descripciones"].ToString();
                                producto.Costo = Convert.ToDecimal(reader["Costo"]);
                                producto.PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(reader["Stock"]);
                                producto.IdUsuario = Convert.ToInt64(reader["IdUsuario"]);

                                listaDeProductos.Add(producto);
                            }
                        }
                    }
                }

            }
            return listaDeProductos;
        }

        public bool CrearProducto(Producto producto)
        {
            try
            {
                string query = "INSERT INTO Producto VALUES (@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario);";

                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Descripciones", SqlDbType.VarChar, 255)).Value = producto.Descripciones;
                        command.Parameters.Add(new SqlParameter("@Costo", SqlDbType.Money)).Value = producto.Costo;
                        command.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Money)).Value = producto.PrecioVenta;
                        command.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int)).Value = producto.Stock;
                        command.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.BigInt)).Value = producto.IdUsuario;
                        command.ExecuteNonQuery();
                    }

                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
           
        }

        public bool ModificarProducto(Producto producto)
        {
            string query = "UPDATE Producto SET Descripciones = @Descripciones, Costo = @Costo, PrecioVenta = @PrecioVenta, Stock = @Stock ,IdUsuario = @IdUsuario " +
                "WHERE Id=@id ;";

            try
            {
                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = producto.Id;
                        command.Parameters.Add(new SqlParameter("@Descripciones", SqlDbType.VarChar, 255)).Value = producto.Descripciones;
                        command.Parameters.Add(new SqlParameter("@Costo", SqlDbType.Money)).Value = producto.Costo;
                        command.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Money)).Value = producto.PrecioVenta;
                        command.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int)).Value = producto.Stock;
                        command.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.BigInt)).Value = producto.IdUsuario;
                        command.ExecuteNonQuery();

                        return true;

                    }
                }
            }
            catch (Exception)
            {
                return false; 
            }

        }

        public bool EliminarProducto(int Id)
        {
            string query = "DELETE FROM Producto WHERE Id = @Id";

            try
            {
                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.BigInt)).Value = Id;
                        command.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
           
        }

    }
}
