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
    /// Service para la clase UniCtaCteEstado
    /// </summary>
    public class UniCtaCteEstadoService
    {
        UniCtaCteEstadoDal uniCtaCteEstadoDal;
        public UniCtaCteEstadoService(UniCtaCteEstadoDal uniCtaCteEstadoDal)
        {
            this.uniCtaCteEstadoDal = uniCtaCteEstadoDal;
        }

        /// <summary>
        /// Obtiene todos los UniCtaCteEstado. No tracking.
        /// </summary>
        public IList<UniCtaCteEstado> GetAll()
        {
            return uniCtaCteEstadoDal.GetAll();
        }

        /// <summary>
        /// Obtiene UniCtaCteEstado por medio de legajo provisional.
        /// </summary>
        public UniCtaCteEstado GetByLegProvi(int legProvi)
        {
            return uniCtaCteEstadoDal.GetByLegProvi(legProvi);
        }

        /// <summary>
        /// Crea un UniCtaCteEstado
        /// </summary>
        public UniCtaCteEstado Create(UniCtaCteEstado uniCtaCteEstado)
        {
            return uniCtaCteEstadoDal.Create(uniCtaCteEstado);
        }

        /// <summary>
        /// Actualiza un UniCtaCteEstado
        /// </summary>
        public UniCtaCteEstado Update(UniCtaCteEstado uniCtaCteEstado)
        {
            return uniCtaCteEstadoDal.Update(uniCtaCteEstado);
        }
          
    }
}