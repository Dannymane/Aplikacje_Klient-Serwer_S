using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using SchoolRegister.DAL.EF;
using SchoolRegister.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        private UserManager<User> _userManager;
        public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }

        public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            try
            {
                if (addGradeToStudentVm == null)
                    throw new ArgumentNullException($"AddGradeToStudentVm is null");

                var userEntity = DbContext.Users.FirstOrDefault(x => x.Id == addGradeToStudentVm.TeacherId);
                if (userEntity == null)
                    throw new ArgumentNullException($"There is no user(teacher) with id {addGradeToStudentVm.TeacherId}");

                var teacherEntity = DbContext.Users.FirstOrDefault(x => x.Id == addGradeToStudentVm.TeacherId);
                if (!_userManager.IsInRoleAsync(teacherEntity!, "Teacher").Result)
                    throw new ArgumentNullException($"Olny teacher can estimate student");

                var studentEntity = DbContext.Users.OfType<Student>().FirstOrDefault(x => x.Id == addGradeToStudentVm.StudentId);
                if (studentEntity == null)
                    throw new ArgumentNullException($"There is no student with id {addGradeToStudentVm.StudentId}");

                var gradeEntity = Mapper.Map<Grade>(addGradeToStudentVm);
                DbContext.Grades.Add(gradeEntity);
                DbContext.SaveChanges();

                var gradeVm = Mapper.Map<GradeVm>(addGradeToStudentVm);
                return gradeVm;
            } catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            try
            {
                if (getGradesVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var studentEntity = DbContext.Users.OfType<Student>().FirstOrDefault(x => x.Id == getGradesVm.StudentId);
                if (studentEntity == null)
                    throw new ArgumentNullException($"There is no student with id {getGradesVm.StudentId}");

                var getterUserEntity = DbContext.Users.FirstOrDefault(x => x.Id == getGradesVm.GetterUserId);
                if (getterUserEntity == null)
                    throw new ArgumentNullException($"There is no user(getter) with id {getGradesVm.GetterUserId}");

                var grades = DbContext.Grades.Where(x => x.StudentId == getGradesVm.StudentId).ToList();
                var gradesVms = Mapper.Map<IEnumerable<GradeVm>>(grades);
                var gradesReportVm = new GradesReportVm
                {
                    Grades = gradesVms,
                    StudentId = getGradesVm.StudentId,
                    StudentFullName = $"{studentEntity.FirstName} {studentEntity.LastName}"
                };
                if (getGradesVm.StudentId == getGradesVm.GetterUserId)
                    return gradesReportVm   ;

                if (_userManager.IsInRoleAsync(getterUserEntity, "teacher").Result)
                    return gradesReportVm;

                if (studentEntity.ParentId == getGradesVm.GetterUserId)
                    return gradesReportVm;

                throw new UnauthorizedAccessException("This user doesn't have permission to see student's grades");
            } catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }

            }
    }
}
