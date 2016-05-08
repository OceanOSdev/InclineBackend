using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TodoListWebApp.DAL;

namespace TodoListWebApp.Controllers
{
    [Authorize]
    public class HealthController : Controller
    {
        private TodoListWebAppContext db = new TodoListWebAppContext();

        // GET: Health
        public ActionResult Index()
        {
            string owner = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View();
        }
    }
}