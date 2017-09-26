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
    /// Servicio CtaCierresDePeriodos
    /// </summary>
    public class CtaCierresDePeriodosService
    {
        private CtaCierresDePeriodosDal ctaCierresDePeriodosDal;

        /// <summary>
        /// Constructor de CtaCierresDePeriodosService
        /// </summary>
        public CtaCierresDePeriodosService(CtaCierresDePeriodosDal ctaCierresDePeriodosDal)
        {
            this.ctaCierresDePeriodosDal = ctaCierresDePeriodosDal;
        }

        /// <summary>
        /// Obtiene todos los ctaMovimientos. No tracking.
        /// </summary>
        public IList<CtaCierresDePeriodos> GetAll()
        {
            return ctaCierresDePeriodosDal.GetAll();
        }

        /// <summary>
        /// Obtiene ctaMovimientos por movimientoNro. No tracking.
        /// </summary>
        public bool MovimientoEnPeriodoContableAbierto(CtaMovimientos ctaMovimiento)
        {
            return ctaCierresDePeriodosDal.MovimientoEnPeriodoContableAbierto(ctaMovimiento);
        }
    }
}
