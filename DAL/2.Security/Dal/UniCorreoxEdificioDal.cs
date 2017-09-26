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
    public class UniCorreoxEdificioDal : DalBase<SecurityEntities>
    {
        public UniCorreoxEdificioDal(SecurityEntities context)
            : base(context)
        {
        }

        public IList<UniCorreoxEdificio> ObtenerEdificioCorreo()
        {
            return context.UniCorreoxEdificio.ToList();
        }
    }
}