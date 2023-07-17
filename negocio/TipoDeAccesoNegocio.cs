using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class TipoDeAccesoNegocio
    {
        public List<TipoDeAcceso> listar()
        {
            List<TipoDeAcceso> lista = new List<TipoDeAcceso>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion from TipoDeAcceso");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    TipoDeAcceso aux = new TipoDeAcceso();
                    aux.Id = (int)datos.Lector["Id"];
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
