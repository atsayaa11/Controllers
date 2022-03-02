using OnlineMobiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMobiles.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        MobilesContext db = new MobilesContext();
        public ActionResult Home()
        {
            return View(db.Products.ToList());
        }
    }
}