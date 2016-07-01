using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//namespace TodoListWebApp.AuthorizationFilters
//{
//    public class InclineAuthorizeAttribute
//    {
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TodoListWebApp.DAL;

namespace TodoListWebApp.AuthorizationFilters
{
    // This attribute limits access of the resource it decorates to the users that have been onboarded
    public class InclineAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string issuer = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Issuer;
            string UPN = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;

            using (TodoListWebAppContext db = new TodoListWebAppContext())
            {
                if (!(
                    // Verifies if the organization to which the caller belongs is trusted.
                    // This onboarding style is not possible in the consent flow originated by a native app shown in this sample,
                    // but it could be achieved by triggering consent from an associated web application.
                    // For details, see the sample https://github.com/AzureADSamples/WebApp-WebAPI-MultiTenant-OpenIdConnect-DotNet
                    (db.Tenants.FirstOrDefault(a => ((a.IssValue == issuer) && (a.AdminConsented))) != null)
                    // Verifies if the caller is in the db of onboarded users.                    
                    || (db.Users.FirstOrDefault(b => (b.UPN == UPN) && (b.TenantID == tenantID)) != null)
                    ))
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                        $"The user {UPN} has not been onboarded. Sign up and try again");
                }
            }
        }
    }

}


