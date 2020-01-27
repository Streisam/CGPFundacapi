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
    public partial class FormBuscarAlumno : Form
    {
        public FormBuscarAlumno()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
        }

        DataTable dt;
        public Cliente ClienteSeleccionado{ get; set; }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            {
                if (dgvBuscar.SelectedRows.Count == 1)
                {
                    int id = Convert.ToInt32(dgvBuscar.CurrentRow.Cells[0].Value);
                    ClienteSeleccionado= ClientesDAL.ObtenerCliente(id);

                    this.Close();
                }
                else
                    MessageBox.Show("Debe de seleccionar una fila");
            }
        }

        private void txtNomApe_TextChanged(object sender, EventArgs e)
        {
            DataView DV = new DataView(dt);
            DV.RowFilter = string.Format("NomApe LIKE '%{0}%'", txtNomApe.Text);
            dgvBuscar.DataSource = DV;
        }

        private void txtCed_TextChanged(object sender, EventArgs e)
        {
            DataView DV = new DataView(dt);
            DV.RowFilter = string.Format("CONVERT(Cedula, System.String) LIKE '%{0}%'", txtCed.Text);
            dgvBuscar.DataSource = DV;
        }

        private void FormBuscarAlumno_Load(object sender, EventArgs e)
        {
            MySqlConnection conexion = BdComun.ObtenerConexion();
            MySqlDataAdapter cDatos2 = new MySqlDataAdapter("SELECT COUNT(*) as repetitions, Clienteid FROM cuotas group by ClienteId having repetitions >1", conexion);
            DataSet ds = new DataSet();
            cDatos2.FillSchema(ds, SchemaType.Source, "cuotas");
            cDatos2.Fill(ds, "cuotas");
            DataTable tblCuotas;
            tblCuotas = ds.Tables["cuotas"];


            MySqlDataAdapter cDatos = new MySqlDataAdapter("SELECT * FROM clientes", conexion);
            dt = new DataTable();
            cDatos.Fill(dt);
            dgvBuscar.DataSource = dt;


            foreach (DataRow drCurrent in tblCuotas.Rows)
            {
                foreach (DataGridViewRow dgvrow in dgvBuscar.Rows)
                {
                    int rrepvalue = Convert.ToInt32(drCurrent["repetitions"].ToString());
                    int ridvalue = Convert.ToInt32(drCurrent["ClienteId"].ToString());
                    if (rrepvalue == 3 && ridvalue == Convert.ToInt32(dgvrow.Cells[0].Value))
                    {
                        dgvrow.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    if (rrepvalue == 4 && ridvalue == Convert.ToInt32(dgvrow.Cells[0].Value))
                    {
                        dgvrow.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    if (rrepvalue == 5 && ridvalue == Convert.ToInt32(dgvrow.Cells[0].Value))
                    {
                        dgvrow.DefaultCellStyle.BackColor = Color.LimeGreen;
                    }
                }
            }

            dgvBuscar.Columns[0].HeaderText = "ID";
            dgvBuscar.Columns[1].HeaderText = "Nombres y Apellidos";
            dgvBuscar.Columns[1].Width = 160;
            dgvBuscar.Columns[3].Width = 140;
            dgvBuscar.Columns[4].HeaderText = "Numero Telefono";
            dgvBuscar.Columns[4].Width = 130;
            dgvBuscar.Columns[5].Width = 150;

            conexion.Close();

        }

        private void dgvBuscar_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                MySqlConnection conexion = BdComun.ObtenerConexion();
                MySqlDataAdapter cDatos2 = new MySqlDataAdapter("SELECT COUNT(*) as repetitions, Clienteid FROM cuotas group by ClienteId having repetitions >1", conexion);
                DataSet ds = new DataSet();
                cDatos2.FillSchema(ds, SchemaType.Source, "cuotas");
                cDatos2.Fill(ds, "cuotas");
                DataTable tblCuotas;
                tblCuotas = ds.Tables["cuotas"];

                foreach (DataRow drCurrent in tblCuotas.Rows)
                {
                    foreach (DataGridViewRow dgvrow in dgvBuscar.Rows)
                    {
                        int rrepvalue = Convert.ToInt32(drCurrent["repetitions"].ToString());
                        int ridvalue = Convert.ToInt32(drCurrent["ClienteId"].ToString());
                        if (rrepvalue == 3 && ridvalue == Convert.ToInt32(dgvrow.Cells[0].Value))
                        {
                            dgvrow.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        if (rrepvalue == 4 && ridvalue == Convert.ToInt32(dgvrow.Cells[0].Value))
                        {
                            dgvrow.DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        if (rrepvalue == 5 && ridvalue == Convert.ToInt32(dgvrow.Cells[0].Value))
                        {
                            dgvrow.DefaultCellStyle.BackColor = Color.LimeGreen;
                        }
                    }
                }

                conexion.Close();

            }

            catch { }

        }

    }
}
