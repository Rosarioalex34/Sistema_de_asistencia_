using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ORUSCURSO.Logica;
namespace ORUSCURSO.Datos
{
   public  class DatosUsuarios
    {
		public bool InsertarUsuarios(LogicaUsuarios parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("insertar_usuario", Conexion.con);
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
				MessageBox.Show(ex.Message);
				return false;
			}
			finally
			{
				Conexion.cerrar();
			}

		}
		public void mostrar_Usuarios(ref DataTable dt)
		{
			//DataTable dt = new DataTable();
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("Select * from Usuarios", Conexion.con);
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
			//return dt;
		}
		public void ObtenerIdUsuario(ref int Idusuario,string Login)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("ObtenerIdUsuario", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@Login", Login);
				Idusuario =Convert.ToInt32(cmd.ExecuteScalar());
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
			finally
			{
				Conexion.cerrar();
			}
		}
		public bool eliminar_Usuarios(LogicaUsuarios parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("eliminar_usuarios", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idusuarios", parametros.IdUsuario);
				cmd.Parameters.AddWithValue("@login", parametros.Login);

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
		public bool restaurar_usuarios(LogicaUsuarios parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("restaurar_usuarios", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idUsuarios", parametros.IdUsuario);
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
		public bool editar_Usuarios(LogicaUsuarios parametros)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("editar_Usuarios", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@idusuarios", parametros.IdUsuario);
				cmd.Parameters.AddWithValue("@nombres", parametros.Nombre);
				cmd.Parameters.AddWithValue("@Login", parametros.Login);
				cmd.Parameters.AddWithValue("@Password", parametros.Password);
				cmd.Parameters.AddWithValue("@Icono", parametros.Icono);

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
		public DataTable buscar_Usuarios(string buscador)
		{
			DataTable dt = new DataTable();
			try
			{
				Conexion.abrir();
				SqlDataAdapter da = new SqlDataAdapter("buscar_usuarios", Conexion.con);
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
		public void VerificarUsuarios(ref string Indicador)
		{
			try
			{
				int Iduser;
				Conexion.abrir();
				SqlCommand da = new SqlCommand("Select idUsuario From Usuarios", Conexion.con);
				Iduser = Convert.ToInt32(da.ExecuteScalar());
				Conexion.cerrar();
				Indicador = "Correcto";
			}
			catch (Exception )
			{

				Indicador = "Incorrecto";
			}
		}
		public void validarUsuario(LogicaUsuarios parametros, ref int id)
		{
			try
			{
				Conexion.abrir();
				SqlCommand cmd = new SqlCommand("validar_usuario", Conexion.con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@password", parametros.Password);
				cmd.Parameters.AddWithValue("@login", parametros.Login);
				id = Convert.ToInt32(cmd.ExecuteScalar());


			}
			catch (Exception )
			{
				id = 0;

			}
			finally
			{
				Conexion.cerrar();
			}
		}
		}
	}

