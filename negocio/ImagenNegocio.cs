using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> listar(int idArticulo)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select Id, IdArticulo, ImagenUrl from Imagenes where idArticulo = " + idArticulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();

                    aux.IdImagen = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.ImagenURL = (string)datos.Lector["ImagenUrl"];

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

        public void agregar(List<Imagen> lista, int ID)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                int tamLista = lista.Count;

                for (int x = 0; x < tamLista; x++)
                {
                    datos.setearConsulta("Insert into IMAGENES (IdArticulo, ImagenURL) values (@IdArticulo, @ImagenURL)");
                    datos.limpiarParametros(datos);
                    datos.setearParametro("@IdArticulo", ID);
                    datos.setearParametro("@ImagenURL", lista[x].ImagenURL);

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

        public void modificar(List<Imagen> lista, int ID)
        {
            AccesoDatos datos = new AccesoDatos();

            List<Imagen> listaEnBD = new List<Imagen>();
            ImagenNegocio negocioIMG = new ImagenNegocio();
            listaEnBD = negocioIMG.listar(ID);

            try
            {
                int tamLista = lista.Count;

                for (int x = 0; x < tamLista; x++)
                {
                    //Si no encuentra la imagen de Lista en listaEnBD, la agrega
                    if (!listaEnBD.Any(img => img.ImagenURL == lista[x].ImagenURL))
                    {
                        datos.setearConsulta("Insert into IMAGENES (IdArticulo, ImagenURL) values (@IdArticulo, @ImagenURL)");
                        datos.limpiarParametros(datos);
                        datos.setearParametro("@IdArticulo", ID);
                        datos.setearParametro("@ImagenURL", lista[x].ImagenURL);
                        datos.ejecutarAccion();
                    }
                }

                tamLista = listaEnBD.Count;

                for (int x = 0; x < tamLista; x++)
                {
                    //si no encuentra la imagen de listaEnBD en Lista, la borra
                    if (!lista.Any(img => img.ImagenURL == listaEnBD[x].ImagenURL))
                    {
                        datos.setearConsulta("Delete FROM IMAGENES WHERE IdArticulo=@idArticulo AND ImagenURL=@imagenURL");
                        datos.limpiarParametros(datos);
                        datos.setearParametro("@imagenURL", listaEnBD[x].ImagenURL);
                        datos.setearParametro("@idArticulo", ID);
                        datos.ejecutarAccion();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void eliminar(int idImagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Delete FROM IMAGENES WHERE Id=@idImagen");
                datos.setearParametro("@idImagen", idImagen);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}