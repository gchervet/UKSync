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
    public class CtaMovimientosDal : DalBase<SecurityEntities>
    {
        public CtaMovimientosDal(SecurityEntities context, ContextFactory contextFactory)
            : base(context, contextFactory)
        {
        }

        /// <summary>
        /// Obtiene todos los ctaMovimientos. No tracking.
        /// </summary>
        public IList<CtaMovimientos> GetAll()
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<CtaMovimientos> ctaMovimientos = context.CtaMovimientos.ToList();

            return ctaMovimientos;
        }

        /// <summary>
        /// Obtiene ctaMovimientos por movimientoNro. No tracking.
        /// </summary>
        public CtaMovimientos GetByMovimientoNro(int movimientoNro)
        {
            context.Configuration.LazyLoadingEnabled = false;

            CtaMovimientos ctaMovimientos = context.CtaMovimientos.Where(m => m.MovimientoNro == movimientoNro).FirstOrDefault();

            return ctaMovimientos;
        }

        /// <summary>
        /// Anula a un movimiento. Si devuelve true, se realizó correctamente.
        /// </summary>
        public bool sp_cta_Anular_Movimiento(int nroMovimiento)
        {
            return context.sp_cta_Anular_Movimiento_Return_Afectadas(nroMovimiento) > 0;
        }

        /// <summary>
        /// Anula todos los movimientos de aplicación y el movimiento.
        /// </summary>
        public void AnularMovimientosDeAplicacionAndMovimientoByNroMovimiento(int nroMovimiento)
        {
            CtaCierresDePeriodos ctaCierresDePeriodo = context.CtaCierresDePeriodos.OrderBy(vdp => vdp.FechaHasta).FirstOrDefault();
            DateTime fechaHastaMaxima = ctaCierresDePeriodo.FechaHasta;

            var query = (from ap in context.CtaMovimientosAplicacion
                         join m in context.CtaMovimientos.Where(m => m.MovimientoTipoId == "PC-APL") on ap.MovimientoNro equals m.MovimientoNro
                         join c1 in context.CtaMovimientosCuotas on ap.MovimientoCuotaClaveAplicado equals c1.Clave
                         join c2 in context.CtaMovimientosCuotas on ap.MovimientoCuotaClaveAplicando equals c2.Clave
                         where (c1.MovimientoNro == nroMovimiento || c2.MovimientoNro == nroMovimiento) && ap.MovimientoNro != nroMovimiento
                         && m.FechaComprobante >= fechaHastaMaxima
                         select m).Distinct();

            foreach (CtaMovimientos movimiento in query)
            {
                context.sp_cta_Anular_MovimientoAPL((int)movimiento.MovimientoNro,"");//REVISAR
            }

            int entidadId = context.CtaMovimientos.Where(m => m.MovimientoNro == nroMovimiento).FirstOrDefault().EntidadId.Value;

            var query2 = (from m in context.CtaMovimientos
                          where m.EntidadId == entidadId && m.MovimientoTipoId == "PC-APL"
                          select m).OrderByDescending(m => m.MovimientoNro);

            foreach (CtaMovimientos movimiento in query2)
            {
                context.sp_cta_Anular_MovimientoAPL((int)movimiento.MovimientoNro,"");//REVISAR
            }

            context.sp_cta_Anular_Movimiento_Return_Afectadas(nroMovimiento);
        }

        /// <summary>
        /// Corregir tarjeta de un movimiento (1 = Maestro, 2 = Visa Débito, 3 = Mastercard, 4 = Visa Crédito)
        /// </summary>
        /// <returns></returns>
        public void CorregirTarjeta(int nroMovimiento, int tipoTarjeta)
        {
            CtaMovimientosFinancieras movFinancieras = context.CtaMovimientosFinancieras.Where(mf => mf.CuentaContable == "1110333" && mf.Orden == 1 && mf.MovimientoNro == nroMovimiento).FirstOrDefault();

            if (movFinancieras != null)
            {
                try
                {
                    movFinancieras.FinancieraPropiaId = ((TarjetaEnum)tipoTarjeta).ToDescriptionString();
                }
                catch (Exception)
                {
                    throw new ArgumentNullException("Tipo de tarjeta incorrecto");
                }

                context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("CtaMovimientosFinancieras");
            }
        }
    }
}