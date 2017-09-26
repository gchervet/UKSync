using ConsumosCapataz.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.DAL
{
    public interface IModelEntitiesModule
    {
        /// <summary>
        /// Setea el contexto sobre el que se ejecutará el módulo.
        /// </summary>
        /// <param name="context">Contexto sobre el que se ejecutará el módulo.</param>
        void SetContext(ModelEntitiesModuleRunner context);
    }
}
