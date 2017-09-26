using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Domain;
using DAL.Security;
using Domain.Security;

namespace Service.Security
{
    /// <summary>
    /// Service para la clase Examen
    /// </summary>
    public class UniExamenesService
    {
        UniExamenesDal uniExamenDal;

        public UniExamenesService(UniExamenesDal uniExamenDal)
        {
            this.uniExamenDal = uniExamenDal;
        }

        public void sp_sav_examen_a_verificar(Nullable<int> legajo, string codmat, Nullable<int> acta, Nullable<System.DateTime> fecha, int folio, string libro, int notadef, bool ausente)
        {
            uniExamenDal.sp_sav_examen_a_verificar(legajo, codmat, acta, fecha, folio, libro, notadef, ausente);
        }
    }
}