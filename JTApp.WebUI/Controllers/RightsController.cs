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
    public class RightsController : Controller
    {
        private IUserRoleService userRoleService;
        private IFunctionService functionService;
        private IUserInfoService userInfoService;
        public RightsController()
        {
            this.userRoleService = ServiceLocator.Instance.GetRef<IUserRoleService>();
            this.functionService = ServiceLocator.Instance.GetRef<IFunctionService>();
            this.userInfoService = ServiceLocator.Instance.GetRef<IUserInfoService>();
        }
        public ActionResult RoleManage()
        {
            IList<UserRoleDataObject> userRoleList = this.userRoleService.GetList();
            ViewData["UserRoleList"] = userRoleList;
            return View();
        }
        public ActionResult EditRole(int? id)
        {
            UserRoleDataObject userRole;
            if (id == null)
            {
                userRole = new UserRoleDataObject();
            }
            else
            {
                userRole = this.userRoleService.GetOne(id.Value);
            }
            ViewData["UserRole"] = userRole;
            return View();
        }
        public ActionResult SaveUserRole(UserRoleDataObject userRole)
        {
            userRole = this.userRoleService.Update(userRole);
            ViewData["UserRole"] = userRole;
            return Redirect("EditRole?id="+userRole.ID);
        }
        public ActionResult AddRights(int? id)
        {
            if (id == null)
                return RedirectToAction("ShowError", "Error", new { Msg = "没有选择角色！" });
            UserRoleDataObject userRole = this.userRoleService.GetOne(id.Value);
            int[] ids = userRole.FunctionList.Select(p => p.ID).ToArray();
            IList<FuncModuleDataObject> functionList = this.functionService.GetList(ids);
            ViewData["UserRoleID"] = userRole.ID;
            ViewData["FunctionList"] = functionList;
            return View();
        }
        public ActionResult AddRightsToUserRole(int? userRoleID, int[] selected)
        {
            if (userRoleID == null)
                return RedirectToAction("ShowError", "Error", new { Msg = "没有选择角色！" });
            this.userRoleService.AddRights(userRoleID.Value, selected);
            return Redirect("EditRole?id=" + userRoleID.Value);

        }
        public ActionResult RemoveRights(int? userRoleID, int[] selected)
        {
            if (userRoleID == null)
                return RedirectToAction("ShowError", "Error", new { Msg = "没有选择角色！" });
            this.userRoleService.RemoveRights(userRoleID.Value, selected);
            return Redirect("EditRole?id=" + userRoleID.Value);
        }
        public ActionResult AddUser(int? pageSize,int? pageIndex,int? id)
        {
            if (id == null)
                return RedirectToAction("ShowError", "Error", new { Msg = "没有选择角色！" });
            pageSize = pageSize ?? 20;
            if (pageIndex == null || pageIndex.Value <= 0)
                pageIndex = 1;

            UserRoleDataObject userRole = this.userRoleService.GetOne(id.Value);
            int[] ids = userRole.UserList.Select(p => p.ID).ToArray();
            IList<UserInfoDataObject> userList = this.userInfoService.GetList(ids);
            int recordCount = userList.Count;
            int pageCount = (recordCount + pageSize.Value - 1) / pageSize.Value;

            ViewData["UserRoleID"] = userRole.ID;
            ViewData["PageSize"] = pageSize.Value;
            ViewData["PageCount"] = pageCount;
            ViewData["PageIndex"] = pageIndex.Value;
            ViewData["RecordCount"] = recordCount;
            ViewData["UserList"] = userList.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value).ToList();
            return View();
        }
        public ActionResult AddUserToUserRole(int? userRoleID,int[] selected)
        {
            if (userRoleID == null)
                return RedirectToAction("ShowError", "Error", new { Msg = "没有选择角色！" });
            this.userRoleService.addUsers(userRoleID.Value, selected);
            return Redirect("EditRole?id=" + userRoleID);
        }
        public ActionResult RemoveUser(int? userRoleID, int[] selected)
        {
            if (userRoleID == null)
                return RedirectToAction("ShowError", "Error", new { Msg = "没有选择角色！" });
            this.userRoleService.RemoveUsers(userRoleID.Value, selected);
            return Redirect("EditRole?id=" + userRoleID.Value);
        }
        public ActionResult RemoveRole(int? id)
        {
            this.userRoleService.RemoveById(id.Value);
            return Redirect("RoleManage");
        }
    }
}