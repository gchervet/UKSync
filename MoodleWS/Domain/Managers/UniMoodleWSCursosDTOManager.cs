using System.Collections.Generic;
using MoodleWS.Common.Entities;
using MoodleWS.Common.Enums;

namespace MoodleWS.Domain.Managers
{
    public class UniMoodleWSCursosDTOManager : ManagerBase<UniMoodleWSCursosDTO>
    {
        #region - Constructor -

        public UniMoodleWSCursosDTOManager() : base()
        { 
        
        }

        #endregion - Constructor -

        #region - Methods -

        public List<UniMoodleWSCursosDTO> ObtenerCursos()
        {
            return base.GetData(StoredProceduresCalls.SP_GET_CURSOS, null);
        }

        #endregion - Methods -
    }
}
