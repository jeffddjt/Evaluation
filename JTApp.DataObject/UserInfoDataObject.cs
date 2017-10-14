using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTApp.DataObject
{
    public class UserInfoDataObject:DataObjectBase
    {
        public string WorkNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Professional { get; set; }
        public int DutiesID { get; set; }
        public bool MajorLeader { get; set; }
        public bool Director { get; set; }
        public bool Instructor { get; set; }
        public bool Secretary { get; set; }
        public List<string> FunctionList { get; set; }
        public int Submited { get; set; }
        public int StyleOfWorkSubmit { get; set; }
        public int[] BeMeasuredUserInfoIDs { get; set; }
        public object DutiesName { get; set; }
    }
}
