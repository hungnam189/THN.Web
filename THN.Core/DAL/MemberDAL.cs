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
    public class MemberDAL : IFMember
    {
        private THN_WebApplicationEntities db;
        bool IFMember.Delete(int userID)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetList(int userID)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var lst = db.Members.Where(m => m.UserID == userID).ToList();
                return lst != null ? lst : null;
            }catch(Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return null;
            }
        }
        /// <summary>
        /// Thêm mới quyền cho nhân viên
        /// </summary>
        /// <param name="lstMember"></param>
        /// <returns></returns>
        public bool Insert(List<MemberModel> lstMember, int userID)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                
                
                if(lstMember != null && lstMember.Count > 0)
                {
                    foreach(var item in lstMember)
                    {
                        Member model = new Member();
                        model.UserID = item.UserID;
                        model.FuncID = item.FuncID;
                        model.CreateBy = ConfigHelper.User.Username;
                        model.CreateDate = DateTime.Now;
                        db.Members.Add(model);
                    }
                    if(db.SaveChanges() > 0)
                        return true;
                }
                return false;

            }catch(Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return false;
            }
        }

        public bool Update(List<MemberModel> update, int UserID)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var lst = db.Members.Where(m => m.UserID == UserID).ToList();
                db.Members.RemoveRange(lst);
                if (db.SaveChanges() > 0)
                {
                    bool rs = this.Insert(update, UserID);
                    return rs;
                }
                return false;
            }catch(Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return false;
            }
        }
    }
}
