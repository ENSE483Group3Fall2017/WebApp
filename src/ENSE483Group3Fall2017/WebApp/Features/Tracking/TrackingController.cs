using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Features.Tracking
{
    public class TrackingController : Controller
    {
        public ActionResult Index(string id)
        {
            return View();
        }
        
        public ActionResult Details(Guid id)
        {
            return View();
        }


    }
}