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

                    //cargar imagenes en la lista de Articulo
                    AccesoDatos datos2 = new AccesoDatos();

                    try
                    {
                        datos2.setearConsulta("Select IdArticulo FROM Favoritos WHERE IdCliente = @IdCliente");
                        datos2.setearParametro("@IdCliente", user.IdUsuario);
                        datos2.ejecutarLectura();

                        List<Favoritos> LFavoritos = new List<Favoritos>();

                        while (datos2.Lector.Read())
                        {
                            Favoritos auxF;

                            if (!(datos2.Lector["IdArticulo"] is DBNull))
                            {
                                auxF = new Favoritos();
                                auxF.IdArticulo = (int)datos2.Lector["IdArticulo"];

                                LFavoritos.Add(auxF);
                            }
                        }

                        user.ListaFavoritos = LFavoritos;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        datos2.cerrarConexion();
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
                datos.setearConsulta("Insert into USUARIOS (DNI, Nombres, Apellidos, Email, Contraseña, RecuperacionContraseña, Telefono, IDDomicilio, TipoAcceso) " +
                                       "VALUES (@DNI, @Nombres, @Apellidos, @Email, @Contraseña, @RecuperacionContraseña, @Telefono, @IDDomicilio, @TipoAcceso)");
                datos.setearParametro("@DNI", user.DNI);
                datos.setearParametro("@Nombres", user.Nombres);
                datos.setearParametro("@Apellidos", user.Apellidos);
                datos.setearParametro("@Email", user.Email);
                datos.setearParametro("@Contraseña", user.Contraseña);
                datos.setearParametro("@RecuperacionContraseña", 123);
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
                datos.setearParametro("@telefono", user.Telefono != null ? (object)user.Telefono : DBNull.Value);
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
        public void cambiarContraseña(Usuario user, string pass)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE USUARIOS SET Contraseña = @Contraseña where DNI = @dni");
                datos.setearParametro("@Contraseña", pass);
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

        public List<Usuario> listarSegunAcceso(int idAcceso, int estadoActivo)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Usuario> lista = new List<Usuario>();

            try
            {
                string consulta = "SELECT Id, DNI, Nombres, Apellidos, Email, Telefono, TipoAcceso, Activo FROM Usuarios WHERE TipoAcceso = @acceso";

                if (estadoActivo == 0 || estadoActivo == 1)
                {
                    consulta += " AND Activo = @estadoActivo";
                    datos.setearParametro("@estadoActivo", estadoActivo);
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@acceso", idAcceso);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    aux.IdUsuario = (int)datos.Lector["Id"];
                    aux.DNI = (int)datos.Lector["DNI"];
                    aux.Nombres = (string)datos.Lector["Nombres"];
                    aux.Apellidos = (string)datos.Lector["Apellidos"];
                    aux.Email = (string)datos.Lector["Email"];

                    if (!(datos.Lector["Telefono"] is DBNull))
                    {
                        aux.Telefono = (string)datos.Lector["Telefono"];
                    }

                    switch ((int)datos.Lector["TipoAcceso"])
                    {
                        case 1:
                            aux.TipoUsuario = TipoUsuario.ADMIN;
                            break;
                        case 2:
                            aux.TipoUsuario = TipoUsuario.EMPLEADO;
                            break;
                        case 3:
                            aux.TipoUsuario = TipoUsuario.CLIENTE;
                            break;
                    }

                    aux.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(aux);
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

        public void cambiarAcceso(int IdUsuario, int acceso)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Usuarios SET TipoAcceso = @acceso FROM Usuarios WHERE Id = @id");
                datos.setearParametro("@id", IdUsuario);
                datos.setearParametro("@acceso", acceso);
                datos.ejecutarLectura();
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
        public Usuario CargarUsuario(int IdUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario user = new Usuario();
            try
            {
                datos.setearConsulta("Select Id, DNI, Nombres, Apellidos, Email, Telefono, IDDomicilio, TipoAcceso FROM Usuarios WHERE @id = Id");
                datos.setearParametro("@id", IdUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    user.IdUsuario = (int)datos.Lector["Id"];
                    user.DNI = (int)datos.Lector["DNI"];
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
                }

                return user;

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
        public Usuario CargarUsuarioxDNI(int dni)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario user = new Usuario();
            try
            {
                datos.setearConsulta("Select Id, DNI, Nombres, Apellidos, Email, Telefono, IDDomicilio, TipoAcceso FROM Usuarios WHERE @dni = DNI");
                datos.setearParametro("@DNI", dni);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    user.IdUsuario = (int)datos.Lector["Id"];
                    user.DNI = (int)datos.Lector["DNI"];
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
                }

                return user;

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

        public void CambiarEstadoActivo(string idUsuario, int Activo)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario user = new Usuario();
            try
            {
                datos.setearConsulta("UPDATE USUARIOS SET Activo = @Activo WHERE @id = Id");
                datos.setearParametro("@id", idUsuario);
                datos.setearParametro("@Activo", Activo);
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
    }
}
