using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THN.Core.Filters;

namespace THN.Web.Areas.Administrator.Controllers
{
    public class HomeController : AuthorizeController
    {
        // GET: Administrator/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}