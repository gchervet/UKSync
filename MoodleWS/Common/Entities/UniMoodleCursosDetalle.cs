using System.Data.Linq.Mapping;

namespace MoodleWS.Common.Entities
{
    [Table(Name = "dbo.uniMoodleCursosDetalle")]
    public class UniMoodleCursosDetalle
    {
        private string _idNumberCurso;
        private int _uniCursoId;

        [Column(Storage = "_idNumberCurso", DbType = "NVarChar(50) NOT NULL", IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public string IdNumberCurso
        {
            get { return this._idNumberCurso; }
            set { this._idNumberCurso = value; }
        }

        [Column(Storage = "_uniCursoId", DbType = "BigInt NOT NULL", IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public int UniCursoId
        {
            get { return this._uniCursoId; }
            set { this._uniCursoId = value; }
        }
    }
}
