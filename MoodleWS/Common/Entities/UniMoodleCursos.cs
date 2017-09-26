using System;
using System.Data.Linq.Mapping;

namespace MoodleWS.Common.Entities
{
    [Table(Name = "dbo.uniMoodleCursos")]
    public class UniMoodleCursos
    {
        private string _idNumber;
        private long _moodleIdCategoria;
        private string _nombreCustom;
        private Nullable<Int64> _moodleId;
        private int _estado;
        private string _error;
        private Nullable<DateTime> _fecha;
        private string _tipo;

        [Column(Storage = "_idNumber", DbType = "NVarChar(50) NOT NULL", IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public string IdNumber 
        {
            get { return this._idNumber; }
            set { this._idNumber = value; }
        }

        [Column(Storage = "_moodleIdCategoria", DbType = "BigInt NOT NULL", CanBeNull = false)]
        public long MoodleIdCategoria
        {
            get { return this._moodleIdCategoria; }
            set { this._moodleIdCategoria = value; }
        }

        [Column(Storage = "_nombreCustom", DbType = "NVarChar(254) NULL", CanBeNull = true)]
        public string NombreCustom
        {
            get { return this._nombreCustom; }
            set { this._nombreCustom = value; }
        }

        [Column(Storage = "_moodleId", DbType = "BigInt NULL", CanBeNull = true)]
        public Nullable<Int64> MoodleId
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

        [Column(Storage = "_tipo", DbType = "Char(1) NOT NULL", CanBeNull = false)]
        public string Tipo
        {
            get { return this._tipo; }
            set { this._tipo = value; }
        }
    }
}
