using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class EvaluationTableDetailDataObject:DataObjectBase
    {
        public int EvaluationID { get; set; }
        public int ReviewID { get; set; }
        public string ReviewName { get; set; }
        public int ReviewParentID { get; set; }
        public double ReviewScore { get; set; }
        public string ReviewContent { get; set; }
        public int Sort { get; set; }
        public double Score { get; set; }
    }
}
