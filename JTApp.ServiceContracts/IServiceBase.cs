using JTApp.DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.ServiceContracts
{
    public interface IServiceBase<TDataObject> where TDataObject:DataObjectBase
    {
        TDataObject GetFirst();
        IList<TDataObject> GetList();
        IList<TDataObject> GetList(int[] ids);
        TDataObject GetOne(int id);
        TDataObject Update(TDataObject dataObject);
        TDataObject Add(TDataObject dataObject);
        int RemoveById(int id);
        int GetCount();
        int RemoveByIds(int[] ids);
        int RemoveAll();
        bool Exists(int id);
    }
}
