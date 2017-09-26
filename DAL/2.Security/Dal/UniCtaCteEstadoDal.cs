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
    /// DAL para la clase UniCtaCteEstado
    /// </summary>
    public class UniCtaCteEstadoDal : DalBase<SecurityEntities>
    {
        public UniCtaCteEstadoDal(SecurityEntities context)
            : base(context)
        {
        }

        /// <summary>
        /// Obtiene todos los UniCtaCteEstado. No tracking.
        /// </summary>
        public IList<UniCtaCteEstado> GetAll()
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<UniCtaCteEstado> ctaCteEstados = context.UniCtaCteEstado.ToList();

            return ctaCteEstados;
        }

        /// <summary>
        /// Obtiene UniCtaCteEstado por medio de legajo provisional.
        /// </summary>
        public UniCtaCteEstado GetByLegProvi(int legProvi)
        {
            context.Configuration.LazyLoadingEnabled = false;

            UniCtaCteEstado ctaCteEstado = context.UniCtaCteEstado.Where(cce => cce.legajo == legProvi).FirstOrDefault();

            return ctaCteEstado;
        }

        /// <summary>
        /// Crea un UniCtaCteEstado
        /// </summary>
        public UniCtaCteEstado Create(UniCtaCteEstado uniCtaCteEstado)
        {
            if (uniCtaCteEstado == null)
            {
                throw new ArgumentNullException("uniCtaCteEstado");
            }
            context.UniCtaCteEstado.Add(uniCtaCteEstado);
            context.SaveChanges();

            return uniCtaCteEstado;
        }

        /// <summary>
        /// Actualiza un UniCtaCteEstado
        /// </summary>
        public UniCtaCteEstado Update(UniCtaCteEstado uniCtaCteEstado)
        {
            if (uniCtaCteEstado == null)
            {
                throw new ArgumentNullException("uniCtaCteEstado");
            }

            UniCtaCteEstado uniCtaCteEstadoDb = context.UniCtaCteEstado.Where(cce => cce.legajo == uniCtaCteEstado.legajo).FirstOrDefault();

            uniCtaCteEstadoDb.fecha = uniCtaCteEstado.fecha;
            uniCtaCteEstadoDb.Permisos = uniCtaCteEstado.Permisos;
            uniCtaCteEstadoDb.Deuda = uniCtaCteEstado.Deuda;
            uniCtaCteEstadoDb.InscrcripcionAFavor = uniCtaCteEstado.InscrcripcionAFavor;
            uniCtaCteEstadoDb.InsmatAnio = uniCtaCteEstado.InsmatAnio;
            uniCtaCteEstadoDb.DeudaSuspension = uniCtaCteEstado.DeudaSuspension;
            uniCtaCteEstadoDb.DeudaBaja = uniCtaCteEstado.DeudaBaja;
            uniCtaCteEstadoDb.Origen = uniCtaCteEstado.Origen;

            context.SaveChanges();

            return uniCtaCteEstado;
        }
    }
}