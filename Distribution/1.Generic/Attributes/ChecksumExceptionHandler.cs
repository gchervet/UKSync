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
    /// Clase ChecksumExceptionHandler
    /// </summary>
    public class ChecksumExceptionHandler : ExceptionFilterAttribute
    {
        /// <summary>
        /// Exception method
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ChecksumException)
            {
                context.Response = context.CreateConflictResponse(HttpConflictReason.Checksum, (context.Exception as ChecksumException).Message);
            }
        }
    }
}