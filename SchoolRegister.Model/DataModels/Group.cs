using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Student> Students { get; set; }
        public IList<SubjectGroup> SubjectGroups { get; set; }
        public Group(int id, string name, IList<Student> students, IList<SubjectGroup> subjectGroups)
        {
            Id = id;
            Name = name;
            Students = students;
            SubjectGroups = subjectGroups;
        }
    }
}
