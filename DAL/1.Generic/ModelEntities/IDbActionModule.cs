using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Representa un modulo de acción para SecurityEntitiesModuleRunner
    /// </summary>
    public interface IDbActionModule : IDbModule
    {
        /// <summary>
        /// Método a ejecutarse cuando se llame al método SaveChanges() de SecurityEntitiesModuleRunner.
        /// </summary>
        void BeforeSave();

        /// <summary>
        /// Método a ejecutarse cuando ya se llamó al método que materializa los cambios en la base de datos.
        /// </summary>
        void AfterSave();
    }
}
