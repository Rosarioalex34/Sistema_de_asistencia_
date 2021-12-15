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
using Sistema_de_asistencia.Presentacion;

namespace Sistema_de_asistencia.Presentacion
{
    public partial class Personal : UserControl
    {
        public Personal()
        {
            InitializeComponent();
        }
        int IdCargos =0;
        int desde = 1;
        int hasta = 10;
        int contador;
        int Idpersonal;
        private int Items_por_pagina = 10;
        string Estado;
        int TotalPaginas;

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            LocalizarDtvCargos();
            panelCargos.Visible = false;
            PanelPaginado.Visible = false;
            PanelRegistros.Visible = true;
            PanelRegistros.Dock = DockStyle.Fill;
            btnGuardarCambioPersonal.Visible = false;
            btnGuardarPersonal.Visible = true;
            Limpiar();
        }
        
        #region Localizacion del datalistaCargos
        private void LocalizarDtvCargos()
        {
            datalistadoCargos.Location = new Point(txtSueldoPorHora.Location.X, txtSueldoPorHora.Location.Y);
            datalistadoCargos.Size = new Size(400, 141);
            datalistadoCargos.Visible = true;
            lblSueldoPorHora.Visible = false;
            PanelBtnGuardarPersonal.Visible = false;
        }
        #endregion

        #region Metodo para la limpieza de los campos

