using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGPFundacapi
{
    public class Cliente
    {
        public int Id_Cliente { get; set; }
        public string NomApe { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string Num_Telf { get; set; }
        public string Diplomado { get; set; }

        public Cliente() { }

        public Cliente(int pId, string pNomApe, string pCedula, string pCorreo, string pNum_Telf, string Diplomado)

        {
            this.Id_Cliente = pId;
            this.NomApe = pNomApe;
            this.Cedula = pCedula;
            this.Correo = pCorreo;
            this.Num_Telf = pNum_Telf;
            this.Diplomado = Diplomado;
        }

    }
}
