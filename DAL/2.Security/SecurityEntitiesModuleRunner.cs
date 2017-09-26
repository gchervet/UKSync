using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Ninject;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using DAL;
using System.Web.Http;
using Domain.Security;

namespace Domain
{
    /// <summary>
    /// Entidad que representa el contexto de acceso a datos.
    /// Ejecuta una lista de módulos cada vez que se ejecuta el comando SaveChanges().
    /// </summary>
    public class SecurityEntitiesModuleRunner : SecurityEntities, IModuleContext
    {
        private DbModuleRunner moduleRunner;

        /// <summary>
        /// Inicializa la clase con los módulos por defecto
        /// </summary>
        public SecurityEntitiesModuleRunner()
            : base()
        {
            moduleRunner = new DbModuleRunner(this);
        }

        /// <summary>
        /// Inicializa la clase
        /// </summary>
        /// <param name="modules">Modulos que debe correr el contexto</param>
        public SecurityEntitiesModuleRunner(params IDbModule[] modules)
            : base()
        {
            moduleRunner = new DbModuleRunner(this, modules);
        }

        /// <summary>
        /// Guarda los cambios corriendo los módulos
        /// </summary>
        public override int SaveChanges()
        {
            return moduleRunner.SaveChanges();
        }

        /// <summary>
        /// Guarda los cambios corriendo o no los módulos según la indicación
        /// </summary>
        /// <param name="runModules">Indica si deben correrse los modulos</param>
        public int SaveChanges(bool runModules)
        {
            if (runModules)
            {
                return moduleRunner.SaveChanges();
            }
            else
            {
                return base.SaveChanges();
            }
        }
    }
}
