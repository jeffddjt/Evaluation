using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluation.CustomAttributes
{
    public class BHCheckTimeOver: ActionFilterAttribute
    {
        private ITimeOverService timeOverService;

        public BHCheckTimeOver()
        {
            this.timeOverService = ServiceLocator.Instance.GetRef<ITimeOverService>();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
                TimeOverDataObject timeOver = timeOverService.GetFirst();
                if (DateTime.Now > timeOver.Expire)
                {
                    UrlHelper Url = new UrlHelper(filterContext.RequestContext);
                    string url = Url.Action("ShowError", "Error", new { Msg = "测评时间已过！" });
                    filterContext.Result = new RedirectResult(url);
                }
        }
    }
}