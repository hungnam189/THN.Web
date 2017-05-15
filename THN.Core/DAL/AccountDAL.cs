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
        
        /// <summary>
        /// Get all User
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            try
            {
                db = new THN_WebApplicationEntities();
                return db.Users.ToList();
            }catch(Exception ex)
            {
                WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public User GetByID(int id)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                User model = new User();
                model = db.Users.Find(id);
                return model;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Get User By Username or Email
        /// </summary>
        /// <param name="uInput"></param>
        /// <returns></returns>
        public User GetByUsernameOrEmail(string uInput)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                User model = new User();
                if (EmailHelper.IsEmail(uInput))
                    model = db.Users.Where(u => u.Email.Contains(uInput)).FirstOrDefault();
                else
                    model = db.Users.Where(u => u.Username.Contains(uInput)).FirstOrDefault();
                return model;
            }
            catch(Exception ex)
            {
                WriteLog(ex);
                return null;
            }
        }

        /// <summary>
        /// Lấy SeretKey for Username
        /// </summary>
        /// <param name="usernameoremail"></param>
        /// <returns></returns>
        public string GetSeretKey(string usernameoremail)
        {
            User model = new User();
            db = new THN_WebApplicationEntities();
            if (EmailHelper.IsEmail(usernameoremail) == true)
                model = db.Users.Where(u => u.Email.Contains(usernameoremail)).FirstOrDefault();
            else
                model = db.Users.Where(u => u.Username.Contains(usernameoremail)).FirstOrDefault();
            
            return model.SeretKey;
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
                if (EmailHelper.IsEmail(username))
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

                if(login != null && !string.IsNullOrEmpty(login.Username) && login.ID > 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                login = new AccountModel();
                return false;
            }
        }

        /// <summary>
        /// update when user login
        /// </summary>
        /// <param name="ipaddress"></param>
        /// <param name="computername"></param>
        /// <param name="datelogin"></param>
        public void LoginUpdate(string ipaddress, string computername, DateTime datelogin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update Account
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public int Update(User update)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// xoá nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Delete(int id)
        {
            try
            {
                db = new THN_WebApplicationEntities();
                var model = db.Users.Find(id);
                if (model == null)
                    return -1;
                db.Users.Remove(model);
                if (db.SaveChanges() > 0)
                    return 1;
                return 0;

            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return -1;
            }
        }

        /// <summary>
        /// Insert new User
        /// </summary>
        /// <param name="add"></param>
        /// <returns></returns>
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
                if (db.SaveChanges() > 0)
                    return 1;
                return 0;
            }
            catch (Exception ex)
            {
                WriteLog(ex);
                return -1;
            }
        }
    }

}
