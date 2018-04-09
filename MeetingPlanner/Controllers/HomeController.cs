﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MeetingPlanner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sacrament Meeting Planner";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Email";

            return View();
        }
    }
}