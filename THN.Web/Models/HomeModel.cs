using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using THN.Core.EntityFramework;
namespace THN.Web.Models
{
    public class HomeModel
    {
        public static AppSetting GetData()
        {
            try
            {
                THN_WebApplicationEntities db = new THN_WebApplicationEntities();
                db.Database.Connection.Open();
                var appsettings = db.AppSettings.Take(1).FirstOrDefault();
                return appsettings;
            }catch(Exception ex)
            {
                Libraries.Utility.WriteLogs.WriteToLogFile(ex);
                return null;
            } 
        }
    }
}