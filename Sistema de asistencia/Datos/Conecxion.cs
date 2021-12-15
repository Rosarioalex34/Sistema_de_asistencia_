using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Sistema_de_asistencia.Datos
{
    public class Conecxion
    {
        public static string conexion = @"Data source =DESKTOP-NDJCG0S; Initial Catalog=OrusAssitence; Integrated Security=true";
        public static SqlConnection con = new SqlConnection(conexion); 

        public static void abrir()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public static void cerrar()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
