using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum TipoNovedadesEnum
    {
        DEFAULT = 0,
        HS_FERIADOS = 1,
        HS_LIC_ENFERMEDAD = 2,
        HS_LIC_ACCIDENTE = 3,
        HS_EXAMEN = 4,
        FALLECIMIENTO = 5,
        ASUETO_ADMINISTRATIVO = 6,
        HS_LIC_GREMIAL = 7,
        HS_LIC_NACIMIENTO = 8,
        HS_MATERNIDAD = 9,
        HS_CATEDRA = 10,
        HS_JUSTIFICADAS = 11,
        HS_EVALUACION = 12
    }
}
