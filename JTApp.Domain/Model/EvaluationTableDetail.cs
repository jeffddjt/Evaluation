using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class EvaluationTableDetail:AggregateRoot
    {
        public virtual EvaluationTable EvaluationTable { get; set; }
        public int EvaluationTableID { get; set; }
        public virtual Review Review { get; set; }
        public double Score { get; set; }
    }
}
