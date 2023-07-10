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
        List<ArticulosXPedido> ListaArtXPedido { get; set; }
        public Pedido()
        {
            formaDePago = new FormaDePago();
        }
    }
}
