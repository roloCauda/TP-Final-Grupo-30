using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class FormaDePagoNegocio
    {
        public List<FormaDePago> listar()
        {
            List<FormaDePago> lista = new List<FormaDePago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion from FormasDePago");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaDePago aux = new FormaDePago();
                    aux.IdFormaDePago = (int)datos.Lector["Id"];
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
