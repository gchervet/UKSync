using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Data.Entity;
using Domain;
using Domain.Security;
using NLog;

namespace DAL.Security
{
    /// <summary>
    /// DAL para la clase UniAranceles
    /// </summary>
    public class UniArancelesDal : DalBase<SecurityEntities>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public UniArancelesDal(SecurityEntities context)
            : base(context)
        {
        }

        /// <summary>
        /// Obtiene todos los uniAranceles. No tracking.
        /// </summary>
        public IList<UniAranceles> GetAll()
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<UniAranceles> aranceles = context.UniAranceles.ToList();

            return aranceles;
        }

        /// <summary>
        /// Baja de arancel facturado
        /// </summary>
        public void BajaArancelFacturado(int legProv, int aniolectivo)
        {
            var query = from m in context.uniMatriculaciones
                        join a in context.UniAranceles on m.Clave equals a.clave
                        where m.legajoProvisorio==legProv && m.ciclo==DateTime.Today.Year
                        select a;
            if (query.Any())
            {
                UniAranceles arancel = query.FirstOrDefault();
                UniAranceles arancelDb = context.UniAranceles.Where(a => a.clave == arancel.clave && a.aniolectivo == aniolectivo).FirstOrDefault();
                if (arancelDb != null)
                {
                    //arancelDb.aniolectivo = arancelDb.aniolectivo * (-1);
                    string queryString = "update UniAranceles set aniolectivo=aniolectivo*-1 where clave=" + arancel.clave + " and aniolectivo=" + aniolectivo;

                    context.Database.ExecuteSqlCommand(queryString);

                    CtaAplicacionDeConsumos aplicacionDeConsumosDb = context.CtaAplicacionDeConsumos.Where(adc => adc.IdentificadorConsumo1 == arancel.clave.ToString() && adc.ConsumoTipoId == "ArancelAn").FirstOrDefault();

                    aplicacionDeConsumosDb.IdentificadorConsumo1 = "";
                    aplicacionDeConsumosDb.IdentificadorConsumo2 = "";

                    context.SaveChanges();

                    CambioMatricula(arancel.clave);

                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException("uniArancel");
                }
            }
            else
            {
                throw new ArgumentNullException("uniArancel");
            }
        }

        /// <summary>
        /// Baja de arancel no facturado
        /// </summary>
        public void BajaArancelNoFacturado(int legProv, int aniolectivo)
        {
            var query = from m in context.uniMatriculaciones
                        join a in context.UniAranceles on m.Clave equals a.clave
                        where m.legajoProvisorio == legProv && m.ciclo == DateTime.Today.Year
                        select a;

            if (query.Any())
            {
                UniAranceles arancel = query.FirstOrDefault();

                long claveArancel = arancel.clave;

                UniAranceles arancelDb = new UniAranceles();

                if (aniolectivo > 0)
                    arancelDb = context.UniAranceles.Where(a => a.clave == claveArancel && a.aniolectivo == aniolectivo).FirstOrDefault();
                else
                    arancelDb = context.UniAranceles.Where(a => a.clave == claveArancel).AsEnumerable().LastOrDefault();

                if (arancelDb != null)
                {
                    context.UniAranceles.Remove(arancelDb);
                    CambioMatricula(claveArancel);
                }
                else
                {
                    throw new ArgumentNullException("uniArancel");
                }
            }
            else
            {
                throw new ArgumentNullException("uniArancel");
            }
        }

        private void CambioMatricula(long claveArancel)
        {
            uniMatriculaciones matriculaDb = context.uniMatriculaciones.Where(m => m.Clave == claveArancel).FirstOrDefault();
            matriculaDb.anioMaximo = matriculaDb.anioMaximo - 1;

            context.SaveChanges();
        }
    }
}