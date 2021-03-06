﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THN.Web.Areas.Administrator.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Administrator/Error
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult JsonAccessFail()
        {
            return Json(new { status = false, message = "Bạn không có quyền truy cập." });
        }

        public ActionResult AccessFail()
        {
            return View();
        }
    }
}