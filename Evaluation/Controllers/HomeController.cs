using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluation.Controllers
{
    public class HomeController : Controller
    {
        private IUserInfoService userInfoService;
        private IArticleService articleService;

        public HomeController()
        {
            this.userInfoService = ServiceLocator.Instance.GetRef<IUserInfoService>();
            this.articleService = ServiceLocator.Instance.GetRef<IArticleService>();
        }

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            //ViewData["Msg"] = "用户名或密码错误!";            
            ViewData["Article"] = getArticle();
            return View();
        }

        private ArticleDataObject getArticle()
        {
            ArticleDataObject article = this.articleService.GetFirst();
            return article;
        }

        [HttpPost]
        public ActionResult CheckLogin(string workNo,string password)
        {
             ViewResult view = new ViewResult();
            if (string.IsNullOrWhiteSpace(workNo) || string.IsNullOrWhiteSpace(password))
            {
                view.ViewData["Msg"] = "用户名或密码不能为空!";
                view.ViewData["Article"] = getArticle();
                view.ViewName = "Login";                
                return view;
            }
            UserInfoDataObject userInfo = this.userInfoService.CheckLogin(workNo, password);
            if (userInfo == null)
            {
                view.ViewData["Msg"] = "用户名或密码错误!";
                view.ViewData["Article"] = getArticle();
                view.ViewName = "Login";
                return view;
            }
            UserInfoDataObject evaUser = this.userInfoService.GetEvaUser(userInfo.ID);

            if (evaUser!= null)
            {
                view.ViewData["Msg"] = "您已完成民主测评！无需再次登录！";
                view.ViewData["Article"] = getArticle();
                view.ViewName = "Login";
                return view;
            }
            userInfo.FunctionList = this.userInfoService.GetFunctionList(userInfo);
            Session["UserInfo"] = userInfo;
            return RedirectToAction("Index", "Admin");

        }
    }
}