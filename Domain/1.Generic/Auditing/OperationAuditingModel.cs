using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Datos necesarios para el registro de auditoría
    /// </summary>
    public class OperationAuditingModel
    {
        public Guid RequestGuid { get; set; }
        public string Username { get; set; }
        public int? IdOperation { get; set; }
    }
}
