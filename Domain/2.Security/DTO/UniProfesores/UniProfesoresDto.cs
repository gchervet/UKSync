using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Dto para listar profesores
    /// </summary>
    public class UniProfesoresDto
    {
        public int legajo { get; set; }
        public string apellido { get; set; }
        public string titobt { get; set; }
        public string tiptit { get; set; }
        public string usrnom { get; set; }
        public Nullable<System.DateTime> ftrn { get; set; }
        public bool Activo { get; set; }
        public Nullable<System.DateTime> ingfec { get; set; }
        public Nullable<System.DateTime> FechaInact { get; set; }
        public int Fila { get; set; }
        public Nullable<int> numin { get; set; }
        public string tratamiento { get; set; }
    }
}
