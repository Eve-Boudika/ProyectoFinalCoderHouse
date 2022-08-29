using ProyectoFinalCoderHouse.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repository
{
    public class UsuarioHandler
    {
        IConfiguration configuration;
        public UsuarioHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Usuario ValidarSesion(string usuario, string contraseña)
        {

          string query = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contraseña;";

            Usuario _usuario = new Usuario();

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar)).Value = usuario;
                    command.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar)).Value = contraseña;


                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuarioNuevo = new Usuario();
                                usuarioNuevo.Id = Convert.ToInt64(dataReader["Id"]);
                                usuarioNuevo.Nombre = dataReader["Nombre"].ToString();
                                usuarioNuevo.Apellido = dataReader["Apellido"].ToString();
                                usuarioNuevo.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuarioNuevo.Contraseña = dataReader["Contraseña"].ToString();
                                usuarioNuevo.Mail = dataReader["Mail"].ToString();

                                _usuario = usuarioNuevo;
                            }

                        }
                    }  
                }
            }

            return _usuario;

        }

        public Usuario TraerUsuario(string nombreUsuario)
        {
            string query = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario;";

            Usuario _usuario = new Usuario();

            using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 20)).Value = nombreUsuario;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt64(dataReader["Id"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();

                                _usuario = usuario;
                            }
                        }
                    }
                }
            }
            return _usuario;
        }

        public bool CrearUsuario(Usuario usuario)
        {
            Usuario usuarioEnBD = TraerUsuario(usuario.NombreUsuario);
            if (usuarioEnBD.NombreUsuario == "")
            {
                string query = "INSERT INTO Usuario VALUES (@Nombre, @Apellido, @NombreUsuario , @Contraseña, @Mail);";

                try
                {
                    using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
                    {

                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 255)).Value = usuario.Nombre;
                            command.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 255)).Value = usuario.Apellido;
                            command.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 255)).Value = usuario.NombreUsuario;
                            command.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar, 255)).Value = usuario.Contraseña;
                            command.Parameters.Add(new SqlParameter("@Mail", SqlDbType.VarChar, 255)).Value = usuario.Mail;
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
            return false;
        }

        public bool ModificarUsuario(Usuario usuario)
        {
            string query = "UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, NombreUsuario = @NombreUsuario , Contraseña = @Contraseña , Mail = @Mail " +
                "WHERE Id=@id ;";

            try
            {
                using (SqlConnection connection = new SqlConnection(configuration.GetConnectionString("ConnectionString")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = usuario.Id;
                        command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 255)).Value = usuario.Nombre;
                        command.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 255)).Value = usuario.Apellido;
                        command.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 255)).Value = usuario.NombreUsuario;
                        command.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar, 255)).Value = usuario.Contraseña;
                        command.Parameters.Add(new SqlParameter("@Mail", SqlDbType.VarChar, 255)).Value = usuario.Mail;
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

        public bool EliminarUsuario(long Id)
        {
            string query = "DELETE FROM Usuario WHERE Id = @Id";

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
