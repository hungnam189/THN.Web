using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using THN.Core.Models;

namespace THN.Core.Helper
{
    public class ConfigHelper
    {
        public static AccountModel User
        {
            get
            {
                AccountModel model = new AccountModel();
                if(HttpContext.Current.Session["THNLogin"] != null)
                    model = (AccountModel)HttpContext.Current.Session["THNLogin"];
                return model;
            }
        }
    }
}
