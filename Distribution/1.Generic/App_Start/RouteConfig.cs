using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Distribution
{
    /// <summary>
    /// RouteConfig
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// RegisterRoutes
        /// </summary>
        /// <param name="routes">routes</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
            
            //routes.MapHttpRoute("DefaultApiGet", "Api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(System.Net.Http.HttpMethod.Get.ToString()) });
        }
    }
}
