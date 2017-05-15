using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace THN.Libraries.Utility
{
    public class Utility
    {
        /// <summary>
        /// Actor : NamTH22 ; Created Date : 14/02/2017 
        /// Get IP Address Client
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            String ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            try
            {
                if (string.IsNullOrEmpty(ip))
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                else
                    ip = ip.Split(',')[0];
            }
            catch (Exception)
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            return ip;
        }


        /// <summary>
        /// Lay MAC Address
        /// </summary>
        /// <returns></returns>
        public static string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }


        /// <summary>
        /// random chuỗi
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            try
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                string strReturn = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
                return strReturn;
            }
            catch
            {
                return "THN@030290";
            }
        }

        /// <summary>
        /// Chuyển title thành Slug
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string GetAlias(string title)
        {
            string value = title.Trim();
            string st = "([!@#$%^&*()_+ =~`<>,.?/\":;'{}])";
            Regex v_reg_regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string v_str_FormD = value.Normalize(NormalizationForm.FormD);

            v_str_FormD = Regex.Replace(v_str_FormD, st, "-", RegexOptions.None);
            v_str_FormD = v_str_FormD.Replace("--", "-");
            v_str_FormD = v_str_FormD.Replace("---", "-");
            v_str_FormD = v_str_FormD.Replace("----", "-");
            v_str_FormD = v_str_FormD.Replace("-----", "-");
            v_str_FormD = v_str_FormD.Replace("------", "-");
            v_str_FormD = v_str_FormD.Replace("-------", "-");
            v_str_FormD = v_str_FormD.Replace("\\", "-");
            v_str_FormD = v_str_FormD.Replace("|", "-");

            value = v_reg_regex.Replace(v_str_FormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
            return value;
        }
    }
}
