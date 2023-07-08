using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion, ImagenURL from MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.IdMarca = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ImagenURL")))
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


        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> listaArt = new List<Articulo>(negocio.listarConSP());

                if (!listaArt.Any(art => art.IdMarca.IdMarca == id))
                {
                    datos.setearConsulta("delete from MARCAS where id=@id");
                    datos.setearParametro("@id", id);
                    datos.ejecutarLectura();
                }
                else
                {
                    //CARTEL QUE HAY ARTICULOS QUE LE PERTENECEN A ESTA MARCA
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

        public Marca cargarMarca(int id)
        {
            Marca aux = new Marca();
            AccesoDatos datos = new AccesoDatos();

            datos.setearSPconParametroMarca("storedMarca", id);
            datos.ejecutarLectura();

            try
            {
                while (datos.Lector.Read())
                {
                    aux.IdMarca = (int)datos.Lector["Id"];

                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenURL"] is DBNull))
                        aux.ImagenURL = (string)datos.Lector["ImagenURL"];
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

        public void agregar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (marca.ImagenURL != null)
                {
                    datos.setearConsulta("insert into MARCAS (Descripcion, ImagenURL) VALUES (@descripcion, @imagenURL)");
                    datos.setearParametro("@descripcion", marca.Descripcion);
                    datos.setearParametro("@imagenURL", marca.ImagenURL);
                    datos.ejecutarLectura();
                }
                else
                {
                    datos.setearConsulta("insert into MARCAS (Descripcion) VALUES (@descripcion)");
                    datos.setearParametro("@descripcion", marca.Descripcion);
                    datos.ejecutarLectura();
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

        public void modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (marca.ImagenURL != null)
                {
                    datos.setearConsulta("update MARCAS set Descripcion = @descripcion, ImagenURL = @imagenURL where Id = @idMarca");
                    datos.setearParametro("@descripcion", marca.Descripcion);
                    datos.setearParametro("@imagenURL", marca.ImagenURL);
                    datos.setearParametro("@idMarca", marca.IdMarca);
                    datos.ejecutarLectura();
                }
                else
                {
                    datos.setearConsulta("update MARCAS set Descripcion = @descripcion, ImagenURL = @imagenURL where Id = @idMarca");
                    datos.setearParametro("@descripcion", marca.Descripcion);
                    datos.setearParametro("@imagenURL", DBNull.Value);
                    datos.setearParametro("@idMarca", marca.IdMarca);
                    datos.ejecutarLectura();
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
    }
}
