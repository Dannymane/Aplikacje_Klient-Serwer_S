using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class AttachDetachSubjectGroupVm
    {
        [Required]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "GroupIdRequiredMsg")]
        public int GroupId { get; set; }
    }
}
