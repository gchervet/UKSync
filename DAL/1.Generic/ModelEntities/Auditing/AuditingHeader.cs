using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AuditingHeader
    {
        public int IdAuditing { get; set; }
        public Nullable<int> IdOperation { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public string Roles { get; set; }
        public bool Audited { get; set; }
    }
}
