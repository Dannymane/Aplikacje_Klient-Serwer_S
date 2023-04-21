using SchoolRegister.Model.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.ViewModels.VM
{
    public class AddGradeToStudentVm
    {
        public DateTime DateOfIssue { get; set; } = DateTime.Now;
        [Required]
        public GradeScale GradeValue { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public int TeacherId { get; set; }
    }
}
