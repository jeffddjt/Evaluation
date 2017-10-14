using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class DepartmentDataObject:DataObjectBase
    {
        public string Name { get; set; }
        public List<DepartmentDataObject> Children { get; set; }
        public int ParentID { get; set; }
        public List<UserInfoDataObject> UserList { get; set; }

        public List<UserInfoDataObject> GetAllUserList()
        {
            List<UserInfoDataObject> list = new List<UserInfoDataObject>();
            list.AddRange(this.UserList);
            foreach (DepartmentDataObject child in Children)
            {
                list.AddRange(child.GetAllUserList());
            }
            return list;
        }
        public int[] GetIDArray()
        {
            List<int> list = new List<int>();
            list.Add(this.ID);
            foreach (DepartmentDataObject child in Children)
            {
                list.AddRange(child.GetIDArray());
            }
            return list.OrderBy(p => p).ToArray();
        }
    }
}
