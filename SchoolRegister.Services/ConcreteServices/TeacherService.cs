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
        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) :
            base(dbContext, mapper, logger)
        {
        }
        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException($"FilterExpression is null");
                var teacher = DbContext.Users
                    .Where(u => u.UserType == (int)RoleValue.Teacher) // filter only Teacher users
                    .OfType<Teacher>() // cast to Teacher type
                    .FirstOrDefault(filterExpression); // apply the filter expression
                var teacherEntity = DbContext.Teachers.FirstOrDefault(filterExpression);
                var teacherVm = Mapper.Map<TeacherVm>(teacherEntity);
                DbContext.Users

                return teacherVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterxpression = null)
        {
            try
            {
                var teacherEntities = DbContext.Teachers.Where(filterxpression ?? (x => true));
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
                var teacherEntity = DbContext.Teachers.FirstOrDefault(x => x.Id == getTeachersGroups.TeacherId);
                if (teacherEntity == null)
                    throw new ArgumentException($"Teacher with id {getTeachersGroups.TeacherId} does not exist");
                var groupVms = Mapper.Map<IEnumerable<GroupVm>>(teacherEntity.Groups);
                return groupVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
        {
    }
}
