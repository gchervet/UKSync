using System;
using System.Data.Linq.Mapping;

namespace MoodleWS.Common.Entities
{
    [Table(Name = "dbo.uniMoodleAgrupamientos")]
    public class UniMoodleAgrupamientos
    {
        private long _clave;
        private int _idNumberUsuario;
        private string _idNumberGrupo;
        private char _operacion;
        private int _estado;
        private string _error;
        private Nullable<DateTime> _fecha;

        [Column(Storage = "_clave", DbType = "BigInt NOT NULL", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public long Clave
        {
            get { return this._clave; }
            set { this._clave = value; }
        }

        [Column(Storage = "_idNumberUsuario", DbType = "Int NOT NULL", CanBeNull = false)]
        public int IdNumberUsuario
        {
            get { return this._idNumberUsuario; }
            set { this._idNumberUsuario = value; }
        }

        [Column(Storage = "_idNumberGrupo", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public string IdNumberGrupo
        {
            get { return this._idNumberGrupo; }
            set { this._idNumberGrupo = value; }
        }

        [Column(Storage = "_operacion", DbType = "Char NOT NULL", CanBeNull = false)]
        public char Operacion
        {
            get { return this._operacion; }
            set { this._operacion = value; }
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
