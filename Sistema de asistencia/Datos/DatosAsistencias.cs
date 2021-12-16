using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_de_asistencia.Logica;

namespace Sistema_de_asistencia.Datos
{
    class DatosAsistencias
    {
        public void buscarAsistenciasId(ref DataTable dt, int IdPersonal)
        {
            try
            {
                Conecxion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("buscarAsistenciasId", Conecxion.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Idpersonal", IdPersonal);
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

        public bool InsertarAsistencias(LogicaAsistencias parametros)
        {
            try
            {
                Conecxion.abrir();
                SqlCommand cmd = new SqlCommand("Insertar_ASISTENCIAS", Conecxion.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Fecha_entrada", parametros.Fecha_entrada);
                cmd.Parameters.AddWithValue("@Fecha_salida", parametros.Fecha_salida);
                cmd.Parameters.AddWithValue("@Estado", parametros.Estado);
                cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
                cmd.Parameters.AddWithValue("@Observacion", parametros.Observacion);
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

        public bool ConfirmarSalida(LogicaAsistencias parametros)
        {
            try
            {
                Conecxion.abrir();
                SqlCommand cmd = new SqlCommand("ConfirmarSalida", Conecxion.con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
                cmd.Parameters.AddWithValue("@Fecha_salida", parametros.Fecha_salida);
                cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
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
