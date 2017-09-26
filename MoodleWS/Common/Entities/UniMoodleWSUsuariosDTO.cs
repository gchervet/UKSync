using System.Data.Linq.Mapping;

namespace MoodleWS.Common.Entities
{
    public class UniMoodleWSUsuariosDTO
    {
        private int _idNumber;
        private string _nombre;
        private string _apellido;
        private string _username;
        private string _mail;

        public int IdNumber
        {
            get { return this._idNumber; }
            set { this._idNumber = value; }
        }

        public string Nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        public string Apellido
        {
            get { return this._apellido; }
            set { this._apellido = value; }
        }

        public string Username
        {
            get { return this._username; }
            set { this._username = value; }
        }

        public string Mail
        {
            get { return this._mail; }
            set { this._mail = value; }
        }
    }
}
