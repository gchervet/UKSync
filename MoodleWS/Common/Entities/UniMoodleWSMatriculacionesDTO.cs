using System.Data.Linq.Mapping;

namespace MoodleWS.Common.Entities
{
    public class UniMoodleWSMatriculacionesDTO
    {
        private long _clave;
        private long _moodleIdUsuario;
        private long _moodleIdCurso;
        private int _moodleIdRol;

        public long Clave
        {
            get { return this._clave; }
            set { this._clave = value; }
        }

        public long MoodleIdUsuario
        {
            get { return this._moodleIdUsuario; }
            set { this._moodleIdUsuario = value; }
        }

        public long MoodleIdCurso
        {
            get { return this._moodleIdCurso; }
            set { this._moodleIdCurso = value; }
        }

        public int MoodleIdRol
        {
            get { return this._moodleIdRol; }
            set { this._moodleIdRol = value; }
        }
    }
}
