using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class BeMeasured:AggregateRoot
    {
        public virtual UserInfo UserInfo { get; set; }
        public virtual List<Measured> MeasuredList { get; set; }
        public virtual List<EvaluationTable> EvaluationTableList { get; set; }
        public virtual List<StyleOfWork> StyleOfWorkList { get; set; }
    }
}
