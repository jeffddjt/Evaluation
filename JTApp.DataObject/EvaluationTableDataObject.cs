using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class EvaluationTableDataObject:DataObjectBase
    {
        public int Year { get; set; }
        public int BeMeasuredID { get; set; }
        public int BeMeasuredUserInfoID { get; set; }
        public int UserInfoID { get; set; }
        public double Ratio { get; set; }
        public bool Submit { get; set; }
        public double Score { get; set; }
        public virtual List<EvaluationTableDetailDataObject> EvaluationTableDetail { get; set; }
    }
}
