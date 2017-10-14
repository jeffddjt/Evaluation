using Evaluation.CustomAttributes;
using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.ServiceContracts;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluation.Controllers
{
    [BHAuthitication]
    public class ReviewController : Controller
    {
        private IReviewService reviewService;
        private ITimeOverService timeOverService;

        public ReviewController()
        {
            this.reviewService = ServiceLocator.Instance.GetRef<IReviewService>();
            this.timeOverService = ServiceLocator.Instance.GetRef<ITimeOverService>();
        }
        // GET: Review
        public ActionResult Index()
        {
            TimeOverDataObject timeover = timeOverService.GetFirst();
            int year = timeover == null ? DateTime.Now.Year : timeover.Year;
            IList<ReviewDataObject> reviewList = reviewService.GetList(year);
            ViewData["ReviewList"] = reviewList.OrderBy(p=>p.Sort).ToList();
            ViewData["Year"] = year;
            return View();
        }
        public void SaveReview(ReviewDataObject review)
        {
            this.reviewService.Update(review);
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
    }
}