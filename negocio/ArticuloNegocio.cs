using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listarConSP()
        {
            AccesoDatos datos = new AccesoDatos();

            List<Articulo> lista = new List<Articulo>();

            try
            {
                datos.setearSP("storedListar");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.IdArticulo = (int)datos.Lector["Id"];

                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Codigo"))))
                        aux.Codigo = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.IdMarca = new Marca();
                    if (!(datos.Lector["marca"] is DBNull))
                    {
                        aux.IdMarca.IdMarca = (int)datos.Lector["IdMarca"];
                        aux.IdMarca.Descripcion = (string)datos.Lector["marca"];
                    }

                    aux.IdCategoria = new Categoria();
                    if (!(datos.Lector["Categoria"] is DBNull))
                    {
                        aux.IdCategoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                        aux.IdCategoria.Descripcion = (string)datos.Lector["Categoria"];
                    }

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];

                    //cargar imagenes en la lista de Articulo
                    AccesoDatos datos2 = new AccesoDatos();

                    try
                    {
                        datos2.setearSPconParametro("storedImg", aux.IdArticulo);
                        datos2.ejecutarLectura();

                        List<Imagen> LImagenes = new List<Imagen>();
                        bool imagenEncontrada = false;

                        while (datos2.Lector.Read())
                        {
                            Imagen auxI;

                            if (!(datos2.Lector["ImagenUrl"] is DBNull))
                            {
                                auxI = new Imagen();
                                auxI.ImagenURL = (string)datos2.Lector["ImagenUrl"];
                                imagenEncontrada = true;

                                LImagenes.Add(auxI);
                            }

                        }

                        if (!imagenEncontrada)
                        {
                            Imagen auxI = new Imagen();
                            auxI.ImagenURL = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
                            LImagenes.Add(auxI);
                        }

                        aux.ListaImagenes = LImagenes;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        datos2.cerrarConexion();
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

        public int agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            int nuevoId = 0;

            try
            {
                //las comillas dobles definen las cadenas en c#, las comillas simples definen las canedas en SQLServer
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio)" +
                                    "OUTPUT Inserted.ID values (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.IdMarca.IdMarca);
                datos.setearParametro("@IdCategoria", nuevo.IdCategoria.IdCategoria);
                datos.setearParametro("@Precio", nuevo.Precio);

                nuevoId = (int)datos.ejecutarEscalar();

                return nuevoId;
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

        public void modificar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @desc, IdMarca = @idMarca, IdCategoria = @idCateg, Precio = @precio Where Id = @id");
                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@desc", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.IdMarca.IdMarca);
                datos.setearParametro("@idCateg", nuevo.IdCategoria.IdCategoria);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@id", nuevo.IdArticulo);

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

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion Marca, C.Descripcion Categoria, Precio, I.ImagenUrl From ARTICULOS A, CATEGORIAS C, MARCAS M, IMAGENES I Where C.Id = A.IdCategoria And M.Id = A.IdMarca and I.IdArticulo = A.Id AND ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += campo + " > " + filtro;
                            break;
                        case "Menor a":
                            consulta += campo + " < " + filtro;
                            break;
                        case "Igual a":
                            consulta += campo + " = " + filtro;
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con":
                            consulta += "A." + campo + " like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "A." + campo + " like '%" + filtro + "'";
                            break;
                        case "Contiene":
                            consulta += "A." + campo + " like '%" + filtro + "%' ";
                            break;
                    }
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.IdArticulo = (int)datos.Lector["Id"];

                    //opcion 1 - validar que no sea NULL
                    //GetOrdinal es para decirle que columna ver
                    //niego si es nulo (busco que no sea nulo)
                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Codigo"))))
                        aux.Codigo = (string)datos.Lector["Codigo"];

                    //opcion 2 - validar que no sea NULL
                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from IMAGENES where IdArticulo=@idArticulo");
                datos.setearParametro("@idArticulo", id);
                datos.ejecutarAccion();

                datos.limpiarParametros(datos);
                datos.setearConsulta("delete from ARTICULOS where id=@idArticulo");
                datos.setearParametro("@idArticulo", id);
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

        public Articulo cargarArticulo(int id)
        {
            Articulo aux = new Articulo();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearSPconParametro("storedArticulo", id);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    aux.IdArticulo = (int)datos.Lector["Id"];

                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Codigo"))))
                        aux.Codigo = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.IdMarca = new Marca();
                    if (!(datos.Lector["marca"] is DBNull))
                    {
                        aux.IdMarca.IdMarca = (int)datos.Lector["IdMarca"];
                        aux.IdMarca.Descripcion = (string)datos.Lector["marca"];
                    }

                    aux.IdCategoria = new Categoria();
                    if (!(datos.Lector["Categoria"] is DBNull))
                    {
                        aux.IdCategoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                        aux.IdCategoria.Descripcion = (string)datos.Lector["Categoria"];
                    }

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];

                    //cargar imagenes en la lista de Articulo
                    AccesoDatos datos2 = new AccesoDatos();

                    try
                    {
                        datos2.setearSPconParametro("storedImg", aux.IdArticulo);
                        datos2.ejecutarLectura();

                        List<Imagen> LImagenes = new List<Imagen>();
                        bool imagenEncontrada = false;

                        while (datos2.Lector.Read())
                        {
                            Imagen auxI;

                            if (!(datos2.Lector["ImagenUrl"] is DBNull))
                            {
                                auxI = new Imagen();
                                auxI.ImagenURL = (string)datos2.Lector["ImagenUrl"];
                                imagenEncontrada = true;

                                LImagenes.Add(auxI);
                            }
                        }

                        if (!imagenEncontrada)
                        {
                            Imagen auxI = new Imagen();
                            auxI.ImagenURL = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
                            LImagenes.Add(auxI);
                        }

                        aux.ListaImagenes = LImagenes;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        datos2.cerrarConexion();
                    }

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

        public List<Articulo> listarConSP(string filtro)
        {
            AccesoDatos datos = new AccesoDatos();

            List<Articulo> lista = new List<Articulo>();

            try
            {
                datos.setearSP("storedFiltro");
                datos.limpiarParametros(datos);
                datos.setearParametro("@filtro", filtro);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.IdArticulo = (int)datos.Lector["Id"];

                    if (!(datos.Lector.IsDBNull(datos.Lector.GetOrdinal("Codigo"))))
                        aux.Codigo = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.IdMarca = new Marca();
                    if (!(datos.Lector["marca"] is DBNull))
                    {
                        aux.IdMarca.IdMarca = (int)datos.Lector["IdMarca"];
                        aux.IdMarca.Descripcion = (string)datos.Lector["marca"];
                    }

                    aux.IdCategoria = new Categoria();
                    if (!(datos.Lector["Categoria"] is DBNull))
                    {
                        aux.IdCategoria.IdCategoria = (int)datos.Lector["IdCategoria"];
                        aux.IdCategoria.Descripcion = (string)datos.Lector["Categoria"];
                    }

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];


                    //cargar imagenes en la lista de Articulo
                    AccesoDatos datos2 = new AccesoDatos();

                    try
                    {
                        datos2.setearSPconParametro("storedImg", aux.IdArticulo);
                        datos2.ejecutarLectura();

                        List<Imagen> LImagenes = new List<Imagen>();
                        bool imagenEncontrada = false;

                        while (datos2.Lector.Read())
                        {
                            Imagen auxI;

                            if (!(datos2.Lector["ImagenUrl"] is DBNull))
                            {
                                auxI = new Imagen();
                                auxI.ImagenURL = (string)datos2.Lector["ImagenUrl"];
                                imagenEncontrada = true;

                                LImagenes.Add(auxI);
                            }

                        }

                        if (!imagenEncontrada)
                        {
                            Imagen auxI = new Imagen();
                            auxI.ImagenURL = "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
                            LImagenes.Add(auxI);
                        }

                        aux.ListaImagenes = LImagenes;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        datos2.cerrarConexion();
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