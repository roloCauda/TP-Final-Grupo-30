using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public Direccion Direccion { get; set; }
        public DateTime Fecha { get; set; }
        public FormaDePago FormaDePago { get; set; }
        public FormaDeEnvio FormaDeEnvio { get; set; }
        public List<ArticulosXPedido> ListaArtXPedido { get; set; }
        public string CodTransaccion { get; set; }
        public string CodSeguimiento { get; set; }
        public string Observaciones { get; set; }
        public string EstadoPedido { get; set; }
        public Pedido()
        {
            Direccion = new Direccion();
            FormaDePago = new FormaDePago();
            FormaDeEnvio = new FormaDeEnvio();
            ListaArtXPedido = new List<ArticulosXPedido>();
        }
    }
}
