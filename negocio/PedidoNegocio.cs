﻿using dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                datos.setearConsulta("select P.Id, P.Fecha, F.Descripcion, P.IdFormaPago, P.IdFormaEnvio, P.CodigoDeTransaccion, " +
                                    "P.CodigoSeguimiento, P.Observaciones, P.EstadoPedido FROM PEDIDOS P " +
                                    "INNER JOIN FormasDePago F ON F.Id = P.IdFormaPago " +
                                    "INNER JOIN FormasDeEnvio E ON E.Id = P.IdFormaEnvio " +
                                    "where P.IdCliente = @IdCliente");
                datos.setearParametro("@IdCliente", IdCliente);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();

                    aux.IdPedido = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.FormaDePago.IdFormaDePago = (int)datos.Lector["IdFormaPago"];
                    aux.FormaDePago.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.FormaDeEnvio.IdFormaDeEnvio = (int)datos.Lector["IdFormaEnvio"];
                    aux.FormaDeEnvio.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["CodigoDeTransaccion"] is DBNull))
                        aux.CodTransaccion = (string)datos.Lector["CodigoDeTransaccion"];

                    if (!(datos.Lector["CodigoSeguimiento"] is DBNull))
                        aux.CodSeguimiento = (string)datos.Lector["CodigoSeguimiento"];

                    if (!(datos.Lector["Observaciones"] is DBNull))
                        aux.Observaciones = (string)datos.Lector["Observaciones"];

                    if (!(datos.Lector["EstadoPedido"] is DBNull))
                        aux.EstadoPedido = (string)datos.Lector["EstadoPedido"];

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
                datos.setearConsulta("select P.Id, P.Fecha, F.Descripcion, P.IdFormaPago, P.IdFormaEnvio, P.CodigoDeTransaccion, " +
                                    "P.CodigoSeguimiento, P.Observaciones, P.EstadoPedido FROM PEDIDOS P " +
                                    "INNER JOIN FormasDePago F ON F.Id = P.IdFormaPago " +
                                    "INNER JOIN FormasDeEnvio E ON E.Id = P.IdFormaEnvio");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();

                    aux.IdPedido = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.FormaDePago.IdFormaDePago = (int)datos.Lector["IdFormaPago"];
                    aux.FormaDePago.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.FormaDeEnvio.IdFormaDeEnvio = (int)datos.Lector["IdFormaEnvio"];
                    if (!(datos.Lector["CodigoDeTransaccion"] is DBNull))
                        aux.CodTransaccion = (string)datos.Lector["CodigoDeTransaccion"];

                    if (!(datos.Lector["CodigoSeguimiento"] is DBNull))
                        aux.CodSeguimiento = (string)datos.Lector["CodigoSeguimiento"];

                    if (!(datos.Lector["Observaciones"] is DBNull))
                        aux.Observaciones = (string)datos.Lector["Observaciones"];

                    if (!(datos.Lector["EstadoPedido"] is DBNull))
                        aux.EstadoPedido = (string)datos.Lector["EstadoPedido"];

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
        public List<Pedido> listarPedidos(string filtroEstado)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select P.Id, P.Fecha, F.Descripcion, P.IdFormaPago, P.IdFormaEnvio, P.CodigoDeTransaccion, " +
                                    "P.CodigoSeguimiento, P.Observaciones, P.EstadoPedido FROM PEDIDOS P " +
                                    "INNER JOIN FormasDePago F ON F.Id = P.IdFormaPago " +
                                    "INNER JOIN FormasDeEnvio E ON E.Id = P.IdFormaEnvio " +
                                    "where EstadoPedido like @filtro");
                datos.setearParametro("@filtro", "%" + filtroEstado + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido aux = new Pedido();

                    aux.IdPedido = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.FormaDePago.IdFormaDePago = (int)datos.Lector["IdFormaPago"];
                    aux.FormaDePago.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.FormaDeEnvio.IdFormaDeEnvio = (int)datos.Lector["IdFormaEnvio"];
                    if (!(datos.Lector["CodigoDeTransaccion"] is DBNull))
                        aux.CodTransaccion = (string)datos.Lector["CodigoDeTransaccion"];

                    if (!(datos.Lector["CodigoSeguimiento"] is DBNull))
                        aux.CodSeguimiento = (string)datos.Lector["CodigoSeguimiento"];

                    if (!(datos.Lector["Observaciones"] is DBNull))
                        aux.Observaciones = (string)datos.Lector["Observaciones"];

                    if (!(datos.Lector["EstadoPedido"] is DBNull))
                        aux.EstadoPedido = (string)datos.Lector["EstadoPedido"];

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
                datos.setearConsulta("INSERT INTO Pedidos (IdCliente, IdFormaPago, IdFormaEnvio, Fecha, IdDireccion) " +
                    "OUTPUT Inserted.ID VALUES (@IdCliente, @IdFormaPago, @IdFormaEnvio, @Fecha, @IdDireccion);");

                datos.setearParametro("@IdFormaPago", pedido.FormaDePago.IdFormaDePago);
                datos.setearParametro("@IdCliente", pedido.IdCliente);
                datos.setearParametro("@IdFormaEnvio", pedido.FormaDeEnvio.IdFormaDeEnvio);
                datos.setearParametro("@Fecha", pedido.Fecha);
                datos.setearParametro("@IdDireccion", pedido.Direccion.IdDireccion != null ? (object)pedido.Direccion.IdDireccion : (object)DBNull.Value);

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

        public int consultaIdEnvio(int IdPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            int aux;
            try
            {
                datos.setearConsulta("SELECT IdFormaEnvio FROM Pedidos WHERE Id = @IdPedido");
                datos.setearParametro("@IdPedido", IdPedido);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = Convert.ToInt32(datos.Lector["IdFormaEnvio"]);
                    return aux;
                }
                else
                {
                    return -1;
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

        public int consultaIdPago(int IdPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            int aux;
            try
            {
                datos.setearConsulta("SELECT IdFormaPago FROM Pedidos WHERE Id = @IdPedido");
                datos.setearParametro("@IdPedido", IdPedido);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux = Convert.ToInt32(datos.Lector["IdFormaPago"]);
                    return aux;
                }
                else
                {
                    return -1;
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

        public bool actualizarPedido(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();
            bool exito = false;

            try
            {
                datos.setearConsulta("UPDATE Pedidos SET IdFormaPago = @idFormaDePago, " +
                                    "IdFormaEnvio = @idFormaDeEnvio, " +
                                    "CodigoDeTransaccion = @CodTrans, " +
                                    "CodigoSeguimiento = @CodSegui, " +
                                    "Observaciones = @observaciones, " +
                                    "EstadoPedido = @estadoPedido WHERE Id = @IdPedido");
                datos.setearParametro("@IdPedido", pedido.IdPedido);
                datos.setearParametro("@idFormaDePago", pedido.FormaDePago.IdFormaDePago);
                datos.setearParametro("@idFormaDeEnvio", pedido.FormaDeEnvio.IdFormaDeEnvio);
                datos.setearParametro("@CodTrans", pedido.CodTransaccion != null ? (object)pedido.CodTransaccion : DBNull.Value);
                datos.setearParametro("@CodSegui", pedido.CodSeguimiento != null ? (object)pedido.CodSeguimiento : DBNull.Value);
                datos.setearParametro("@observaciones", pedido.Observaciones != null ? (object)pedido.Observaciones : DBNull.Value);
                datos.setearParametro("@estadoPedido", pedido.EstadoPedido);

                datos.ejecutarAccion();
                exito = true; // Si llega aquí sin lanzar excepciones, la actualización fue exitosa
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

            return exito;
        }

        public Pedido cargarPedido(int Idpedido)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();
            Pedido aux = new Pedido();

            try
            {
                datos.setearConsulta("select P.Id, P.Fecha, F.Descripcion, P.IdFormaPago, P.IdFormaEnvio, P.CodigoDeTransaccion, " +
                                    "P.CodigoSeguimiento, P.Observaciones, P.EstadoPedido FROM PEDIDOS P " +
                                    "INNER JOIN FormasDePago F ON F.Id = P.IdFormaPago " +
                                    "INNER JOIN FormasDeEnvio E ON E.Id = P.IdFormaEnvio " +
                                    "where P.Id = @IdPedido");
                datos.setearParametro("@IdPedido", Idpedido);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux.IdPedido = (int)datos.Lector["Id"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.FormaDePago.IdFormaDePago = (int)datos.Lector["IdFormaPago"];
                    aux.FormaDePago.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.FormaDeEnvio.IdFormaDeEnvio = (int)datos.Lector["IdFormaEnvio"];
                    if (!(datos.Lector["CodigoDeTransaccion"] is DBNull))
                        aux.CodTransaccion = (string)datos.Lector["CodigoDeTransaccion"];

                    if (!(datos.Lector["CodigoSeguimiento"] is DBNull))
                        aux.CodSeguimiento = (string)datos.Lector["CodigoSeguimiento"];

                    if (!(datos.Lector["Observaciones"] is DBNull))
                        aux.Observaciones = (string)datos.Lector["Observaciones"];

                    if (!(datos.Lector["EstadoPedido"] is DBNull))
                        aux.EstadoPedido = (string)datos.Lector["EstadoPedido"];
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
