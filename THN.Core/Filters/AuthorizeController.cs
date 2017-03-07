using System.Web.Mvc;

namespace THN.Core.Filters
{
    [AuthorizeFilter]
    public class AuthorizeController : Controller
    {
        protected string ErrorMsg { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
