using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.Domain
{
    public class RecipeDto
    {
        public string Code { get; set; }
        public List<RecipeComponentDto> Components { get; set; }
        public string Description { get; set; }
        public int MixingTime { get; set; }
        public int PreMixingTime { get; set; }
        public int Version { get; set; }
    }
}
