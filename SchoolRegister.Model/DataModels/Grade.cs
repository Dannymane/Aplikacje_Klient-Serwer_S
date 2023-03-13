using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Model.DataModels
{
    public class Grade
    {
        public DateTime DateOfIssue { get; set; }
        public GradeScale GradeValue { get; set; }
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public Grade(DateTime dateOfIssue, GradeScale gradeValue, Subject subject, int subjectId, int studentId, Student student)
        {
            DateOfIssue = dateOfIssue;
            GradeValue = gradeValue;
            Subject = subject;
            SubjectId = subjectId;
            StudentId = studentId;
            Student = student;
        }
    }
}
