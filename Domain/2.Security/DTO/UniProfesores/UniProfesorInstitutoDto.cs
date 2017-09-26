using System;

namespace Domain
{
    /// <summary>
    /// Dto para listar profesores
    /// </summary>
    public class UniProfesorInstitutoDto
    {
        public string CorreoInstituto { get; set; }

        public string CorreoCopia { get; set; }

        public string CursoId { get; set; }

        public int ClaseNro { get; set; }

        public int LegajoProfesor { get; set; }

        public string apellido { get; set; }

        public string Usuario { get; set; }

        public System.DateTime FechaPlanificada { get; set; }

        public string turno { get; set; }

        public Nullable<int> codins { get; set; }

        public int codcur { get; set; }

        public string codmat { get; set; }

        public string comision { get; set; }

        public Nullable<System.DateTime> HoraInicio { get; set; }

        public Nullable<System.DateTime> HoraFin { get; set; }

        public int EstadoAviso { get; set; }
    }
}
