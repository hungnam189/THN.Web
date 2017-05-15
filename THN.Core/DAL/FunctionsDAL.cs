using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THN.Core.EntityFramework;
using THN.Core.Helper;
using THN.Core.Interface;
using THN.Core.Models;
using THN.Libraries.Utility;

namespace THN.Core.DAL
{
    public class FunctionsDAL : BaseDAL, IFFunction
    {
        private THN_WebApplicationEntities db;
        

        /// <summary>
        /// Get All Data
        /// </summary>
        /// <returns></returns>
        public List<Function> GetAll()
        {
            List<Function> lst = new List<Function>();
            try
            {
                db = new THN_WebApplicationEntities();
                lst = db.Functions.ToList();
            }catch(Exception ex)
            {
                WriteLog(ex);
            }
            return lst;
        }


        public List<FunctionViewModel> GetMenu()
        {
            try
            {
                List<FunctionViewModel> lst = new List<FunctionViewModel>();
                db = new THN_WebApplicationEntities();
                //Lấy danh sách menu gốc parent = 0
                //var lstParent = db.Functions.Where(f => f.Parent == 0 && f.Level == 1 && f.IsMenu == false).ToList();
                var lstFunction = (from m in db.Members
                           join f in db.Functions on m.FuncID equals f.ID
                           where m.UserID == ConfigHelper.User.ID // && f.Parent == 0 && f.Level == 1 && f.IsMenu == false
                           select f).ToList();
                List<int> lstFuncID = lstFunction.Select(f => (int)f.Parent).Distinct().ToList();
                List<Function> lstRoot = db.Functions.Where(f => lstFuncID.Contains(f.ID)).ToList();
                
                var functionRoot = lstRoot.Where(f => f.Parent == 0 && f.Level == 1 && f.IsMenu == false).ToList();
                //Lấy menu con level 1
                foreach (var item in functionRoot)
                {
                    FunctionViewModel model = new FunctionViewModel();
                    model.Id = item.ID;
                    model.Name = item.Name;
                    model.Icon = item.Icon;
                    model.Controller = item.ControllerName;
                    model.Action = item.ActionName;
                    model.Parent = (int)item.Parent;
                    model.IsMenu = (bool)item.IsMenu;
                    model.Level = (int)item.Level;
                    model.ListChild = lstFunction.Where(f => f.Parent == item.ID && f.Level == 2).Select(f => new FunctionViewModel
                    {
                        Id = f.ID, Name = f.Name, Icon = f.Icon, Controller = f.ControllerName,
                        IsMenu = (bool)f.IsMenu, Level = (int)f.Level,
                        Action = f.ActionName, Parent = (int)f.Parent
                    }).Distinct().ToList();
                    //lstFunction.Remove(item); 
                    lst.Add(model);
                }
                return lst;
            }catch(Exception ex)
            {
                WriteLog(ex);
                return null;
            }
        }

        //public List<Function>

        /// <summary>
        /// Get Fucntion by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Function GetByID(int id)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var fucntion = db.Functions.Find(id);
                return fucntion;
            }catch(Exception ex)
            {
                WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Get list Fuction by parent
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public List<Function> GetByParent(int parent)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var lstParent = db.Functions.Where(f => f.Parent == parent).ToList();
                return lstParent;
            }catch(Exception ex)
            {
                WriteLog(ex);
                return null;
            }
            
        }


        /// <summary>
        /// Add new Fucntion
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
        public bool Insert(Function add)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                db.Functions.Add(add);
                if(db.SaveChanges() > 0)
                    return true;
                return false;
            }catch(Exception ex)
            {
                WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Update the function in database
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public bool Update(Function update)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var model = db.Functions.Find(update.ID);
                if (model == null)
                    return false;
                if (db.SaveChanges() > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return false;
            }
        }

        /// <summary>
        /// Delete a Function
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var model = db.Functions.Find(id);
                if (model == null)
                    return false;
                if (db.SaveChanges() > 0)
                    return true;
                return false;
            }catch(Exception ex)
            {
                WriteLog(ex);
                return false;
            }
            
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Kiểm tra quyền truy cập User
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool GetAccess(string controller, string action, int userID)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                //var function = db.Functions.Where(f => f.ControllerName == controller && f.ActionName == action).ToList();
                var lst = (from m in db.Members
                          join f in db.Functions on m.FuncID equals f.ID
                          where m.UserID == userID && f.ControllerName == controller && f.ActionName == action
                          select f).ToList();
                
                if (lst != null && lst.Count > 0)
                {
                    return true;
                }
                return false;
            }catch(Exception ex)
            {
                WriteLog(ex);
                return false;
            }
        }
    }
}
