using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace THN.Libraries.Utility
{
    public class Securities
    {
        /// <summary>
        /// Encode Password Input
        /// </summary>
        /// <param name="originalPassword">Original Password</param>
        /// <returns></returns>
        public static string EncryptPassword(string originalPassword)
        {
            try
            {
                Byte[] originalBytes;
                Byte[] encodedBytes;
                MD5 md5;
                md5 = new MD5CryptoServiceProvider();
                //originalBytes = UTF8Encoding.Default.GetBytes(originalPassword);
                originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
                encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
