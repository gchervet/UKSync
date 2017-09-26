using System;
using System.Collections.Generic;
using System.Linq;
using MoodleWS.Common.DC;
using MoodleWS.Common.Entities;
using MoodleWS.Common.Enums;
using MoodleWS.Domain.Managers;

namespace MoodleWS.Domain.BusinessLogic
{
    public class MoodleWSBusiness
    {
        #region - Constants -

        #endregion - Constants -

        #region - Properties -

        #endregion - Properties -

        #region - Managers -

        #region - UniMoodleWSAgrupamientosDTOManager -

        private UniMoodleWSAgrupamientosDTOManager _uniMoodleWSAgrupamientosDTOManager;

        private UniMoodleWSAgrupamientosDTOManager UniMoodleWSAgrupamientosDTOManager
        {
            get { if (this._uniMoodleWSAgrupamientosDTOManager == null) this._uniMoodleWSAgrupamientosDTOManager = new UniMoodleWSAgrupamientosDTOManager(); return this._uniMoodleWSAgrupamientosDTOManager; }
        }

        #endregion - UniMoodleWSAgrupamientosDTOManager -

        #region - UniMoodleWSCursosDTOManager -

        private UniMoodleWSCursosDTOManager _uniMoodleWSCursosDTOManager;

        private UniMoodleWSCursosDTOManager UniMoodleWSCursosDTOManager
        {
            get { if (this._uniMoodleWSCursosDTOManager == null) this._uniMoodleWSCursosDTOManager = new UniMoodleWSCursosDTOManager(); return this._uniMoodleWSCursosDTOManager; }
        }

        #endregion - UniMoodleWSCursosDTOManager -

        #region - UniMoodleWSGruposDTOManager -

        private UniMoodleWSGruposDTOManager _uniMoodleWSGruposDTOManager;

        private UniMoodleWSGruposDTOManager UniMoodleWSGruposDTOManager
        {
            get { if (this._uniMoodleWSGruposDTOManager == null) this._uniMoodleWSGruposDTOManager = new UniMoodleWSGruposDTOManager(); return this._uniMoodleWSGruposDTOManager; }
        }

        #endregion - UniMoodleWSGruposDTOManager -

        #region - UniMoodleWSMatriculacionesDTOManager -

        private UniMoodleWSMatriculacionesDTOManager _uniMoodleWSMatriculacionesDTOManager;

        private UniMoodleWSMatriculacionesDTOManager UniMoodleWSMatriculacionesDTOManager
        {
            get { if (this._uniMoodleWSMatriculacionesDTOManager == null) this._uniMoodleWSMatriculacionesDTOManager = new UniMoodleWSMatriculacionesDTOManager(); return this._uniMoodleWSMatriculacionesDTOManager; }
        }

        #endregion - UniMoodleWSMatriculacionesDTOManager -

        #region - UniMoodleWSUsuariosDTOManager -

        private UniMoodleWSUsuariosDTOManager _uniMoodleWSUsuariosDTOManager;

        private UniMoodleWSUsuariosDTOManager UniMoodleWSUsuariosDTOManager
        {
            get { if (this._uniMoodleWSUsuariosDTOManager == null) this._uniMoodleWSUsuariosDTOManager = new UniMoodleWSUsuariosDTOManager(); return this._uniMoodleWSUsuariosDTOManager; }
        }

        #endregion - UniMoodleWSUsuariosDTOManager -

        #region - UniMoodleWSUsuariosModificarDTOManager -

        private UniMoodleWSUsuariosModificarDTOManager _UniMoodleWSUsuariosModificarDTOManager;

        private UniMoodleWSUsuariosModificarDTOManager UniMoodleWSUsuariosModificarDTOManager
        {
            get { if (this._UniMoodleWSUsuariosModificarDTOManager == null) this._UniMoodleWSUsuariosModificarDTOManager = new UniMoodleWSUsuariosModificarDTOManager(); return this._UniMoodleWSUsuariosModificarDTOManager; }
        }

