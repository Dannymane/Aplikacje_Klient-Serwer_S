using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class GradeVm
    {
        public DateTime DateOfIssue { get; set; }
        public GradeScale GradeValue { get; set; }
        public virtual SubjectVm Subject { get; set; } = null!;
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public virtual StudentVm Student { get; set; } = null!;
    }
}
