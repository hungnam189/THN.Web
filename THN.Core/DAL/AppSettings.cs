using System;
using System.Linq;
using THN.Core.EntityFramework;
using THN.Libraries.Utility;

namespace THN.Core.DAL
{
    public class AppSettingDAL
    {
        public static AppSetting GetSingle()
        {
            try
            {
                THN_WebApplicationEntities db = new THN_WebApplicationEntities();
                var model = db.AppSettings.Take(1).FirstOrDefault();
                return model;
            }catch(Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return null;
            }
        }
    }
}
