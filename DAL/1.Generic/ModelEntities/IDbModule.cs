using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDbModule
    {
        /// <summary>
        /// Setea el contexto sobre el que se ejecutará el módulo.
        /// </summary>
        /// <param name="context">Contexto sobre el que se ejecutará el módulo.</param>
        void SetContext(DbContext context);
    }
}
