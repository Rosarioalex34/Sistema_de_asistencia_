using ORUSCURSO.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ORUSCURSO.Datos
{
  public class DatosAsistencias
    {
		public DataTable buscarAsistenciasId(int Idpersonal)
		{
			DataTable dt = new DataTable();
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("buscarAsistenciasId", Conexion.con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				da.SelectCommand.Parameters.AddWithValue("@Idpersonal", Idpersonal);
				da.Fill(dt);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
				Conexion.cerrar();
			}
			return dt;
		}
		public bool InsertarAsistencias(LogicaAsistencias parametros)
		{
			try 
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("Insertar_ASISTENCIAS", Conexion.con);
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
				MessageBox.Show(ex.Message);
				return false;
			}
			finally
			{
				Conexion.cerrar();
			}
		}
		public bool ConfirmarSalida(LogicaAsistencias parametros)
		{
			try
			{
				Conexion.abrir();

				SqlCommand cmd = new SqlCommand("ConfirmarSalida", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Id_personal", parametros.Id_personal);
				cmd.Parameters.AddWithValue("@Fecha_salida", parametros.Fecha_salida);
				cmd.Parameters.AddWithValue("@Horas", parametros.Horas);
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

	}
}
