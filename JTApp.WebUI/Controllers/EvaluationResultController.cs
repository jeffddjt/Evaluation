using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.Infrastructure.Common;
using JTApp.ServiceContracts;
using JTApp.WebUI.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JTApp.WebUI.Controllers
{

    public class EvaluationResultController : Controller
    {
        private IUserInfoService userInfoService;
        private IBeMeasuredService beMeasuredService;
        private IDepartmentService departmentService;
        private IReviewService reviewService;
        private ITimeOverService timeOverService;
        public EvaluationResultController()
        {
            this.userInfoService = ServiceLocator.Instance.GetRef<IUserInfoService>();
            this.beMeasuredService = ServiceLocator.Instance.GetRef<IBeMeasuredService>();
            this.departmentService = ServiceLocator.Instance.GetRef<IDepartmentService>();
            this.reviewService = ServiceLocator.Instance.GetRef<IReviewService>();
            this.timeOverService = ServiceLocator.Instance.GetRef<ITimeOverService>();
        }
        public ActionResult Already(int? pageSize, int? pageIndex)
        {
            pageSize = pageSize ?? 20;
            if (pageIndex == null || pageIndex.Value < 1)
                pageIndex = 1;            

            ViewResult view = new ViewResult();
            JTPager pager = new JTPager() { Size = pageSize.Value, Index = pageIndex.Value };
            IList<UserInfoDataObject> userlist = this.userInfoService.GetAlready(pager);

            view.ViewData["PageSize"] = pager.Size;
            view.ViewData["PageIndex"] = pager.Index;
            view.ViewData["PageCount"] = pager.Count;
            view.ViewData["RecordCount"] = pager.Total;
            view.ViewData["UserList"] = userlist;
            view.ViewName = "EvaluationQuery";
            return view;
        }

        public ActionResult Havent(int? pageSize, int? pageIndex)
        {
            pageSize = pageSize ?? 20;
            if (pageIndex == null || pageIndex.Value < 1)
                pageIndex = 1;

            ViewResult view = new ViewResult();
            JTPager pager = new JTPager() { Size = pageSize.Value, Index = pageIndex.Value };
            IList<UserInfoDataObject> userlist = this.userInfoService.GetHavent(pager);

            view.ViewData["PageSize"] = pager.Size;
            view.ViewData["PageIndex"] = pager.Index;
            view.ViewData["PageCount"] = pager.Count;
            view.ViewData["RecordCount"] = pager.Total;
            view.ViewData["UserList"] = userlist;
            view.ViewName = "EvaluationQuery";
            return view;

        }
        public ActionResult StyleOfWorkAlready(int? pageSize, int? pageIndex)
        {
            pageSize = pageSize ?? 20;
            if (pageIndex == null || pageIndex.Value < 1)
                pageIndex = 1;

            ViewResult view = new ViewResult();
            JTPager pager = new JTPager() { Size = pageSize.Value, Index = pageIndex.Value };
            IList<UserInfoDataObject> userlist = this.userInfoService.StyleOfWorkAlready(pager);

            view.ViewData["PageSize"] = pager.Size;
            view.ViewData["PageIndex"] = pager.Index;
            view.ViewData["PageCount"] = pager.Count;
            view.ViewData["RecordCount"] = pager.Total;
            view.ViewData["UserList"] = userlist;
            view.ViewName = "EvaluationQuery";
            return view;

        }
        public ActionResult StyleOfWorkHavent(int? pageSize, int? pageIndex)
        {
            pageSize = pageSize ?? 20;
            if (pageIndex == null || pageIndex.Value < 1)
                pageIndex = 1;

            ViewResult view = new ViewResult();
            JTPager pager = new JTPager() { Size = pageSize.Value, Index = pageIndex.Value };
            IList<UserInfoDataObject> userlist = this.userInfoService.StyleOfWorkHavent(pager);

            view.ViewData["PageSize"] = pager.Size;
            view.ViewData["PageIndex"] = pager.Index;
            view.ViewData["PageCount"] = pager.Count;
            view.ViewData["RecordCount"] = pager.Total;
            view.ViewData["UserList"] = userlist;
            view.ViewName = "EvaluationQuery";
            return view;

        }

        public ActionResult GetEvaluationCount(int? deptID, int? pageSize, int? pageIndex)
        {
            return GetTotalCount(deptID, pageSize, pageIndex);
        }
        public ActionResult GetStyleOfWorkCount(int? deptID, int? pageSize, int? pageIndex)
        {
            return GetTotalCount(deptID, pageSize, pageIndex);
        }

        private ActionResult GetTotalCount(int? deptID, int? pageSize, int? pageIndex)
        {
            pageSize = pageSize ?? 20;
            if (pageIndex == null || pageIndex.Value < 1)
                pageIndex = 1;

            ViewResult view = new ViewResult();
            JTPager pager = new JTPager() { Size = pageSize.Value, Index = pageIndex.Value };

            int year = this.timeOverService.GetFirst()?.Year ?? DateTime.Now.Year;
            DepartmentDataObject dept = this.departmentService.GetOne(deptID.Value);
            int[] ids = dept.GetIDArray();
            IList<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetTotal(ids,year,pager);
            view.ViewData["PageSize"] = pager.Size;
            view.ViewData["PageIndex"] = pager.Index;
            view.ViewData["PageCount"] = pager.Count;
            view.ViewData["RecordCount"] = pager.Total;
            view.ViewData["DeptID"] = deptID.Value;
            view.ViewName = "EvaluationCountResult";
            view.ViewData["BeMeasuredList"] = beMeasuredList;
            return view;
        }
        public FileContentResult Download_GetEvaluationCount(int? deptID)
        {
            int year = this.timeOverService.GetFirst()?.Year ?? DateTime.Now.Year;
            DepartmentDataObject dept = this.departmentService.GetOne(deptID.Value);
            int[] ids = dept.GetIDArray();
            List<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetList(ids,year);
            List<double> ratios = new List<double>();
            beMeasuredList.ForEach(
                item =>
                ratios.AddRange(item.MeasuredList.GroupBy(p => p.Ratio).Select(p => p.Key).ToArray())
                );
            ratios = ratios.Distinct().OrderBy(p => -p).ToList();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("工号"), new DataColumn("姓名") });
            foreach (double ratio in ratios)
            {
                dt.Columns.Add(new DataColumn(string.Format("{0}%投票人数", ratio)));
            }
            foreach (BeMeasuredDataObject bm in beMeasuredList)
            {
                DataRow row = dt.NewRow();
                row["工号"] = bm.UserInfo.WorkNo;
                row["姓名"] = bm.UserInfo.UserName;
                foreach (double ratio in ratios)
                {
                    row[string.Format("{0}%投票人数", ratio)] = bm.EvaluationTableList.Where(p => p.Ratio == ratio && p.Submit).Count();
                }
                dt.Rows.Add(row);
            }
            string fileName = string.Format("民主测评({0})投票数", dept.Name);
            byte[] buf = ExcelHelper.ExportToExcel(dt, fileName);
            return File(buf, "application/octet-stream", Server.UrlEncode(fileName + ".xls"));
        }
        public FileContentResult Download_GetStyleOfWorkCount(int? deptID)
        {
            int year = this.timeOverService.GetFirst()?.Year ?? DateTime.Now.Year;
            DepartmentDataObject dept = this.departmentService.GetOne(deptID.Value);
            int[] ids = dept.GetIDArray();
            List<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetList(ids,year);
            List<double> ratios = new List<double>();
            beMeasuredList.ForEach(
                item =>
                ratios.AddRange(item.MeasuredList.GroupBy(p => p.Ratio).Select(p => p.Key).ToArray())
                );
            ratios = ratios.Distinct().OrderBy(p => -p).ToList();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("工号"), new DataColumn("姓名") });
            foreach (double ratio in ratios)
            {
                dt.Columns.Add(new DataColumn(string.Format("{0}%投票人数", ratio)));
            }
            foreach (BeMeasuredDataObject bm in beMeasuredList)
            {
                DataRow row = dt.NewRow();
                row["工号"] = bm.UserInfo.WorkNo;
                row["姓名"] = bm.UserInfo.UserName;
                foreach (double ratio in ratios)
                {
                    row[string.Format("{0}%投票人数", ratio)] = bm.StyleOfWorkList.Where(p => p.Ratio == ratio && p.Score>0).Count();
                }
                dt.Rows.Add(row);
            }
            string fileName = string.Format("德和作风({0})投票数", dept.Name);
            byte[] buf = ExcelHelper.ExportToExcel(dt, fileName);
            return File(buf, "application/octet-stream", Server.UrlEncode(fileName + ".xls"));
        }
        public ActionResult EvaluationTotal(int? deptID,int? pageSize,int? pageIndex)
        {
            pageSize = pageSize ?? 20;
            if (pageIndex == null || pageIndex.Value < 1)
                pageIndex = 1;
            DepartmentDataObject dept = this.departmentService.GetOne( deptID.Value);
            int[] ids = dept.GetIDArray();
            int year = this.timeOverService.GetFirst().Year;
            List<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetList(ids, year);
            int recordCount = beMeasuredList.Count();
            int pageCount = (recordCount + pageSize.Value - 1) / pageSize.Value;
            if (pageIndex > pageCount)
                pageIndex = pageCount;
            IList<ReviewDataObject> reviewList = this.reviewService.GetList(year);
            beMeasuredList = beMeasuredList.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("工号") ,new DataColumn("姓名")});
            foreach (ReviewDataObject review in reviewList)
            {
                dt.Columns.Add(new DataColumn(review.Name));
            }
            dt.Columns.Add(new DataColumn("总分"));
            foreach (BeMeasuredDataObject bm in beMeasuredList)
            {
                DataRow row = dt.NewRow();
                row["工号"] = bm.UserInfo.WorkNo;
                row["姓名"] = bm.UserInfo.UserName;
                double sum = 0;
                foreach (ReviewDataObject review in reviewList)
                {
                    var query = bm.EvaluationTableList.Where(p=>p.Submit).GroupBy(p => p.Ratio)
                        .Select(p => new
                        {
                            Ratio = p.Key,
                            Score = p.Sum(k => k.EvaluationTableDetail.Where(t => t.ReviewID == review.ID).Sum(s => s.Score)) * p.Key / 100 / p.Count()
                        });
                    double score = query.Sum(p => p.Score);
                    row[review.Name] = score;
                    sum += score;
                }
                row["总分"] = sum;
                dt.Rows.Add(row);
            }
            ViewResult view = new ViewResult();
            view.ViewName = "EvaluationTotal";
            view.ViewData["PageSize"] = pageSize.Value;
            view.ViewData["PageIndex"] = pageIndex.Value;
            view.ViewData["PageCount"] = pageCount;
            view.ViewData["RecordCount"] = recordCount;
            view.ViewData["DeptID"] = deptID.Value;
            view.ViewData["DataTable"] = dt;
            return view;
        }
        public ActionResult StyleOfWorkTotal(int? deptID, int? pageSize, int? pageIndex)
        {
            pageSize = pageSize ?? 20;
            if (pageIndex == null || pageIndex.Value < 1)
                pageIndex = 1;
            DepartmentDataObject dept = this.departmentService.GetOne(deptID.Value);
            int[] ids = dept.GetIDArray();
            int year = this.timeOverService.GetFirst().Year;
            List<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetList(ids,year);
            int recordCount = beMeasuredList.Count();
            int pageCount = (recordCount + pageSize.Value - 1) / pageSize.Value;
            if (pageIndex > pageCount)
                pageIndex = pageCount;
            IList<ReviewDataObject> reviewList = this.reviewService.GetList(year);
            beMeasuredList = beMeasuredList.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("工号"), new DataColumn("姓名"),new DataColumn("分数") });
            foreach (BeMeasuredDataObject bm in beMeasuredList)
            {
                DataRow row = dt.NewRow();
                row["工号"] = bm.UserInfo.WorkNo;
                row["姓名"] = bm.UserInfo.UserName;
                var query = bm.StyleOfWorkList.Where(p=>p.Score>0).GroupBy(p => p.Ratio)
                    .Select(p =>
                        new
                        {
                            Ratio=p.Key,
                            Score=p.Sum(k=>k.Score)*p.Key/100/p.Count()
                        }
                    );
                row["分数"] = query.Sum(p => p.Score);
                dt.Rows.Add(row);
            }
            ViewResult view = new ViewResult();
            view.ViewName = "EvaluationTotal";
            view.ViewData["PageSize"] = pageSize.Value;
            view.ViewData["PageIndex"] = pageIndex.Value;
            view.ViewData["PageCount"] = pageCount;
            view.ViewData["RecordCount"] = recordCount;
            view.ViewData["DeptID"] = deptID.Value;
            view.ViewData["DataTable"] = dt;
            return view;
        }
        public ActionResult StyleOfWorkTotalCount(int? deptID, int? pageSize, int? pageIndex)
        {
            pageSize = pageSize ?? 20;
            if (pageIndex == null || pageIndex.Value < 1)
                pageIndex = 1;
            DepartmentDataObject dept = this.departmentService.GetOne(deptID.Value);
            int[] ids = dept.GetIDArray();
            int year = this.timeOverService.GetFirst().Year;
            List<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetList(ids,year);
            int recordCount = beMeasuredList.Count();
            int pageCount = (recordCount + pageSize.Value - 1) / pageSize.Value;
            if (pageIndex > pageCount)
                pageIndex = pageCount;
            IList<ReviewDataObject> reviewList = this.reviewService.GetList(year);
            beMeasuredList = beMeasuredList.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] 
            {
                new DataColumn("工号"),
                new DataColumn("姓名"),
                new DataColumn("没有"),
                new DataColumn("有些"),
                new DataColumn("很严重")
            }
            );
            foreach (BeMeasuredDataObject bm in beMeasuredList)
            {
                DataRow row = dt.NewRow();
                row["工号"] = bm.UserInfo.WorkNo;
                row["姓名"] = bm.UserInfo.UserName;
                row["没有"] = bm.StyleOfWorkList.Count((p) => { return p.Score == 100; });
                row["有些"] = bm.StyleOfWorkList.Count((p) => { return p.Score == 80; });
                row["很严重"] = bm.StyleOfWorkList.Count((p) => { return p.Score == 50; });
                dt.Rows.Add(row);
            }
            ViewResult view = new ViewResult();
            view.ViewName = "EvaluationTotal";
            view.ViewData["PageSize"] = pageSize.Value;
            view.ViewData["PageIndex"] = pageIndex.Value;
            view.ViewData["PageCount"] = pageCount;
            view.ViewData["RecordCount"] = recordCount;
            view.ViewData["DeptID"] = deptID.Value;
            view.ViewData["DataTable"] = dt;
            return view;
        }
        public FileContentResult Download_EvaluationTotal(int? deptID)
        {
            DepartmentDataObject dept = this.departmentService.GetOne(deptID.Value);
            int[] ids = dept.GetIDArray();
            int year = this.timeOverService.GetFirst().Year;
            List<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetList(ids,year);
            int recordCount = beMeasuredList.Count();
            IList<ReviewDataObject> reviewList = this.reviewService.GetList(year);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("工号"), new DataColumn("姓名") });
            foreach (ReviewDataObject review in reviewList)
            {
                dt.Columns.Add(new DataColumn(review.Name));
            }
            dt.Columns.Add(new DataColumn("总分"));
            foreach (BeMeasuredDataObject bm in beMeasuredList)
            {
                DataRow row = dt.NewRow();
                row["工号"] = bm.UserInfo.WorkNo;
                row["姓名"] = bm.UserInfo.UserName;
                double sum = 0;
                foreach (ReviewDataObject review in reviewList)
                {
                    var query = bm.EvaluationTableList.GroupBy(p => p.Ratio)
                        .Select(p => new
                        {
                            Ratio = p.Key,
                            Score = p.Sum(k => k.EvaluationTableDetail.Where(t => t.ReviewID == review.ID).Sum(s => s.Score)) * p.Key / 100 / p.Count()
                        });
                    double score = query.Sum(p => p.Score);
                    row[review.Name] = score;
                    sum += score;
                }
                row["总分"] = sum;
                dt.Rows.Add(row);
            }
            string fileName = "民主测评总评(" + dept.Name + ")";
            byte[] buf = ExcelHelper.ExportToExcel(dt, fileName);
            return File(buf, "application/octet-stream", Server.UrlEncode(fileName+".xls"));
        }
        public FileContentResult Download_StyleOfWorkTotal(int? deptID)
        {
            DepartmentDataObject dept = this.departmentService.GetOne(deptID.Value);
            int[] ids = dept.GetIDArray();
            int year = this.timeOverService.GetFirst().Year;
            List<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetList(ids, year);
            IList<ReviewDataObject> reviewList = this.reviewService.GetList(year);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[] { new DataColumn("工号"), new DataColumn("姓名"), new DataColumn("分数") });
            foreach (BeMeasuredDataObject bm in beMeasuredList)
            {
                DataRow row = dt.NewRow();
                row["工号"] = bm.UserInfo.WorkNo;
                row["姓名"] = bm.UserInfo.UserName;
                var query = bm.StyleOfWorkList.GroupBy(p => p.Ratio)
                    .Select(p =>
                        new
                        {
                            Ratio = p.Key,
                            Score = p.Sum(k => k.Score) * p.Key / 100 / p.Count()
                        }
                    );
                row["分数"] = query.Sum(p => p.Score);
                dt.Rows.Add(row);
            }

            string fileName = "德和作风测评结果(" + dept.Name + ")";
            byte[] buf = ExcelHelper.ExportToExcel(dt, fileName);
            return File(buf, "application/octet-stream", Server.UrlEncode(fileName + ".xls"));
        }
        public FileContentResult Download_StyleOfWorkTotalCount(int? deptID)
        {
            DepartmentDataObject dept = this.departmentService.GetOne(deptID.Value);
            int[] ids = dept.GetIDArray();
            int year = this.timeOverService.GetFirst().Year;
            List<BeMeasuredDataObject> beMeasuredList = this.beMeasuredService.GetList(ids,year);
            IList<ReviewDataObject> reviewList = this.reviewService.GetList(year);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("工号"),
                new DataColumn("姓名"),
                new DataColumn("没有"),
                new DataColumn("有些"),
                new DataColumn("很严重")
            }
            );
            foreach (BeMeasuredDataObject bm in beMeasuredList)
            {
                DataRow row = dt.NewRow();
                row["工号"] = bm.UserInfo.WorkNo;
                row["姓名"] = bm.UserInfo.UserName;
                row["没有"] = bm.StyleOfWorkList.Count((p) => { return p.Score == 100; });
                row["有些"] = bm.StyleOfWorkList.Count((p) => { return p.Score == 80; });
                row["很严重"] = bm.StyleOfWorkList.Count((p) => { return p.Score == 50; });
                dt.Rows.Add(row);
            }

            string fileName = "德和作风测评票数(" + dept.Name + ")";
            byte[] buf = ExcelHelper.ExportToExcel(dt, fileName);
            return File(buf, "application/octet-stream", Server.UrlEncode(fileName + ".xls"));
        }
    }
}