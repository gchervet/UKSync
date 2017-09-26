using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Net.Http;
using Horizon.ProyectoBase.Domain;
using System.Web.Http.Controllers;
using System.Security.Principal;
using Horizon.ProyectoBase.DAL;
using System.Net.Http.Headers;
using Ninject;

namespace Horizon.ProyectoBase.Distribution.Attributes
{
    /// <summary>
    /// Atributo de autorización
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class RoleAuthorizeAttribute : AuthorizationFilterAttribute
    {
        [Inject]
        public SessionTokenDal sessionTokenDal { get; set; }

        [Inject]
        public UserDal userDal { get; set; }

        /// <summary>
        /// Calls when a process requests authorization
        /// </summary>
        /// <param name="actionContext">The action context, which encapsulates information for using System.Web.Http.Filters.AuthorizationFilterAttribute</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            ICollection<LinkedOperationsAttribute> operationAttributes = actionContext.ActionDescriptor.GetCustomAttributes<LinkedOperationsAttribute>();
            ICollection<AllowAnonymousAttribute> actionAnonymousAttributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();
            ICollection<AllowAnonymousAttribute> controllerAnonymousAttributes = actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();

            try
            {
                bool actionAllowsAnonymous = actionAnonymousAttributes.Count > 0 || controllerAnonymousAttributes.Count > 0;

                if (actionAllowsAnonymous)
                {
                    //No hay operaciones definidas. Se permiten anonymous
                    IEnumerable<string> authOnlyHeader;
                    if (actionContext.Request.Headers.TryGetValues("authorization-only", out authOnlyHeader) && authOnlyHeader.First() == "true")
                    {
                        actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    }

                    return;
                }
                else
                {
                    //Obtengo las operaciones del primer atributo asociado a la acción (No deberia haber dos atributos linkedoperation asociados a la misma acción)
                    List<Operations> actionLinkedOperations = operationAttributes.Count > 0 ? operationAttributes.First().AllowedOperations : null;

                    var authorizationHeader = actionContext.Request.Headers.Authorization;

                    if (authorizationHeader != null)
                    {
                        if (authorizationHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(authorizationHeader.Parameter))
                        {
                            bool isAuthorized = IsAuthorized(authorizationHeader, actionLinkedOperations);

                            if (isAuthorized)
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
                }

                //Ninguna de las verificaciones anteriores dió verdadera. Devuelvo un código 401: No autorizado
                actionContext.CreateUnauthorizedResponse(UnauthorizedReasonEnum.UnauthorizedOperation);
            }
            finally
            {
                SaveOperationAuditModel(actionContext, operationAttributes);
            }

            //actionContext.CreateUnauthorizedResponse(UnauthorizedReasonEnum.TooManyLoginAttempts);
        }

        /// <summary>
        /// Almacena un modelo de auditoría para la operación actual
        /// </summary>
        /// <param name="actionContext">actionContext</param>
        /// <param name="linkedOps">Lista de linked operations de la acción realizandose actualmente</param>
        /// <param name="linkedGroups">Lista de linked operation groups de la acción realizandose actualmente</param>
        private void SaveOperationAuditModel(HttpActionContext actionContext, ICollection<LinkedOperationsAttribute> linkedOps)
        {

            Guid requestGuid = actionContext.Request.GetCorrelationId();

            int actualOperationId = 0;
            int actualGroupId = 0;
            if (linkedOps.Count > 0)
            {
                foreach (var linkedOp in linkedOps)
                {
                    if (linkedOp.AllowedOperations.Count > 0)
                    {
                        actualOperationId = (int)linkedOp.AllowedOperations.First();
                    }
                }
            }
            string user = string.Empty;
            if (HttpContext.Current.User == null || HttpContext.Current.User.Identity == null || string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
            {
                try
                {
                    if (IsAuthorized(actionContext.Request.Headers.Authorization, new List<Operations>()))
                    {
                        user = HttpContext.Current.User.Identity.Name;
                    }
                }
                catch
                {
                    user = string.Empty;
                }
            }
            else
            {
                user = HttpContext.Current.User.Identity.Name;
            }
            AuditingProvider.SetOperationAuditingModel(user, actualOperationId, actualGroupId, requestGuid);
        }

        /// <summary>
        /// Comprueba que un token sea válido y que el usuario asociado a este esté autorizado para realizar una de las operaciones indicadas
        /// </summary>
        /// <param name="authorizationHeader">Header de autorizacion</param>
        /// <param name="parameters">Operaciones requeridas por la accion</param>
        /// <param name="parameters">Grupos de operaciones requeridos por la accion</param>
        /// <returns>True si está autorizado, false ne caso contrario</returns>
        private bool IsAuthorized(AuthenticationHeaderValue authorizationHeader, List<Operations> operations)
        {
            SessionToken sessionToken = sessionTokenDal.GetSessionToken(authorizationHeader.Parameter);

            if (sessionToken == null)
            {
                return false;
            }

            if (UserGotOperations(operations, sessionToken))
            {
                string[] roles = new string[0];
                if (sessionToken.User.IsRoot)
                {
                    roles = new string[] { "root" };
                }

                var currentPrincipal = new GenericPrincipal(new GenericIdentity(sessionToken.User.Username), roles);
                HttpContext.Current.User = currentPrincipal;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Comprueba que un usuario tenga alguna de las operaciones indicadas
        /// </summary>
        /// <param name="parameters">Listado de operaciones a chequear</param>
        /// <param name="parameters">Listado de grupos de operaciones a chequear</param>
        /// <param name="sessionToken">Token de identificacion del usuario</param>
        /// <returns>True si posee alguna de las operaciones, false ne caso contrario</returns>
        private bool UserGotOperations(List<Operations> operations, SessionToken sessionToken)
        {
            if (operations != null && operations.Count > 0)
            {
                return userDal.CheckUserOperations(sessionToken.User_IdUser, operations);
            }
            else
            {
                return true;
            }
        }
    }
}