using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using MoodleWS.Common.Entities;
using MoodleWS.Common.Globalization;

namespace MoodleWS.Common.DC
{
    public class MoodleWSDataContext : DataContext
    {
        private const string EXEC = "exec ";

        public Table<UniMoodleAgrupamientos> UniMoodleAgrupamientos;
        public Table<UniMoodleCursos> UniMoodleCursos;
        public Table<UniMoodleCursosDetalle> UniMoodleCursosDetalle;
        public Table<UniMoodleGrupos> UniMoodleGrupos;
        public Table<UniMoodleGruposDetalle> UniMoodleGruposDetalle;
        public Table<UniMoodleMatriculaciones> UniMoodleMatriculaciones;
        public Table<UniMoodleUsuarios> UniMoodleUsuarios;

        public MoodleWSDataContext(string connString) : base(connString)
        {
            //this.ObjectTrackingEnabled = true;
        }

        public List<T> GetDataWithoutParameters<T>(string procname)
        {
            if (!string.IsNullOrEmpty(procname))
            {
                this.DeferredLoadingEnabled = false;
                var query = this.ExecuteQuery<T>(MoodleWSDataContext.EXEC + procname);
                List<T> results = query.ToList();
                return results;
            }
            else
            {
                throw new Exception(MensajesErrores.ERROR_NO_SE_ENCUENTRA_CONSULTA);
            }
        }

        public List<T> GetDataWithParameters<T>(string procname, object[] parameters)
        {
            if (!string.IsNullOrEmpty(procname))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    this.DeferredLoadingEnabled = false;
                    var query = this.ExecuteQuery<T>(MoodleWSDataContext.EXEC + procname, parameters);
                    List<T> results = query.ToList();
                    return results;
                }

                return null;
            }
            else
            {
                throw new Exception(MensajesErrores.ERROR_NO_SE_ENCUENTRA_CONSULTA);
            }
        }
    }
}
