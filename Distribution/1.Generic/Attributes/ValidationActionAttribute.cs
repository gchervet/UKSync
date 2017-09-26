using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Distribution.Attributes
{
    /// <summary>
    /// Atributo aplicado a acciones para interceptar llamadas y realizar validaciones de modelo.
    /// </summary>
    public class ValidationActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// Intercepta las llamadas a acciones webapi y valida el estado del modelo.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, modelState);
            }
        }
    }
}