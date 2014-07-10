using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace DayCarePL
{
    public partial class Logger
    {
        public static void Write(LogType eLogType, ModuleToLog eModule, string strMethodName, string LogMessage, string strUserId)
        {

             Configuration myConfiguration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
           string LogFilePath = myConfiguration.FilePath.ToLower().Remove(myConfiguration.FilePath.ToLower().IndexOf("web.config")) + "\\Logs";

            //string LogFilePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/Logs");


            //Create directory for Year/Month/
            LogFilePath = string.Format("{0}/{1}/{2}", LogFilePath, DateTime.Now.Year.ToString(), DateTime.Now.ToString("MMM"));
            DirectoryInfo ObjDirectoryInfo = new DirectoryInfo(LogFilePath);
            //string strLogFile = string.Format("{0}/{1}.log", LogFilePath, DateTime.Now.ToString("dd-MMM-yyyy"));

            string strLogFile = string.Format("{0}/{1}.log", LogFilePath, strUserId + "_" + DateTime.Now.ToString("ddMMMyyyy"));
            try
            {
                if (ObjDirectoryInfo.Exists == false)
                {
                    ObjDirectoryInfo.Create();
                }

                StreamWriter sw = new StreamWriter(strLogFile, true);

                sw.WriteLine(string.Format("[{0}][{1}][{2}][{3}][{4}] {5}", DateTime.Now.ToString(), eLogType.ToString(), eModule.ToString(), strMethodName, strUserId, LogMessage));
                sw.Close();
                sw.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

    }

}
