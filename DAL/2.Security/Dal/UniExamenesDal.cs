using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;
using Domain;
using Domain.Security;
using Domain.Negocio;

namespace DAL.Security
{
    /// <summary>
    /// DAL para la clase Examen
    /// </summary>
    public class UniExamenesDal : DalBase<SecurityEntities>
    {
        public UniExamenesDal(SecurityEntities context)
            : base(context)
        {
        }

        public void sp_sav_examen_a_verificar(Nullable<int> legajo, string codmat, Nullable<int> acta, Nullable<System.DateTime> fecha, int folio, string libro, int notadef, bool ausente)
        {
            context.sp_sav_examen_a_verificar(legajo, codmat, acta, fecha, folio, libro, notadef, ausente? 1 : 0, null, null);
        }
    }
}