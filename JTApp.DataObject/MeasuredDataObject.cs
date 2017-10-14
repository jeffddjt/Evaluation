using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class MeasuredDataObject
    {
        public double Ratio { get; set; }
        public int BeMeasuredID { get; set; }
        public virtual List<UserInfoDataObject> UserList { get; set; }
    }
}
