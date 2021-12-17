using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sistema_de_asistencia.Datos;
using Sistema_de_asistencia.Logica;

namespace Sistema_de_asistencia.Presentacion
{
    public partial class ControlUsuarios : UserControl
    {
        public ControlUsuarios()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Limpiar();
            HabilitarPaneles();
            MostrarModulos();
        }
        //Metodo para limpiar
        private void Limpiar()
        {
            txtNombre.Clear();
            txtContrasena.Clear();
            txtUsuario.Clear();
        }
        //Proceso para habilitar los paneles
        private void HabilitarPaneles()
        {
            panelRegistros.Visible = true;
            lblAnucioIcono.Visible = true;
            panelIcono.Visible = false;
            panelRegistros.Dock = DockStyle.Fill;
            panelRegistros.BringToFront();
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;
        }
        //Proceso para mostrar los modulos
        private void MostrarModulos()
        {
            DatosModulos funcion = new DatosModulos();
            DataTable dt = new DataTable();
            funcion.MostrarModulos(ref dt);
            datalistadoUsuarios.DataSource = dt;
        }
    }
}
