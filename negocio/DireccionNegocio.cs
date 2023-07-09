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
        public int AgregarDireccion(Direccion dire)
        {
            AccesoDatos datos = new AccesoDatos();
            int nuevoId = 0;

            try
            {
                datos.setearConsulta("Insert into DIRECCIONES (Calle, Numero, Piso, Departamento, CP, IdLocalidad, IdProvincia)" +
                                    "OUTPUT Inserted.ID values (@Calle, @Numero,@Piso, @Departamento, @CP, @IdLocalidad, @IdProvincia)");
                datos.setearParametro("@Calle", dire.Calle);
                datos.setearParametro("@Numero", dire.Numero);
                datos.setearParametro("@Piso", dire.Piso.HasValue ? (object)dire.Piso.Value : DBNull.Value);
                datos.setearParametro("@Departamento", string.IsNullOrEmpty(dire.Departamento) ? DBNull.Value : (object)dire.Departamento);
                datos.setearParametro("@CP", dire.CodPostal);
                datos.setearParametro("@IdLocalidad", dire.Localidad.Id);
                datos.setearParametro("@IdProvincia", dire.Provincia.Id);

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
    }
}
