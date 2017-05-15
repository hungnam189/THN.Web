using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace THN.Libraries.Utility
{
    public class FileHelper
    {
        #region Delete File
        /// <summary>
        /// Delete file From Server
        /// </summary>
        /// <param name="path">path file name</param>
        /// <returns>bool</returns>
        public static bool delete(string path)
        {
            string fullPath = HttpContext.Current.Request.MapPath(path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public static bool delete(string path, string fileName)
        {
            try
            {
                // Xoá file cũ
                string filePath = HttpContext.Current.Server.MapPath(path + fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion Delete File

        #region Upload Images
        public static string UploadImage(string path, string filename)
        {
            var context = HttpContext.Current;
            string strReturn = string.Empty;
            strReturn = "no_images.jpg";
            try
            {
                var file = context.Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    if (!Directory.Exists(context.Server.MapPath(path)))
                    {
                        Directory.CreateDirectory(context.Server.MapPath(path));
                    }

                    string fileType = Path.GetExtension(file.FileName);
                    if (ValidateExtension(fileType) == true)
                    {

                        string date = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                        string folder = context.Server.MapPath(path);
                        string targetPath = Path.Combine(folder, filename + "-" + date + fileType);
                        file.SaveAs(targetPath);
                        strReturn = filename + "-" + date + fileType;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
            }
            return strReturn;
        }


        public static List<string> UploadImages(string path, string filename)
        {
            var httpContext = HttpContext.Current;
            try
            {
                List<string> lst = new List<string>();
                if (httpContext.Request.Files.Count > 0)
                {
                    for (int i = 0; i < httpContext.Request.Files.Count; i++)
                    {
                        var file = httpContext.Request.Files[i];
                        string imgReturn = "";
                        if (file.ContentLength > 0)
                        {
                            if (!Directory.Exists(httpContext.Server.MapPath(path)))
                            {
                                Directory.CreateDirectory(httpContext.Server.MapPath(path));
                            }

                            string fileType = Path.GetExtension(file.FileName);
                            string fileSave = filename + "-0" + i;
                            if (ValidateExtension(fileType) == true)
                            {

                                string date = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                                string folder = httpContext.Server.MapPath(path);
                                string targetPath = Path.Combine(folder, fileSave + "-" + date + fileType);
                                file.SaveAs(targetPath);
                                imgReturn = fileSave + "-" + date + fileType;
                                lst.Add(imgReturn);
                            }
                        }
                    }
                }
                return lst;
            }
            catch (Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return null;
            }
        }

        public static bool ValidateExtension(string extension)
        {
            extension = extension.ToLower();
            switch (extension)
            {
                case ".jpg":
                    return true;
                case ".png":
                    return true;
                case ".gif":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }


        public static bool SaveResizeImage(Image img, int width, string path)
        {
            try
            {
                //Lấy chiều cao ban đầu của hình ảnh
                int originalW = img.Width;
                int originalH = img.Height;
                //kích thước resize
                //int resizeW = width;
                int resizedH = (originalH * width) / originalW;
                Bitmap b = new Bitmap(width, resizedH);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.Bicubic;    // Specify here
                g.DrawImage(img, 0, 0, width, resizedH);
                g.Dispose();
                b.Save(path);
                return true;
            }
            catch(Exception ex)
            {
                WriteLogs.WriteToLogFile(ex);
                return false;
            }
            
        }

        #endregion Upload Images
    }
}
