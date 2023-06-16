using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using dominio;

namespace negocio
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        public SqlDataReader Lector
        {
            get { return lector; }
        }
        
        public AccesoDatos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=BD_ecommerce; integrated security=true;");
            //conexion = new SqlConnection("server=.\\UTNSQLSERVER; database=BD_ecommerce; integrated security=true;");

            comando = new SqlCommand();
        }
        public void setearSP(string sp)
        {
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
        }

        public void setearSPconParametro(string sp, int id)
        {            
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = sp;
            limpiarParametros(this);

            comando.Parameters.AddWithValue("@IdArticulo", id);
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void cerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }

        public void setearParametro(string nombre, object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();

                comando.ExecuteNonQuery();//ejecuta la sentencia en la BD

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public int ejecutarEscalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();

                int nuevoId = (int)comando.ExecuteScalar();

                return nuevoId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void limpiarParametros(AccesoDatos dato)
        {
            dato.comando.Parameters.Clear();
        }
    }
}
