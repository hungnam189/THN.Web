using System.Web;
using THN.Core.Models;

namespace THN.Core.Filters
{
    public class AccountLogin
    {
        public static AccountModel User
        {
            get
            {
                AccountModel loginModel = new AccountModel();
                loginModel = (AccountModel)HttpContext.Current.Session["THNLogin"];
                return loginModel;
            }
        }
    }
}
