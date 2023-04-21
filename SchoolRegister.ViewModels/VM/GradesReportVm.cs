using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class GradesReportVm
    {
        public string StudentFullName { get; set; } = null!;

        public int Studentid { get; set; }
        public IEnumerable<GradeVm>? Grades { get; set; }
    }
}
