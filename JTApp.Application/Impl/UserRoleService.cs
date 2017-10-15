using JTApp.DataObject;
using JTApp.Domain.Model;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;

namespace JTApp.Application.Impl
{
    public class UserRoleService : ServiceBase<UserRoleDataObject, UserRole>, IUserRoleService
    {
        public UserRoleService(IRepository<UserRole> repository) : base(repository)
        {
        }

        public void AddRights(int userRoleID, int[] selected)
        {
            UserRole userRole = this.Repository.FindByID(userRoleID);
            IList<FuncModule> funcList = this.Repository.Context.DoGet<FuncModule>(p => selected.Contains(p.ID)).ToList();
            userRole.FunctionList.AddRange(funcList);
            this.Repository.Update(userRole);
            this.Repository.Commit();
        }

        public void addUsers(int userRoleID, int[] selected)
        {
            UserRole userRole = this.Repository.FindByID(userRoleID);
            IList<UserInfo> userList = this.Repository.Context.DoGet<UserInfo>(p => selected.Contains(p.ID)).ToList();
            userRole.UserList.AddRange(userList);
            this.Repository.Update(userRole);
            this.Repository.Commit();
        }

        public void RemoveRights(int userRoleID, int[] selected)
        {
            UserRole userRole=this.Repository.FindByID(userRoleID);
            IList<FuncModule> funcList = userRole.FunctionList.Where(p => selected.Contains(p.ID)).ToList();
            foreach (FuncModule func in funcList)
            {
                userRole.FunctionList.Remove(func);
            }
            this.Repository.Update(userRole);
            this.Repository.Commit();
        }

        public void RemoveUsers(int userRoleID, int[] selected)
        {
            UserRole userRole = this.Repository.FindByID(userRoleID);
            IList<UserInfo> userList = userRole.UserList.Where(p => selected.Contains(p.ID)).ToList();
            foreach (UserInfo user in userList)
            {
                userRole.UserList.Remove(user);
            }
            this.Repository.Update(userRole);
            this.Repository.Commit();
        }
    }
}
