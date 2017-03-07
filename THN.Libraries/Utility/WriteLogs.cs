using System;
using System.IO;
using System.Web;

namespace THN.Libraries.Utility
{
    public class WriteLogs
    {
        /// <summary>
        /// ghi log file
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteToLogFile(Exception ex)
        {
            try
            {
                StreamWriter writer;
                string str = ex.Message + " ; " + ex.ToString();
                string str2 = string.Empty;
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("//Log//")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("//Log//"));
                }
                string path = HttpContext.Current.Server.MapPath("//Log//" + string.Format("{0}-{1}-{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year) + ".txt");
                str2 = string.Format("{0} : {1}", DateTime.Now, HttpContext.Current.Request.Path + " " + str);
                string text = "";
                if (!File.Exists(path))
                {
                    writer = new StreamWriter(path);
                }
                else
                {
                    text = System.IO.File.ReadAllText(path);
                    writer = File.AppendText(path);
                }
                writer.WriteLine(str2);
                writer.WriteLine();
                writer.Close();
            }
            catch (Exception)
            {
            }
        }


        /// <summary>
        /// ghi log file bằng chuỗi
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteToLogFile(string ex)
        {
            try
            {
                StreamWriter writer;
                string str = ex;
                string str2 = string.Empty;
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("//Log//")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("//Log//"));
                }
                string path = HttpContext.Current.Server.MapPath("//Log//" + string.Format("{0}-{1}-{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year) + ".txt");
                str2 = string.Format("{0} : {1}", DateTime.Now, HttpContext.Current.Request.Path + " " + str);
                string text = String.Empty;
                if (!File.Exists(path))
                {
                    writer = new StreamWriter(path);
                }
                else
                {
                    text = System.IO.File.ReadAllText(path);
                    writer = File.AppendText(path);
                }
                writer.WriteLine(str2);
                writer.WriteLine();
                writer.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
