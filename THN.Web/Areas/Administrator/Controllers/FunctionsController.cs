using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THN.Core.DAL;
using THN.Core.EntityFramework;
using THN.Core.Filters;
using THN.Core.Models;

namespace THN.Web.Areas.Administrator.Controllers
{
    public class FunctionsController : AuthorizeController
    {
        #region GET DATA Function 
        // GET: Administrator/Functions
        public ActionResult Index()
        {
            FunctionsDAL db = new FunctionsDAL();
            var lst = db.GetAll();
            return View(lst);   
        }

        public ActionResult GetHtmlMenu()
        {
            FunctionsDAL db = new FunctionsDAL();
            var lst = db.GetAll();
            return PartialView(lst);
        }

        public ActionResult GetHtmlFunction()
        {
            FunctionsDAL db = new FunctionsDAL();
            var lst = db.GetAll();
            return PartialView(lst);
        }

        #endregion GET DATA Function

        #region UserForFunction
        public ActionResult AddUserForFunction()
        {
            UserFunction model = new UserFunction();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserForFunction(FormCollection fc)
        {
            //UserFunction model = new UserFunction();
            string userID = fc["UserID"].ToString();
            int uID = 0;
            int.TryParse(userID, out uID);
            string strFunction = fc["Function"];
            var lstFunction = strFunction.Split(',').ToList();
            List<MemberModel> lst = new List<MemberModel>();
            foreach (var item in lstFunction)
            {
                MemberModel m = new MemberModel();
                m.UserID = uID;
                m.FuncID = int.Parse(item);
                m.CreatedBy = Core.Helper.ConfigHelper.User.Username;
                m.CreatedDate = DateTime.Now;
                lst.Add(m);
            }
            MemberDAL dal = new MemberDAL();
            var lstCheck = dal.GetList(uID);
            bool rs = false;
            if (lstCheck != null && lstCheck.Count > 0)
            {
                rs = dal.Update(lst, uID);
            }else
                rs = dal.Insert(lst, uID);
            if (rs)
                ViewData["SuccessMsg"] = "Thêm thành công.";
            else
                ViewData["ErrorMsg"] = "Thêm không thành công.";
            return View();
        }

        //public ActionResult EditUserForFunction(int userId)
        //{

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditUserForFunction(UserFunction update)
        //{
        //    return View(update);
        //}

        #endregion UserForFunction

        #region Create Function
        public ActionResult Create()
        {
            FunctionModel model = new FunctionModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FunctionModel add)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FunctionsDAL db = new FunctionsDAL();
                    Function func = new Function();
                    func.Name = add.Name;
                    func.Icon = add.Icon;
                    func.ControllerName = add.ControllerName;
                    func.ActionName = add.ActionName;
                    func.Path = add.Path;
                    func.Visibled = true;
                    func.Parent = add.Parent;
                    func.IsMenu = add.IsMenu;
                    func.Level = add.Level;
                    if (db.Insert(func))
                        return RedirectToAction("Create", "Functions");
                    ViewData["ErrorMsg"] = "Lưu không thành công!";
                }
            }catch(Exception ex)
            {
                ViewData["ErrorMsg"] = "Error : " + ex.Message;
            }
            return View(add);
        }
        #endregion Create Function

        #region Edit Function
        public ActionResult Edit(int? id)
        {
            FunctionsDAL db = new FunctionsDAL();
            var func = db.GetByID((int)id);
            return View(func);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FunctionModel update)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Function func = new Function();
                    func.ID = update.ID;
                    func.Name = update.Name;
                    func.Icon = update.Icon;
                    func.ControllerName = update.ControllerName;
                    func.ActionName = update.ActionName;
                    func.Path = update.Path;
                    func.Visibled = update.Visbled;
                    func.Parent = update.Parent;
                    func.Level = update.Level;
                    func.IsMenu = update.IsMenu;
                    FunctionsDAL db = new FunctionsDAL();
                    if (db.Update(func))
                        return RedirectToAction("Index", "Functions");
                    ViewData["ErrorMsg"] = "Lưu không thành công!";
                }
            }catch(Exception ex)
            {
                ViewData["ErrorMsg"] = "Error :" + ex.Message;
            }
            return View(update);
        }
        #endregion Edit Function

        #region Delete Function
        public JsonResult Delete(int? id)
        {
            FunctionsDAL db = new FunctionsDAL();
            var lst = db.GetByParent((int)id);
            if (lst != null && lst.Count > 0)
                return Json(new { status = false, message = "Xoá không thành công. Tồn tại menu con!" }, JsonRequestBehavior.AllowGet);
            if (db.Delete((int)id))
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { status = false, message = "Xoá không thành công!" }, JsonRequestBehavior.AllowGet);
        }

        #endregion Delete Function
    }
}