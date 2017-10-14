using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class TimeOver:AggregateRoot
    {
        public int Year { get; set; }
        public DateTime DateTime { get; set; }
    }
}
