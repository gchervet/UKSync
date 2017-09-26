using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.Domain
{
    public class Order
    {
        public string Type { get; set; }
        public string Number { get; set; }
        public EnsolStockMovement FinishedProduct { get; set; }
        public List<EnsolStockMovement> RawMaterials { get; set; }
    }
}
