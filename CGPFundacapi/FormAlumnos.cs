using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace CGPFundacapi
{
    public partial class FormAlumnos : Form
    {
        public FormAlumnos()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public Cliente clienteActual { get; set; }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Habilitar();
            btnActualizar.Enabled = false;
            btnCuotas.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomApe.Text) || string.IsNullOrWhiteSpace(txtCedula.Text)
                 || string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtNumTelf.Text) || string.IsNullOrWhiteSpace(txtDiplomado.Text))
                { 

                    MessageBox.Show("Hay Uno o mas Campos Vacios!", "Campos Vacios!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            else
                {
                    Cliente pCliente = new Cliente();
                    pCliente.NomApe = txtNomApe.Text.Trim();
                    pCliente.Cedula = txtCedula.Text.Trim();
                    pCliente.Correo = txtCorreo.Text.Trim();
                    pCliente.Num_Telf = txtNumTelf.Text.Trim();
                    pCliente.Diplomado = txtDiplomado.Text.Trim();

                    int resultado = ClientesDAL.Agregar(pCliente);
                    if (resultado > 0)
                        {
                            MessageBox.Show("Cliente Guardado Con Exito!!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Limpiar();
                            Deshabilitar();
                        }

                    else
                        {
                            MessageBox.Show("No se pudo guardar el cliente", "Fallo!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                 }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FormBuscarAlumno buscar = new FormBuscarAlumno();
            buscar.ShowDialog();
            
            if (buscar.ClienteSeleccionado != null) {

                clienteActual = buscar.ClienteSeleccionado;
                txtNomApe.Text = buscar.ClienteSeleccionado.NomApe;
                txtCedula.Text = buscar.ClienteSeleccionado.Cedula;
                txtCorreo.Text = buscar.ClienteSeleccionado.Correo;
                txtNumTelf.Text = buscar.ClienteSeleccionado.Num_Telf;
                txtDiplomado.Text = buscar.ClienteSeleccionado.Diplomado;
                txtId.Text = buscar.ClienteSeleccionado.Id_Cliente.ToString();

                btnActualizar.Enabled = true;
                Habilitar();
                btnGuardar.Enabled = false;
                btnCuotas.Enabled = true;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtNomApe.Text) || string.IsNullOrWhiteSpace(txtCedula.Text)
                  || string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtNumTelf.Text) || string.IsNullOrWhiteSpace(txtDiplomado.Text))
            {
                MessageBox.Show("Hay Uno o mas Campos Vacios!", "Campos Vacios!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else
            {
                Cliente pCliente = new Cliente();

                pCliente.NomApe = txtNomApe.Text.Trim();
                pCliente.Cedula = txtCedula.Text.Trim();
                pCliente.Correo = txtCorreo.Text.Trim();
                pCliente.Num_Telf = txtNumTelf.Text.Trim();
                pCliente.Diplomado = txtDiplomado.Text.Trim();
                pCliente.Id_Cliente = clienteActual.Id_Cliente;

                if (ClientesDAL.Actualizar(pCliente) > 0)
                {
                    MessageBox.Show("Los datos del cliente se actualizaron", "Datos Actualizados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Limpiar();
                    Deshabilitar();
                }
            
                else
                {
                    MessageBox.Show("No se pudo actualizar", "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }

        }

        void Limpiar()
        {
            txtNomApe.Clear();
            txtCedula.Clear();
            txtCorreo.Clear();
            txtNumTelf.Clear();
            txtDiplomado.Clear();
            txtId.Clear();

        }

        void Habilitar()
        {
            txtNomApe.Enabled = true;
            txtCedula.Enabled = true;
            txtCorreo.Enabled = true;
            txtNumTelf.Enabled = true;
            txtDiplomado.Enabled = true;
            btnGuardar.Enabled = true;
            btnCancelar.Enabled = true;
        }

        void Deshabilitar()
        {
            txtNomApe.Enabled = false;
            txtCedula.Enabled = false;
            txtCorreo.Enabled = false;
            txtNumTelf.Enabled = false;
            txtDiplomado.Enabled = false;
            btnGuardar.Enabled = false;
            btnActualizar.Enabled = false;
            btnCancelar.Enabled = false;
            btnCuotas.Enabled = false;
            btnNuevo.Enabled = true;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Deshabilitar();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Deshabilitar();
        }

        private void btnCuotas_Click(object sender, EventArgs e)
        {
            FormAlumnoPago cuotas = new FormAlumnoPago(this);
            cuotas.ShowDialog();
        }

 
    }
}
