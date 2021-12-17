using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Sistema_de_asistencia.Datos
{
    class DatosModulos
    {
        public void MostrarModulos (ref DataTable dt)
        {
            try
            {
                Conecxion.abrir();
                SqlDataAdapter da = new SqlDataAdapter("Select * from Modulos", Conecxion.con);
                da.Fill(dt);

            }
            catch ( Exception ex)
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
