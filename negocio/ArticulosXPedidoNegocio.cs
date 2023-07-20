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
        public List<ArticulosXPedido> listarConSP(int idPedido)
        {
            List<ArticulosXPedido> lista = new List<ArticulosXPedido>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select A.Nombre, A.Descripcion, AP.Cantidad, AP.PrecioUnitario, AP.PrecioUnitario * AP.Cantidad as 'Precio Total', i.ImagenURL " +
                    "FROM ARTICULOSxPEDIDO AP " +
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

                    if (!(datos.Lector["ImagenURL"] is DBNull))
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
        public void cargarEnBDlistaArticulos(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                int tamLista = pedido.ListaArtXPedido.Count;

                for (int x = 0; x < tamLista; x++)
                {
                    datos.setearConsulta("Insert into ARTICULOSxPEDIDO (IdPedido, IdArticulo, Cantidad, PrecioUnitario) " +
                        "values (@IdPedido, @IdArticulo, @Cantidad, @PrecioUnitario)");
                    datos.limpiarParametros(datos);
                    datos.setearParametro("@IdPedido", pedido.IdPedido);
                    datos.setearParametro("@IdArticulo", pedido.ListaArtXPedido[x].IdArticulo);
                    datos.setearParametro("@Cantidad", pedido.ListaArtXPedido[x].Cantidad);
                    datos.setearParametro("@PrecioUnitario", pedido.ListaArtXPedido[x].PrecioUnitario);

                    datos.ejecutarAccion();
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

        public List<ArticulosXPedido> agregarListaApedido(List<ItemCarrito> listaCarrito)
        {
            int cantidadCarrito = listaCarrito.Count;
            List<ArticulosXPedido> lista = new List<ArticulosXPedido>();

            for (int i = 0; i < cantidadCarrito; i++)
            {
                ArticulosXPedido aux = new ArticulosXPedido();
                aux.IdArticulo = listaCarrito[i].Articulo.IdArticulo;
                aux.Nombre = listaCarrito[i].Articulo.Nombre;
                aux.Descripcion = listaCarrito[i].Articulo.Descripcion;
                aux.Cantidad = listaCarrito[i].Cantidad;
                aux.PrecioUnitario = listaCarrito[i].Articulo.Precio;
                aux.PrecioTotal = listaCarrito[i].Articulo.Precio * listaCarrito[i].Cantidad;
                aux.ImagenURL = listaCarrito[i].Articulo.ListaImagenes[0].ImagenURL;

                lista.Add(aux);
            }

            return lista;
        }
    }
}
