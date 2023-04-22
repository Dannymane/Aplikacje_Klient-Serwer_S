using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentVm> GetStudentAsync(Expression<Func<Student, bool>> filterExpression);
        Task<IEnumerable<StudentVm>> GetStudentsAsync(Expression<Func<Student, bool>>? filterExpression = null);
    }
}
