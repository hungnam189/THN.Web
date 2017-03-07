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
    public class AccountDAL : BaseDAL, IFAccount
    {
        private THN_WebApplicationEntities db;
        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByUsernameOrEmail(string uInput)
        {
            throw new NotImplementedException();
        }

        public string GetSeretKey(string usernameoremail)
        {
            User u = new User();

            return "";
        }

        public int Insert(User add)
        {
            //1 : true; 0: false; -1: Exception; 2: Đã tồn tại
            try
            {
                db = new THN_WebApplicationEntities();
                var check = db.Users.Where(u => u.Username == add.Username || u.Email == add.Email).ToList();
                if (check.Count > 0)
                    return 2;
                db.Users.Add(add);
                if(db.SaveChanges() > 0)
                    return 1;
                return 0;
            }catch(Exception ex)
            {
                WriteLog(ex);
                return -1;
            }
        }

        /// <summary>
        /// Login Website
        /// </summary>
        /// <param name="username">username or email</param>
        /// <param name="password">password login</param>
        /// <param name="login">AccountModel</param>
        /// <returns></returns>
        public bool Login(string username, string password, out AccountModel login)
        {
            try
            {
                User user = new User();
                db = new THN_WebApplicationEntities();
                AccountModel model = new AccountModel();
                if (ExtEmail.IsEmail(username))
                    user = db.Users.Where(u => u.Email == username).FirstOrDefault();
                else
                    user = db.Users.Where(u => u.Username.Contains(username)).FirstOrDefault();
                if (user == null)
                {
                    login = model;
                    return false;
                }
                else
                {
                    string seretKey = user.SeretKey;
                    string pssword = Securities.EncryptPassword(password + seretKey);
                    if (user.UPassword.Contains(pssword))
                    {
                        user.IPAddress = Utility.GetClientIP();
                        user.LastLogin = DateTime.Now;

                        db.SaveChanges();

                        model.ID = (int)user.ID;
                        model.Username = user.Username;
                        model.Email = user.Email;
                        model.Name = user.Name;
                        model.Address = user.Addrress;
                        model.Phone = user.Mobile;
                        model.IPAdress = user.IPAddress;
                        model.LastDateLogin = (DateTime)user.LastLogin;
                    }
                    login = model;
                }
                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                login = new AccountModel();
                return false;
            }
        }

        public void LoginUpdate(string ipaddress, string computername, DateTime datelogin)
        {
            throw new NotImplementedException();
        }

        public int Update(User update)
        {
            throw new NotImplementedException();
        }
    }
}
