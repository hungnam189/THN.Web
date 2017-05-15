using System.Collections.Generic;
using System.Web.Mvc;
using THN.Core.Filters;
using THN.Core.DAL;
using THN.Core.Models;
using System;
using Newtonsoft.Json;

namespace THN.Web.Areas.Administrator.Controllers
{
    public class CategoryController : AuthorizeController
    {
        #region Get View 
        // GET: Administrator/Category
        public ActionResult Index()
        {
            var CategoryDAL = new CategoryDAL();
            var lstCategories = CategoryDAL.getAll();
            return View(lstCategories);
        }

        /// <summary>
        /// Return View Combobox
        /// </summary>
        /// <returns></returns>
        public PartialViewResult getViewDropListCate()
        {
            var CategoryDAL = new CategoryDAL();
            var lstCategories = CategoryDAL.getAll();
            return PartialView(lstCategories);
        }
        #endregion Get View 

        #region Insert
        // GET: Administrator/Product/Create
        public ActionResult Create()
        {
            CategoryModel model = new CategoryModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryModel add)
        {
            if(ModelState.IsValid)
            {
                CategoryDAL db = new CategoryDAL();
                if(db.insert(add) == 1)
                {
                    TempData["SuccessMsg"] = "Thêm mới thành công!";
                    return RedirectToAction("Index");
                }
                TempData["ErrorMsg"] = "Thêm mới không thành công!";
            }
            return View(add);
        }

        #endregion Insert

        #region Edit
        public ActionResult Edit(int? id)
        {
            if (id <= 0)
                return HttpNotFound();
            CategoryDAL db = new CategoryDAL();
            var model = db.getById((int)id);
            if(model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryModel update)
        {
            if (ModelState.IsValid)
            {
                CategoryDAL db = new CategoryDAL();
                if (db.update(update) == 1)
                {
                    TempData["SuccessMsg"] = "Cập nhật thành công!";
                    return RedirectToAction("Index");
                }
                TempData["ErrorMsg"] = "Cập nhật không thành công!";
            }
            return View(update);
        }
        #endregion Edit

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="cateId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string cateId)
        {
            if (string.IsNullOrEmpty(cateId))
                return Json(false, JsonRequestBehavior.AllowGet);
            int id = 0;
            int.TryParse(cateId, out id);
            if(id > 0)
            {
                CategoryDAL db = new CategoryDAL();
                if (db.delete(id) == 1)
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion Delete

        #region Orther Function
        public PartialViewResult PartialForm(CategoryModel model)
        {
            return PartialView(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult ChageVisibled(string cateId, string cateVisibled)
        {
            if(string.IsNullOrEmpty(cateId) || string.IsNullOrEmpty(cateVisibled))
                return Json(false, JsonRequestBehavior.AllowGet);
            CategoryDAL db = new CategoryDAL();
            int id = -1, visibled = -1;
            int.TryParse(cateId, out id); int.TryParse(cateVisibled, out visibled);
            if(id > 0 && visibled != -1)
            {
                if (db.changeVisibled(id, visibled) == 1)
                    return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeOrderBy(string json)
        {
            try
            {
                List<CategoryChangeOrderByModel> lst = new List<CategoryChangeOrderByModel>();
                lst = JsonConvert.DeserializeObject<List<CategoryChangeOrderByModel>>(json);
                CategoryDAL db = new CategoryDAL();
                if(db.changeOrderBy(lst) == 1)
                    return Json(new { status = true, message = "Success" }, JsonRequestBehavior.AllowGet);
                return Json(new { status = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion Orther Function
    }
}