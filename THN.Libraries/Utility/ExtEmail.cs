using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace THN.Libraries.Utility
{
    public class ExtEmail
    {
        public static bool IsEmail(string email)
        {
            var boolEmail = new EmailAddressAttribute();
            return boolEmail.IsValid(email);
        }
    }
}
