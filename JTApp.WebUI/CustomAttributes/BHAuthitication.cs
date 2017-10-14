using JTApp.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace JTApp.WebUI.CustomAttributes
{
    public class BHAuthitication : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            UserInfoDataObject userInfo = filterContext.HttpContext.Session["UserInfo"] as UserInfoDataObject;
            if (userInfo == null)
            {
                //UrlHelper Url = new UrlHelper(filterContext.RequestContext);
                //string url = Url.Action("Login", "Home");
                //filterContext.Result = new RedirectResult(url);
                HttpResponseBase response = filterContext.HttpContext.Response;
                response.Write("<script>alert('登录超时，请重新登录!');window.top.location.href='/Home/Login/';</script>");
                
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }
    }
}