using dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace negocio
{
    public class StockNegocio
    {
        public void descontarStock(List<ArticulosXPedido> lista)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE STOCK SET Cantidad = Cantidad - @Cantidad WHERE IdArticulo = @IdArticulo");

                foreach (ArticulosXPedido item in lista)
                {
                    datos.setearParametro("@cantidad", item.Cantidad);
                    datos.setearParametro("@idArticulo", item.IdArticulo);
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

        public int consultarStock(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            int cantidad;
            try
            {
                datos.setearConsulta("SELECT Cantidad FROM STOCK WHERE IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);

                cantidad = datos.ejecutarEscalar();

                return cantidad;
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
