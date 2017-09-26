using System.Collections.Generic;
using MoodleWS.Common.Entities;
using MoodleWS.Common.Enums;

namespace MoodleWS.Domain.Managers
{
    public class UniMoodleWSUsuariosModificarDTOManager : ManagerBase<UniMoodleWSUsuariosModificarDTO>
    {
        #region - Constructor -

        public UniMoodleWSUsuariosModificarDTOManager() : base()
        { 
        
        }

        #endregion - Constructor -

        #region - Methods -

        public List<UniMoodleWSUsuariosModificarDTO> ObtenerUsuariosModificar()
        {
            return base.GetData(StoredProceduresCalls.SP_GET_USUARIOS_MODIFICAR, null);
        }

        #endregion - Methods -
    }
}
