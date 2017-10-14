using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class TimeOverDataObject:DataObjectBase
    {
        public int Year { get; set; }
        public string Date { get; set; }
        public int Hour { get; set; }
        public DateTime Expire
        {
            get
            {
                return DateTime.Parse(Date).AddHours(Hour);
            }
        }
    }
}
