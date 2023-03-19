using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Teacher : User
    {
        public IList<Subject> Subjects { get; set; }
        public string Title { get; set; }
        public Teacher(string firstName_, string lastName_, string title_, IList<Subject> subjects_) : 
            base(firstName_, lastName_)
        {
            Title = title_;
            Subjects = subjects_;
        }

    }
}
