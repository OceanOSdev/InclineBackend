﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TodoListWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }
        public ActionResult Error(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }

        public ActionResult Team()
        {
            return View();
        }

        public ActionResult Technology()
        {
            return View();
        }
    }
}