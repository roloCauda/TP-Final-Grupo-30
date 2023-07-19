using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace dominio
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Marca")]
        public Marca IdMarca { get; set; }
        [DisplayName("Categoría")]
        public Categoria IdCategoria { get; set; }

        public decimal Precio { get; set; }
        public int stock { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
        public List<Imagen> ListaImagenes { get; set;}

        public Articulo() //ver
        {
            ListaImagenes = new List<Imagen>();
        }
    }

}
