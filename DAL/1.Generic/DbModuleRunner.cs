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
    public class DbModuleRunner
    {

        private DbContext context;

        /// <summary>
        /// Incializa la instancia de DbModuleRunner con los módulos por defecto cargados.
        /// </summary>
        public DbModuleRunner(DbContext context)
            : base()
        {
            this.context = context;
            this.Modules = new List<IDbModule>();

            this.AddModule(new DbEntityErrorModule());
        }

        /// <summary>
        /// Incializa la instancia de SecurityEntitiesModuleRunner con los módulos especificados via parámetro. 
        /// ADVERTENCIA: De esta forma no se cargarán los módulos por defecto.
        /// </summary>
        public DbModuleRunner(DbContext context, params IDbModule[] modules) : base()
        {
            this.context = context;
            this.Modules = new List<IDbModule>();

            if (modules != null)
            {
                foreach (IDbModule module in modules)
                {
                    this.AddModule(module);
                }
            }
        }

        public IList<IDbModule> Modules { get; set; }

        /// <summary>
        /// Agrega un módulo a la pila de ejecución de modulos que se corren a la hora de ejecutar SaveChanges() 
        /// </summary>
        /// <param name="module">Modulo que se agrega a la lista</param>
        public void AddModule(IDbModule module)
        {
            module.SetContext(context);
            Modules.Add(module);
        }

        /// <summary>
        /// Override de SaveChanges(). Muestra los errores de validación con detalle.
        /// </summary>
        /// <returns>The number of state entries written to the underlying database. This can include state entries for entities and/or relationships. Relationship state entries are created for many-to-many relationships and relationships where there is no foreign key property included in the entity class (often referredto as independent associations).</returns>
        public int SaveChanges()
        {
            try
            {
                foreach (IDbActionModule module in GetModulesByInterface<IDbActionModule>())
                {
                    module.BeforeSave();
                }
                int result = this.SaveChanges(false);
                foreach (IDbActionModule module in GetModulesByInterface<IDbActionModule>())
                {
                    module.AfterSave();
                }
                return result;
            }
            catch (Exception e)
            {
                foreach (IDbErrorModule module in GetModulesByInterface<IDbErrorModule>())
                {
                    e = module.OnError(e);
                    if (e == null)
                    {
                        break;
                    }
                }
                if (e != null)
                {
                    throw e;
                }
            }
            return 0;
        }

        /// <summary>
        /// Guarda los cambios de las entidades en la base de datos.
        /// </summary>
        /// <param name="runModules">Especifica si debe ejecutar la lista de módulos para esta operación</param>
        /// <returns>The number of state entries written to the underlying database.</returns>
        public int SaveChanges(bool runModules)
        {
            if (!runModules)
            {
                if (context is IModuleContext)
                {
                    return (context as IModuleContext).SaveChanges(false);
                }
                else
                {
                    throw new InvalidOperationException("El contexto debe implementar IModuleContext");
                }
                
            }
            else
            {
                return SaveChanges();
            }
        }

        /// <summary>
        /// Obtiene los módulos registrados que implementen una determinada interface.
        /// </summary>
        /// <typeparam name="T">Interface que deben implementar los múdulos</typeparam>
        /// <returns>Lista de módulos que implementan una determinada interface</returns>
        private IList<T> GetModulesByInterface<T>()
        {
            return Modules.Where(x => x.GetType().GetInterfaces().Contains(typeof(T))).Select(x => (T)x).ToList();
        }
    }
}
