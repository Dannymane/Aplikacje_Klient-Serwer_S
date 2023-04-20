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
        public virtual Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public Grade(DateTime dateOfIssue = new DateTime(), GradeScale gradeValue = 0, int subjectId = 0, int studentId = 0) //do i need constructor? 
        {//i cant initiate migration when constructor has a SUbject and Student argument
            DateOfIssue = dateOfIssue;
            GradeValue = gradeValue;
            SubjectId = subjectId;
            StudentId = studentId;
        }
    }
}
