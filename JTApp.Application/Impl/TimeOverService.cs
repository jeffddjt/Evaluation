using JTApp.DataObject;
using JTApp.Domain.Model;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;

namespace JTApp.Application.Impl
{
    public class TimeOverService : ServiceBase<TimeOverDataObject, TimeOver>, ITimeOverService
    {
        public TimeOverService(IRepository<TimeOver> repository) : base(repository)
        {
        }
    }
}
