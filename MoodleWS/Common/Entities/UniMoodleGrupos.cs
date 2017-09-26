using System;
using System.Data.Linq.Mapping;

namespace MoodleWS.Common.Entities
{
    [Table(Name = "dbo.uniMoodleGrupos")]
    public class UniMoodleGrupos
    {
        private string _idNumber;
        private string _idNumberCurso;
        private string _nombreCustom;
        private long? _moodleId;
        private int _estado;
        private string _error;
        private Nullable<DateTime> _fecha;

        [Column(Storage = "_idNumber", DbType = "VarChar(50) NOT NULL", IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public string IdNumber
        {
            get { return this._idNumber; }
            set { this._idNumber = value; }
        }

        [Column(Storage = "_idNumberCurso", DbType = "VarChar(50) NOT NULL", IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public string IdNumberCurso
        {
            get { return this._idNumberCurso; }
            set { this._idNumberCurso = value; }
        }

        [Column(Storage = "_nombreCustom", DbType = "NVarChar(254) NULL", CanBeNull = true)]
        public string NombreCustom
        {
            get { return this._nombreCustom; }
            set { this._nombreCustom = value; }
        }

        [Column(Storage = "_moodleId", DbType = "BigInt NULL", CanBeNull = true)]
        public long? MoodleId
        {
            get { return this._moodleId; }
            set { this._moodleId = value; }
        }

        [Column(Storage = "_estado", DbType = "Int NOT NULL", CanBeNull = false)]
        public int Estado
        {
            get { return this._estado; }
            set { this._estado = value; }
        }

        [Column(Storage = "_error", DbType = "NVarChar(max) NULL", CanBeNull = true)]
        public string Error
        {
            get { return this._error; }
            set { this._error = value; }
        }

        [Column(Storage = "_fecha", DbType = "DateTime NULL", CanBeNull = true)]
        public Nullable<DateTime> Fecha
        {
            get { return this._fecha; }
            set { this._fecha = value; }
        }
    }
}
