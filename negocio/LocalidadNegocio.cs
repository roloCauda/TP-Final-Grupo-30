using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class LocalidadNegocio
    {
        public List<Localidad> listar()
        {
            List<Localidad> lista = new List<Localidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, IdProvincia, Descripcion from LOCALIDADES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Localidad aux = new Localidad();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdProvincia = (int)datos.Lector["IdProvincia"];
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

        public Localidad cargarLocalidad(Localidad localidad)
        {
            List<Localidad> lista = new List<Localidad>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, IdProvincia, Descripcion from LOCALIDADES where Id = @id");
                datos.setearParametro("@id", localidad.Id);
                datos.ejecutarLectura();

                    Localidad aux = new Localidad();
                while (datos.Lector.Read())
                {
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdProvincia = (int)datos.Lector["IdProvincia"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                }
                    return aux;

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
