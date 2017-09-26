using DAL;
using Ninject;
using Ninject.Modules;
using Ninject.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace Distribution
{
    /// <summary>
    /// Web Api Application!
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication, IRequiresSessionState
    {
        /// <summary>
        /// Configuración de startup de la aplicación
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            FilterConfig.RegisterHttpFilters(GlobalConfiguration.Configuration.Filters);

            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //DAL
            DAL.App_Start.DalInitializer.Initialize();
        }

        /// <summary>
        /// Crea la sesión para los request de webapi
        /// </summary>
        protected void Application_PostAuthorizeRequest()
        {
            if (HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.ToLower().Contains("api"))
            {
                HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
            }
        }
    }
}
