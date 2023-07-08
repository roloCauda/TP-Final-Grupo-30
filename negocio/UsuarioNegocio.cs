using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    user.IdDireccion = new Direccion();
                    if (!(datos.Lector["IDDomicilio"] is DBNull))
                    {
                        user.IdDireccion.IdDireccion = (int)datos.Lector["IDDomicilio"];
                    }

                    switch ((int)datos.Lector["TipoAcceso"])
                    {
                        case 1:
                            user.TipoUsuario = TipoUsuario.CLIENTE;
                            break;
                        case 2:
                            user.TipoUsuario = TipoUsuario.EMPLEADO;
                            break;
                        case 3:
                            user.TipoUsuario = TipoUsuario.ADMIN;
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
    }
}
