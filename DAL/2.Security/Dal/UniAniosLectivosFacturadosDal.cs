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
    /// DAL para la clase UniAniosLectivosFacturados
    /// </summary>
    public class UniAniosLectivosFacturadosDal : DalBase<SecurityEntities>
    {
        public UniAniosLectivosFacturadosDal(SecurityEntities context)
            : base(context)
        {
        }

        /// <summary>
        /// Obtiene todos los UniAniosLectivosFacturados. No tracking.
        /// </summary>
        public IList<UniAniosLectivosFacturados> GetAll()
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<UniAniosLectivosFacturados> aniosLectivosFacturados = context.UniAniosLectivosFacturados.ToList();

            return aniosLectivosFacturados;
        }

        /// <summary>
        /// Obtiene todos los UniAniosLectivosFacturados por legajo provisional. No tracking.
        /// </summary>
        public IList<UniAniosLectivosFacturados> GetAllByLegajo(int legProvi)
        {
            context.Configuration.LazyLoadingEnabled = false;

            IList<UniAniosLectivosFacturados> aniosLectivosFacturados = context.UniAniosLectivosFacturados.Where(alf => alf.legajo == legProvi).ToList();

            return aniosLectivosFacturados;
        }

        /// <summary>
        /// Crea un UniAniosLectivosFacturados
        /// </summary>
        public UniAniosLectivosFacturados Create(UniAniosLectivosFacturados uniAniosLectivosFacturados)
        {
            if (uniAniosLectivosFacturados == null)
            {
                throw new ArgumentNullException("uniAniosLectivosFacturados");
            }
            context.UniAniosLectivosFacturados.Add(uniAniosLectivosFacturados);
            context.SaveChanges();

            return uniAniosLectivosFacturados;
        }
    }
}