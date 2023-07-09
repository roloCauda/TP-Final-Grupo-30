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
                datos.setearConsulta("Select Nombres, Apellidos, Email, Telefono, IDDomicilio, TipoAcceso FROM Usuarios WHERE @dni = DNI and @Contraseña = Contraseña");
                datos.setearParametro("@dni", user.DNI);
                datos.setearParametro("@Contraseña", user.Contraseña);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
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

    }
}

