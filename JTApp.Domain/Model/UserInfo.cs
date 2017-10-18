using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace JTApp.Domain.Model
{
    public class UserInfo : AggregateRoot
    {
        public string WorkNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Professional { get; set; }
        public virtual int? DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        public virtual int? DutiesID { get; set; }
        public virtual Duties Duties { get; set; }
        public bool MajorLeader { get; set; }
        public bool Director { get; set; }
        public bool Instructor { get; set; }
        public bool Secretary { get; set; }
        //学工办主任
        public bool General { get; set; }
        //民主党派
        public bool Democratic { get; set; }

        public virtual List<Measured> MeasuredList { get; set; }
        public virtual List<UserRole> UserRole { get; set; }
        public virtual List<EvaluationTable> EvaluationTable { get; set; }
        public virtual List<StyleOfWork> StyleOfWork { get; set; }
    }
}
