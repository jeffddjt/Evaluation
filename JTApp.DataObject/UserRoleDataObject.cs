using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class UserRoleDataObject:DataObjectBase
    {
        public string Name { get; set; }
        public List<FuncModuleDataObject> FunctionList { get; set; }
        public List<UserInfoDataObject> UserList { get; set; }

        public UserRoleDataObject()
        {
            this.FunctionList = new List<FuncModuleDataObject>();
            this.UserList = new List<UserInfoDataObject>();
        }
    }
}
