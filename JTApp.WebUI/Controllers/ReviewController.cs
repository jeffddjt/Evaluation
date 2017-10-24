using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.ServiceContracts;
using JTApp.WebUI.CustomAttributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JTApp.WebUI.Controllers
{

    public class ReviewController : Controller
    {
        private IReviewService reviewService;
        private ITimeOverService timeOverService;
        private IEvaluationLevelService evaluationLevelService;

        public ReviewController()
        {
            this.reviewService = ServiceLocator.Instance.GetRef<IReviewService>();
            this.timeOverService = ServiceLocator.Instance.GetRef<ITimeOverService>();
            this.evaluationLevelService = ServiceLocator.Instance.GetRef<IEvaluationLevelService>();
        }
        // GET: Review
        public ActionResult Index()
        {
            TimeOverDataObject timeover = timeOverService.GetFirst();
            int year = timeover == null ? DateTime.Now.Year : timeover.Year;
            IList<ReviewDataObject> reviewList = reviewService.GetList(year);
            EvaluationLevelDataObject level = this.evaluationLevelService.GetFirst();
            if (level == null)
                level = this.evaluationLevelService.Add(new EvaluationLevelDataObject() { Level = 1 });
            ViewData["ReviewList"] = reviewList.OrderBy(p=>p.Sort).ToList();
            ViewData["Year"] = year;
            ViewData["EvaluationLevel"] = level;
            return View();
        }
        public void SaveReview(ReviewDataObject review)
        {
            if (review.ID != 0)
                this.reviewService.Update(review);
            else
                this.reviewService.Add(review);
        }
        public void RemoveReview(int id)
        {
            this.reviewService.RemoveById(id);
        }
        public string GetReview(int? id)
        {
            if (id == null)
                return string.Empty;

            ReviewDataObject review = this.reviewService.GetOne(id.Value);
            JObject obj = JObject.FromObject(review);
            return obj.ToString();
        }
        public bool SetLevel(int? level)
        {
            if (level == null)
                return false;
            this.evaluationLevelService.SetLevel(level.Value);
            return true;
        }
    }
}