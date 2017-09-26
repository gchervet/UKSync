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
    /// DAL para la clase SNControladorEdificio
    /// </summary>
    public class SNControladorEdificioDal : DalBase<SecurityEntities>
    {
        public SNControladorEdificioDal(SecurityEntities context, ContextFactory contextFactory)
            : base(context, contextFactory)
        {
        }

        public IList<SN_Controlador_Edificio> GetAll()
        {
            return context.SN_Controlador_Edificio.ToList();
        }

        public int GetByControlador(int controladorId)
        {
            SN_Controlador_Edificio sNControladorEdificio = context.SN_Controlador_Edificio.Where(ce => ce.IdControlador == controladorId).FirstOrDefault();
            if (sNControladorEdificio != null)
                return sNControladorEdificio.CodIns;
            else
                return 0;
        }
    }
}