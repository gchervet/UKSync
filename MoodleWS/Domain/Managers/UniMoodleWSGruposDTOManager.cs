using System.Collections.Generic;
using MoodleWS.Common.Entities;
using MoodleWS.Common.Enums;

namespace MoodleWS.Domain.Managers
{
    public class UniMoodleWSGruposDTOManager : ManagerBase<UniMoodleWSGruposDTO>
    {
        #region - Constructor -

        public UniMoodleWSGruposDTOManager() : base()
        { 
        
        }

        #endregion - Constructor -

        #region - Methods -

        public List<UniMoodleWSGruposDTO> ObtenerGrupos()
        {
            return base.GetData(StoredProceduresCalls.SP_GET_GRUPOS, null);
        }

        #endregion - Methods -
    }
}
