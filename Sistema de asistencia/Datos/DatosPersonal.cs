using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Sistema_de_asistencia.Logica;
using System.Windows.Forms;

namespace Sistema_de_asistencia.Datos
{
   public class DatosPersonal
    {
        public bool InsertarPersonal(LogicaPersonal parametros)
        {
            try
            {
                Conecxion.abrir();
                SqlCommand com = new SqlCommand("InsertarPersonal", Conecxion.con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                com.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                com.Parameters.AddWithValue("@Pais", parametros.Pais);
                com.Parameters.AddWithValue("@Id_cargos", parametros.Id_cargos);
                com.Parameters.AddWithValue("SueldoPorHora", parametros.SueldoPorHoras);
                com.ExecuteNonQuery();
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
        public bool EditarPersonal(LogicaPersonal parametros)
        {
            try
            {
                Conecxion.abrir();
                SqlCommand com = new SqlCommand("EditarPersonal", Conecxion.con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
                com.Parameters.AddWithValue("@Nombres", parametros.Nombres);
                com.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
                com.Parameters.AddWithValue("@Pais", parametros.Pais);
                com.Parameters.AddWithValue("@Id_cargos", parametros.Id_cargos);
                com.Parameters.AddWithValue("SueldoPorHora", parametros.SueldoPorHoras);
                com.ExecuteNonQuery();
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
        public bool eliminarPersonal(LogicaPersonal parametros)
        {
            try
            {
                Conecxion.abrir();
                SqlCommand com = new SqlCommand("eliminarPersonal", Conecxion.con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Idpersonal", parametros.Id_personal);             
                com.ExecuteNonQuery();
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
        public void MostrarPersonal(ref DataTable dt, int desde, int hasta)
        {
            try
            {
                Conecxion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("MostarPersonal",Conecxion.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
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
        public void BuscarPersonal(ref DataTable dt, int desde, int hasta, string buscador)
        {
            try
            {
                Conecxion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("BuscarPersonal", Conecxion.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Desde", desde);
                da.SelectCommand.Parameters.AddWithValue("@Hasta", hasta);
                da.SelectCommand.Parameters.AddWithValue("@Buscador", buscador);
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



    }
}
