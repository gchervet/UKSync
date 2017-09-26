using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using Domain;
using System.Net;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace Domain
{
    /// <summary>
    /// Métodos de extensión para ActionContext
    /// </summary>
    public static class HttpActionContextExtensions
    {
        private static Dictionary<HttpUnauthorizedReason, string> errorMessages = new Dictionary<HttpUnauthorizedReason, string>()
        {
            { HttpUnauthorizedReason.CredentialsChanged, "Credentials changed" },
            { HttpUnauthorizedReason.TooManyLoginAttempts, "Too many login attempts" },
            { HttpUnauthorizedReason.InvalidCredentials, "Invalid credentials" },
            { HttpUnauthorizedReason.UnauthorizedOperation, "Unauthorized operation" },
            { HttpUnauthorizedReason.BlockedUser, "Blocked user" },
            { HttpUnauthorizedReason.UserActivationPending, "User activation pending" },
            { HttpUnauthorizedReason.PasswordChangePending, "Password change pending" },
            { HttpUnauthorizedReason.AnotherDeviceInUse, "User already active in another device" },
            { HttpUnauthorizedReason.OnlyOneAttempt, "Has only one attempt" }
        };

        /// <summary>
        /// Crea un response no autorizado
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static HttpResponseMessage CreateUnauthorizedResponse(this HttpActionContext actionContext, HttpUnauthorizedReason reason)
        {
            string message;
            errorMessages.TryGetValue(reason, out message);

            JObject error = new JObject();
            error.Add("code", (int)reason);
            error.Add("reason", message);

            actionContext.Response = actionContext.Request.CreateResponse<JObject>(HttpStatusCode.Unauthorized, error);

            return actionContext.Response;
        }

        /// <summary>
        /// Crea un response no autorizado
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static HttpResponseMessage CreateUnauthorizedResponse(this HttpActionExecutedContext actionContext, HttpUnauthorizedReason reason)
        {
            string message;
            errorMessages.TryGetValue(reason, out message);

            JObject error = new JObject();
            error.Add("code", (int)reason);
            error.Add("reason", message);

            actionContext.Response = actionContext.Request.CreateResponse<JObject>(HttpStatusCode.Unauthorized, error);

            return actionContext.Response;
        }


        /// <summary>
        /// Crea un response de conflicto
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        public static HttpResponseMessage CreateConflictResponse(this HttpActionExecutedContext actionContext, HttpConflictReason reason, object data = null)
        {

            JObject error = new JObject();
            error.Add("code", (int)reason);
            error.Add("reason", System.Enum.GetName(typeof(HttpConflictReason), reason));

            try
            {
                error.Add("data", JsonConvert.SerializeObject(data));
            }
            catch
            {
                error.Add("data", "string.Empty");
            }
            

            actionContext.Response = actionContext.Request.CreateResponse<JObject>(HttpStatusCode.Conflict, error);

            return actionContext.Response;
        }
    }
}