using JTApp.DataObject;
using JTApp.Domain.Model;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;
using JTApp.Application.AutoMap;

namespace JTApp.Application.Impl
{
    public class DepartmentService : ServiceBase<DepartmentDataObject, Department>, IDepartmentService
    {
        public DepartmentService(IRepository<Department> repository) : base(repository)
        {
        }

        public override DepartmentDataObject Add(DepartmentDataObject dataObject)
        {
            Department dept = this.Repository.Create();
            dept.Name = dataObject.Name;
            if (dataObject.ParentID != 0)
                dept.Parent = this.Repository.FindByID(dataObject.ParentID);
            this.Repository.Add(dept);
            this.Repository.Commit();
            return JTMapper.Map<Department, DepartmentDataObject>(dept);
        }
        public override DepartmentDataObject Update(DepartmentDataObject dataObject)
        {
            Department dept = this.Repository.FindByID(dataObject.ID);
            dept.Name = dataObject.Name;
            if (dataObject.ParentID != 0)
                dept.Parent = this.Repository.FindByID(dataObject.ParentID);
            this.Repository.Update(dept);
            this.Repository.Commit();
            return JTMapper.Map<Department, DepartmentDataObject>(dept);

        }
        public int AddUserInfo(int departmentID, int[] selected)
        {
            Department dept = this.Repository.FindByID(departmentID);
            IList<UserInfo> userInfoList = this.Repository.Context.DoGet<UserInfo>(p => selected.Contains(p.ID)).ToList();
            dept.UserList.AddRange(userInfoList);
            return this.Repository.Commit();
        }

        public IList<DepartmentDataObject> GetRootList()
        {
            return JTMapper.Map<IList<Department>, IList<DepartmentDataObject>>(this.Repository.Get(p => p.Parent == null).ToList());
        }

        public IList<TreeNodeDataObject> GetTreeNodes()
        {
            return JTMapper.Map<IList<DepartmentDataObject>, IList<TreeNodeDataObject>>(this.GetRootList());
        }


        public void RemoveUserInfo(int departmentID, int[] selected)
        {
            Department dept = this.Repository.FindByID(departmentID);
            IList<UserInfo> userList = dept.UserList.Where(p => selected.Contains(p.ID)).ToList();
            foreach (UserInfo user in userList)
            {
                dept.UserList.Remove(user);
            }
            this.Repository.Commit();
        }
    }
}
