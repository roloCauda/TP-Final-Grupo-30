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
        public DateTime Fecha { get; set; }
        public FormaDePago formaDePago { get; set; }
        public FormaDeEnvio formaDeEnvio { get; set; }
        public List<ArticulosXPedido> ListaArtXPedido { get; set; }

        public string CodTransaccion { get; set; }
        public string CodSeguimiento { get; set; }
        public string Observaciones { get; set; }
        public string EstadoPedido { get; set; }
        public Pedido()
        {
            formaDePago = new FormaDePago();
            formaDeEnvio = new FormaDeEnvio();
            ListaArtXPedido = new List<ArticulosXPedido>();
        }
    }
}
