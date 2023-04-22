using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class AddOrUpdateGroupVm
    {
        public int? Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "NameRequiredMsg")]
        public string Name { get; set; }
    }
}