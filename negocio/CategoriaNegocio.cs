using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select Id, Descripcion, ImagenURL from CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.IdCategoria = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenURL"] is DBNull))
                        aux.ImagenURL = (string)datos.Lector["ImagenURL"];

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

                if(!listaArt.Any(art => art.IdCategoria.IdCategoria == id))
                {
                    datos.setearConsulta("delete from CATEGORIAS where id=@id");
                    datos.setearParametro("@id", id);
                    datos.ejecutarLectura();
                }
                else
                {
                    //CARTEL QUE HAY ARTICULOS QUE LE PERTENECEN A ESTA CATEGORIA
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

        public void agregar(Categoria cat)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if(cat.ImagenURL != null)
                {
                    datos.setearConsulta("insert into CATEGORIAS (Descripcion, ImagenURL) VALUES (@descripcion, @imagenURL)");
                    datos.setearParametro("@descripcion", cat.Descripcion);
                    datos.setearParametro("@imagenURL", cat.ImagenURL);
                    datos.ejecutarLectura();
                }
                else
                {
                    datos.setearConsulta("insert into CATEGORIAS (Descripcion) VALUES (@descripcion)");
                    datos.setearParametro("@descripcion", cat.Descripcion);
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

        public void modificar(Categoria cat)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                if (cat.ImagenURL != null)
                {
                    datos.setearConsulta("update CATEGORIAS set Descripcion = @descripcion, ImagenURL = @imagenURL where Id = @idCategoria");
                    datos.setearParametro("@descripcion", cat.Descripcion);
                    datos.setearParametro("@imagenURL", cat.ImagenURL);
                    datos.setearParametro("@idCategoria", cat.IdCategoria);
                    datos.ejecutarLectura();
                }
                else
                {
                    datos.setearConsulta("update CATEGORIAS set Descripcion = @descripcion, ImagenURL = @imagenURL where Id = @idCategoria");
                    datos.setearParametro("@descripcion", cat.Descripcion);
                    datos.setearParametro("@imagenURL", DBNull.Value);
                    datos.setearParametro("@idCategoria", cat.IdCategoria);
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

        public Categoria cargarCategoria(int id)
        {
            Categoria aux = new Categoria();
            AccesoDatos datos = new AccesoDatos();

            datos.setearSPconParametroCategoria("storedCategoria", id);
            datos.ejecutarLectura();

            try
            {
                while (datos.Lector.Read())
                {
                    aux.IdCategoria = (int)datos.Lector["Id"];

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
    }
}
