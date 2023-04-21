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
            if(addGradeToStudentVm == null)
                throw new ArgumentNullException($"AddGradeToStudentVm is null");
            var teacherEntity = DbContext.Users.FirstOrDefault(x => x.Id == addGradeToStudentVm.TeacherId);
            _userManager.IsInRoleAsync(teacherEntity, "Teacher");
            //if(addGradeToStudentVm.TeacherId)
            var gradeEntity = Mapper.Map<Grade>(addGradeToStudentVm);
            DbContext.Grades.Add(gradeEntity);

        }

        public GradesReportVm GetGradesReportForStudent(GetGradesReportVm getGradesVm)
        {
            throw new NotImplementedException();
        }
    }
}
