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
    public class DutiesService : ServiceBase<DutiesDataObject, Duties>, IDutiesService
    {
        public DutiesService(IRepository<Duties> repository) : base(repository)
        {
        }
    }
}
