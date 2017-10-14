using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class DutiesDataObject:DataObjectBase
    {
        public string Name { get; set; }
        public List<UserInfoDataObject> UserList { get; set; }
    }
}
