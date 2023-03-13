using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Parent: User
    {
        public List<Student>? Students { get; set; }
        public Parent(string? firstName_, string? lastName_, List<Student>? students_ = null) : base(firstName_, lastName_)
        {
            Students = students_;

        }
    }
}
