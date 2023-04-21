using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class TeachersGroupsVm : UserVm
    {
        public int TeacherId
        {
            get{ return Id; }
            set{ Id = value;}
        }
        public IList<GroupVm> Groups { get; set; } = null!;
    }
}
