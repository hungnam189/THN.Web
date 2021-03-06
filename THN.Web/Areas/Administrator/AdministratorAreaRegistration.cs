﻿using System.Web.Mvc;

namespace THN.Web.Areas.Administrator
{
    public class AdministratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administrator_default",
                "Administrator/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "THN.Web.Areas.Administrator.Controllers" }
            );
            context.MapRoute(
                "Admin",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "THN.Web.Areas.Administrator.Controllers" }
            );
        }
    }
}