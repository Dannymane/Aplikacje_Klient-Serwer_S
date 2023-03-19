using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<SubjectGroup> SubjectGroups { get; set; }
        public Teacher Tracher {get; set;}
        public int? TeacherId { get; set; }
        public IList<Grade> Grades { get; set; }
        public Subject(int id, string name, string description, IList<SubjectGroup> subjectGroups, Teacher tracher,
            int? teacherId, IList<Grade> grades)
        {
            Id = id;
            Name = name;
            Description = description;
            SubjectGroups = subjectGroups;
            Tracher = tracher;
            TeacherId = teacherId;
            Grades = grades;
        }
    }
}
