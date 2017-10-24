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

        public void SetLevel(int level)
        {
            EvaluationLevel el = this.Repository.GetFirst();
            if (el == null)
            {
                el = this.Repository.Create();
                el.Level = level;
                this.Repository.Add(el);
            }
            else
            {
                el.Level = level;
                this.Repository.Update(el);
            }
            this.Repository.Commit();
        }
    }
}
