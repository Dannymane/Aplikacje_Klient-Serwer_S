using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        private UserManager<User> _userManager;


        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager) :
            base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }
        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException($"FilterExpression is null");
                var TeacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterExpression);
                var TeacherVm = Mapper.Map<TeacherVm>(TeacherEntity);
                return TeacherVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>>? filterxpression = null)
        {
            try
            {
                var teacherEntities = DbContext.Users.OfType<Teacher>().Where(filterxpression ?? (x => true));
                var teacherVms = Mapper.Map<IEnumerable<TeacherVm>>(teacherEntities);
                return teacherVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
            try
            {
                if (getTeachersGroups == null)
                    throw new ArgumentNullException($"View model parameter is null");
                var teacherEntity = DbContext.Users.OfType<Teacher>()
                    .FirstOrDefault(x => x.Id == getTeachersGroups.TeacherId);
                if (teacherEntity == null)
                    throw new ArgumentException($"Teacher with id {getTeachersGroups.TeacherId} does not exist");
                getTeachersGroups = Mapper.Map<TeachersGroupsVm>(teacherEntity);
                return getTeachersGroups.Groups;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
