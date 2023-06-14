using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Carrito
    {
        public int cantArt { get; set; }
        public List<ItemCarrito> ListaItems { get; set; }
        public decimal total { get; set; }

        public Carrito()
        {
            ListaItems = new List<ItemCarrito>();
        }
    }
}
