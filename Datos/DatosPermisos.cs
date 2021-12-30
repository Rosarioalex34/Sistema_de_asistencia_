using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ORUSCURSO.Logica;
using System.Windows.Forms;

namespace ORUSCURSO.Datos
{
    public class DatosPermisos
    {
        public bool Insertar_Permisos(LogicaPermisos parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("insertar_Permisos", Conexion.con);
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
                Conexion.cerrar();
            }
        }
        public DataTable mostrar_Permisos(LogicaPermisos parametros)
        {
            DataTable dt = new DataTable();
            try
            {
                Conexion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("mostrar_Permisos", Conexion.con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@idusuarios", parametros.IdUsuarios);

                da.Fill(dt);

                Conexion.cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dt;
        }
        public bool Eliminar_Permisos(LogicaPermisos parametros)
        {
            try
            {
                Conexion.abrir();
                SqlCommand cmd = new SqlCommand("Eliminar_Permisos", Conexion.con);
                cmd.CommandType = CommandType.StoredProcedure;
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
                Conexion.cerrar();
            }
        }
    }
}
