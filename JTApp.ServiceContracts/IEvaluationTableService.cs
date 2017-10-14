using JTApp.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.ServiceContracts
{
    public interface IEvaluationTableService : IServiceBase<EvaluationTableDataObject>
    {
        EvaluationTableDataObject GetOne(int beMeasuredUserInfoID, int userInfoID, int year);
        EvaluationTableDataObject Add(int beMeasuredID, int userInfoID);
        void Update(int evalID);
    }
}
