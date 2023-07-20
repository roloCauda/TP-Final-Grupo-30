using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class DireccionNegocio
    {
        public int AgregarDireccion(Direccion direUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            int nuevoId = 0;

            try
            {
                datos.setearConsulta("Insert into DIRECCIONES (Calle, Numero, Piso, Departamento, CP, IdLocalidad, IdProvincia)" +
                                    "OUTPUT Inserted.ID values (@Calle, @Numero,@Piso, @Departamento, @CP, @IdLocalidad, @IdProvincia)");
                datos.setearParametro("@Calle", direUsuario.Calle);
                datos.setearParametro("@Numero", direUsuario.Numero);
                datos.setearParametro("@Piso", direUsuario.Piso.HasValue ? (object)direUsuario.Piso.Value : DBNull.Value);
                datos.setearParametro("@Departamento", string.IsNullOrEmpty(direUsuario.Departamento) ? DBNull.Value : (object)direUsuario.Departamento);
                datos.setearParametro("@CP", direUsuario.CodPostal);
                datos.setearParametro("@IdLocalidad", direUsuario.Localidad.Id);
                datos.setearParametro("@IdProvincia", direUsuario.Provincia.Id);

                nuevoId = (int)datos.ejecutarEscalar();

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

        public Direccion CargarDireccion(Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                direccion.Localidad = new Localidad();
                direccion.Provincia = new Provincia();
                
                datos.setearConsulta("Select Calle, Numero, Piso, Departamento, CP, IdLocalidad, IdProvincia FROM DIRECCIONES where ID = @ID");
                datos.setearParametro("@ID", direccion.IdDireccion);

                datos.ejecutarLectura();

                if(datos.Lector.Read())
                {
                    direccion.Calle = (string)datos.Lector["Calle"];
                    direccion.Numero = (int)datos.Lector["Numero"];
                    if (!(datos.Lector["Piso"] is DBNull))
                    {
                        direccion.Piso = (int)datos.Lector["Piso"];
                    }
                    if (!(datos.Lector["Departamento"] is DBNull))
                    {
                        direccion.Departamento = (string)datos.Lector["Departamento"];
                    }                   
                    direccion.CodPostal = (string)datos.Lector["CP"];
                    direccion.Localidad.Id = (int)datos.Lector["IdLocalidad"];
                    direccion.Provincia.Id = (int)datos.Lector["IdProvincia"];
                }
                    return direccion;
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
        public void actualizarDireccion(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE Direcciones SET Calle = @Calle, Numero = @Numero, Piso = @Piso, Departamento = @Departamento, CP = @CP, IdLocalidad = @IdLocalidad, IdProvincia = @IdProvincia " +
                                    "where Id = @id");
                datos.setearParametro("@Calle", user.direccion.Calle);
                datos.setearParametro("@Numero", user.direccion.Numero);
                datos.setearParametro("@Piso", user.direccion.Piso != null ? (object)user.direccion.Piso : DBNull.Value);
                datos.setearParametro("@Departamento", user.direccion.Departamento != null ? (object)user.direccion.Departamento : DBNull.Value);
                datos.setearParametro("@CP", user.direccion.CodPostal);
                datos.setearParametro("@IdLocalidad", user.direccion.Localidad.Id);
                datos.setearParametro("@IdProvincia", user.direccion.Provincia.Id);
                datos.setearParametro("@id", user.direccion.IdDireccion);

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
