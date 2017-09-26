using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Distribution.Attributes
{
    /// <summary>
    /// Atributo que señala que será necesaria una conexión https para realizar una consulta a esta acción
    /// </summary>
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Fuerza las conexiones a utilizar el protocolo https
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            //TODO:Mover al filterconfig?
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps && !Properties.Settings.Default.ForceHttp)
            {
                HandleNonHttpsRequest(actionContext);
            }
            else
            {
                base.OnAuthorization(actionContext);
            }
        }

        /// <summary>
        /// Manejo de conexiones no https.
        /// </summary>
        /// <param name="actionContext"></param>
        protected virtual void HandleNonHttpsRequest(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
            actionContext.Response.ReasonPhrase = "SSL Required";
        }
    }
}