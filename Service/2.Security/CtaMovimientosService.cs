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
    /// Servicio UniProfesoresHs
    /// </summary>
    public class CtaMovimientosService
    {
        private CtaMovimientosDal ctaMovimientosDal;

        /// <summary>
        /// Constructor de CtaMovimientosService
        /// </summary>
        public CtaMovimientosService(CtaMovimientosDal ctaMovimientosDal)
        {
            this.ctaMovimientosDal = ctaMovimientosDal;
        }

        /// <summary>
        /// Obtiene todos los ctaMovimientos. No tracking.
        /// </summary>
        public IList<CtaMovimientos> GetAll()
        {
            return ctaMovimientosDal.GetAll();
        }

        /// <summary>
        /// Obtiene ctaMovimientos por movimientoNro. No tracking.
        /// </summary>
        public CtaMovimientos GetByMovimientoNro(int movimientoNro)
        {
            return ctaMovimientosDal.GetByMovimientoNro(movimientoNro);
        }

        /// <summary>
        /// Anula a un movimiento. Si devuelve true, se realizó correctamente.
        /// </summary>
        public bool sp_cta_Anular_Movimiento(int nroMovimiento)
        {
            return ctaMovimientosDal.sp_cta_Anular_Movimiento(nroMovimiento);
        }

        /// <summary>
        /// Corregir tarjeta de un movimiento (1 = Maestro, 2 = Visa Débito, 3 = Mastercard, 4 = Visa Crédito)
        /// </summary>
        /// <returns></returns>
        public void CorregirTarjeta(int nroMovimiento, int tipoTarjeta)
        {
            ctaMovimientosDal.CorregirTarjeta(nroMovimiento, tipoTarjeta);
        }

    }
}
