using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class TreeNodeDataObject:DataObjectBase
    {
        public string text { get; set; }
        public List<TreeNodeDataObject> nodes { get; set; }
    }
}
