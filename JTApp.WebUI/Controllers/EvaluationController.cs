using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.ServiceContracts;
using JTApp.WebUI.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JTApp.WebUI.Controllers
{

    [BHCheckTimeOver]
    public class EvaluationController : Controller
    {
        private IEvaluationTableService evaluationService;
        private IEvaluationTableDetailService evaluationTableDetailService;
        private IReviewService reviewService;
        private IStyleOfWorkService styleOfWorkService;
        private ITimeOverService timeOverService;
        private IEvaluationLevelService evaluationLevelService;
        public EvaluationController()
        {
            this.evaluationService = ServiceLocator.Instance.GetRef<IEvaluationTableService>();
            this.evaluationTableDetailService = ServiceLocator.Instance.GetRef<IEvaluationTableDetailService>();
            this.reviewService = ServiceLocator.Instance.GetRef<IReviewService>();
            this.styleOfWorkService = ServiceLocator.Instance.GetRef<IStyleOfWorkService>();
            this.timeOverService = ServiceLocator.Instance.GetRef<ITimeOverService>();
            this.evaluationLevelService = ServiceLocator.Instance.GetRef<IEvaluationLevelService>();
        }
        public ActionResult FillEvaluationTable(int? id)
        {
            EvaluationLevelDataObject level;
            if (this.evaluationLevelService.GetCount() <= 0)
                level = this.evaluationLevelService.Add(new EvaluationLevelDataObject() { Level = 1 });
            else
                level = this.evaluationLevelService.GetFirst();

            UserInfoDataObject userInfo = Session["UserInfo"] as UserInfoDataObject;
            int year = this.timeOverService.GetFirst().Year;
            EvaluationTableDataObject evaluationTable = this.evaluationService.GetOne(id.Value,userInfo.ID,year);
            if (evaluationTable == null)
                evaluationTable = this.evaluationService.Add(id.Value,userInfo.ID);
            ViewData["EvaluationTable"] = evaluationTable;
            return View(string.Format("FillEvaluationTable{0}", level.Level));
        }
        public ActionResult SaveEvaluationTable(int? id)
        {
            UserInfoDataObject userInfo = Session["UserInfo"] as UserInfoDataObject;
            string args= Request.QueryString.ToString();
            string[] argsArr = args.Split('&');
            try
            {
                foreach (string arg in argsArr)
                {
                    if (arg.Split('=')[0] != "id")
                    {
                        int detailID = int.Parse(arg.Split('=')[0].Split('_')[1]);
                        double score = double.Parse(arg.Split('=')[1]);
                        this.evaluationTableDetailService.Update(detailID, score);
                    }
                    else if(arg.Split('=')[0]=="id")
                    {
                        int evalID = int.Parse(arg.Split('=')[1]);
                        this.evaluationService.Update(evalID);
                    }
                }
            }
            catch(Exception ex) 
            {
                return RedirectToAction("ShowError", "Error", new { Msg = ex.Message});
            }
            int evaluationUserID = this.evaluationService.GetOne(id.Value).BeMeasuredUserInfoID;
            return Redirect("FillEvaluationTable?id="+ evaluationUserID );
        }

        public ActionResult StyleOfWork(int? id)
        {
            UserInfoDataObject userInfo = Session["UserInfo"] as UserInfoDataObject;
            int year = this.timeOverService.GetFirst().Year;
            StyleOfWorkDataObject styleOfWork = this.styleOfWorkService.GetOne(id.Value,userInfo.ID,year);
            if (styleOfWork == null)
                styleOfWork = this.styleOfWorkService.Add(id.Value,userInfo.ID);
            ViewData["StyleOfWork"] = styleOfWork;
            return View();
        }

        [HttpPost]
        public ActionResult SaveStyleOfWork(int? id, int? Score)
        {
            if (Score == null)
                return Redirect("StyleOfWork?id=" + this.styleOfWorkService.GetOne(id.Value).BeMeasuredUserInfoID);
            StyleOfWorkDataObject styleOfWork = this.styleOfWorkService.Update(id.Value, Score.Value);
            return Redirect("StyleOfWork?id=" + styleOfWork.BeMeasuredUserInfoID);
        }


    }
}