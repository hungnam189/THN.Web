using System;

namespace THN.Core.DAL
{
    public class BaseDAL
    {
        public static void WriteLog(Exception ex)
        {
            THN.Libraries.Utility.WriteLogs.WriteToLogFile(ex);
        }
    }
}
