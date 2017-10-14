﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.Domain.Model
{
    public class UserRole:AggregateRoot
    {
        public string Name { get; set; }
        public virtual List<FuncModule> FunctionList { get; set; }
        public virtual List<UserInfo> UserList { get; set; }
    }
}
