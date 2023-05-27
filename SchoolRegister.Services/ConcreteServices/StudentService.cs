using AutoMapper;
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
    public class StudentService : BaseService, IStudentService
    {
        public StudentService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {

        }

        public async Task<StudentVm> GetStudentAsync(Expression<Func<Student, bool>> filterExpression)
        {
            try
            {

            if(filterExpression == null)
                throw new ArgumentNullException("FilterExpression is null");

            var studentEntity = await DbContext.Users.OfType<Student>().FirstOrDefaultAsync(filterExpression);
            if(studentEntity == null)
                throw new ArgumentNullException("Student not found");

            var studentVm = Mapper.Map<StudentVm>(studentEntity);
            return studentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        //how to async this method?
        public async Task<IEnumerable<StudentVm>> GetStudentsAsync(Expression<Func<Student, bool>>? filterExpression = null)
        {
            try
            { 
                var studentEntitiesQuery = DbContext.Users.OfType<Student>().AsQueryable();
                if (studentEntitiesQuery == null)
                    throw new ArgumentNullException("Students not found");
                if (filterExpression != null)
                    studentEntitiesQuery = studentEntitiesQuery.Where(filterExpression);
                var studentEntities = await studentEntitiesQuery.ToListAsync();
                var studentVms = Mapper.Map<IEnumerable<StudentVm>>(studentEntities);
                return studentVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }

}
