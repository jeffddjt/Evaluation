using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class FuncModule:AggregateRoot
    {
        public string ModuleName { get; set; }
        public virtual List<UserRole> UserRole { get; set; }
    }
}
