using System.Data.Linq.Mapping;

namespace MoodleWS.Common.Entities
{
    public class UniMoodleWSCursosDTO
    {
        private string _idNumber;
        private string _nombre;
        private string _nombreCorto;
        private string _descripcion;
        private long _moodleIdCategoria;

        [Column(Storage = "_idNumber", DbType = "VarChar(50) NOT NULL", CanBeNull = false)]
        public string IdNumber 
        {
            get { return this._idNumber; }
            set { this._idNumber = value; }
        }

        [Column(Storage = "_nombre", DbType = "VarChar(254) NOT NULL", CanBeNull = false)]
        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        [Column(Storage = "_nombre", DbType = "VarChar(254) NOT NULL", CanBeNull = false)]
        public string NombreCorto
        {
            get { return this._nombreCorto; }
            set { this._nombreCorto = value; }
        }

        [Column(Storage = "_descripcion", DbType = "VarChar(254) NOT NULL", CanBeNull = false)]
        public string Descripcion
        {
            get { return this._descripcion; }
            set { this._descripcion = value; }
        }

        [Column(Storage = "_moodleIdCategoria", DbType = "BigInt NOT NULL", CanBeNull = false)]
        public long MoodleIdCategoria
        {
            get { return this._moodleIdCategoria; }
            set { this._moodleIdCategoria = value; }
        }
    }
}
