﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ubg_Api_v4.Controllers
{
    //Standard Home Controller from ASP.NET
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();

            //comment
        }
    }
}
