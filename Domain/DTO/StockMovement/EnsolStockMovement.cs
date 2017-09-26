using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.Domain
{
    public class EnsolStockMovement
    {
        public int IdCompany { get; set; }
        public int ProcessOrder { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Batch { get; set; }
        public decimal Quantity { get; set; }
        public string ECAStorageCode { get; set; }
        public string ICAStorageCode { get; set; }
        public string Message { get; set; }
        public string ECA { get; set; }
    }
}
