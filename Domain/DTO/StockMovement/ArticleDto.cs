using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumosCapataz.Domain
{
    public class ArticleDto
    {
        public string Code { get; set; }
        public double Density { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public double SalesEquivalences { get; set; }
        public string StockUmCode { get; set; }
        public int UsefulLifeDays { get; set; }
    }
}
