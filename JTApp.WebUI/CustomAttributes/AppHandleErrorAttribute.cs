using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JTApp.WebUI.CustomAttributes
{
    public class AppHandleErrorAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.Result = new RedirectResult("/Errors/ShowError?Msg=" + ex.Message);
        }
    }
}