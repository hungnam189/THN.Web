using System.Web.Mvc;
using THN.Core.Models;

namespace THN.Core.Filters
{
    [AuthorizeFilter]
    public class AuthorizeController : Controller
    {
        protected string ErrorMsg { get; set; }
        //public AuthorizeController()
        //{
        //    string actionName = Request.RequestContext.RouteData.Values["action"].ToString();
        //    string controllerName = Request.RequestContext.RouteData.Values["controller"].ToString();
            
        //}
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
