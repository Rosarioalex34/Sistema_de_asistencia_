using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Sistema_de_asistencia.Datos
{
	public class DatosModulos
	{
		public DataTable mostrar_Modulos()
		{
			DataTable dt = new DataTable();
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("Select * from Modulos", Conexion.con);
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
	}
}
