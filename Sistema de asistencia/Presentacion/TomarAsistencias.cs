using Sistema_de_asistencia.Datos;
using Sistema_de_asistencia.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_asistencia.Presentacion
{
    public partial class TomarAsistencias : Form
    {
        public TomarAsistencias()
        {
            InitializeComponent();
        }

        string Identificacion;
        int IdPersonal;
        int Contador;
        DateTime Fecharegistro;

        
        //Hora y fecha
        private void timerHora_Tick(object sender, EventArgs e)
        {
            lblhora2.Text = DateTime.Now.ToString("hh:mm:ss");
            lblfecha.Text = DateTime.Now.ToShortDateString();
        }
        //Buscador
        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            BuscarPersonalIdentidad();
            if (Identificacion == txtIdentificacion.Text)
            {
                buscarAsistenciasId();
                if (Contador == 0)
                {
                    DialogResult resultado = MessageBox.Show("¿Agregar una Observacion?", "Observacion", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (resultado == DialogResult.OK)
                    {
                        panelObservacion.Visible = true;
                        panelObservacion.Location = new Point(Panel1.Location.X,Panel1.Location.Y);
                        panelObservacion.Size = new Size(Panel1.Width, Panel1.Height);
                        panelObservacion.BringToFront();
                        txtObservacion.Clear();
                        txtObservacion.Focus();
                        
                    }
                    else
                    {
                        InsertarAsistencias();
                    }

                }
                else
                {
                    ConfirmarSalida();
                }
            }
        }

        private void ConfirmarSalida()
        {
            LogicaAsistencias parametros = new LogicaAsistencias();
            DatosAsistencias funcion = new DatosAsistencias();
            parametros.Id_personal = IdPersonal;
            parametros.Fecha_salida = DateTime.Now;
            parametros.Horas = Bases.DateDiff(Bases.DateInterval.Hour, Fecharegistro, DateTime.Now);
            if (funcion.ConfirmarSalida(parametros) == true)
            {
                txtaviso.Text = "SALIDA REGISTRADA";
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
            }
        }
        private void InsertarAsistencias()
        {
            if (string.IsNullOrEmpty(txtObservacion.Text))
            {
                txtObservacion.Text = "-";
            }
            LogicaAsistencias parametros = new LogicaAsistencias();
            DatosAsistencias funion = new DatosAsistencias();
            parametros.Id_personal = IdPersonal;
            parametros.Fecha_entrada = DateTime.Now;
            parametros.Fecha_salida = DateTime.Now;
            parametros.Estado = "ENTRADA";
            parametros.Horas = 0;
            parametros.Observacion = txtObservacion.Text;
            if (funion.InsertarAsistencias(parametros)== true)
            {
                txtaviso.Text = "ENTRADA REGISTRADA";
                txtIdentificacion.Clear();
                txtIdentificacion.Focus();
                panelObservacion.Visible = false;
            }
        }
        private void buscarAsistenciasId()
        {
            DataTable dt = new DataTable();
            DatosAsistencias funcion = new DatosAsistencias();
            funcion.buscarAsistenciasId(ref dt, IdPersonal);
            Contador = dt.Rows.Count;
            if(Contador > 0)
            {
                Fecharegistro = Convert.ToDateTime(dt.Rows[0]["Fecha_entrada"]);
            }
        }
        private void BuscarPersonalIdentidad()
        {
            DataTable dt = new DataTable();
            DatosPersonal funcion = new DatosPersonal();
            funcion.BuscarPersonalIdentidad(ref dt,txtIdentificacion.Text);
            if (dt.Rows.Count > 0)
            {
                Identificacion = dt.Rows[0]["Identificacion"].ToString();
                IdPersonal = Convert.ToInt32(dt.Rows[0]["Id_personal"]);
                txtNombre.Text = dt.Rows[0]["Nombres"].ToString();
            }

        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            InsertarAsistencias();
        }
    }
}
