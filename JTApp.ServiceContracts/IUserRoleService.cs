using JTApp.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.ServiceContracts
{
    public interface IUserRoleService : IServiceBase<UserRoleDataObject>
    {
        void AddRights(int userRoleID, int[] selected);
        void RemoveRights(int userRoleID, int[] selected);
        void addUsers(int userRoleID, int[] selected);
        void RemoveUsers(int userRoleID, int[] selected);
    }
}
