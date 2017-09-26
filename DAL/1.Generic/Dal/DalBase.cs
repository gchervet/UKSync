using AutoMapper;
using Domain;
using Domain.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

/// <summary> 
/// Contiene métodos para el acceso a datos.
/// </summary>
namespace DAL
{
    /// <summary>
    /// Proporciona una clase definición base para  el manejo de  acceso a datos.
    /// </summary>
    public class DalBase<T> where T : DbContext
    {    
        /// <summary>
        /// Inicializa la clase.
        /// </summary>
        /// <param name="context">Objeto de ámbito para el  manejo de datos.</param>
        public DalBase(T context)
        {
            this.context = context;
        }

        public DalBase(T context, ContextFactory contextFactory)
        {
            this.context = context;
            this.contextFactory = contextFactory;
        }

        /// <summary>
        /// Obtiene el objeto de administración de acceso a datos.
        /// </summary>
        protected T context { get; private set; }

        /// <summary>
        /// Obtiene el objeto proveedor de administración de acceso a datos.
        /// </summary>
        protected ContextFactory contextFactory { get; private set; }

    }   
}
