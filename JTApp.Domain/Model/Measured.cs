using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class Measured:AggregateRoot
    {        
        public double Ratio { get; set; }
        public virtual BeMeasured BeMeasured { get; set; }
        public virtual List<UserInfo> UserList { get; set; }
    }
}
