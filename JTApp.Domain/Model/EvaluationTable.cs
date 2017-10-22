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
        public virtual List<EvaluationTableDetail> EvaluationTableDetail { get; set; }
        public double Score
        {
            get
            {
                return this.EvaluationTableDetail != null && this.EvaluationTableDetail.Count > 0 
                    ? 
                    this.EvaluationTableDetail.Where(p=>p.Review.Parent==null).Sum(p => p.Score)
                    : 0;
            }
        }
    }
}
