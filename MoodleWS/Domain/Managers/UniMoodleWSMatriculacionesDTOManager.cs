using System.Collections.Generic;
using MoodleWS.Common.Entities;
using MoodleWS.Common.Enums;

namespace MoodleWS.Domain.Managers
{
    public class UniMoodleWSMatriculacionesDTOManager : ManagerBase<UniMoodleWSMatriculacionesDTO>
    {
        #region - Constructor -

        public UniMoodleWSMatriculacionesDTOManager() : base()
        { 
        
        }

        #endregion - Constructor -

        #region - Methods -

        public List<UniMoodleWSMatriculacionesDTO> ObtenerMatriculaciones(char operacion)
        {
            return base.GetData(StoredProceduresCalls.SP_GET_MATRICULACIONES, new object[] { operacion });
        }

        #endregion - Methods -
    }
}
