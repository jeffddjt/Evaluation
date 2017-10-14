using Evaluation.CustomAttributes;
using JTApp.DataObject;
using JTApp.Infrastructure;
using JTApp.Infrastructure.Common;
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
    public class DepartmentController : Controller
    {
        private IDepartmentService departmentService;
        private IUserInfoService userInfoService;

        public DepartmentController()
        {
            this.departmentService = ServiceLocator.Instance.GetRef<IDepartmentService>();
            this.userInfoService = ServiceLocator.Instance.GetRef<IUserInfoService>();
        }
        public ActionResult DepartmentManage()
        {
            ViewData["Tree"] = GetDepartmentTree();
            return View();
        }
        public ActionResult DepartmentEdit(int? id,int? pageSize,int? pageIndex)
        {
            if (id == null || id.Value == 0)
            {
                return RedirectToAction("ShowError", "Error", new { Msg = "请选择组织机构!" });
            }
            if (pageSize == null || pageSize.Value <= 0)
                pageSize = 20;
            if (pageIndex == null || pageIndex.Value <= 0)
                pageIndex = 1;            
            DepartmentDataObject dept = departmentService.GetOne(id.Value);
            int recordCount = dept.UserList.Count;
            int pageCount = (recordCount + pageSize.Value - 1) / pageSize.Value;
            if (pageIndex.Value > pageCount)
                pageIndex = pageCount;
            List<UserInfoDataObject> userList = dept.UserList.OrderBy(p=>p.WorkNo).Skip((pageIndex.Value - 1) * pageSize.Value)
                .Take(pageSize.Value).ToList();
            
            ViewData["Department"] = dept;
            ViewData["DeptList"] = departmentService.GetList();
            ViewData["UserList"] = userList;
            ViewData["PageSize"] = pageSize.Value;
            ViewData["PageIndex"] = pageIndex.Value;
            ViewData["PageCount"] = pageCount;
            ViewData["RecordCount"] = recordCount;
            return View();
        }
        public string GetDepartmentTree(DepartmentDataObject parent = null)
        {
            IList<TreeNodeDataObject> treeNodes = this.departmentService.GetTreeNodes();
            JArray arr = JArray.FromObject(treeNodes);
            return arr.ToString();
        }
        public ActionResult DepartmentAdd(int? id)
        {
            if (id != null)
            {
                ViewData["ParentID"] = id.Value;
                ViewData["DeptList"] = departmentService.GetList();
                return View();
            }
            return RedirectToAction("ShowError", "Error", new { Msg = "格式不正确!" });
        }
        public ActionResult SaveDepartment(DepartmentDataObject department)
        {
            department = departmentService.Update(department);
            return RedirectToAction("DepartmentEdit", "Department", new { id = department.ID });
        }
        public ActionResult DepartmentRemove(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ShowError", "Error", new { Msg = "格式不正确!" });
            }
            int parentID = departmentService.RemoveById(id.Value);
            return RedirectToAction("DepartmentEdit", "Department", new { id = parentID });
        }
        public ActionResult AddUser(int? deptID, int? id, int? pageSize, int? pageIndex)
        {
            
            if (deptID == null)
                return Redirect(Request.UrlReferrer.ToString());

            JTPager pager = new JTPager();
            pager.Size = pageSize == null || pageSize.Value <= 0 ? 20 : pageSize.Value;
            pager.Index = pageIndex == null || pageIndex.Value <= 0 ? 1 : pageIndex.Value;

            DepartmentDataObject dept = this.departmentService.GetOne(deptID.Value);
            int[] ids = dept.UserList.Select(p => p.ID).ToArray();
            IList<UserInfoDataObject> userList = this.userInfoService.GetList(ids, pager);
            ViewData["PageSize"] = pager.Size;
            ViewData["PageIndex"] = pager.Index;
            ViewData["PageCount"] = pager.Count;
            ViewData["RecordCount"] = pager.Total;
            ViewData["Department"] = dept;
            ViewData["UserList"] = userList;

            return View();
        }
        public string AddUserToDepartment(int? deptID,int[] selected)
        {
            if (deptID == null || selected == null || selected.Length <= 0)
                return "成员添加失败!";
            int count=this.departmentService.AddUserInfo(deptID.Value, selected);
            if (count > 0)
                return "成功添加" + count.ToString() + "名成员!";
            else
                return "成员添加失败!";
        }
        public void RemoveUser(int? deptID, int[] selected)
        {
            if (deptID == null || selected == null || selected.Length <= 0)
                return;
            this.departmentService.RemoveUserInfo(deptID.Value, selected);
        }
    }
}