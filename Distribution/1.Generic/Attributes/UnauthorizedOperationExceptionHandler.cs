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
    public class UnauthorizedOperationExceptionHandler : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is UnauthorizedOperationException)
            {
                context.Response = context.CreateUnauthorizedResponse(HttpUnauthorizedReason.UnauthorizedOperation);
            }
        }
    }
}