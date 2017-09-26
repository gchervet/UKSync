using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Dto para paginación
    /// </summary>
    public class PagedList<T>
    {
        public PagedList() 
        {
        }

        public PagedList(IList<T> rows, int total)
        {
            this.Rows = rows;
            this.Total = total;
        }

        public PagedList(IQueryable<T> allElements, int limit, int offset)
        {
            this.Total = allElements.Count();
            IQueryable<T> selectedElements = allElements.Skip(offset).Take(limit);
            this.Rows = selectedElements.ToList();
        }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("rows")]
        public ICollection<T> Rows { get; set; }
    }
}