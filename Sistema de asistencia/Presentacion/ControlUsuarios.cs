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
using System.IO;
namespace Sistema_de_asistencia.Presentacion
{
    public partial class ControlUsuarios : UserControl
    {
        public ControlUsuarios()
        {
            InitializeComponent();
        }
        int IdUsuarios;
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
            datalistadoModulos.DataSource = dt;
            datalistadoModulos.Columns[1].Visible = false;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombre.Text))
            {
                if (!string.IsNullOrEmpty(txtUsuario.Text))
                {
                    if (!string.IsNullOrEmpty(txtContrasena.Text))
                    {
                        if(lblAnucioIcono.Visible == false)
                        {
                            insertar_usuarios();
                        }
                        else
                        {
                            MessageBox.Show("Ingrese un Icono");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese la contraseña");
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese el Usuario");
                }
            }
            else
            {
                MessageBox.Show("Ingrese el nombre");
            }
        }
        private void insertar_usuarios()
        {
            LogicaUsuarios parametros = new LogicaUsuarios();
            DatosUsuarios funcion = new DatosUsuarios();
            parametros.Nombre = txtNombre.Text;
            parametros.Login = txtUsuario.Text;
            parametros.Password = txtContrasena.Text;
            MemoryStream ms = new MemoryStream();
            Icono.Image.Save(ms, Icono.Image.RawFormat);
            parametros.Icono = ms.GetBuffer();
            if (funcion.InsertarUsuarios(parametros)==true)
            {
                ObtenerIdUsuario();
                Insertar_Permisos();
            }
        }
        private void Insertar_Permisos()
        {
            foreach(DataGridViewRow row in datalistadoModulos.Rows)
            {
                int IdModulos = Convert.ToInt32(row.Cells["IdModulos"].Value);
                bool marcado = Convert.ToBoolean(row.Cells["Marcar"].Value);
                if (marcado == true)
                {
                    LogicaPermisos parametros = new LogicaPermisos();
                    DatosPermisos funcion = new DatosPermisos();
                    parametros.IdModulos = IdModulos;
                    parametros.IdUsuarios = IdUsuarios;
                    funcion.Insertar_Permisos(parametros);
                    
                }
                MostrarUsuarios();
                panelRegistros.Visible = false;
            }
        }
        private void MostrarUsuarios()
        {
            DataTable dt = new DataTable();
            DatosUsuarios funcion = new DatosUsuarios();
            funcion.MostrarUsuarios(ref dt);
            dataListadoUsuarios.DataSource = dt;
            DiseñaDtvUsuarios();
        }
        public void DiseñaDtvUsuarios()
        {
            Bases.DiseñoDtv(ref dataListadoUsuarios);
            Bases.DiseñoDtvEliminar(ref dataListadoUsuarios);
            dataListadoUsuarios.Columns[2].Visible = false;
            dataListadoUsuarios.Columns[5].Visible = false;
            dataListadoUsuarios.Columns[6].Visible = false;
        }
        private void ObtenerIdUsuario()
        {
            DatosUsuarios funcion = new DatosUsuarios();
            funcion.ObtenerIdUsuario(ref IdUsuarios, txtUsuario.Text);

        }
        private void lblAnucioIcono_Click(object sender, EventArgs e)
        {
            MostrarPanelIcono();
        }
        private void MostrarPanelIcono()
        {
            panelIcono.Visible = true;
            panelIcono.Dock = DockStyle.Fill;
            panelIcono.BringToFront();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox3.Image;
            OcultarPanelIcono();
        }
        private void OcultarPanelIcono()
        {
            panelIcono.Visible = false;
            lblAnucioIcono.Visible = false;
            Icono.Visible = true;
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox4.Image;
            OcultarPanelIcono();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox5.Image;
            OcultarPanelIcono();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox6.Image;
            OcultarPanelIcono();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox7.Image;
            OcultarPanelIcono();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox8.Image;
            OcultarPanelIcono();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox9.Image;
            OcultarPanelIcono();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Icono.Image = pictureBox10.Image;
            OcultarPanelIcono();
        }

        private void AgregarIconoPC_Click(object sender, EventArgs e)
        {
            dlg.InitialDirectory = "";
            dlg.Filter = "Imagenes| *.jpg;*.png";
            dlg.FilterIndex = 2;
            dlg.Title = "Cargador de imagenes";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Icono.BackgroundImage = null;
                Icono.Image = new Bitmap(dlg.FileName);
                OcultarPanelIcono();
            }
        }
        private void Icono_Click(object sender, EventArgs e)
        {
            MostrarPanelIcono();
        }

        private void lblAnucioIcono_Click_1(object sender, EventArgs e)
        {
            MostrarPanelIcono();
        }

        private void ControlUsuarios_Load(object sender, EventArgs e)
        {
            MostrarUsuarios();
        }

        private void txtContrasena_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            panelRegistros.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OcultarPanelIcono();
        }
    }
}
