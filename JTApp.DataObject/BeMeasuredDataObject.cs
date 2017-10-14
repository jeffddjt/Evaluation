using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class BeMeasuredDataObject:DataObjectBase
    {
        public virtual UserInfoDataObject UserInfo { get; set; }
        public virtual List<EvaluationTableDataObject> EvaluationTableList { get; set; }
        public virtual List<MeasuredDataObject> MeasuredList { get; set; }
        public virtual List<StyleOfWorkDataObject> StyleOfWorkList { get; set; }
    }
}
