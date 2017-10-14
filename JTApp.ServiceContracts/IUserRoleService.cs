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
        void AddRights(int value, int[] selected);
        void RemoveRights(int value, int[] selected);
        void addUsers(int value, int[] selected);
        void RemoveUsers(int value, int[] selected);
    }
}
