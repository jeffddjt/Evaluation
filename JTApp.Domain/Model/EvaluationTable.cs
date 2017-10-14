using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class EvaluationTable:AggregateRoot
    {
        public int Year { get; set; }
        public virtual BeMeasured BeMeasured { get; set; }
        public int BeMeasuredID { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public int UserInfoID { get; set; }
        public double Ratio { get; set; }
        public bool Submit { get; set; }
        public double Score { get; set; }
        public virtual List<EvaluationTableDetail> EvaluationTableDetail { get; set; }
    }
}
