using JTApp.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.ServiceContracts
{
    public interface IEvaluationTableDetailService : IServiceBase<EvaluationTableDetailDataObject>
    {
        void Update(int detailID, double score);
    }
}
