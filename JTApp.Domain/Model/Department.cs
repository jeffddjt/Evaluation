using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class Department:AggregateRoot
    {
        public string Name { get; set; }
        public virtual List<Department> Children { get; set; }
        public virtual Department Parent { get; set; }
        public virtual List<UserInfo> UserList { get; set; }
    }
}
