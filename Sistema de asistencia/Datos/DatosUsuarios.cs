using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Sistema_de_asistencia.Logica;
using Sistema_de_asistencia.Datos;

namespace Sistema_de_asistencia.Datos
{
    class DatosUsuarios
    {
        public bool InsertarUsuarios(LogicaUsuarios parametros)
        {
            try
            {
                Conecxion.abrir();
                SqlCommand cmd = new SqlCommand("insertar_usuario", Conecxion.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombres", parametros.Nombre);
                cmd.Parameters.AddWithValue("@Login", parametros.Login);
                cmd.Parameters.AddWithValue("@Password", parametros.Password);
                cmd.Parameters.AddWithValue("@Icono", parametros.Icono);
                cmd.Parameters.AddWithValue("@Estado", "ACTIVO");
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
                return false;
            }
            finally
            {
                Conecxion.cerrar();
            }
        }
        public void MostrarUsuarios(ref DataTable dt)
        {
            try
            {
                Conecxion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("select * from Usuarios", Conecxion.con);
                da.Fill(dt);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);

            }
            finally
            {
                Conecxion.cerrar();
            }
        }
        public void ObtenerIdUsuario(ref int IdUsuarios, string Login)
        {
            try
            {
                Conecxion.abrir();
                SqlCommand cmd = new SqlCommand("ObtenerIdUsuario", Conecxion.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Login", Login);
                IdUsuarios = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.StackTrace);
            }
            finally
            {
                Conecxion.cerrar();
            }
        }
    }
}
