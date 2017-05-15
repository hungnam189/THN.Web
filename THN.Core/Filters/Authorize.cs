using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using THN.Core.DAL;
using THN.Core.Helper;
using THN.Core.Models;
using THN.Libraries.Utility;

namespace THN.Core.Filters
{
    public class AuthorizeFilter : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        status = "401"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                //xhr status code 401 to redirect
                filterContext.HttpContext.Response.StatusCode = 401;
            }
            else
                base.HandleUnauthorizedRequest(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            try
            {
                //string urlLogin = WebConfigurationManager.ConnectionStrings["webLogin"] != null ? WebConfigurationManager.ConnectionStrings["webLogin"].ConnectionString : "";
                string url = httpContext.Request.Url.ToString();
                //urlLogin = urlLogin + HttpUtility.UrlEncode(url);

                var db = new THN.Core.EntityFramework.THN_WebApplicationEntities();
                string actionName = httpContext.Request.RequestContext.RouteData.Values["action"].ToString();
                string controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

                FunctionSessionModel sessionModel = new FunctionSessionModel();
                sessionModel.Controller = controllerName;
                sessionModel.Action = actionName;
                httpContext.Session["THNMenu"] = sessionModel;


                //Kiểm tra Session Login
                AccountModel model = (AccountModel)httpContext.Session["THNLogin"];
                if (model == null || string.IsNullOrEmpty(model.Username))
                {
                    if (httpContext.Request.IsAjaxRequest())
                    {
                        httpContext.Response.Redirect("/Administrator/Error/JsonAccessFail");
                        return false;
                    }
                    httpContext.Response.Redirect("/Administrator/Account/Login?ReturnUrl=" + HttpUtility.UrlEncode(url));
                    return false;
                }

                ////Kiểm tra quyền truy cập
                //FunctionsDAL funcDAL = new FunctionsDAL();
                //bool access = funcDAL.GetAccess(controllerName, actionName, ConfigHelper.User.ID);
                //if (access == false)
                //    httpContext.Response.Redirect("/Administrator/Error/AccessFail");
                //return access;
                return true;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return false;
            }
        }
    }
}
