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
                datos.setearParametro("@Piso", dire.Piso);
                datos.setearParametro("@Departamento", dire.Departamento);
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
    }
}
