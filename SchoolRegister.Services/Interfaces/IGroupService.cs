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
    public interface IGroupService
    {
        Task<GroupVm> AddOrUpdateGroupAsync(AddOrUpdateGroupVm addOrEditGroupVm);
        Task<StudentVm> AttachStudentToGroupAsync(AttachDetachStudentToGroupVm attachStudentToGroupVm);
        Task<GroupVm> AttachSubjectToGroupAsync(AttachDetachSubjectGroupVm attachSubjectGroupVm);
        Task<SubjectVm> AttachTeacherToSubjectAsync(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        Task<StudentVm> DetachStudentFromGroupAsync(AttachDetachStudentToGroupVm detachStudentFromGroupVm);
        Task<GroupVm> DetachSubjectFromGroupAsync(AttachDetachSubjectGroupVm detachSubjectGroupVm);
        Task<SubjectVm> DetachTeacherFromSubjectAsync(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm);
        Task<GroupVm> GetGroupAsync(Expression<Func<Group, bool>> filterExpression);
        Task<IEnumerable<GroupVm>> GetGroupsAsync(Expression<Func<Group, bool>>? filterExpression = null);

    }
}
