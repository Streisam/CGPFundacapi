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
using System.Globalization;

namespace CGPFundacapi
{
    public partial class FormAlumnoPago : Form
    {

        FormAlumnos fa;
        int indexRow;

        public FormAlumnoPago(FormAlumnos fclnt)
        {
            InitializeComponent();
            this.fa = fclnt;
            StartPosition = FormStartPosition.CenterScreen;
        }


        private void ClientesPago_Load(object sender, EventArgs e)
        {
            var culture = CultureInfo.CreateSpecificCulture("es-VE");
            culture.NumberFormat.CurrencyDecimalSeparator = ",";
            culture.NumberFormat.CurrencyGroupSeparator = ".";
            culture.NumberFormat.CurrencySymbol = "BS";
            txtAlumno.Text = fa.txtNomApe.Text;
            txtDip.Text = fa.txtDiplomado.Text;
            txtId.Text = fa.txtId.Text;

            MySqlConnection conexion = BdComun.ObtenerConexion();
            DataTable dt = new DataTable();
            MySqlDataAdapter cDatos = new MySqlDataAdapter("SELECT Num_ReciboFC AS 'Numero de Recibo Fundacapi', Num_Cuota AS 'Nº Cuota' , Monto, Fecha_Pago AS 'Fecha de Pago', Num_Ref AS 'Referencia de Banco', Banco, Tipo_Pago AS 'Tipo de Pago' FROM cuotas WHERE ClienteId =" + int.Parse(txtId.Text), conexion);
            cDatos.Fill(dt);
            dgvCuotas.DataSource = dt;
            conexion.Close();
            SumarCuotas();
            dgvCuotas.DefaultCellStyle.FormatProvider = culture;
            dgvCuotas.Columns[2].DefaultCellStyle.Format = "c2";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int numRows = dgvCuotas.Rows.Count;
            if (numRows >= 5)
            { MessageBox.Show("No pueden ser mas de 5 cuotas! ", "Limite de cuotas Excedido!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                if ( string.IsNullOrWhiteSpace(cbCuota.Text) || string.IsNullOrWhiteSpace(txtMonto.Text)
                    || string.IsNullOrWhiteSpace(dtpFechaPago.Text) || string.IsNullOrWhiteSpace(txtNumRef.Text) || string.IsNullOrWhiteSpace(txtBanco.Text)
                    || string.IsNullOrWhiteSpace(txtTipoP.Text) || string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Hay Uno o mas Campos Vacios!", "Campos Vacios!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Cuota pCuota = new Cuota();
                    pCuota.Num_Cuota = Int32.Parse(cbCuota.Text);
                    pCuota.Monto = Decimal.Parse(txtMonto.Text);
                    pCuota.Fecha_Pago = dtpFechaPago.Value.Year + "/" + dtpFechaPago.Value.Month + "/" + dtpFechaPago.Value.Day;
                    pCuota.Num_Referencia = txtNumRef.Text.Trim();
                    pCuota.Banco = txtBanco.Text.Trim();
                    pCuota.Tipo_Pago = txtTipoP.Text.Trim();
                    pCuota.Cliente_id = Int32.Parse(txtId.Text);

                    int resultado = ClientesDAL.AgregarCuota(pCuota);
                    if (resultado > 0)
                    {
                        MessageBox.Show("Cuota Guardada Con Exito!!", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ObtenerCuota();
                        SumarCuotas();
                        Limpiar();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo guardar la cuota", "Fallido!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }


                }
            }
        }

        public void ObtenerCuota()
        {
            MySqlConnection conexion = BdComun.ObtenerConexion();
            DataTable dt = new DataTable();
            MySqlDataAdapter cDatos = new MySqlDataAdapter("SELECT Num_ReciboFC AS 'Numero de Recibo Fundacapi', Num_Cuota AS 'Nº Cuota' , Monto, Fecha_Pago AS 'Fecha de Pago', Num_Ref AS 'Referencia de Banco', Banco, Tipo_Pago AS 'Tipo de Pago' FROM cuotas WHERE ClienteId =" + int.Parse(txtId.Text), conexion);
            cDatos.Fill(dt);
            dgvCuotas.DataSource = dt;

            conexion.Close();
        }

        public void SumarCuotas()
        {
            var culture = CultureInfo.CreateSpecificCulture("es-VE");
            culture.NumberFormat.CurrencyDecimalSeparator = ",";
            culture.NumberFormat.CurrencyGroupSeparator = ".";
            culture.NumberFormat.CurrencySymbol = "BS";
            dgvCuotas.AllowUserToAddRows = false;
            int sum = 0;
            for (int i = 0; i <= dgvCuotas.Rows.Count - 1; i++)
            {
                sum = sum + int.Parse(dgvCuotas.Rows[i].Cells[2].Value.ToString());
            }
            txtTotalP.Text = sum.ToString("C2", culture) ;

            
        }

        void Limpiar()
        {
            txtMonto.Clear();
            dtpFechaPago.ResetText();
            cbCuota.ResetText();
            txtNumRef.Clear();
            txtBanco.Clear();
            txtTipoP.Clear();

        }

        private void dgvCuotas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                indexRow = e.RowIndex;
                DataGridViewRow row = dgvCuotas.Rows[indexRow];

                cbCuota.Text = row.Cells[1].Value.ToString();
                txtMonto.Text = row.Cells[2].Value.ToString();
                dtpFechaPago.Text = row.Cells[3].Value.ToString();
                txtNumRef.Text = row.Cells[4].Value.ToString();
                txtBanco.Text = row.Cells[5].Value.ToString();
                txtTipoP.Text = row.Cells[6].Value.ToString();

            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvCuotas.SelectedRows.Count == 1)
            { 
                if   ( string.IsNullOrWhiteSpace(cbCuota.Text) || string.IsNullOrWhiteSpace(txtMonto.Text)
                    || string.IsNullOrWhiteSpace(dtpFechaPago.Text) || string.IsNullOrWhiteSpace(txtNumRef.Text) || string.IsNullOrWhiteSpace(txtBanco.Text)
                    || string.IsNullOrWhiteSpace(txtTipoP.Text) || string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Hay Uno o mas Campos Vacios!", "Campos Vacios!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    Cuota pCuota = new Cuota();

                    pCuota.Num_Cuota = Int32.Parse(cbCuota.Text);
                    pCuota.Monto = Decimal.Parse(txtMonto.Text);
                    pCuota.Fecha_Pago = dtpFechaPago.Value.Year + "/" + dtpFechaPago.Value.Month + "/" + dtpFechaPago.Value.Day;
                    pCuota.Num_Referencia = txtNumRef.Text.Trim();
                    pCuota.Banco = txtBanco.Text.Trim();
                    pCuota.Tipo_Pago = txtTipoP.Text.Trim();
                    pCuota.Cliente_id = Int32.Parse(txtId.Text);
                    pCuota.Num_ReciboFcC = Convert.ToInt32(dgvCuotas.CurrentRow.Cells[0].Value);

                    if (ClientesDAL.ActualizarC(pCuota) > 0)
                    {
                        MessageBox.Show("Los datos del cliente se actualizaron", "Datos Actualizados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvCuotas.Columns[2].DefaultCellStyle.Format = "c";
                        ObtenerCuota();
                        SumarCuotas();
                        Limpiar();
                    }

                    else
                    {
                        MessageBox.Show("No se pudo actualizar", "Error al Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
            else
            {
                MessageBox.Show("Debe de seleccionar una fila");
            }
        }

    }
}
