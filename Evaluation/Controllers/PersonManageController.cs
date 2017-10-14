using Evaluation.CustomAttributes;
using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.Infrastructure.Common;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluation.Controllers
{
    [BHAuthitication]
    public class PersonManageController : Controller
    {
        private IUserInfoService userInfoService;
        private IDepartmentService departmentService;
        private IDutiesService dutiesService;
        private IBeMeasuredService beMeasuredService;
        public PersonManageController()
        {
            this.userInfoService = ServiceLocator.Instance.GetRef<IUserInfoService>();
            this.departmentService = ServiceLocator.Instance.GetRef<IDepartmentService>();
            this.dutiesService = ServiceLocator.Instance.GetRef<IDutiesService>();
            this.beMeasuredService = ServiceLocator.Instance.GetRef<IBeMeasuredService>();
        }
        public ActionResult PartakePerson(int? pageSize,int? pageIndex)
        {
            JTPager pager = new JTPager();
            pager.Size = pageSize == null || pageSize.Value <= 0 ? 20 : pageSize.Value;
            pager.Index = pageIndex == null || pageIndex.Value <= 0 ? 1 : pageIndex.Value;
            List<UserInfoDataObject> userList = userInfoService.GetList(pager);
            ViewData["UserList"] = userList;
            ViewData["PageSize"] = pager.Size;
            ViewData["PageIndex"] = pager.Index;
            ViewData["PageCount"] = pager.Count;
            ViewData["RecordCount"] = pager.Total;
            return View();
        }

        public string RemovePerson(int[] selected)
        {
            if (selected == null)
                return "false";
            int count= this.userInfoService.RemoveByIds(selected);
            if (count > 0)
                return "true";
            return "false";
        }
        public ActionResult EditUserInfo(int? id)
        {
            UserInfoDataObject userInfo;
            if (id == null)
                userInfo = new UserInfoDataObject();
            else
                userInfo = this.userInfoService.GetOne(id.Value);
            ViewData["DepartmentList"] = this.departmentService.GetList();
            ViewData["Duties"] = this.dutiesService.GetList();
            return View(userInfo);
        }
        public ActionResult SaveUserInfo(UserInfoDataObject userInfo)
        {
            userInfo = this.userInfoService.Update(userInfo);
            return RedirectToAction("PartakePerson");
        }
        public ActionResult ImportUserInfo(HttpPostedFileBase file)
        {
            if (file == null)
                return View();

            MemoryStream ms = new MemoryStream();
            file.InputStream.CopyTo(ms);
            DataTable dt = ExcelHelper.ImportExcel(ms);
            IList<UserInfoDataObject> list = new List<UserInfoDataObject>();
            IList<DepartmentDataObject> deptList = this.departmentService.GetList();
            IList<DutiesDataObject> dutiesList = this.dutiesService.GetList();
            foreach (DataRow row in dt.Rows)
            {
                UserInfoDataObject userInfo = new UserInfoDataObject();
                userInfo.WorkNo = row[0].ToString();
                userInfo.UserName = row[1].ToString();
                userInfo.Password = "12345";
                DepartmentDataObject dept = deptList.FirstOrDefault(p => p.Name == row[2].ToString());
                userInfo.DepartmentID = dept == null ? 0 : dept.ID;
                userInfo.Professional = row[3].ToString();
                DutiesDataObject duty = dutiesList.FirstOrDefault(p => p.Name == row[4].ToString());
                userInfo.DutiesID = duty == null ? 0 : duty.ID;
                userInfo.MajorLeader = row[5].ToString() == "是" ? true : false;
                userInfo.Director = row[6].ToString() == "是";
                userInfo.Instructor = row[7].ToString() == "是";
                userInfo.Secretary = row[8].ToString() == "是";
                list.Add(userInfo);
            }
            int count = this.userInfoService.Import(list);
            ViewData["Msg"] = string.Format("{0}条数据导入成功!", count);         
            return View();
        }
        public FileContentResult TempleteTable()
        {
            string path = Server.MapPath("/Templete/");
            string fileName = "人员导入模板.xls";
            string[] deptList = this.departmentService.GetList().Select(p => p.Name).ToArray();
            string[] dutiesList = this.dutiesService.GetList().Select(p => p.Name).ToArray();
            byte[] buf =ExcelHelper.ModifyTemplete(path+fileName, deptList, dutiesList);
            return File(buf, "application/octet-stream", Server.UrlEncode(fileName));
        }
        public int DeleteAllUserInfo()
        {
            return this.userInfoService.RemoveAll();
        }

        public ActionResult Person(int? pageSize, int? pageIndex)
        {
            JTPager pager = new JTPager();
            pager.Size = pageSize == null || pageSize.Value <= 0 ? 20 : pageSize.Value;
            pager.Index = pageIndex == null || pageIndex.Value <= 0 ? 1 : pageIndex.Value;

            IList<BeMeasuredDataObject> beMeasureList = this.beMeasuredService.GetList(pager);
            ViewData["BeMeasureList"] = beMeasureList;
            ViewData["PageSize"] = pager.Size;
            ViewData["PageIndex"] = pager.Index;
            ViewData["PageCount"] = pager.Count;
            ViewData["RecordCount"] = pager.Total;
            return View();
        }
        public ActionResult AddBeMeasured(int? pageSize,int? pageIndex)
        {
            JTPager pager = new JTPager();
            pager.Size = pageSize == null || pageSize.Value <= 0 ? 20 : pageSize.Value;
            pager.Index = pageIndex == null || pageIndex.Value <= 0 ? 1 : pageIndex.Value;

            int[] beIDs = this.beMeasuredService.GetList().Select(p => p.UserInfo.ID).ToArray();
            IList<UserInfoDataObject> userList = this.userInfoService.GetList(beIDs, pager);
            ViewData["UserList"] = userList;
            ViewData["PageSize"] = pager.Size;
            ViewData["PageIndex"] = pager.Index;
            ViewData["PageCount"] = pager.Count;
            ViewData["RecordCount"] = pager.Total;
            return View();
        }
        public string AddUserToBemeasured(int[] selected)
        {
            int count = this.beMeasuredService.Add(selected);
            return count.ToString() + "条数据添加成功!";
        }
        public ActionResult EditBeMeasured(int? id)
        {
            if (id == null)
                return RedirectToAction("Person");
            BeMeasuredDataObject bm = this.beMeasuredService.GetOne(id.Value);
            ViewData["BeMeasured"] = bm;
            return View();
        }
        public ActionResult AddUserInfoToMeasured(int? id,int? pageSize,int? pageIndex)
        {
            if (id == null)
                return Redirect(Request.UrlReferrer.ToString());
            JTPager pager = new JTPager();
            pager.Size = pageSize == null || pageSize.Value <= 0 ? 20 : pageSize.Value;
            pager.Index = pageIndex == null || pageIndex.Value <= 0 ? 1 : pageIndex.Value;


            BeMeasuredDataObject bm = this.beMeasuredService.GetOne(id.Value);
            List<int> ids = new List<int>();
            foreach (MeasuredDataObject measured in bm.MeasuredList)
            {
                ids.AddRange(measured.UserList.Select(p => p.ID).ToArray());
            }
            IList<UserInfoDataObject> userList = this.userInfoService.GetList(ids.ToArray(), pager);
            IList<DepartmentDataObject> deptList = this.departmentService.GetList();
            IList<DutiesDataObject> dutiesList = this.dutiesService.GetList();

            ViewData["DeptList"] = deptList;
            ViewData["DutiesList"] = dutiesList;
            ViewData["UserList"] = userList;
            ViewData["BeMeasured"] = bm;
            ViewData["PageSize"] = pager.Size;
            ViewData["PageIndex"] = pager.Index;
            ViewData["PageCount"] = pager.Count;
            ViewData["RecordCount"] = pager.Total;

            return View();
        }
        public ActionResult QueryPerson(UserInfoDataObject userInfo,int? id)
        {
            ViewResult view = new ViewResult();
            view.ViewName = "AddUserInfoToMeasured";
            int? pageSize = 20;
            int? pageIndex = 1;
            if (pageSize == null || pageSize.Value <= 0)
                pageSize = 20;
            if (pageIndex == null || pageIndex.Value <= 0)
                pageIndex = 1;


            BeMeasuredDataObject bm = this.beMeasuredService.GetOne(id.Value);
            List<int> ids = new List<int>();
            foreach (MeasuredDataObject measured in bm.MeasuredList)
            {
                ids.AddRange(measured.UserList.Select(p => p.ID).ToArray());
            }
            IList<UserInfoDataObject> userList = this.userInfoService.GetList(ids.ToArray());
            if (!string.IsNullOrWhiteSpace(userInfo.WorkNo))
                userList = userList.Where(p => p.WorkNo == userInfo.WorkNo).ToList();
            if (!string.IsNullOrWhiteSpace(userInfo.UserName))
                userList = userList.Where(p => p.UserName == userInfo.UserName).ToList();
            if (userInfo.DepartmentID != 0)
            {
                userList = userList.Where(p => this.departmentService.GetOne(userInfo.DepartmentID).GetIDArray().Contains(p.DepartmentID)).ToList();
             }
            if (userInfo.DutiesID != 0)
            {
                userList = userList.Where(p => this.dutiesService.GetOne(userInfo.DutiesID).ID == p.DutiesID).ToList();
            }
            if (!string.IsNullOrWhiteSpace(userInfo.Professional))
                userList = userList.Where(p => p.Professional == userInfo.Professional).ToList();
            if (userInfo.MajorLeader)
                userList = userList.Where(p => p.MajorLeader).ToList();
            if (userInfo.Director)
                userList = userList.Where(p => p.Director).ToList();
            if (userInfo.Instructor)
                userList = userList.Where(p => p.Instructor).ToList();
            if (userInfo.Secretary)
                userList = userList.Where(p => p.Secretary).ToList();
            int recordCount = userList.Count;

            int pageCount = (recordCount + pageSize.Value - 1) / pageSize.Value;
            if (pageIndex.Value > pageCount && pageCount != 0)
                pageIndex = pageCount;

            IList<DepartmentDataObject> deptList = this.departmentService.GetList();
            IList<DutiesDataObject> dutiesList = this.dutiesService.GetList();

            view.ViewData["DeptList"] = deptList;
            view.ViewData["DutiesList"] = dutiesList;
            view.ViewData["UserList"] = userList.Take(pageSize.Value).ToList();
            view.ViewData["BeMeasured"] = bm;
            view.ViewData["PageSize"] = pageSize.Value;
            view.ViewData["PageIndex"] = pageIndex.Value;
            view.ViewData["PageCount"] = pageCount;
            view.ViewData["RecordCount"] = recordCount;
            return view;
        } 
        public string SaveUserToBemeasured(int[] selected, int? ratio,int? ID)
        {
            if (selected == null || selected.Length <= 0 || ratio == null)
                return "添加失败！";
            int result=this.beMeasuredService.AddUserInfo(selected, ratio.Value,ID.Value);
            return result > 0 ? "添加成功!" : "添加失败!";
        }
        public void RemoveUserInfoFromBeMeasured(int[] selected, int? beMeasuredID)
        {
            this.beMeasuredService.RemoveMeasuredUser(selected, beMeasuredID.Value);
        }
        public string RemoveBeMeasured(int[] selected)
        {
            try
            {
                this.beMeasuredService.RemoveByIds(selected);
                return "true";
            }
            catch
            {
                return "false";
            }
            
        }
    }
}