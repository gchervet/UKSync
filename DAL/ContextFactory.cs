using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumosCapataz.Domain;

namespace ConsumosCapataz.DAL
{
    /// <summary>
    /// Se encarga de almacenar en sesión y proveer los datos de la auditoría de la operación actual
    /// </summary>
    public class ContextFactory
    {
        /// <summary>
        /// Devuelve New ModelEntities
        /// </summary>
        /// <returns></returns>
        public virtual ModelEntities GetContext()
        {
            return new ModelEntitiesModuleRunner();
        }
    }
}