        public void Limpiar()
        {
            txtNombres.Clear();
            txtIdentificacion.Clear();
            txtSueldoPorHora.Clear();
            txtCargos.Clear();
            BuscarCargos();
           
        }
        #endregion
        private void btnGuardarPersonal_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    if (!string.IsNullOrEmpty(cbxPais.Text))
                    {
                        if (IdCargos > 0)
                        {
                            if (!string.IsNullOrEmpty(txtSueldoPorHora.Text))
                            {
                                InsertarPersonal();
                            }
                        }
                    }
                }
            }
        }

        #region Metodo Insertar Personal
        private void InsertarPersonal()
        {
            LogicaPersonal parametros = new LogicaPersonal();
            DatosPersonal funcion = new DatosPersonal();
            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = cbxPais.Text;
            parametros.Id_cargos = IdCargos;
            parametros.SueldoPorHoras = Convert.ToDouble(txtSueldoPorHora.Text);
            if (funcion.InsertarPersonal(parametros) == true)
            {
                MostrarPersonal();
                PanelRegistros.Visible = false;
            }
        
        }
        #endregion

        private void MostrarPersonal()
        {
            DataTable dt = new DataTable();
            DatosPersonal funcion = new DatosPersonal();
            funcion.MostrarPersonal(ref dt, desde,hasta);
            dataListadoPersonal.DataSource = dt;
            DiseñarDtvPersonal();
        }

        private void DiseñarDtvPersonal()
        {
            Bases.DiseñoDtv(ref dataListadoPersonal);
            PanelPaginado.Visible = true;
            dataListadoPersonal.Columns[2].Visible = false;
            dataListadoPersonal.Columns[7].Visible = false;
        }

        #region Metodo Insertar Cargos
        private void InsertarCargos()
        {
            if(!string.IsNullOrEmpty(txtCargosP.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoPorHoraP.Text))
                {
                    LogicaCargos parametros = new LogicaCargos();
                    DCargos funcion = new DCargos();
                    parametros.Cargos = txtCargosP.Text;
                    parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHoraP.Text);
                    if (funcion.insertar_Cargo(parametros) == true)
                    {
                        txtCargos.Clear();
                        BuscarCargos();
                        panelCargos.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agregue el cargo por favor", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agregue el cargo por favor", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }
        #endregion

        #region Metodo Buscar Cargos
        private void BuscarCargos()
        {
            DataTable dt = new DataTable();
            DCargos funcion = new DCargos();
            funcion.buscarCargos(ref dt, txtCargos.Text);
            datalistadoCargos.DataSource = dt;
            Bases.DiseñoDtv(ref datalistadoCargos);
            datalistadoCargos.Columns[1].Visible = false;
            datalistadoCargos.Columns[3].Visible = false;
        }
        #endregion

        private void PanelRegistros_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtCargos_TextChanged(object sender, EventArgs e)
        {
            BuscarCargos();
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            panelCargos.Visible = true;
            panelCargos.Dock = DockStyle.Fill;
            panelCargos.BringToFront();
            btnGuardarP.Visible = true;
            btnGuardarCambiosP.Visible = false;
            txtCargosP.Clear();
            txtSueldoPorHoraP.Clear();
        }
        #region Boton de guardar del panel de cargos
        private void btnGuardarP_Click(object sender, EventArgs e)
        {
            InsertarCargos();
        }
        #endregion
        private void txtSueldoporhoraP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtCargosP, e);
        }

        private void txtSueldoPorHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoPorHora, e);
        }

        private void datalistadoCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datalistadoCargos.Columns["Editar"].Index)
            {
                EditarCargo();
            }
            if (e.ColumnIndex == datalistadoCargos.Columns["Cargos"].Index)
            {
                ObtenerDatosCargos();
            }
        }
        #region Metodo para Obtener ls datos
        private void ObtenerDatosCargos()
        {
            IdCargos = Convert.ToInt32(datalistadoCargos.SelectedCells[1].Value);
            txtCargos.Text = datalistadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoPorHora.Text = datalistadoCargos.SelectedCells[3].Value.ToString();
            datalistadoCargos.Visible = false;
            PanelBtnGuardarPersonal.Visible = true;
            lblSueldoPorHora.Visible = true;
        }
        #endregion

        #region Metodo para editar los datos
        private void EditarCargo()
        {
            IdCargos = Convert.ToInt32(datalistadoCargos.SelectedCells[1].Value);
            txtCargosP.Text = datalistadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoPorHoraP.Text = datalistadoCargos.SelectedCells[3].Value.ToString();
            btnGuardarP.Visible = false;
            btnGuardarCambiosP.Visible = true;
            txtCargosP.Focus();
            txtCargosP.SelectAll();
            panelCargos.Visible = true;
            panelCargos.Dock = DockStyle.Fill;
            panelCargos.BringToFront();
        }
        #endregion

        private void btnVolverCargos_Click(object sender, EventArgs e)
        {
            panelCargos.Visible = false;
        }

        private void btnVolverPersonal_Click(object sender, EventArgs e)
        {
            PanelRegistros.Visible = false;
        }

        private void btnGuardarCambiosP_Click(object sender, EventArgs e)
        {
            EditarCargosPersonal();
        }     

        #region Metodo para editar los cargos 
        private void EditarCargosPersonal()
        {
            LogicaCargos parametros = new LogicaCargos();
            DCargos funcion = new DCargos();
            parametros.Id_cargos = IdCargos;
            parametros.Cargos = txtCargosP.Text;
            parametros.SueldoPorHora = Convert.ToDouble(txtSueldoPorHoraP.Text);
            if (funcion.editarCargo(parametros) == true)
            {
                txtCargos.Clear();
                BuscarCargos();
                panelCargos.Visible = false;
            }
        }
        #endregion

        private void Personal_Load(object sender, EventArgs e)
        {
            MostrarPersonal();
            
        }

        private void dataListadoPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListadoPersonal.Columns["Eliminar"].Index)
            {
                DialogResult result = MessageBox.Show("¿Solo se cambiara el estado para que no pueda acceder, Desea continuar?", "Eliminando reguistros", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    EliminarPersonal();
                }
               
            }
            if (e.ColumnIndex == dataListadoPersonal.Columns["EditarP"].Index)
            {
                ObtenerDatos();
            }
        }

        private void ObtenerDatos()
        {
            Idpersonal = Convert.ToInt32(dataListadoPersonal.SelectedCells[2].Value);
            Estado = dataListadoPersonal.SelectedCells[8].Value.ToString();
            if (Estado == "ELIMINADO")
            {
                RestaurarPersonal();
            }
            else
            {
                txtNombres.Text = dataListadoPersonal.SelectedCells[3].Value.ToString();
                txtIdentificacion.Text = dataListadoPersonal.SelectedCells[4].Value.ToString();
                cbxPais.Text = dataListadoPersonal.SelectedCells[10].Value.ToString();
                txtCargos.Text = dataListadoPersonal.SelectedCells[6].Value.ToString();
                IdCargos = Convert.ToInt32( dataListadoPersonal.SelectedCells[7].Value);
                txtSueldoPorHora.Text = dataListadoPersonal.SelectedCells[5].Value.ToString();
                PanelPaginado.Visible = false;
                PanelRegistros.Visible = true;
                PanelRegistros.Dock = DockStyle.Fill;
                datalistadoCargos.Visible = false;
                lblSueldoPorHora.Visible = true;
                PanelBtnGuardarPersonal.Visible = true;
                btnGuardarPersonal.Visible = false;
                btnGuardarCambioPersonal.Visible = true;
                panelCargos.Visible = false;

            }

        }
        private void RestaurarPersonal()
        {

        }
        private void EliminarPersonal()
        {
            Idpersonal = Convert.ToInt32(dataListadoPersonal.SelectedCells[2].Value);
            LogicaPersonal parametros = new LogicaPersonal();
            DatosPersonal funcion = new DatosPersonal();
            parametros.Id_personal = Idpersonal;
            if (funcion.eliminarPersonal(parametros) == true)
            {
                MostrarPersonal();
            }
        }
    }
}
