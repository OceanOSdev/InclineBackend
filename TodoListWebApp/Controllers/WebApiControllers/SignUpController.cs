using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

using TodoListWebApp.DAL;
using TodoListWebApp.Models;

namespace TodoListWebApp.Controllers
{

    [Authorize]
    public class SignUpController : ApiController
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // This method is a placeholder for your onboarding logic.
        // The information provided in the parameters should be used to determine whether the caller
        // (represented by the token securing the call) should be stored as a valid user of the API
        [HttpPost]
        public void Onboard([FromBody]string name)
        {
            // here "name" is just a placeholder for the real data your app would require from the caller
            // if (MyCustomOnboardingDataValidation(name))
            string upn = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value;
            string tenantID = ClaimsPrincipal.Current.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value;
            if (db.Users.FirstOrDefault(a => (a.UPN == upn) && (a.TenantID == tenantID)) == null)
            {
                // add the caller to the collection of valid users
                db.Users.Add(new User { UPN = upn, TenantID = tenantID });
            }
            db.SaveChanges();
        }
    }
}
