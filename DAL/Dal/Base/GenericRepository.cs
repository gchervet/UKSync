using AutoMapper;
using ConsumosCapataz.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

/// <summary> 
/// Contiene métodos para el acceso a datos.
/// </summary>
namespace ConsumosCapataz.DAL
{
    /// <summary>
    /// Proporciona una clase definición base para  el manejo de  acceso a datos.
    /// </summary>    
    /// <typeparam name="TDAL">Clase de manejo de datos.</typeparam>
    /// <typeparam name="TDTO">Clase de definición de negocio.</typeparam>
    public class GenericRepository<TDAL, TDTO>
        where TDAL : class
        where TDTO : class, new()
    {

        private DbSet<TDAL> _dbSet;

        /// <summary>
        /// Inicializa la clase.
        /// </summary>
        /// <param name="context">Objeto de ámbito para el  manejo de datos.</param>
        public GenericRepository(ModelEntities context)
        {
            Context = context;
            _dbSet = context.Set<TDAL>();
        }

        /// <summary>
        /// Inicializa la clase.
        /// </summary>
        /// <param name="context">Objeto de ámbito para el  manejo de datos.</param>
        public GenericRepository(ModelEntities context, ContextFactory contextFactory)
        {
            this.Context = context;
            this.ContextFactory = contextFactory;
            _dbSet = context.Set<TDAL>();
        }

        /// <summary>
        /// Obtiene el objeto de administración de acceso a datos.
        /// </summary>
        protected ModelEntities Context { get; private set; }

        /// <summary>
        /// Obtiene el objeto de administración de acceso a datos.
        /// </summary>
        protected ContextFactory ContextFactory { get; private set; }

        /// <summary>
        /// Método modificable que  obtiene un  objetos de negocio por el identificador.
        /// </summary>
        /// <param name="id">Identificador del objeto de negocio.</param>
        /// <returns>Objetos  de negocio.</returns>
        public virtual TDTO GetById(object id)
        {
            TDTO rtn = new TDTO();
            TDAL rtndal = _dbSet.Find(id);

            if (rtndal != null)
            {
                rtn = Mapper.DynamicMap<TDTO>(rtndal);
            }
            return rtn;
        }

        /// <summary>
        /// Método modificable que obtiene todos los objetos de negocio.
        /// </summary>
        /// <returns>Colección de objetos  de negocio.</returns>
        public virtual IEnumerable<TDTO> GetAll()
        {
            IQueryable<TDAL> objT = _dbSet;
            return Mapper.DynamicMap<IEnumerable<TDTO>>(objT.ToList());
        }

        /// <summary>
        /// Método modificable  que se ejecuta antes de insertar un objeto de negocio.
        /// </summary>
        /// <param name="entity">Objeto de negocio a insertar.</param>
        public virtual void BeforeInsert(TDTO entity) { }

        /// <summary>
        /// Inserta un objeto de negocio y retorna el objeto insertado.
        /// </summary>
        /// <param name="entity">Objeto de negocio a insertar.</param>
        /// <returns>Objeto de negocio a insertado.</returns>
        public TDTO Insert(TDTO entity)
        {
            BeforeInsert(entity);
            TDAL model = Mapper.DynamicMap<TDAL>(entity);
            _dbSet.Add(model);
            Context.SaveChanges();
            return Save(model);
        }

        /// <summary>
        /// Método modificable  que se ejecuta antes de actualizar un objeto de negocio.
        /// </summary>
        /// <param name="entity">Objeto de negocio a actualizar.</param>
        public virtual void BeforeUpdate(TDTO entity) { }

        /// <summary>
        /// Actualiza un objeto de negocio y retorna el objeto actualizado.
        /// </summary>
        /// <param name="entity">Objeto de negocio a actualizar.</param>
        /// <returns>Objeto de negocio a actualizado.</returns>
        public TDTO Update(TDTO entity)
        {
            BeforeUpdate(entity);
            TDAL model = Mapper.DynamicMap<TDAL>(entity);

            _dbSet.Attach(model);

            Context.Entry(model).State = EntityState.Modified;
            return Save(model);
        }

        /// <summary>
        /// Borra un objeto de negocio.
        /// </summary>
        /// <param name="entity">objeto de negocio a borrar.</param>
        public void Delete(TDTO entity)
        {

            TDAL model = Mapper.DynamicMap<TDAL>(entity);
            if (Context.Entry(model).State == EntityState.Detached)
            {
                _dbSet.Attach(model);
            }
            _dbSet.Remove(model);
        }

        /// <summary>
        /// Borra un objeto de negocio   por el identificador.
        /// </summary>
        /// <param name="id">Identificador del objeto de negocio.</param>
        public void DeleteById(object id)
        {
            TDAL model = _dbSet.Find(id);
            if (model != null)
            {
                if (Context.Entry(model).State == EntityState.Detached)
                {
                    _dbSet.Attach(model);
                }
                _dbSet.Remove(model);
            }
        }

        /// <summary>
        /// Cambia las propiedades del objeto de datos por la del objeto de negocio,  retorna el objeto actualizado.
        /// </summary>
        ///<param name="model">Objeto de datos a actualizar.</param>
        /// <param name="entity">Objeto de negocio a actualizado.</param>
        /// <returns>Objeto de negocio a actualizado.</returns>
        protected TDTO SetValues(TDAL model, TDTO entity)
        {
            var map = Mapper.DynamicMap<TDAL>(entity);
            Context.Entry(model).CurrentValues.SetValues(map);
            return Save(model);
        }

        /// <summary>
        /// Obtiene el primer objeto de negocio que coincida con los parámetros indicado.
        /// </summary>
        /// <param name="predicate">Expresión landa que establece los objetos de negocio a buscar.</param>
        /// <returns>Objetos  de negocio.</returns>
        protected TDTO GetSingleByParam(Expression<Func<TDAL, bool>> predicate)
        {
            TDTO rtn = new TDTO();
            TDAL rtndal = _dbSet.SingleOrDefault(predicate);
            if (rtndal != null)
            {
                rtn = Mapper.DynamicMap<TDTO>(rtndal);
            }
            return rtn;
        }

        /// <summary>
        /// Obtiene una colección de objetos de negocio que coincida con los parámetros indicado.
        /// </summary>
        /// <param name="predicate">Expresión landa que establece los objetos de negocio a buscar.</param>
        /// <returns>Colección de objetos  de negocio.</returns>
        protected IEnumerable<TDTO> GetAllByParam(Expression<Func<TDAL, bool>> predicate)
        {
            IEnumerable<TDAL> rtnDal = _dbSet.Where(predicate).ToList();
            return Mapper.DynamicMap<IEnumerable<TDTO>>(rtnDal);
        }

        /// <summary>
        /// Busca el objeto de datos por el identificador
        /// </summary>
        /// <param name="id">Identificador del objeto de datos.</param>
        /// <returns>Objetos  de datos.</returns>
        protected TDAL Find(object id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Realiza la acción sobre el objeto que está en el ámbito de datos y retorna un objeto de negocio.
        /// </summary>
        /// <param name="model">objeto de dato.</param>
        /// <returns>objeto de negocio.</returns>
        private TDTO Save(TDAL model)
        {
            Context.SaveChanges();
            return Mapper.DynamicMap<TDTO>(model);
        }
    }
}
