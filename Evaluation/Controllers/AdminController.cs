using Evaluation.CustomAttributes;
using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.ServiceContracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluation.Controllers
{
    [BHAuthitication]
    public class AdminController : Controller
    {
        private IDepartmentService departmentService;
        private IBeMeasuredService beMeasuredService;
        private IUserInfoService userInfoService;
        private ITimeOverService timeOverService;

        public AdminController()
        {
            this.departmentService = ServiceLocator.Instance.GetRef<IDepartmentService>();
            this.beMeasuredService = ServiceLocator.Instance.GetRef<IBeMeasuredService>();
            this.userInfoService = ServiceLocator.Instance.GetRef<IUserInfoService>();
            this.timeOverService = ServiceLocator.Instance.GetRef<ITimeOverService>();
        }

        // GET: Admin
        public ActionResult Index()
        {
            IList<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetList();
            ViewData["UserInfo"] = Session["UserInfo"] as UserInfoDataObject;
            ViewData["DepartmentList"] = this.departmentService.GetRootList();
            return View();
        }
        public ActionResult ChangePassword(int? id, string password)
        {
            this.userInfoService.ChangePassword(id.Value,password);
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public string ResetPassword(int? id)
        {
            this.userInfoService.ChangePassword(id.Value,"12345");
            return "密码重置成功!";
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult Time()
        {
            TimeOverDataObject timeOver = this.timeOverService.GetFirst();
            if (timeOver == null)
                return RedirectToAction("ShowError", "Error", new {Msg="未设定测评结束时间!" });

            ViewData["TimeOver"] = timeOver;
            return View();
        }
        public ActionResult TimeOver()
        {
            TimeOverDataObject timeover = timeOverService.GetFirst();
            if (timeover == null)
                timeover = new TimeOverDataObject() {Year=DateTime.Now.Year,Date=DateTime.Now.ToString("yyyy-MM-dd"),Hour=DateTime.Now.Hour };
            ViewData["TimeOver"] = timeover;
            return View();
        }
        public ActionResult SaveTimeOver(TimeOverDataObject timeOver)
        {
            this.timeOverService.Update(timeOver);
            return RedirectToAction("TimeOver", "Admin");
        }
        public ActionResult UserCenter()
        {
            UserInfoDataObject userInfo = Session["UserInfo"] as UserInfoDataObject;
            ViewData["UserInfo"] = userInfo;
            return View();
        }
    }
}