using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using MoodleWS.Common.DC;
using MoodleWS.Common.Globalization;

namespace MoodleWS.Domain.Managers
{
    public abstract class ManagerBase<TEntity> where TEntity : class
    {
        #region - Constants -

        private const string CONNECTION_STRING_CONFIG = "ConnString";
        public static string CONNECTION_STRING = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_CONFIG].ConnectionString;
        public const string FORMAT_STRING = "{0}|{1}";

        #endregion  - Constants -

        #region - Properties -

        #endregion - Properties -

        #region - Constructor -

        public ManagerBase()
        {

        }

        #endregion - Constructor -

        #region - Methods -

        public virtual void Crear(TEntity entity)
        {
            using (MoodleWSDataContext dataContext = new MoodleWSDataContext(CONNECTION_STRING))
            {
                Table<TEntity> tabla = dataContext.GetTable<TEntity>();

                if (tabla != null)
                {
                    tabla.InsertOnSubmit(entity);

                    dataContext.SubmitChanges();
                }
                else
                {
                    throw new Exception(MensajesErrores.ERROR_AL_GUARDAR);
                }
            }
        }

        public virtual void CrearLote(List<TEntity> entityList)
        {
            using (MoodleWSDataContext dataContext = new MoodleWSDataContext(CONNECTION_STRING))
            {
                Table<TEntity> tabla = dataContext.GetTable<TEntity>();

                if (tabla != null)
                {
                    tabla.InsertAllOnSubmit(entityList);

                    dataContext.SubmitChanges();
                }
                else
                {
                    throw new Exception(MensajesErrores.ERROR_AL_GUARDAR);
                }
            }
        }

        public virtual void SubmitChanges()
        {
            try
            {
                using (MoodleWSDataContext dataContext = new MoodleWSDataContext(CONNECTION_STRING))
                {
                    dataContext.SubmitChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception(MensajesErrores.ERROR_AL_GUARDAR);
            }
        }

        public virtual List<TEntity> GetData(string procname, object[] parameters)
        {
            try
            {
                using (MoodleWSDataContext dataContext = new MoodleWSDataContext(CONNECTION_STRING))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        return dataContext.GetDataWithParameters<TEntity>(procname, parameters);
                    }
                    else
                    {
                        return dataContext.GetDataWithoutParameters<TEntity>(procname);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(MensajesErrores.ERROR_AL_EJECUTAR_CONSULTA);
            }
        }

        #endregion - Methods -
    }

}
