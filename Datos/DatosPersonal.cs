using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Sistema_de_asistencia.Logica;
using System.Windows.Forms;
namespace Sistema_de_asistencia.Datos
{
   public  class DatosPersonal
    {
        public bool InsertarPersonal(LogicaPersonal parametros )
        {
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("InsertarPersonal", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
				cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
				cmd.Parameters.AddWithValue("@Pais", parametros.Pais );
				cmd.Parameters.AddWithValue("@Id_cargos", parametros.Id_cargo);
				cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHoras);
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
				Conexion.cerrar();
			}
		}
		public bool editarPersonal(LogicaPersonal parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("editarPersonal", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
				cmd.Parameters.AddWithValue("@Nombres", parametros.Nombres);
				cmd.Parameters.AddWithValue("@Identificacion", parametros.Identificacion);
				cmd.Parameters.AddWithValue("@Pais", parametros.Pais);
				cmd.Parameters.AddWithValue("@Id_cargos", parametros.Id_cargo);
				cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHoras);
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
				Conexion.cerrar();
			}
		}
		public bool eliminarPersonal(LogicaPersonal parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("eliminarPersonal", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Idpersonal", parametros.Id_personal);;
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
				Conexion.cerrar();
			}
		}
		public DataTable MostrarPersonal(int desde,int hasta)
		{
			DataTable dt = new DataTable();
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("MostarPersonal", Conexion.con);
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
				Conexion.cerrar();
			}
			return dt;
		}
		public DataTable BuscarPersonal(int desde, int hasta,string buscador)
		{
			DataTable dt = new DataTable();
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("BuscarPersonal", Conexion.con);
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
				Conexion.cerrar();
			}
			return dt;
		}
		public DataTable BuscarPersonalIdentidad(string buscador)
		{
			DataTable dt = new DataTable();
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("BuscarPersonalIdentidad", Conexion.con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@Buscador", buscador);
				da.Fill(dt);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.StackTrace);
			}
			finally
			{
				Conexion.cerrar();
			}
			return dt;
		}
		public bool restaurar_personal(LogicaPersonal parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("restaurar_personal", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Idpersonal", parametros.Id_personal); ;
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
				Conexion.cerrar();
			}
		}
		public void ContarPersonal(ref int Contador)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("select Count(Id_personal) from Personal", Conexion.con);
				Contador =Convert.ToInt32( cmd.ExecuteScalar());
			}
			catch (Exception )
			{
				Contador = 0;
			}
			finally
			{
				Conexion.cerrar();
			}
		}

	}
}
