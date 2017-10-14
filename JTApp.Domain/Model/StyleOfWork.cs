using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class StyleOfWork:AggregateRoot
    {
        public int Year { get; set; }
        public virtual BeMeasured BeMeasured { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public double Ratio { get; set; }
        public int Score { get; set; }
        public int BeMeasuredID { get; set; }
    }
}
