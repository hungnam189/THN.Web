using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THN.Core.EntityFramework;
using THN.Core.Interface;
using THN.Core.Models;
using THN.Libraries.Utility;

namespace THN.Core.DAL
{
    public class MemberDAL : IFMember
    {
        bool IFMember.Delete(int userID)
        {
            throw new NotImplementedException();
        }

        List<Member> IFMember.GetList(int userID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(List<MemberModel> lstMember)
        {
            try
            {
                if(lstMember != null && lstMember.Count > 0)
                {
                    foreach(var item in lstMember)
                    {
                        Member model = new Member();
                        model.UserID = item.UserID;
                        model.FuncID = item.FuncID;
                        model.CreateBy = 
                    }
                }
                return false;

            }catch(Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return false;
            }
        }

        bool IFMember.Update(MemberModel update)
        {
            throw new NotImplementedException();
        }
    }
}
