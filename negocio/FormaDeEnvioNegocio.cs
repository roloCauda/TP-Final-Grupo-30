using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class FormaDeEnvioNegocio
    {
        public List<FormaDeEnvio> listar()
        {
            List<FormaDeEnvio> lista = new List<FormaDeEnvio>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion from FormasDeEnvio");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaDeEnvio aux = new FormaDeEnvio();
                    aux.IdFormaDeEnvio = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

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
    }
}
