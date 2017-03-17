using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Config General
/// </summary>
public class Config
{
    /// <summary>
    /// Get Url : http://example.com or https://example.com
    /// </summary>
    /// <returns></returns>
    public static string getUrlHost()
    {
        try
        {
            string urlReturn = "";
            HttpContext context = HttpContext.Current;
            string host = context.Request.Url.Host;
            if (context.Request.IsSecureConnection)
                urlReturn = "https://" + host;
            else
                urlReturn = "http://" + host;
            return urlReturn;
        }
        catch
        {
            return "";
        }
    }

    /// <summary>
    /// Get Full Url
    /// </summary>
    /// <param name="link"></param>
    /// <returns></returns>
    public static string fulllUrl(string link)
    {
        string urlReturn = "";
        HttpContext context = HttpContext.Current;
        string host = context.Request.Url.Host;
        if (context.Request.IsSecureConnection)
            urlReturn = "https://" + host;
        else
            urlReturn = "http://" + host;
        urlReturn += urlReturn + link;
        return urlReturn;
    }
}