using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> listarPedidosPorCliente(int IdCliente)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select P.Id, P.Fecha, F.Descripcion from PEDIDOS P INNER JOIN FormasDePago F ON F.Id = P.Id where P.IdCliente = @IdCliente");
                datos.setearParametro("@IdCliente", IdCliente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();

                    aux.IdPedido = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.formaDePago.Descripcion = (string)datos.Lector["Descripcion"];

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
        public List<Pedido> listarPedidos()
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select P.Id, P.Fecha, F.Descripcion from PEDIDOS P " +
                    "INNER JOIN FormasDePago F ON F.Id = P.Id " +
                    "INNER JOIN USUARIOS U ON P.IdCliente = U.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();

                    aux.IdPedido = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.formaDePago.Descripcion = (string)datos.Lector["Descripcion"];

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

        public decimal totalPedido(int idPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select sum(AP.PrecioUnitario * AP.Cantidad) as 'Total' FROM ARTICULOSxPEDIDO AP where AP.IdPedido = @idPedido");
                datos.setearParametro("@idPedido", idPedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    if (!(datos.Lector["Total"] is DBNull))
                    {
                        return (decimal)datos.Lector["Total"];
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int agregarPedido(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                

                int nuevoId = (int)datos.ejecutarEscalar();

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
    }
}
