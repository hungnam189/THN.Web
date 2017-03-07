using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THN.Core.EntityFramework;
using THN.Core.Interface;
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
        public bool GetAccess(string controller, string action)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var function = db.Functions.Where(f => f.ControllerName == controller && f.ActionName == action).ToList();
                if (function != null && function.Count > 0)
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
