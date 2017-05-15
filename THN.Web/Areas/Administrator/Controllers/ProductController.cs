using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THN.Core.DAL;
using THN.Core.Filters;
using THN.Core.Models;

namespace THN.Web.Areas.Administrator.Controllers
{
    public class ProductController : AuthorizeController
    {
        #region Select data
        // GET: Administrator/Product
        public ActionResult Index()
        {
            ProductDAL db = new ProductDAL();
            var lst = db.GetAll();
            return View(lst);
        }

        public ActionResult Detail(int? id)
        {
            return View();
        }

        public PartialViewResult ProductForm(ProductModel model)
        {
            return PartialView(model);
        }
        #endregion Select data

        #region Create
        public ActionResult Create()
        {
            ProductModel model = new ProductModel();
            ProductDAL db = new ProductDAL();
            model.Visibled = true;
            model.OrderBy = db.GetAll().Count;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductModel add)
        {
            if(ModelState.IsValid)
            {
                ProductDAL db = new ProductDAL();
                if(db.Insert(add) == 1)
                {
                    TempData["SuccessMsg"] = "Thêm mới sản phẩm thành công!";
                    return RedirectToAction("Index");
                }
            }
            TempData["ErrorMsg"] = "Thêm sản phẩm không thành công!";
            return View(add);
        }
        #endregion Create

        #region Edit
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel update)
        {
            if(ModelState.IsValid)
            {
                ProductDAL db = new ProductDAL();
                if(db.Update(update) == 1)
                {
                    TempData["SuccessMsg"] = "Cập nhật " + update.Name + " sản phẩm thành công!";
                    return RedirectToAction("Index");
                }
            }
            TempData["ErrorMsg"] = "Cập nhật sản phẩm không thành công!";
            return View(update);
        }
        #endregion Edit

        #region Delete & Orther Function

        [HttpPost]
        public JsonResult Delete(string pId)
        {
            if (string.IsNullOrEmpty(pId))
                return Json(false, JsonRequestBehavior.AllowGet);
            int productID = 0; int.TryParse(pId, out productID);
            if(productID <= 0)
                return Json(false, JsonRequestBehavior.AllowGet);
            ProductDAL db = new ProductDAL();
            if (db.Delete(productID) == 1)
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Change Status Display
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="visibled"></param>
        /// <returns></returns>
        public JsonResult ChageVisibled(string pId, string visibled)
        {
            if (string.IsNullOrEmpty(pId) || string.IsNullOrEmpty(visibled))
                return Json(new { status = false, message = "Input is wrong" }, JsonRequestBehavior.AllowGet);
            int productId = 0, productVisibled = 0;
            int.TryParse(pId, out productId); int.TryParse(visibled, out productVisibled);
            if(productId <= 0)
                return Json(new { status = false, message = "Not found." }, JsonRequestBehavior.AllowGet);
            ProductDAL db = new ProductDAL();
            if (db.ChangeVisibled(productId, productVisibled) == 1)
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangeStatus(string pId, string pStatus, string pAction)
        {
            if (string.IsNullOrEmpty(pId) || string.IsNullOrEmpty(pStatus))
                return Json(new { status = false, message = "Input is wrong" }, JsonRequestBehavior.AllowGet);
            int productId = 0, productStatus = 0;
            int.TryParse(pId, out productId); int.TryParse(pStatus, out productStatus);
            if (productId <= 0)
                return Json(new { status = false, message = "Not found." }, JsonRequestBehavior.AllowGet);
            ProductDAL db = new ProductDAL();
            int executeStatus = 0;
            if (pAction == "isnew")
                executeStatus = db.ChangeIsNew(productId, productStatus);
            else if (pAction == "ishome")
                executeStatus = db.ChangeIsHome(productId, productStatus);
            else if (pAction == "ishot")
                executeStatus = db.ChangeIsHot(productId, productStatus);
            if (executeStatus == 1)
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        }

        #endregion Delete & Orther Function

        #region Upload Product Images
        public ActionResult UploadImages(int? ProductID)
        {
            ViewBag.ProductID = ProductID;
            ProductDAL db = new ProductDAL();
            var product = db.GetByID((int)ProductID);
            if (product == null)
                return RedirectToAction("Edit", "Product", new { ID = ProductID });
            return View();
        }

        [HttpPost]
        public ActionResult UploadImages()
        {

            if (Request.Files.Count > 0)
            {
                int ProductID = 0;
                int.TryParse(Request.Form.GetValues("ProductID")[0], out ProductID);
                string[] lstImageTitle = Request.Form.GetValues("ImgTitle");
                string[] lstImageAlt = Request.Form.GetValues("AltImage");
                string[] lstOrderBy = Request.Form.GetValues("OrderBy");

                ProductDAL db = new ProductDAL();
                if(db.UploadImages(ProductID, lstImageTitle, lstImageTitle, lstOrderBy) == 1)
                    return RedirectToAction("Edit", "Product", new { ID = ProductID });
            }
            return View();
        }

        #endregion Upload Product Images

    }
}