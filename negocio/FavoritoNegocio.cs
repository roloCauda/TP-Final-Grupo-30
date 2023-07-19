using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class FavoritoNegocio
    {
        public void AgregarFavorito(int IdUsuario, int IdArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into FAVORITOS (IdCliente, IdArticulo) " +
                                       "VALUES (@IdUsuario, @NIdArticulo)");
                datos.setearParametro("@IdUsuario", IdUsuario);
                datos.setearParametro("@NIdArticulo", IdArticulo);

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
        public void QuitarFavorito(int IdUsuario, int IdArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Delete FROM FAVORITOS WHERE IdCliente = @IdUsuario AND IdArticulo = @IdArticulo");
                datos.setearParametro("@IdUsuario", IdUsuario);
                datos.setearParametro("@IdArticulo", IdArticulo);

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
        public List<Articulo> listarFavoritosPorCliente(int IdCliente)
        {
            List<Articulo> lista = new List<Articulo>();

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select A.Id, A.Nombre, A.Descripcion, A.Precio, i.ImagenURL " +
                    "FROM Favoritos F " +
                    "INNER JOIN ARTICULOS A ON A.Id = F.IdArticulo " +
                    "left join Imagenes I ON A.Id = I.IdArticulo " +
                    "where F.IdCliente = @IdCliente " +
                    "group by A.Id, A.Nombre, A.Descripcion, A.Precio, i.ImagenURL");
                datos.setearParametro("@IdCliente", IdCliente);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.IdArticulo = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    Imagen imagen = new Imagen();
                    if (!(datos.Lector["ImagenURL"] is DBNull))
                    {
                        imagen.ImagenURL = (string)datos.Lector["ImagenURL"];
                    }
                    else
                    {
                        imagen.ImagenURL = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
                    }

                    aux.ListaImagenes.Add(imagen);

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
