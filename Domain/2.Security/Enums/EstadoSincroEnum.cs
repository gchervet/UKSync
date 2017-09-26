using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum EstadoSincroEnum
    {
        ParaEnviar = 1,
        EnProceso = 2,
        Enviado = 3,
        Ignorado = 4
    }
}
