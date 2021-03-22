using AbasteceMais.Domain.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AbasteceMais.API.Auth
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        private ResponseError resp;

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                var authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                var usernamePasswordArray = decodedAuthenticationToken.Split(':');
                var userName = usernamePasswordArray[0];
                var password = usernamePasswordArray[1];

                // Replace this with your own system of security / means of validating credentials
                var isValid = userName == "AbasteceMais" && password == "1N&O5QixN$dw";

                // Acesso concedido 
                if (isValid)
                {
                    var principal = new GenericPrincipal(new GenericIdentity(userName), null);
                    Thread.CurrentPrincipal = principal;

                    return;
                }
                // acesso negado, credenciais invalidas
                else
                {

                    resp = new ResponseError
                    {
                        Success = false,
                        Status = Convert.ToInt32(-3),
                        Message = "Invalid server credentials"
                    };

                    actionContext.Response =
                       actionContext.Request.CreateResponse(HttpStatusCode.OK,
                          resp);
                    return;

                }
            }

            HandleUnathorized(actionContext);
        }
        // acesso negado, sem credenciais
        private static void HandleUnathorized(HttpActionContext actionContext)
        {
            ResponseError resp;
            resp = new ResponseError
            {
                Success = false,
                Status = Convert.ToInt32(-3),
                Message = "Invalid server credentials"
            };

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, resp);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='Data' location = 'http://localhost:");
        }
    }
}