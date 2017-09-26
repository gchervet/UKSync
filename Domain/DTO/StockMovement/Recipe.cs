using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.Domain
{
    public class Recipe
    {
        public string Code { get; set; }
        public List<RecipeComponent> Components { get; set; }
        public string Description { get; set; }
        public int MixingTime { get; set; }
        public int PreMixingTime { get; set; }
        public int Version { get; set; }
    }
}
