using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Domain;

namespace Distribution.Attributes
{
    /// <summary>
    /// Clase ConcurrencyExceptionHandler
    /// </summary>
    public class InvalidArgumentExceptionHandler : ExceptionFilterAttribute
    {
        /// <summary>
        /// Exception method
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ArgumentException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.NotFound, "Not Found. An argument is invalid.");
            }
        }
    }
}