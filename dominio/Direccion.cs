using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Direccion
    {
        public int? IdDireccion { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
        public int? Piso { get; set; }
        public string Departamento { get; set; }
        public string CodPostal { get; set; }
        public Localidad Localidad { get; set; }
        public Provincia Provincia { get; set; }

        public Direccion()
        {
            Localidad = new Localidad();
            Provincia = new Provincia();
        }
    }
}
