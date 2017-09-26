
namespace MoodleWS.Common.Entities
{
    public class UniMoodleWSAgrupamientosDTO
    {
        private long _clave;
        private long _moodleIdUsuario;
        private long _moodleIdGrupo;

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

        public long MoodleIdGrupo
        {
            get { return this._moodleIdGrupo; }
            set { this._moodleIdGrupo = value; }
        }
    }
}
