using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THN.Core.Interface;
using THN.Core.EntityFramework;
using THN.Core.Models;
using THN.Libraries.Utility;

namespace THN.Core.DAL
{
    public class CategoryDAL : IFCategory
    {
        private THN_WebApplicationEntities db;
        public CategoryDAL()
        {
            db = new THN_WebApplicationEntities();
        }

        /// <summary>
        /// Get All List Categories
        /// </summary>
        /// <returns></returns>
        public List<CategoryModel> getAll()
        {
            //List<CategoryModel> lst = new List<CategoryModel>();
            try
            {
                var lst = db.Categories.Select(c => new CategoryModel
                {
                    Id = c.ID,
                    Name = c.Name,
                    Slug = c.Slug,
                    Picture = c.Picture,
                    MetaTitle = c.MetaTitle,
                    MetaKeyWord = c.MetaKeyword,
                    MetaDescription = c.MetaDescription,
                    Parent = c.Parent,
                    Visibled = (int)c.Visible,
                    OrderBy = (int)c.OrderBy
                }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                Libraries.Utility.WriteLogs.WriteToLogFile(ex);
            }
            return null;
        }

        /// <summary>
        /// Get List Categories by parent id
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public List<CategoryModel> getByParent(int parent)
        {
            try
            {
                var lst = db.Categories.Where(c => c.Parent == parent).OrderByDescending(c => c.OrderBy).Select(c => new CategoryModel
                {
                    Id = c.ID,
                    Name = c.Name,
                    Slug = c.Slug,
                    Picture = c.Picture,
                    MetaTitle = c.MetaTitle,
                    MetaKeyWord = c.MetaKeyword,
                    MetaDescription = c.MetaDescription,
                    Parent = c.Parent,
                    Visibled = (int)c.Visible,
                    OrderBy = (int)c.OrderBy
                }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                Libraries.Utility.WriteLogs.WriteToLogFile(ex);
            }
            return null;
        }

        /// <summary>
        /// Get List by category name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<CategoryModel> getByCategoryName(string name)
        {
            try
            {
                var lst = db.Categories.Where(c => c.Name == name).OrderByDescending(c => c.OrderBy).Select(c => new CategoryModel
                {
                    Id = c.ID,
                    Name = c.Name,
                    Slug = c.Slug,
                    Picture = c.Picture,
                    MetaTitle = c.MetaTitle,
                    MetaKeyWord = c.MetaKeyword,
                    MetaDescription = c.MetaDescription,
                    Parent = c.Parent,
                    Visibled = (int)c.Visible,
                    OrderBy = (int)c.OrderBy
                }).ToList();
                return lst;
            }
            catch (Exception ex)
            {
                Libraries.Utility.WriteLogs.WriteToLogFile(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Category By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CategoryModel getById(int id)
        {
            try
            {
                return db.Categories.Where(c => c.ID.Equals(id)).Select(c => new CategoryModel
                {
                    Id = c.ID,
                    Name = c.Name,
                    Slug = c.Slug,
                    Picture = c.Picture,
                    MetaTitle = c.MetaTitle,
                    MetaDescription = c.MetaDescription,
                    MetaKeyWord = c.MetaKeyword,
                    Parent = c.Parent,
                    Visibled = (int)c.Visible,
                    OrderBy = (int)c.OrderBy
                }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Libraries.Utility.WriteLogs.WriteToLogFile(ex);
            }
            return null;
        }

        /// <summary>
        /// Insert new category
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public int insert(CategoryModel add)
        {
            try
            {
                Category model = new Category();
                model.Name = add.Name;
                model.Slug = !string.IsNullOrEmpty(add.Slug) ? add.Slug : Utility.GetAlias(add.Name);
                model.Parent = add.Parent;
                model.Visible = add.Visibled;
                model.OrderBy = add.OrderBy;
                model.MetaTitle = add.MetaTitle;
                model.MetaKeyword = add.MetaKeyWord;
                model.MetaDescription = add.MetaDescription;
                db.Categories.Add(model);
                if (db.SaveChanges() > 0)
                    return 1;

            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public int update(CategoryModel update)
        {
            try
            {
                var model = db.Categories.Find(update.Id);
                if (model != null)
                {
                    model.Name = update.Name;
                    model.Slug = !string.IsNullOrEmpty(update.Slug) ? update.Slug : Utility.GetAlias(update.Name);
                    model.Parent = update.Parent;
                    model.OrderBy = update.OrderBy;
                    model.Visible = update.Visibled;
                    model.MetaTitle = update.MetaTitle;
                    model.MetaKeyword = update.MetaKeyWord;
                    model.MetaDescription = update.MetaDescription;
                    if (db.SaveChanges() > 0)
                        return 1;
                    return 0;
                }
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
            }
            return -1;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delete(int id)
        {
            try
            {
                var model = db.Categories.Find(id);
                if (model == null)
                    return 0;
                db.Categories.Remove(model);
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
        /// Change Status Visibled
        /// </summary>
        /// <param name="cateId"></param>
        /// <param name="visibled"></param>
        /// <returns></returns>
        public int changeVisibled(int cateId, int visibled)
        {
            try
            {
                var model = db.Categories.Find(cateId);
                if (model != null)
                {
                    model.Visible = visibled;
                    if (db.SaveChanges() > 0)
                        return 1;
                }
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Update OrderBy
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public int changeOrderBy(List<CategoryChangeOrderByModel> lst)
        {
            try
            {
                foreach(var model in lst)
                {
                    var category = db.Categories.Find(model.CateId);
                    category.OrderBy = model.OrderBy;
                    db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                }
                if (db.SaveChanges() > 0)
                    return 1;
                return 0;
            }catch(Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return -1;
            }
        }
    }
}
