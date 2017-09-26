using System;
using System.Configuration;

namespace MoodleWS.Common.Enums
{
    public static class ConfigEnum
    {
        public static string MoodleBaseURL = ConfigurationManager.AppSettings["MoodleBaseURL"];
        public static string MoodleRestServerPath = ConfigurationManager.AppSettings["MoodleRestServerPath"];
        public static string MoodleWSToken = ConfigurationManager.AppSettings["MoodleWSToken"];
        public static string MoodleWSUsername = ConfigurationManager.AppSettings["MoodleWSUsername"];
        public static string MoodleWSTokenParameter = ConfigurationManager.AppSettings["MoodleWSTokenParameter"];
        public static string MoodleWSFunctionNameParameter = ConfigurationManager.AppSettings["MoodleWSFunctionNameParameter"];
        public static string MoodleRestServiceFormat = ConfigurationManager.AppSettings["MoodleRestServiceFormat"];
        public static string MoodleRestServiceFormatParameter = ConfigurationManager.AppSettings["MoodleRestServiceFormatParameter"];
        public static string MoodleCourseFormat = ConfigurationManager.AppSettings["MoodleCourseFormat"];
        public static string MoodleUserAuthMethod = ConfigurationManager.AppSettings["MoodleUserAuthMethod"];
        public static string MoodleUserPasswordDefault = ConfigurationManager.AppSettings["MoodleUserPasswordDefault"];
        public static char OperacionAlta = Convert.ToChar(ConfigurationManager.AppSettings["OperacionAlta"]);
        public static char OperacionBaja = Convert.ToChar(ConfigurationManager.AppSettings["OperacionBaja"]);
        public static string CursoFechaInicio = Convert.ToDateTime(ConfigurationManager.AppSettings["CursoFechaInicio"]).ToUniversalTime().Subtract(new DateTime(1970,1,1)).TotalSeconds.ToString();
        public static string OpcionCursoCantidadTemas = ConfigurationManager.AppSettings["OpcionCursoCantidadTemas"];
        public static string OpcionCursoPaginacion = ConfigurationManager.AppSettings["OpcionCursoPaginacion"];
        public static string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
    }
}
