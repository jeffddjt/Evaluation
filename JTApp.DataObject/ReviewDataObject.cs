using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class ReviewDataObject:DataObjectBase
    {
        public int Year { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }
        public string Content { get; set; }
        public int Sort { get; set; }
        public virtual int ParentID { get; set; }
        public virtual List<ReviewDataObject> Children { get; set; }
    }
}
