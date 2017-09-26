using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodleWS.Common.Entities
{
    public class UniMoodleWSUsuariosModificarDTO
    {
        private int _idNumber;
        private long? _moodleId;

        public int IdNumber
        {
            get { return this._idNumber; }
            set { this._idNumber = value; }
        }

        public long? MoodleId
        {
            get { return this._moodleId; }
            set { this._moodleId = value; }
        }
    }
}
