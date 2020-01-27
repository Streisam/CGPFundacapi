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
     public class ClientesDAL
    {
        public static int Agregar(Cliente pCliente)
        {

            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(string.Format("Insert into clientes (NomApe, Cedula, Correo, Num_Telefono, Diplomado) values ('{0}','{1}','{2}','{3}','{4}')",
                 pCliente.NomApe, pCliente.Cedula, pCliente.Correo, pCliente.Num_Telf, pCliente.Diplomado), BdComun.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static int AgregarCuota(Cuota pCuota)
        {

            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(string.Format("Insert into cuotas (Num_cuota, Monto, Fecha_Pago, Num_Ref, Banco, Tipo_Pago, ClienteId) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                pCuota.Num_Cuota, pCuota.Monto, pCuota.Fecha_Pago, pCuota.Num_Referencia, pCuota.Banco, pCuota.Tipo_Pago, pCuota.Cliente_id), BdComun.ObtenerConexion());
            retorno = comando.ExecuteNonQuery();
            return retorno;
        }

        public static List<Cliente> Buscar(string pCedula, string pNomApe)
        {
            List<Cliente> _lista = new List<Cliente>();

            MySqlCommand _comando = new MySqlCommand(String.Format(
           "SELECT IdCliente, NomApe, Cedula, Correo, Num_Telefono, Diplomado FROM clientes  where Cedula ='{0}' or NomApe='{1}'", pCedula, pNomApe), BdComun.ObtenerConexion());
            MySqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                Cliente pCliente = new Cliente();
                pCliente.Id_Cliente = _reader.GetInt32(0);
                pCliente.NomApe = _reader.GetString(1);
                pCliente.Cedula = _reader.GetString(2);
                pCliente.Correo = _reader.GetString(3);
                pCliente.Num_Telf = _reader.GetString(4);
                pCliente.Diplomado = _reader.GetString(5);

                _lista.Add(pCliente);
            }

            return _lista;
        }

        public static Cliente ObtenerCliente(int pId)
        {
            Cliente pCliente = new Cliente();
            MySqlConnection conexion = BdComun.ObtenerConexion();

            MySqlCommand _comando = new MySqlCommand(String.Format("SELECT IdCliente, NomApe, Cedula, Correo, Num_Telefono, Diplomado FROM clientes where IdCliente={0}", pId), conexion);
            MySqlDataReader _reader = _comando.ExecuteReader();
            while (_reader.Read())
            {
                pCliente.Id_Cliente = _reader.GetInt32(0);
                pCliente.NomApe = _reader.GetString(1);
                pCliente.Cedula = _reader.GetString(2);
                pCliente.Correo = _reader.GetString(3);
                pCliente.Num_Telf = _reader.GetString(4);
                pCliente.Diplomado = _reader.GetString(5);

            }

            conexion.Close();
            return pCliente;

        }
        
       

        public static int Actualizar(Cliente pCliente)
        {
            int retorno = 0;
            MySqlConnection conexion = BdComun.ObtenerConexion();

            MySqlCommand comando = new MySqlCommand(string.Format("Update clientes set NomApe='{0}', Cedula='{1}', Correo='{2}', Num_Telefono='{3}', Diplomado='{4}' where IdCliente={5}",
                pCliente.NomApe, pCliente.Cedula, pCliente.Correo, pCliente.Num_Telf,pCliente.Diplomado, pCliente.Id_Cliente), conexion);

            retorno = comando.ExecuteNonQuery();
            conexion.Close();

            return retorno;


        }

        public static int ActualizarC(Cuota pCuota)
        {
            int retorno = 0;
            MySqlConnection conexion = BdComun.ObtenerConexion();

            MySqlCommand comando = new MySqlCommand(string.Format("Update cuotas set Num_Cuota='{0}', Monto='{1}', Fecha_Pago='{2}', Num_Ref='{3}', Banco='{4}', Tipo_Pago='{5}', ClienteId='{6}' where Num_ReciboFC={7}",
                pCuota.Num_Cuota, pCuota.Monto, pCuota.Fecha_Pago, pCuota.Num_Referencia, pCuota.Banco, pCuota.Tipo_Pago, pCuota.Cliente_id, pCuota.Num_ReciboFcC), conexion);

            retorno = comando.ExecuteNonQuery();
            conexion.Close();

            return retorno;


        }

    }
}
