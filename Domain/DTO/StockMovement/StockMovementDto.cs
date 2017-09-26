using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.Domain
{
    public class StockMovementDto
    {
        public string Batch { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string OrderNo { get; set; }
        public decimal Quantity { get; set; }
    }
}
