using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Distribution.Attributes
{
    /// <summary>
    /// Filtro creado para admitir peticiones de tipo OPTIONS en acciones de controladores.
    /// </summary>
    public class PreflightRequestsHandler : DelegatingHandler
    {
        /// <summary>
        /// Responde con un ok a las request de options
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method.Method.ToUpper().Contains("OPTIONS"))
            {
                var response = new HttpResponseMessage { StatusCode = HttpStatusCode.OK };

                var tsc = new TaskCompletionSource<HttpResponseMessage>();
                tsc.SetResult(response);
                return tsc.Task;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}