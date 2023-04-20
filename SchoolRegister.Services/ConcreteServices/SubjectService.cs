using AutoMapper;
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
    public class SubjectService : BaseService, ISubjectService
    {
        public SubjectService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) :
            base(dbContext, mapper, logger)
        {

        }

        public SubjectVm AddOrUpdateSubject(AddOrUpdateSubjectVm addOrUpdateVm)
        {
            try
            {
                if (addOrUpdateVm == null)
                    throw new ArgumentNullException($"View model parameter is null");
                var subjectEntity = Mapper.Map<Subject>(addOrUpdateVm); //AddOrUpdateSubjectVm -> Subject
                if (!addOrUpdateVm.Id.HasValue || addOrUpdateVm.Id == 0)
                    DbContext.Subjects.Add(subjectEntity);
                else
                    DbContext.Subjects.Update(subjectEntity);
                DbContext.SaveChanges();//od tego momentu subjectEntity ma id(jeśli nie miał)
                var subjectVm = Mapper.Map<SubjectVm>(subjectEntity); //Subject -> SubjectVm
                //pytanie: w SubjectVm wskazaliśmy, że niektóre pola = null!, natomiast w linijce wyżej 
                //tworzy się obiekt zmapowany z obiektu z nie wszystkimi polami
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public SubjectVm GetSubject(Expression<Func<Subject, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException($" FilterExpression is null");
                var subjectEntity = DbContext.Subjects.FirstOrDefault(filterExpression);
                var subjectVm = Mapper.Map<SubjectVm>(subjectEntity);
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw; //re-thrown exeption to the code that called method
            }
        }

        public IEnumerable<SubjectVm> GetSubjects(Expression<Func<Subject, bool>> filterExpression = null)
        {
            try
            {
                var subjectEntities = DbContext.Subjects.AsQueryable();
                if (filterExpression != null)
                    subjectEntities = subjectEntities.Where(filterExpression);
                var subjectVms = Mapper.Map<IEnumerable<SubjectVm>>(subjectEntities);
                return subjectVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }

        }
    }
}
