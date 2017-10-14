using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class FuncModuleDataObject:DataObjectBase
    {
        public string ModuleName { get; set; }
        public List<int> UserRoleIDList { get; set; }
    }
}
