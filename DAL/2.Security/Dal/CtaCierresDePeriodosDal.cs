using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;
using Domain;
using Domain.Security;

namespace DAL.Security
{
    /// <summary>
    /// DAL para la clase CtaMovimientos
    /// </summary>
    public class CtaCierresDePeriodosDal : DalBase<SecurityEntities>
    {
        public CtaCierresDePeriodosDal(SecurityEntities context, ContextFactory contextFactory)
            : base(context, contextFactory)
        {
        }

        /// <summary>
        /// Obtiene todos los ctaCierresDePeriodos. No tracking.
        /// </summary>
        public IList<CtaCierresDePeriodos> GetAll()
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<CtaCierresDePeriodos> ctaCierresDePeriodos = context.CtaCierresDePeriodos.ToList();

            return ctaCierresDePeriodos;
        }

        /// <summary>
        /// Verifica si el movimiento pertenece a un periodo contable abierto.
        /// </summary>
        public bool MovimientoEnPeriodoContableAbierto(CtaMovimientos ctaMovimiento)
        {
            context.Configuration.LazyLoadingEnabled = false;

            CtaCierresDePeriodos ctaCierresDePeriodo = context.CtaCierresDePeriodos.OrderBy(vdp => vdp.FechaHasta).FirstOrDefault();

            if (ctaMovimiento.FechaComprobante > ctaCierresDePeriodo.FechaHasta)
                return true;
            return false;
        }
    }
}