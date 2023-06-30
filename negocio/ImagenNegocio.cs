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

            try
            {
                int tamLista = lista.Count;

                for (int x = 0; x < tamLista; x++)
                {
                    datos.setearConsulta("select count (*) from IMAGENES where ImagenUrl=@imagenURL and IdArticulo=@idArticulo");
                    datos.limpiarParametros(datos);
                    datos.setearParametro("@imagenURL", lista[x].ImagenURL);
                    datos.setearParametro("@idArticulo", ID);

                    if (datos.ejecutarEscalar() == 0) //No la encontro, entonces la agregamos
                    {
                        datos.setearConsulta("Insert into IMAGENES (IdArticulo, ImagenURL) values (@IdArticulo, @ImagenURL)");
                        datos.limpiarParametros(datos);
                        datos.setearParametro("@IdArticulo", ID);
                        datos.setearParametro("@ImagenURL", lista[x].ImagenURL);

                        datos.ejecutarAccion();
                    }
                }

                /*int tamListaBorrar = listaBorrar.Count;

                for (int x = 0; x < tamListaBorrar; x++)
                {
                    datos.setearConsulta("select count (*) from IMAGENES where ImagenUrl=@imagenURL and IdArticulo=@idArticulo");
                    datos.limpiarParametros(datos);
                    datos.setearParametro("@imagenURL", listaBorrar[x].ImagenURL);
                    datos.setearParametro("@idArticulo", iDArticulo);

                    if (datos.ejecutarEscalar() > 0) //Si lo encontro, lo elimina
                    {
                        datos.setearConsulta("Delete FROM IMAGENES WHERE IdArticulo=@idArticulo AND ImagenURL=@imagenURL");
                        datos.limpiarParametros(datos);
                        datos.setearParametro("@imagenURL", listaBorrar[x].ImagenURL);
                        datos.setearParametro("@idArticulo", iDArticulo);
                        datos.ejecutarAccion();
                    }
                }*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public void eliminar(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Delete FROM IMAGENES WHERE IdArticulo=@idArticulo");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}