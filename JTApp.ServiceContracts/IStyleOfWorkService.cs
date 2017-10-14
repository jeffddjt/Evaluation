using JTApp.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.ServiceContracts
{
    public interface IStyleOfWorkService : IServiceBase<StyleOfWorkDataObject>
    {
        StyleOfWorkDataObject GetOne(int beMeasuredUserInfoID, int userInfoID, int year);
        StyleOfWorkDataObject Add(int beMeasuredID, int userInfoID);
        StyleOfWorkDataObject Update(int id, int score);
    }
}
