using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace negocio
{
    public class UsuarioNegocio
    {
        public bool Loguear(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select Id, Nombres, Apellidos, Email, Telefono, IDDomicilio, TipoAcceso FROM Usuarios WHERE @dni = DNI and @Contraseña = Contraseña");
                datos.setearParametro("@dni", user.DNI);
                datos.setearParametro("@Contraseña", user.Contraseña);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    user.IdUsuario = (int)datos.Lector["Id"];
                    user.Nombres = (string)datos.Lector["Nombres"];
                    user.Apellidos = (string)datos.Lector["Apellidos"];
                    user.Email = (string)datos.Lector["Email"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                    {
                        user.Telefono = (string)datos.Lector["Telefono"];
                    }

                    user.direccion = new Direccion();
                    if (!(datos.Lector["IDDomicilio"] is DBNull))
                    {
                        user.direccion.IdDireccion = (int)datos.Lector["IDDomicilio"];
                    }

                    switch ((int)datos.Lector["TipoAcceso"])
                    {
                        case 1:
                            user.TipoUsuario = TipoUsuario.ADMIN;
                            break;
                        case 2:
                            user.TipoUsuario = TipoUsuario.EMPLEADO;
                            break;
                        case 3:
                            user.TipoUsuario = TipoUsuario.CLIENTE;
                            break;
                    }

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void AgregarUsuario(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into USUARIOS (DNI, Nombres, Apellidos, Email, Contraseña, Telefono, IDDomicilio, TipoAcceso) " +
                                       "VALUES (@DNI, @Nombres, @Apellidos, @Email, @Contraseña, @Telefono, @IDDomicilio, @TipoAcceso)");
                datos.setearParametro("@DNI", user.DNI);
                datos.setearParametro("@Nombres", user.Nombres);
                datos.setearParametro("@Apellidos", user.Apellidos);
                datos.setearParametro("@Email", user.Email);
                datos.setearParametro("@Contraseña", user.Contraseña);
                datos.setearParametro("@Telefono", string.IsNullOrEmpty(user.Telefono) ? DBNull.Value : (object)user.Telefono);
                datos.setearParametro("@IDDomicilio", user.direccion.IdDireccion);
                datos.setearParametro("@TipoAcceso", 3);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool SiEstaRegistrado(int DNI)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select DNI from USUARIOS where DNI = @dni");
                datos.setearParametro("@dni", DNI);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        
        public void actualizarUsuario(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE USUARIOS SET Nombres = @nombres, Apellidos = @apellidos, Email = @email, Telefono = @telefono " +
                                    "where DNI = @dni");
                datos.setearParametro("@nombres", user.Nombres);
                datos.setearParametro("@apellidos", user.Apellidos);
                datos.setearParametro("@email", user.Email);
                datos.setearParametro("@telefono", user.Telefono != null? (object)user.Telefono : DBNull.Value);
                datos.setearParametro("@dni", user.DNI);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        
        public void actualizarContraseña(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE USUARIOS SET Contraseña = @Contraseña where DNI = @dni");
                datos.setearParametro("@Contraseña", user.Contraseña);
                datos.setearParametro("@dni", user.DNI);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public int AgregarUsuarioSinLoguear(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (Nombres, Apellidos, Email, Contraseña, Telefono, IDDomicilio, TipoAcceso) " +
                                    "OUTPUT Inserted.ID values (@Nombres, @Apellidos, @Email, @Contraseña, @Telefono, @IDDomicilio, @TipoAcceso)");

                datos.setearParametro("@Nombres", user.Nombres);
                datos.setearParametro("@Apellidos", user.Apellidos);
                datos.setearParametro("@Email", user.Email);
                datos.setearParametro("@Contraseña", user.Contraseña);
                datos.setearParametro("@Telefono", string.IsNullOrEmpty(user.Telefono) ? (object)DBNull.Value : user.Telefono);
                datos.setearParametro("@IDDomicilio", string.IsNullOrEmpty(user.direccion.IdDireccion.ToString()) ? (object)DBNull.Value : user.direccion.IdDireccion);
                datos.setearParametro("@TipoAcceso", 3);

                int nuevoId = (int)datos.ejecutarEscalar();

                return nuevoId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Usuario> listarSegunAcceso(int idAcceso)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();

            try
            {
                Usuario aux = new Usuario();

                datos.setearConsulta("SELECT DNI, Nombres, Apellidos, Telefono, TipoAcceso FROM Usuarios WHERE TipoAcceso = @acceso");
                datos.setearParametro("@acceso", idAcceso);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario usuario = new Usuario();
                    usuario.DNI = (int)datos.Lector["DNI"];
                    usuario.Nombres = (string)datos.Lector["Nombres"];
                    usuario.Apellidos = (string)datos.Lector["Apellidos"];
                    usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : null;
                    usuario.TipoUsuario = (TipoUsuario)(int)datos.Lector["TipoAcceso"];

                    lista.Add(usuario);
                }

                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
