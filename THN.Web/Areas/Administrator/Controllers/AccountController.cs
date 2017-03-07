using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THN.Core.DAL;
using THN.Core.EntityFramework;
using THN.Core.Filters;
using THN.Core.Models;
using THN.Libraries.Utility;

namespace THN.Web.Areas.Administrator.Controllers
{
    public class AccountController : Controller
    {
        private AccountDAL db;
        //[AuthorizeFilter]
        // GET: Administrator/Account
        public ActionResult Index()
        {
            return View();
        }

        #region Insert Account
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountModelView add)
        {
            if(ModelState.IsValid)
            {
                db = new AccountDAL();
                User user = new User();
                user.Name = add.Name;
                user.Username = add.Username;
                user.Email = add.Email;
                user.Addrress = add.Address;
                user.Mobile = add.Phone;
                string seretKey = Utility.RandomString(8);
                user.UPassword = Securities.EncryptPassword(add.Password + seretKey);
                int rs = db.Insert(user);
                switch (rs)
                {
                    case 1: ViewData["SuccessMsg"] = "Thêm thành công!"; break;
                    case 0: ViewData["ErrorMsg"] = "Thêm không thành công!"; break;
                    case 2: ViewData["ErrorMsg"] = "Thêm không thành công, Tài khoản bị trùng!"; break;
                    default: ViewData["ErrorMsg"] = "Thêm không thành công, Có phát sinh lỗi!"; break;
                }
                string btnAction = Request.Form["btnSave"].ToString();
                if (btnAction == "SaveAndCreate")
                    return RedirectToAction("Create");
                return RedirectToAction("Index");
            }
            return View(add);
        }
        #endregion Insert Account


        #region Login Website
        //GET: Administrator/Login
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountModelLogin login, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                db = new AccountDAL();
                AccountModel model = new AccountModel();
                if (db.Login(login.UsernameOrEmail, login.Password, out model))
                {
                    Session["THNLogin"] = model;
                    if (!string.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);
                    return RedirectToAction("Index", "Home");
                }
                ViewData["ErrorMsg"] = "Login thất bại, vui lòng kiểm tra Username hoặc Password.";
            }
            return View(login);
        }
        #endregion Login Website
        
        #region Logout Website
        public ActionResult Logout()
        {
            if(Session["THNLogin"] != null)
            {
                Session["THNLogin"] = null;
                Session.Abandon();
                Session.Clear();
            }
            return RedirectToAction("Login", "Account");
        }
        #endregion
    }
}