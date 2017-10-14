using JTApp.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.ServiceContracts
{
    public interface IReviewService : IServiceBase<ReviewDataObject>
    {
        IList<ReviewDataObject> GetList(int year);
    }
}
