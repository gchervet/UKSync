using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.Domain
{
    public class CreaOTDto
    {
        public CreaOTDto() { }

        public string CodigoArticulo;
        public Nullable<decimal> Cantidad;
        public string CodigoDeposito;
        public string Usuario;
        public Nullable<int> CodigoUsuario;
        public string Terminal;
    }
}
