using System.Collections.Generic;
using MoodleWS.Common.Entities;
using MoodleWS.Common.Enums;

namespace MoodleWS.Domain.Managers
{
    public class UniMoodleWSAgrupamientosDTOManager : ManagerBase<UniMoodleWSAgrupamientosDTO>
    {
        #region - Constructor -

        public UniMoodleWSAgrupamientosDTOManager() : base()
        { 
        
        }

        #endregion - Constructor -

        #region - Methods -

        public List<UniMoodleWSAgrupamientosDTO> ObtenerAgrupamientos(char operacion)
        {
            return base.GetData(StoredProceduresCalls.SP_GET_AGRUPAMIENTOS, new object[] { operacion });
        }

        #endregion - Methods -
    }
}
