using System.Data.Linq.Mapping;

namespace MoodleWS.Common.Entities
{
    public class UniMoodleWSGruposDTO
    {
        private string _idNumber;
        private string _nombre;
        private long _moodleIdCurso;
        private string _descripcion;

        public string IdNumber
        {
            get { return this._idNumber; }
            set { this._idNumber = value; }
        }

        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        public long MoodleIdCurso
        {
            get { return this._moodleIdCurso; }
            set { this._moodleIdCurso = value; }
        }

        public string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }
    }
}
