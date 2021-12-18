using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Sistema_de_asistencia.Datos;
using Sistema_de_asistencia.Logica;
using System.Windows.Forms;
using System.Data;

namespace Sistema_de_asistencia.Datos
{
    public class DatosPermisos
    {
        public bool Insertar_Permisos(LogicaPermisos parametros)
        {
            try
            {
                Conecxion.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Permisos", Conecxion.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdModulos", parametros.IdModulos);
                cmd.Parameters.AddWithValue("@IdUsuarios", parametros.IdUsuarios);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
                
            }
            finally
            {
                Conecxion.cerrar();
            }
        }
        public void mostrar_Permisos(ref DataTable dt, LogicaPermisos parametros)
        {
            try
            {
                Conecxion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_Permisos", Conecxion.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idusuarios", parametros.IdUsuarios);
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
        public bool eliminar_Permisos(LogicaPermisos parametros)
        {
            try
            {
                Conecxion.abrir();
                SqlCommand cmd = new SqlCommand("eliminar_Permisos", Conecxion.con);
                cmd.CommandType = CommandType.StoredProcedure;              
                cmd.Parameters.AddWithValue("@IdUsuarios", parametros.IdUsuarios);
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

    }
}
