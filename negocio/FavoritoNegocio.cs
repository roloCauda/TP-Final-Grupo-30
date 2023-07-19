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
    }
}
