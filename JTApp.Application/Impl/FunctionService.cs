using JTApp.DataObject;
using JTApp.Domain.Model;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;
using JTApp.Application.AutoMap;

namespace JTApp.Application.Impl
{
    public class FunctionService : ServiceBase<FuncModuleDataObject, FuncModule>, IFunctionService
    {
        public FunctionService(IRepository<FuncModule> repository) : base(repository)
        {
        }
        public override IList<FuncModuleDataObject> GetList(int[] ids)
        {
            return JTMapper.Map<IList<FuncModule>, IList<FuncModuleDataObject>>(this.Repository.Get(p => !ids.Contains(p.ID)).ToList());
        }
    }
}
