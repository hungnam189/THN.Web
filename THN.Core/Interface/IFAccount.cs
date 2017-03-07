using System;
using System.Collections.Generic;
using THN.Core.EntityFramework;
using THN.Core.Models;

namespace THN.Core.Interface
{
    public interface IFAccount
    {
        bool Login(string username, string password, out AccountModel model);
        void LoginUpdate(string ipaddress, string computername, DateTime datelogin);
        List<User> GetAll();
        User GetByID(int id);
        User GetByUsernameOrEmail(string uInput);
        string GetSeretKey(string usernameoremail);
        int Insert(User add);
        int Update(User update);
        int Delete(int id);
    }
}
