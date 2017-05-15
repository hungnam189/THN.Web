using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using THN.Core.EntityFramework;
using THN.Core.Filters;
using THN.Core.Interface;
using THN.Core.Models;
using THN.Libraries.Utility;

namespace THN.Core.DAL
{
    public class ProductDAL : IFProduct
    {
        private THN_WebApplicationEntities db;

        /// <summary>
        /// Get All Product
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAll()
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var lst = db.Products.OrderBy(p => p.OrderBy).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
            }
            return null;
        }
        /// <summary>
        /// Get Product by category id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public List<Product> GetByCategoryID(int categoryId)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                return db.Products.Where(p => p.CategoryID == categoryId).OrderBy(p => p.OrderBy).ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Product by product name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Product> GetByName(string name)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                return db.Products.Where(p => p.Name.Contains(name)).OrderBy(p => p.OrderBy).ToList();
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Product by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetByID(int id)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                return db.Products.Find(id);
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return null;
            }

        }

        /// <summary>
        /// Add new Product
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public int Insert(ProductModel add)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                Product model = new Product();
                model.Name = add.Name;
                model.Slug = !string.IsNullOrEmpty(add.Slug) ? add.Slug : Utility.GetAlias(add.Name);
                model.ShortDescription = add.ShortDescription;
                model.Detail = add.Detail;
                model.Visibled = add.Visibled == true ? 1 : 0;
                model.IsNew = add.IsNew;
                model.IsHome = add.IsHome;
                model.IsHot = add.IsHot;
                model.OrderBy = add.OrderBy;
                model.CategoryID = add.CategoryID;
                model.MenufactureID = 1;
                model.MetaTitle = add.MetaTitle;
                model.MetaKeyword = add.MetaKeyword;
                model.MetaDescription = add.MetaDescription;
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = AccountLogin.User.Username;
                model.PriceOld = add.PriceOld;
                model.PricceNew = add.PriceNew;
                string folderSave = "~/Images/product/" + DateTime.Now.ToString("MMyyyy");
                model.Picture = FileHelper.UploadImages(folderSave, model.Slug)[0].ToString();
                db.Products.Add(model);
                if (db.SaveChanges() > 0)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }

        }

        /// <summary>
        /// Edit product by ID
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public int Update(ProductModel update)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                Product model = db.Products.Find(update.Id);
                model.Name = update.Name;
                model.Slug = !string.IsNullOrEmpty(update.Slug) ? update.Slug : Utility.GetAlias(update.Name);
                model.ShortDescription = update.ShortDescription;
                model.Detail = update.Detail;
                model.Visibled = update.Visibled == true ? 1 : 0;
                model.IsNew = update.IsNew;
                model.IsHome = update.IsHome;
                model.IsHot = update.IsHot;
                model.OrderBy = update.OrderBy;
                model.CategoryID = update.CategoryID;
                model.MenufactureID = 1;
                model.MetaTitle = update.MetaTitle;
                model.MetaKeyword = update.MetaKeyword;
                model.MetaDescription = update.MetaDescription;
                model.PriceOld = update.PriceOld;
                model.PricceNew = update.PriceNew;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = AccountLogin.User.Username;
                if (db.SaveChanges() > 0)
                {
                    if (HttpContext.Current.Request.Files.Count > 0)
                    {
                        string oldImage = model.Picture;
                        DateTime timeSave = (DateTime)model.CreatedDate;
                        string folderSave = "~/Images/product/" + timeSave.ToString("MMyyyy");
                        model.Picture = FileHelper.UploadImage(folderSave, model.Slug);
                        FileHelper.delete(folderSave, oldImage);
                    }
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }
        }

        /// <summary>
        /// Delete a Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var produdct = db.Products.Find(id);
                if (produdct == null || produdct.ID <= 0)
                    return 0;
                var lstImages = db.ProductImages.Where(p => p.ProductID == produdct.ID).ToList();
                if (lstImages.Count > 0)
                {
                    string folder = "P" + id;
                    db.ProductImages.RemoveRange(lstImages);
                    foreach (var img in lstImages)
                    {
                        string path = "~/Images/product/" + folder + "/" + img.FileSave;
                        FileHelper.delete(path);
                    }
                }

                db.Products.Remove(produdct);
                if (db.SaveChanges() > 0)
                {
                    FileHelper.delete("~/Images/product/thump/" + produdct.Picture);
                    return 1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }

        }

        /// <summary>
        /// Get Count View detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int CountView(int id)
        {
            return 1;
        }

        /// <summary>
        /// Update When a people view detail product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateDetailView(int id)
        {
            return 1;
        }

        /// <summary>
        /// Change display
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="visibled"></param>
        /// <returns></returns>
        public int ChangeVisibled(int pId, int visibled)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var product = db.Products.Find(pId);
                if (product == null || product.ID <= 0)
                    return 0;
                product.Visibled = visibled;
                if (db.SaveChanges() > 0)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }
        }

        public int ChangeIsNew(int pId, int isnew)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var product = db.Products.Find(pId);
                if (product == null || product.ID <= 0)
                    return 0;
                product.IsNew = isnew == 1 ? true : false;
                if (db.SaveChanges() > 0)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }
        }

        public int ChangeIsHot(int pId, int ishot)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var product = db.Products.Find(pId);
                if (product == null || product.ID <= 0)
                    return 0;
                product.IsHot = ishot == 1 ? true : false;
                if (db.SaveChanges() > 0)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }
        }

        /// <summary>
        /// Change status home
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int ChangeIsHome(int pId, int status)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var product = db.Products.Find(pId);
                if (product == null || product.ID <= 0)
                    return 0;
                product.IsHome = status == 1 ? true : false;
                if (db.SaveChanges() > 0)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }
        }


        public int UploadImages(int ProductID, string[] listImageTitle, string[] listImageAlt, string[] lstOrderBy)
        {
            try
            {

                db = new THN_WebApplicationEntities();
                var product = GetByID(ProductID);
                if (product == null || product.ID <= 0)
                    return 0;
                string folder = "~/Images/product/" + "P" + product.ID;
                List<string> lstImages = FileHelper.UploadImages(folder, product.Slug);
                ProductImage pm;
                if (lstImages.Count > 0)
                {
                    var i = 0;
                    foreach (var str in lstImages)
                    {
                        pm = new ProductImage();
                        pm.Title = !string.IsNullOrEmpty(listImageTitle[i]) ? listImageTitle[i] : product.Name;
                        pm.AltImages = !string.IsNullOrEmpty(listImageAlt[i]) ? listImageAlt[i] : product.MetaTitle;
                        pm.OrderBy = !string.IsNullOrEmpty(lstOrderBy[i]) ? int.Parse(lstOrderBy[i]) : (i + 1);
                        pm.FileSave = str;
                        pm.ProductID = product.ID;
                        db.ProductImages.Add(pm);
                        i = i + 1;
                    }
                }
                if (db.SaveChanges() > 0)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }

        }

    }
}
