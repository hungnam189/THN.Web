using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THN.Core.Helper
{
    public enum ValidateConfig
    {
        Required= 1,
        StringLength = 2,
        Range =3,
        MinLength = 4,
        Email = 5
    }
    public class ValidateMsgHelper
    {
        /// <summary>
        /// Defines a require field
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string getRequired(string name)
        {
            return name + " không được để rỗng";
        }

        /// <summary>
        /// Defines a maximum length for a string field
        /// </summary>
        /// <param name="name"></param>
        /// <param name="strLength"></param>
        /// <returns></returns>
        public static string getStrigLength(string name, int strLength)
        {
            return name + " tối đa " + strLength + " kí tự.";
        }

        /// <summary>
        /// Gives a maximum and minimum value for a numeric field
        /// </summary>
        /// <param name="name"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static string getRange(string name, object minimum, object maximum)
        {
            return name + " trong khoảng " + minimum + " đến " + maximum;
        }

        public static string getMinLength(string name, int minLength)
        {
            return name + " ít nhất " + minLength + " kí tự.";
        }

        public static string getEmail(string name)
        {
            return name + " không phải định dạng Email.";
        }

        //public static string getMsg(,string name)

    }
}
