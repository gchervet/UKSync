using System.Data.Linq.Mapping;

namespace MoodleWS.Common.Entities
{
    [Table(Name = "dbo.uniMoodleGruposDetalle")]
    public class UniMoodleGruposDetalle
    {
        private string _idNumberGrupo;
        private int _uniCursoId;

        [Column(Storage = "_idNumberGrupo", DbType = "NVarChar(50) NOT NULL", IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public string IdNumberGrupo
        {
            get { return this._idNumberGrupo; }
            set { this._idNumberGrupo = value; }
        }

        [Column(Storage = "_uniCursoId", DbType = "BigInt NOT NULL", IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public int UniCursoId
        {
            get { return this._uniCursoId; }
            set { this._uniCursoId = value; }
        }
    }
}
