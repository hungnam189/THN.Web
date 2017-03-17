using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using THN.Core.Models;
using THN.Core.EntityFramework;

namespace THN.Core.Interface
{
    interface IFMember
    {
        bool Insert(List<MemberModel> lstMember);
        bool Update(List<MemberModel> lstMember);
        bool Delete(int userID);
        List<Member> GetList(int userID);
    }
}
