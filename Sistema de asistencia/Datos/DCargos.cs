using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_de_asistencia.Datos;
using Sistema_de_asistencia.Logica;

namespace Sistema_de_asistencia.Datos
{
    class DCargos
    {
		public bool insertar_Cargo(LogicaCargos parametros)
		{
			try
			{
				Conecxion.abrir();
				SqlCommand cmd = new SqlCommand("insertar_Cargo", Conecxion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Cargos", parametros.Cargos);
				cmd.Parameters.AddWithValue("@SueldoPorHora", parametros.SueldoPorHora);
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
		public bool editarCargo(LogicaCargos parametros)
		{
			try
			{
				Conecxion.abrir();
				SqlCommand cmd = new SqlCommand("editarCargo", Conecxion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@id", parametros.Id_cargos);
				cmd.Parameters.AddWithValue("@Cargo", parametros.Cargos);
				cmd.Parameters.AddWithValue("@Sueldo", parametros.SueldoPorHora);
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
		public void buscarCargos(ref DataTable dt, string buscador)
		{
			try
			{
				Conecxion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("buscarCargos", Conecxion.con);
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
				Conecxion.cerrar();
			}
		}
	}
}
