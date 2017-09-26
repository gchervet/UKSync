using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.Domain
{
    public class RecipeComponent
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Mill { get; set; }
        public double Percentage { get; set; }
        public double Quantity { get; set; }
    }
}
