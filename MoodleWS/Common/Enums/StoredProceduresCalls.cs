
namespace MoodleWS.Common.Enums
{
    public abstract class StoredProceduresCalls
    {
        public const string SP_GET_CURSOS = "sp_get_uniMoodle_ws_cursos";
        public const string SP_GET_GRUPOS = "sp_get_uniMoodle_ws_grupos";
        public const string SP_GET_MATRICULACIONES = "sp_get_uniMoodle_ws_matriculaciones {0}";
        public const string SP_GET_AGRUPAMIENTOS = "sp_get_uniMoodle_ws_agrupamientos {0}";
        public const string SP_GET_USUARIOS = "sp_get_uniMoodle_ws_usuarios";
        public const string SP_GET_USUARIOS_MODIFICAR = "sp_get_uniMoodle_ws_usuarios_modificar";
    }
}
