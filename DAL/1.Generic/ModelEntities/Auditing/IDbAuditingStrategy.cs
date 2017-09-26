using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDbAuditingStrategy
    {
        /// <summary>
        /// Guarda un header de auditoria en la base de datos correspondiente
        /// </summary>
        /// <param name="header">El header a guardar</param>
        AuditingHeader SaveAuditingHeader(AuditingHeader header);

        /// <summary>
        /// Devuelve el tipo correspondiente a los headers de auditoría de la base de datos actual
        /// </summary>
        /// <returns>El tipo correspondiente a los headers de auditoría de la base de datos actual</returns>
        Type GetAuditingType();
    }
}
