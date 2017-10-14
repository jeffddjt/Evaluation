using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BHWeb.Service
{
    public class DepartmentService:BaseService<Department,DepartmentDataObject>
    {
        public override DepartmentDataObject Update(DepartmentDataObject model)
        {
            if (model.ID == 0)
                return Add(model);
            Department parent = this.DataEntity.FirstOrDefault(p => p.ID == model.ParentID);
            Department curr = this.DataEntity.FirstOrDefault(p => p.ID == model.ID);
            curr.Name = model.Name;
            curr.Parent = parent;
            this.entity.SaveChanges();
            return model;
        }
        public override DepartmentDataObject Add(DepartmentDataObject model)
        {
            if (model.ParentID == 0)
                return base.Add(model);
            Department dept = this.DataEntity.FirstOrDefault(p => p.ID == model.ParentID);
            Department deptAdd = this.DataEntity.Create();
            deptAdd.Name = model.Name;
            dept.Children.Add(deptAdd);
            this.entity.SaveChanges();
            return BHMapper.Map<Department, DepartmentDataObject>(deptAdd);            
        }
        public override int RemoveById(int id)
        {
            Department dept = this.DataEntity.FirstOrDefault(p => p.ID == id);
            Department parent = dept.Parent;
            this.DataEntity.Remove(dept);
            this.entity.SaveChanges();
            return parent == null ? 0 : parent.ID;
        }

        public int AddUserInfo(int deptID, int[] userIDs)
        {
            try
            {
                Department dept = this.DataEntity.FirstOrDefault(p => p.ID == deptID);
                List<UserInfo> userList = this.entity.UserInfo.Where(p => userIDs.Contains(p.ID)).ToList();
                dept.UserList.AddRange(userList);
                this.entity.SaveChanges();
                return userIDs.Length;
            }
            catch
            {
                return 0;
            }
        }

        public void RemoveUserInfo(int deptId, int[] userIDs)
        {
            Department dept = this.DataEntity.FirstOrDefault(p => p.ID == deptId);
            dept.UserList.RemoveAll(p => userIDs.Contains(p.ID));
            this.entity.SaveChanges();
        }
    }
}
