using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// Representa un modulo de error para SecurityEntitiesModuleRunner
    /// </summary>
    public interface IDbErrorModule : IDbModule
    {
        /// <summary>
        /// Método a ejecutarse cuando ocurra un error en SaveChanges() de SecurityEntitiesModuleRunner.
        /// </summary>
        /// <param name="error">Excepción lanzada por el contexto al ejecutar SaveChanges()</param>
        /// <returns>Debe devolver una excepción si desea continuarse ejecutando el resto de los módulos. Null si la excepción ya ha sido controlada y no debe lanzarse a una capa superior de la ejecución.</returns>
        Exception OnError(Exception error);
    }
}
