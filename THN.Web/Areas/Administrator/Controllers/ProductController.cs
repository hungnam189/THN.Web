using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THN.Core.Filters;

namespace THN.Web.Areas.Administrator.Controllers
{
    public class ProductController : AuthorizeController
    {
        // GET: Administrator/Product
        public ActionResult Index()
        {
            return View();
        }
    }
}