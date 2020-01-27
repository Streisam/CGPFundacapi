using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGPFundacapi
{
    public class Cuota
    {
        public int Num_Cuota { get; set; }
        public int Num_ReciboFcC { get; set; }
        public decimal Monto { get; set; }
        public string Fecha_Pago { get; set; }
        public string Num_Referencia { get; set; }
        public string Banco { get; set; }
        public string Tipo_Pago { get; set; }
        public int Cliente_id { get; set; }

        public Cuota() { }

        public Cuota(int pNum_C, int pNum_RecFcC, decimal pMonto, string pFecha_Pago, string pNum_Ref, string pBanco, string Tipo_P, int pCli_Id)

        {
            this.Num_Cuota = pNum_C;
            this.Num_ReciboFcC = pNum_RecFcC;
            this.Monto = pMonto;
            this.Fecha_Pago = pFecha_Pago;
            this.Num_Referencia = pNum_Ref;
            this.Banco = pBanco;
            this.Tipo_Pago = Tipo_P;
            this.Cliente_id = pCli_Id;
        }

        public static implicit operator Cuota(int v)
        {
            throw new NotImplementedException();
        }
    }
}
