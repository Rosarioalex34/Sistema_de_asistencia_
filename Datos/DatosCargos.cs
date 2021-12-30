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
   public class DatosCargos
    {
		public bool insertar_Cargo(LogicaCargos parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("insertar_Cargo", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Cargos", parametros.Cargos);
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
		public bool editar_Cargo(LogicaCargos parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("editar_Cargo", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id", parametros.Id_cargos);
				cmd.Parameters.AddWithValue("@Cargos", parametros.Cargos);
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
		public DataTable buscarCargos(string buscador)
		{
			DataTable dt = new DataTable();
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("buscarCargos", Conexion.con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@buscador", buscador);
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
	}
}
