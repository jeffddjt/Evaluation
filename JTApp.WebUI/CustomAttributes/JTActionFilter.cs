using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JTApp.WebUI.CustomAttributes
{
    public class JTActionFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase httpContext = filterContext.HttpContext;
            if (httpContext.Session["UserInfo"] == null
                && filterContext.RouteData.Values["controller"].ToString() != "Home")
            {
                if (httpContext.Request.IsAjaxRequest())
                {
                    httpContext.Response.Headers.Add("sessionstatus", "timeout");
                    httpContext.Response.End();
                }
                else
                {
                    httpContext.Response.Write("<script>alert('登录超时，请重新登录!');window.top.location.href='/Home/Login/';</script>");
                    httpContext.Response.End();
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}