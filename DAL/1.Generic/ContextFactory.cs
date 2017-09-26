using Domain;
using Domain.Negocio;
using Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL
{
    /// <summary>
    /// Se encarga de almacenar en sesión y proveer los datos de la auditoría de la operación actual
    /// </summary>
    public class ContextFactory
    {
        /// <summary>
        /// Devuelve New SecurityEntities
        /// </summary>
        /// <returns></returns>
        public virtual SecurityEntities GetSecurityContext()
        {
            return new SecurityEntitiesModuleRunner();
        }

        /// <summary>
        /// Devuelve New SecurityEntities
        /// </summary>
        /// <returns></returns>
        public virtual ModelEntities GetNegocioContext()
        {
            return new ModelEntitiesModuleRunner();
        }
    }
}