using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Net.Http;
using Domain;
using System.Web.Http.Controllers;
using System.Security.Principal;
using DAL;
using System.Net.Http.Headers;
using Ninject;

namespace Distribution.Attributes
{
    /// <summary>
    /// Atributo de autorización
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizeOnlyAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// Calls when a process requests authorization
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for using System.Web.Http.Filters.AuthorizationFilterAttribute</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IEnumerable<string> authOnlyHeader;
            if (actionContext.Request.Headers.TryGetValues("authorization-only", out authOnlyHeader) && authOnlyHeader.First() == "true")
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }

            return;
        }
    }
}