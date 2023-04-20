using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Teacher : User
    {

        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public virtual IList<Subject> Subjects { get; set; } 
        public string Title { get; set; }
        public Teacher(string firstName = "", string lastName = "") : base(firstName, lastName)
        {
        }

    }
}
