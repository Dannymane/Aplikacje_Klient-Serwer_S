using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class StudentVm : UserVm
    {
        public  string GroupName { get; set; } = null!;
        public int? GroupId { get; set; }
        public virtual IList<GradeVm> Grades { get; set; }
        //Parent object in Student class --> ParentName (or any anouther name)
        public virtual string ParentName { get; set; } = null!;//because I want to displaj parent name and surname
        public int? ParentId { get; set; }

    }
}
