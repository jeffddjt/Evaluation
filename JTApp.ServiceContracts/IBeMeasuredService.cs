using JTApp.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JTApp.Infrastructure.Common;

namespace JTApp.ServiceContracts
{
    public interface IBeMeasuredService : IServiceBase<BeMeasuredDataObject>
    {
        IList<BeMeasuredDataObject> GetList(JTPager pager);
        int Add(int[] selected);
        int AddUserInfo(int[] selected, int ratio, int beMeasuredID);
        void RemoveMeasuredUser(int[] selected, int beMeasuredID);
        IList<BeMeasuredDataObject> GetTotal(int[] ids, int year, JTPager pager);
        List<BeMeasuredDataObject> GetList(int[] ids, int year);
        void ModifyRatio(int beMeasuredID, int userID, double ratio);
    }
}
