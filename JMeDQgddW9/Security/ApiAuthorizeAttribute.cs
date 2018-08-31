using JMeDQgddW9.Core.Entities;
using JMeDQgddW9.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace JMeDQgddW9.Security
{
    /// <summary>
    /// Custom AuthorizeAttribute
    /// </summary>
    public sealed class ApiAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// On authorization
        /// </summary>
        /// <param name="context">Authorization filter context</param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.ActionDescriptor.FilterDescriptors
                .Any(f => f.Filter.GetType() == (typeof(Microsoft.AspNetCore.Mvc.Authorization.AllowAnonymousFilter))))
            {
                return;
            }

            if (Authorize(filterContext.HttpContext))
            {
                return;
            }

            throw new UnauthorizedAccessException(Properties.Resources.UnAuthorizeException);
        }

        /// <summary>
        /// Verify login and token
        /// </summary>
        /// <param name="httpContext">Request http context</param>
        /// <returns></returns>
        private bool Authorize(HttpContext httpContext)
        {
            var authenticationService = httpContext.RequestServices.GetService(typeof(IAuthenticationService));

            try
            {
                string headerToken = httpContext.Request.Headers["Authorization"].ToString();
                string headerLogin = httpContext.Request.Headers["Login"].ToString();
                Token token = ((IAuthenticationService)authenticationService).GetToken(headerLogin);
                if (token.ExpirationDate > DateTime.Now)
                {
                    return token.TokenValue == headerToken;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}