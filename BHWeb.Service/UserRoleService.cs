using BHWeb.Dao.Model;
using BHWeb.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHWeb.Service
{
    public class UserRoleService:BaseService<UserRole,UserRoleDataObject>
    {
        public override UserRoleDataObject Update(UserRoleDataObject model)
        {
            UserRole userRole = this.DataEntity.FirstOrDefault(p => p.ID == model.ID);
            if (userRole == null)
                return base.Update(model);
            else
            {
                userRole.Name = model.Name;
                this.entity.SaveChanges();
                return BHMapper.Map<UserRole, UserRoleDataObject>(userRole);
            }
        }

        public void AddRights(int userRoleID, int[] selected)
        {
            UserRole userRole = this.DataEntity.FirstOrDefault(p => p.ID == userRoleID);
            List<FuncModule> funcList = this.entity.FuncModule.Where(p => selected.Contains(p.ID)).ToList();
            userRole.FunctionList.AddRange(funcList);
            this.entity.SaveChanges();
        }

        public void RemoveRights(int userRoleID, int[] selected)
        {
            UserRole userRole = this.DataEntity.FirstOrDefault(p => p.ID == userRoleID);
            userRole.FunctionList.RemoveAll(p=>selected.Contains(p.ID));            
            this.entity.SaveChanges();
        }

        public void addUsers(int userRoleID, int[] selected)
        {
            UserRole userRole = this.DataEntity.FirstOrDefault(p => p.ID == userRoleID);
            List<UserInfo> userList = this.entity.UserInfo.Where(p => selected.Contains(p.ID)).ToList();
            userRole.UserList.AddRange(userList);
            this.entity.SaveChanges();
        }
        public void RemoveUsers(int userRoleID, int[] selected)
        {
            UserRole userRole = this.DataEntity.FirstOrDefault(p => p.ID == userRoleID);
            userRole.UserList.RemoveAll(p => selected.Contains(p.ID));
            this.entity.SaveChanges();
        }
    }
}
