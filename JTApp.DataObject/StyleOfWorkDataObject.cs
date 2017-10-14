using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class StyleOfWorkDataObject:DataObjectBase
    {
        public int Year { get; set; }
        public virtual UserInfoDataObject Userinfo { get; set; }
        public double Ratio { get; set; }
        public int Score { get; set; }        
        public int BeMeasuredID { get; set; }
        public string BeMeasuredUserInfoID { get; set; }
        public string BeMeasuredUserName { get; set; }
    }
}
