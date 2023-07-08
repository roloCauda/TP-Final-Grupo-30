using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public enum TipoUsuario
    {
        CLIENTE = 1,
        EMPLEADO = 2,
        ADMIN = 3
    }
    public class Usuario
    {
        public int DNI { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }
        public Direccion IdDireccion { get; set; }
        public TipoUsuario TipoUsuario { get; set; }

        public Usuario(int dni, string pass, int num)
        {
            DNI = dni;
            Contraseña = pass;
            switch (num)
            {
                case 1:
                    TipoUsuario = TipoUsuario.CLIENTE;
                    break;
                case 2:
                    TipoUsuario = TipoUsuario.EMPLEADO;
                    break;
                case 3:
                    TipoUsuario = TipoUsuario.ADMIN;
                    break;
                default:

                    break;
            }
        }
    }
}
