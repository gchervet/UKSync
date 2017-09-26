using System.Collections.Generic;
using MoodleWS.Common.Entities;
using MoodleWS.Common.Enums;

namespace MoodleWS.Domain.Managers
{
    public class UniMoodleWSUsuariosDTOManager : ManagerBase<UniMoodleWSUsuariosDTO>
    {
        #region - Constructor -

        public UniMoodleWSUsuariosDTOManager() : base()
        { 
        
        }

        #endregion - Constructor -

        #region - Methods -

        public List<UniMoodleWSUsuariosDTO> ObtenerUsuarios()
        {
            return base.GetData(StoredProceduresCalls.SP_GET_USUARIOS, null);
        }

        #endregion - Methods -
    }
}
