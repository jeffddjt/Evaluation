using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.DataObject;

namespace JTApp.ServiceContracts
{
    public interface IDepartmentService:IServiceBase<DepartmentDataObject>
    {
        IList<TreeNodeDataObject> GetTreeNodes();
        int AddUserInfo(int departmentID, int[] selected);
        void RemoveUserInfo(int departmentID, int[] selected);
        IList<DepartmentDataObject> GetRootList();        
    }
}
