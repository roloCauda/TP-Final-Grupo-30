using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Marca
    {
        public int IdMarca { get; set; }
        public string Descripcion { get; set; }

        public string ImagenURL { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }
}
