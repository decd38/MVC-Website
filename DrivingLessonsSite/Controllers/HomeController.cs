﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrivingLessonsSite.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {           
            return View();
        }

        public ActionResult Pricing()
        {
            return View();
        }

        public ActionResult BookLesson()
        {
            return View();
        }


        public ActionResult Contact()
        {            
            return View();
        }
    }
}