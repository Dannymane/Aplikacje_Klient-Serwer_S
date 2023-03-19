using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SchoolRegister.Model.DataModels
{
    public class User : IdentityUser<int> //iternal class??? Then anouther assemblies dont have an access? ChatGPT: Yes
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public User(string firstName, string lastName) {
            FirstName = firstName;
            LastName = lastName;
            RegistrationDate = DateTime.Now;
        }
    }
}
