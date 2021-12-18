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
        int Contador;
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
        
        //Localizacion del datalistaCargos
        private void LocalizarDtvCargos()
        {
            datalistadoCargos.Location = new Point(txtSueldoPorHora.Location.X, txtSueldoPorHora.Location.Y);
            datalistadoCargos.Size = new Size(469, 141);
            datalistadoCargos.Visible = true;
            lblSueldoPorHora.Visible = false;
            PanelBtnGuardarPersonal.Visible = false;
        }
        //Metodo para la limpieza de los campos
        public void Limpiar()
        {
            txtNombres.Clear();
            txtIdentificacion.Clear();
            txtSueldoPorHora.Clear();
            txtCargos.Clear();
            BuscarCargos();
           
        }
        //Validaciones del boton guardar personal
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
        //Metodo Insertar Personal
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
                ReiniciarPaginado();
                MostrarPersonal();
                PanelRegistros.Visible = false;
            }
        
        }
        private void MostrarPersonal()
        {
            DataTable dt = new DataTable();
            DatosPersonal funcion = new DatosPersonal();
            funcion.MostrarPersonal(ref dt, desde,hasta);
            dataListadoPersonal.DataSource = dt;
            DiseñarDtvPersonal();
        }
        //llamado del diseño del datalistadopersonal desde bases 
        private void DiseñarDtvPersonal()
        {
            Bases.DiseñoDtv(ref dataListadoPersonal);
            Bases.DiseñoDtvEliminar(ref dataListadoPersonal);
            PanelPaginado.Visible = true;
            dataListadoPersonal.Columns[2].Visible = false;
            dataListadoPersonal.Columns[7].Visible = false;
        }
        //Metodo Insertar Cargos
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
        //Metodo Buscar Cargos
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
        private void PanelRegistros_Paint(object sender, PaintEventArgs e)
        {

        }
        //para mostrar los cargos que ya estan registrados 
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
        //Boton de guardar del panel de cargos
        private void btnGuardarP_Click(object sender, EventArgs e)
        {
            InsertarCargos();
        }
        //llamada del metodo decimales desde bases pata validacionde parametros del texbox Cargos del datalistadocargos
        private void txtSueldoporhoraP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtCargosP, e);
        }
        //llamada del metodo decimales desde bases pata validacionde parametros del texbox Sueldoporhora 
        private void txtSueldoPorHora_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoPorHora, e);
        }
        //Metodo para agregar los cargos al datagridcargos
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
        // Metodo para Obtener ls datos
        private void ObtenerDatosCargos()
        {
            IdCargos = Convert.ToInt32(datalistadoCargos.SelectedCells[1].Value);
            txtCargos.Text = datalistadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoPorHora.Text = datalistadoCargos.SelectedCells[3].Value.ToString();
            datalistadoCargos.Visible = false;
            PanelBtnGuardarPersonal.Visible = true;
            lblSueldoPorHora.Visible = true;
        }
        //Metodo para editar los datos
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
        private void btnVolverCargos_Click(object sender, EventArgs e)
        {
            panelCargos.Visible = false;
        }
        private void btnVolverPersonal_Click(object sender, EventArgs e)
        {
            PanelRegistros.Visible = false;
        }
        //boton de guardar los cambios editados de un cargo 
        private void btnGuardarCambiosP_Click(object sender, EventArgs e)
        {
            EditarCargosPersonal();
        }     
        //Metodo para editar los cargos 
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
        //Contenido del formulario personal al abrirlo 
        private void Personal_Load(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal(); 
        }
        //Funcion para iniciar el paginado 
        private void ReiniciarPaginado()
        {
            desde = 1;
            hasta = 10;
            Contar();

            if (Contador > hasta)
            {

                btnPaginaSiguiente.Visible = true;
                btnPaginaAnterior.Visible = false;
                btnUltimaPagina.Visible = true;
                btnPrimeraPagina.Visible = true;
            }
            else
            {

                btnPaginaSiguiente.Visible = false;
                btnPaginaAnterior.Visible = false;
                btnUltimaPagina.Visible = false;
                btnPrimeraPagina.Visible = false;
            }
            Paginar();
        }
        //Botonoes de eliminar y editar del datalistadopersonal
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
        //Metodo para poner Estado ACTIVO al personal registrado o eliminarlo
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
                LocalizarDtvCargos();
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
        //Metodo para restaurar o cambiar el estado a ACTIVO de personal
        private void RestaurarPersonal()
        {
            DialogResult result = MessageBox.Show("¿Este personal se elimino, desar volver a habilitarlo?", "Restauracion de registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                HabilitarPersonal();
            }
        }
        //Metodo para 
        private void HabilitarPersonal()
        {
            LogicaPersonal parametros = new LogicaPersonal();
            DatosPersonal funcion = new DatosPersonal();
            parametros.Id_personal = Idpersonal;
            if (funcion.Restaurar_Personal(parametros)==true)
            {
                MostrarPersonal();
            }
        }
        //Metodo para eliminar personal
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
        //Timer para que se vean los eliminado al actualizar el datagridpersonal
        private void timer1_Tick(object sender, EventArgs e)
        {
            DiseñarDtvPersonal();
            timer1.Enabled = false;
        }
        //Boton editar personal o guardar le edicion de un personal
        private void btnGuardarCambioPersonal_Click(object sender, EventArgs e)
        {
            EditarPersonal();
        }
        //Medoto de guardar la edicion de un personal
        private void EditarPersonal()
        {
            LogicaPersonal parametros = new LogicaPersonal();
            DatosPersonal funcion = new DatosPersonal();
            parametros.Id_personal = Idpersonal;
            parametros.Nombres = txtNombres.Text;
            parametros.Identificacion = txtIdentificacion.Text;
            parametros.Pais = cbxPais.Text;
            parametros.Id_cargos = IdCargos;
            parametros.SueldoPorHoras = Convert.ToDouble(txtSueldoPorHora.Text);
            if (funcion.EditarPersonal(parametros) == true)
            {
                MostrarPersonal();
                PanelRegistros.Visible=false;
            }
        }
        #region PAGINADO
        private void btnPaginaSiguiente_Click(object sender, EventArgs e)
        {
            desde += 10;
            hasta += 10;
            MostrarPersonal();
            Contar();
            if (Contador > hasta)
            {
                btnPaginaSiguiente.Visible = true;
                btnPaginaAnterior.Visible = true;
            }
            else
            {
                btnPaginaSiguiente.Visible = false;
                btnPaginaAnterior.Visible = true;
            }
            Paginar();

        }
        private void Paginar()
        {
            try
            {
                lblPagina.Text = (hasta / Items_por_pagina).ToString();
                lblTotalPaginas.Text = Math.Ceiling(Convert.ToSingle(Contador) / Items_por_pagina).ToString();
                TotalPaginas = Convert.ToInt32(lblTotalPaginas.Text);
            }
            catch (Exception)
            {

            }
        }
        //metodo para el paginado
        private void Contar()
        {
            DatosPersonal funcion = new DatosPersonal();
            funcion.ContarPersonal(ref Contador);
        }
        private void btnPaginaAnterior_Click(object sender, EventArgs e)
        {
            desde -= 10;
            hasta -= 10;
            MostrarPersonal();
            Contar();
            if (Contador > hasta)
            {
                btnPaginaSiguiente.Visible = true;
                btnPaginaAnterior.Visible = true;
            }
            else
            {
                btnPaginaSiguiente.Visible = false;
                btnPaginaAnterior.Visible = true;
            }
            if (desde == 1)
            {
                ReiniciarPaginado();
            }
            Paginar();
        }
        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            hasta = TotalPaginas * Items_por_pagina;
            desde = hasta - 9;
            MostrarPersonal();
            Contar();
             
            if (Contador > hasta)
            {
                btnPaginaSiguiente.Visible = true;
                btnPaginaAnterior.Visible = true;
            }
            else
            {
                btnPaginaSiguiente.Visible = false;
                btnPaginaAnterior.Visible = true;
            }
            Paginar();
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal();
        }
        #endregion

        #region BUSCADOR
        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            BuscarPersonal();
        }
        private void BuscarPersonal()
        {
            DataTable dt = new DataTable();
            DatosPersonal funcion = new DatosPersonal();
            funcion.BuscarPersonal(ref dt, desde,hasta, txtBuscador.Text);
            dataListadoPersonal.DataSource = dt;
            DiseñarDtvPersonal();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal();
        }
        #endregion

        private void PanelPaginado_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}
