using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class AddOrUpdateTeacherVm : UserVm
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public virtual IList<SubjectVm> Subjects { get; set; } = null!;
        [Required]
        public string Title { get; set; }

    }
}