        #endregion - UniMoodleWSUsuariosModificarDTOManager -

        #endregion - Managers -

        #region - Constructor -

        public MoodleWSBusiness()
        {
            
        }

        #endregion - Constructor -

        #region - Methods -

        public List<UniMoodleWSAgrupamientosDTO> ObtenerAgrupamientos(char operacion)
        {
            return this.UniMoodleWSAgrupamientosDTOManager.ObtenerAgrupamientos(operacion);
        }

        public List<UniMoodleWSCursosDTO> ObtenerCursos()
        {
            return this.UniMoodleWSCursosDTOManager.ObtenerCursos();
        }

        public List<UniMoodleWSGruposDTO> ObtenerGrupos()
        {
            return this.UniMoodleWSGruposDTOManager.ObtenerGrupos();
        }

        public List<UniMoodleWSMatriculacionesDTO> ObtenerMatriculaciones(char operacion)
        {
            return this.UniMoodleWSMatriculacionesDTOManager.ObtenerMatriculaciones(operacion);
        }

        public List<UniMoodleWSUsuariosDTO> ObtenerUsuarios()
        {
            return this.UniMoodleWSUsuariosDTOManager.ObtenerUsuarios();
        }

        public List<UniMoodleWSUsuariosModificarDTO> ObtenerUsuariosModificar()
        {
            return this.UniMoodleWSUsuariosModificarDTOManager.ObtenerUsuariosModificar();
        }

        public void ActualizarCurso(string idNumber, long moodleId, int estado, string error)
        {
            using (MoodleWSDataContext ctx = new MoodleWSDataContext(ConfigEnum.ConnString))
            {
                UniMoodleCursos obj = ctx.UniMoodleCursos.Single(c => c.IdNumber == idNumber);
                if (moodleId > 0) obj.MoodleId = moodleId;
                obj.Estado = estado;
                obj.Fecha = DateTime.Now;
                obj.Error = error;
                ctx.SubmitChanges();
            }
        }

        public void ActualizarUsuario(int idNumber, long moodleId, int estado, string error)
        {
            using (MoodleWSDataContext ctx = new MoodleWSDataContext(ConfigEnum.ConnString))
            {
                UniMoodleUsuarios obj = ctx.UniMoodleUsuarios.Single(c => c.IdNumber == idNumber);
                if (moodleId > 0) obj.MoodleId = moodleId;
                obj.Estado = estado;
                obj.Fecha = DateTime.Now;
                obj.Error = error;
                ctx.SubmitChanges();
            }
        }

        public void ActualizarGrupo(string idNumber, long moodleId, int estado, string error)
        {
            using (MoodleWSDataContext ctx = new MoodleWSDataContext(ConfigEnum.ConnString))
            {
                UniMoodleGrupos obj = ctx.UniMoodleGrupos.Single(c => c.IdNumber == idNumber);
                if (moodleId > 0) obj.MoodleId = moodleId;
                obj.Estado = estado;
                obj.Fecha = DateTime.Now;
                obj.Error = error;
                ctx.SubmitChanges();
            }
        }

        public void ActualizarAgrupamiento(long clave, int estado, string error)
        {
            using (MoodleWSDataContext ctx = new MoodleWSDataContext(ConfigEnum.ConnString))
            {
                UniMoodleAgrupamientos obj = ctx.UniMoodleAgrupamientos.Single(c => c.Clave == clave);
                obj.Estado = estado;
                obj.Fecha = DateTime.Now;
                obj.Error = error;
                ctx.SubmitChanges();
            }
        }

        public void ActualizarMatriculacion(long clave, int estado, string error)
        {
            using (MoodleWSDataContext ctx = new MoodleWSDataContext(ConfigEnum.ConnString))
            {
                UniMoodleMatriculaciones obj = ctx.UniMoodleMatriculaciones.Single(c => c.Clave == clave);
                obj.Estado = estado;
                obj.Fecha = DateTime.Now;
                obj.Error = error;
                ctx.SubmitChanges();
            }
        }

        #endregion - Methods -
    }
}
