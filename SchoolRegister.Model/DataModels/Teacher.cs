using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Teacher : User
    {
        //I didn't perform migration after commenting out these two properties,
        //becouse User class already has them and I think it should work well

        //public string FirstName { get; set; } 
        //public string LastName { get; set; } 
        public virtual IList<Subject> Subjects { get; set; } 
        public string Title { get; set; }
        public Teacher(string firstName = "", string lastName = "") : base(firstName, lastName)
        {
        }

    }
}
