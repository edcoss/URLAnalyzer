using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using log4net;
using System.IO;
using System.Reflection;
using System.Web.Http.ExceptionHandling;
using System.Web;

namespace URLAnalyzer.Foundation.ExceptionHandling
{
    public class ExceptionManagerApi : ExceptionLogger
    {
        ILog logger = null;
        public ExceptionManagerApi()
        { 
            var log4NetConfigFilePath = HttpContext.Current.Server.MapPath("~/log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));
        }
        public override void Log(ExceptionLoggerContext context)
        {
            logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Error(Environment.NewLine + " Excetion Time: " + System.DateTime.Now + Environment.NewLine  
                + " Exception File Path Controller: " + context.ExceptionContext.ControllerContext.Controller.ToString() + Environment.NewLine
                + " Exception File Path Action: " + (context.ExceptionContext.ControllerContext.RouteData.Values.Count > 0 ? context.ExceptionContext.ControllerContext.RouteData.Values["action"] : "N/A") + Environment.NewLine
                + " Exception Message: " + context.Exception.Message.ToString() + Environment.NewLine
                + " Exception Stacktrace: " + context.Exception.StackTrace); 
        }
        public void Log(string ex)
        {
            logger = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            logger.Error(ex);
        }

    }
}
