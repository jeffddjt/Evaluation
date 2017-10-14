
using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.ServiceContracts;
using JTApp.WebUI.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JTApp.WebUI.Controllers
{
    [BHAuthitication]
    public class ArticleController : Controller
    {
        private IArticleService articleService;
        public ArticleController()
        {
            this.articleService = ServiceLocator.Instance.GetRef<IArticleService>();
        }
        // GET: Article
        public ActionResult Index()
        {
            ArticleDataObject article = articleService.GetFirst();
            if (article == null)
                article = new ArticleDataObject();
            ViewData["Article"] = article;
            return View();
        }
        public ActionResult SaveArticle(ArticleDataObject article)
        {
            articleService.Update(article);
            return RedirectToAction("Index", "Article");
        }
    }
}