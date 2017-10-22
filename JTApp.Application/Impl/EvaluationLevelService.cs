using JTApp.DataObject;
using JTApp.Domain.Model;
using JTApp.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Domain;
using JTApp.Domain.Repository;

namespace JTApp.Application.Impl
{
    public class EvaluationLevelService : ServiceBase<EvaluationLevelDataObject, EvaluationLevel>, IEvaluationLevelService
    {
        public EvaluationLevelService(IEvaluationLevelRepository repository) : base(repository)
        {
        }
    }
}
