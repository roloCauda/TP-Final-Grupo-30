using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace negocio
{
    public class ArticulosXPedidoNegocio
    {
        public List<ArticulosXPedido> listarConSP (int idPedido)
        {
            List<ArticulosXPedido> lista = new List<ArticulosXPedido>();

            AccesoDatos datos = new AccesoDatos ();

            try
            {
                datos.setearConsulta("Select A.Nombre, A.Descripcion, AP.Cantidad, AP.PrecioUnitario, AP.PrecioUnitario * AP.Cantidad as 'Precio Total', i.ImagenURL " +
                    "FROM FormasDePago F " +
                    "inner join ARTICULOSxPEDIDO AP on AP.IdPedido = AP.IdPedido " +
                    "Inner join Articulos A on A.Id = AP.IdArticulo " +
                    "left join Imagenes I ON AP.IdArticulo = i.IdArticulo " +
                    "where AP.IdPedido = @IdPedido " +
                    "group by A.Nombre, A.Descripcion, AP.Cantidad, AP.PrecioUnitario, i.ImagenURL");
                datos.setearParametro("@IdPedido", idPedido);

                datos.ejecutarLectura();                
                
                while (datos.Lector.Read())
                {
                    ArticulosXPedido aux = new ArticulosXPedido();

                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    aux.PrecioTotal = (decimal)datos.Lector["Precio Total"];

                    if(!(datos.Lector["ImagenURL"] is DBNull))
                    {
                        aux.ImagenURL = (string)datos.Lector["ImagenURL"];
                    }
                    else
                    {
                        aux.ImagenURL = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
                    }

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
