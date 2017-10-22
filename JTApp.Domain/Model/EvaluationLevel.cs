using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class EvaluationLevel:AggregateRoot
    {
        public int Level { get; set; }
    }
}
