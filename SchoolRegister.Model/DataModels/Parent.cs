﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Parent : User
    {
        public virtual IList<Student> Students { get; set; }
        public Parent(string firstName, string lastName) : base(firstName, lastName)
        {

        }
    }
}
