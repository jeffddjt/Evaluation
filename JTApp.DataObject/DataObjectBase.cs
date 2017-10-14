using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class DataObjectBase
    {
        public int ID { get; set; }

        public override string ToString()
        {
            string str = string.Empty;
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(this, null);
                str += string.Format("{0}:{1},", property.Name, value);
            }
            return str.Remove(str.Length - 1);
        }
    }
}
