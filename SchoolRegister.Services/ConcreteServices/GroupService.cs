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
    public class GroupService : BaseService, IGroupService
    {
        private readonly UserManager<User> _userManager;
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger, UserManager<User> userManager)
            : base(dbContext, mapper, logger)
        {
            _userManager = userManager;
        }
        
        public async Task<GroupVm> AddOrUpdateGroupAsync(AddOrUpdateGroupVm addOrEditGroupVm)
        {
            try
            {
                if (addOrEditGroupVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var groupEntity = Mapper.Map<Group>(addOrEditGroupVm);
                if (!addOrEditGroupVm.Id.HasValue || addOrEditGroupVm.Id == 0)
                    await DbContext.Groups.AddAsync(groupEntity);
                else
                    DbContext.Groups.Update(groupEntity);
                await DbContext.SaveChangesAsync();

                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<StudentVm> AttachStudentToGroupAsync(AttachDetachStudentToGroupVm attachStudentToGroupVm)
        {
            try
            {
                if (attachStudentToGroupVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var studentEntity = await DbContext.Users.OfType<Student>().FirstOrDefaultAsync(s => s.Id == attachStudentToGroupVm.StudentId);
                if (studentEntity == null)
                    throw new KeyNotFoundException($"There is no student with id: {attachStudentToGroupVm.StudentId}");

                var groupEntity = await DbContext.Groups.FirstOrDefaultAsync(g => g.Id == attachStudentToGroupVm.GroupId);
                if (groupEntity == null)
                    throw new KeyNotFoundException($"There is no group with id: {attachStudentToGroupVm.GroupId}");

                studentEntity.GroupId = attachStudentToGroupVm.GroupId;
                DbContext.Users.Update(studentEntity);
                await DbContext.SaveChangesAsync();

                var studentVm = Mapper.Map<StudentVm>(studentEntity);
                return studentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<StudentVm> DetachStudentFromGroupAsync(AttachDetachStudentToGroupVm detachStudentFromGroupVm)
        {
            try
            {
                if (detachStudentFromGroupVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var studentEntity = await DbContext.Users.OfType<Student>().FirstOrDefaultAsync(s => s.Id == detachStudentFromGroupVm.StudentId);
                if (studentEntity == null)
                    throw new KeyNotFoundException($"There is no student with id: {detachStudentFromGroupVm.StudentId}");

                var groupEntity = await DbContext.Groups.FirstOrDefaultAsync(g => g.Id == detachStudentFromGroupVm.GroupId);
                if (groupEntity == null)
                    throw new KeyNotFoundException($"There is no group with id: {detachStudentFromGroupVm.GroupId}");

                if (studentEntity.GroupId != detachStudentFromGroupVm.GroupId)
                    throw new InvalidOperationException("Student is not assigned to this group");
                studentEntity.GroupId = null;
                DbContext.Users.Update(studentEntity);
                await DbContext.SaveChangesAsync();

                var studentVm = Mapper.Map<StudentVm>(studentEntity);
                return studentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<GroupVm> AttachSubjectToGroupAsync(AttachDetachSubjectGroupVm attachSubjectGroupVm)
        {
            try
            {
                if (attachSubjectGroupVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var subjectEntity = await DbContext.Subjects
                    .FirstOrDefaultAsync(s => s.Id == attachSubjectGroupVm.SubjectId);
                if (subjectEntity == null)
                    throw new KeyNotFoundException($"There is no subject with id: {attachSubjectGroupVm.SubjectId}");

                var groupEntity = await DbContext.Groups.FirstOrDefaultAsync(g => g.Id == attachSubjectGroupVm.GroupId);
                if (groupEntity == null)
                    throw new KeyNotFoundException($"There is no group with id: {attachSubjectGroupVm.GroupId}");

                var subjectGroupEntity = await DbContext.SubjectGroups.FirstOrDefaultAsync(sg =>
                    (sg.SubjectId == attachSubjectGroupVm.SubjectId && sg.GroupId == attachSubjectGroupVm.GroupId));
                if (subjectGroupEntity != null)
                    throw new InvalidOperationException("Pair SubjectGroup is already exists in the database");

                subjectGroupEntity = Mapper.Map<SubjectGroup>(attachSubjectGroupVm);

                await DbContext.SubjectGroups.AddAsync(subjectGroupEntity);
                await DbContext.SaveChangesAsync();

                var goupsVm = Mapper.Map<GroupVm>(groupEntity);
                return goupsVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

       



        public async Task<GroupVm> DetachSubjectFromGroupAsync(AttachDetachSubjectGroupVm detachSubjectGroupVm)
        {
            try
            {
                if (detachSubjectGroupVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                //var subjectEntity = await DbContext.Subjects.FirstOrDefaultAsync(s => s.Id == detachSubjectGroupVm.SubjectId);
                //if (subjectEntity == null)
                //    throw new KeyNotFoundException($"There is no subject with id: {detachSubjectGroupVm.SubjectId}");

                var groupEntity = await DbContext.Groups.FirstOrDefaultAsync(g => g.Id == detachSubjectGroupVm.GroupId);
                if (groupEntity == null)
                    throw new KeyNotFoundException($"There is no group with id: {detachSubjectGroupVm.GroupId}");

                var subjectGroupEntity = await DbContext.SubjectGroups.FirstOrDefaultAsync(sg =>
                    (sg.SubjectId == detachSubjectGroupVm.SubjectId && sg.GroupId == detachSubjectGroupVm.GroupId));
                if (subjectGroupEntity == null)
                    throw new InvalidOperationException("Pair SubjectGroup has not found in the database");

                DbContext.SubjectGroups.Remove(subjectGroupEntity);
                await DbContext.SaveChangesAsync();

                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<SubjectVm> AttachTeacherToSubjectAsync(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            try
            {
                if (attachDetachSubjectToTeacherVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var subjectEntity = await DbContext.Subjects.FirstOrDefaultAsync(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
                if (subjectEntity == null)
                    throw new KeyNotFoundException($"There is no subject with id: {attachDetachSubjectToTeacherVm.SubjectId}");

                var teacherEntity = await DbContext.Users.OfType<Teacher>()
                    .FirstOrDefaultAsync(t => t.Id == attachDetachSubjectToTeacherVm.TeacherId);
                if (teacherEntity == null)
                    throw new KeyNotFoundException($"There is no teacher with id: {attachDetachSubjectToTeacherVm.TeacherId}");

                subjectEntity.TeacherId = attachDetachSubjectToTeacherVm.TeacherId;
                DbContext.Subjects.Update(subjectEntity);
                await DbContext.SaveChangesAsync();

                var subjectVm = Mapper.Map<SubjectVm>(subjectEntity);
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public async Task<SubjectVm> DetachTeacherFromSubjectAsync(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            try
            {
                if (attachDetachSubjectToTeacherVm == null)
                    throw new ArgumentNullException("View model parameter is null");

                var subjectEntity = await DbContext.Subjects.FirstOrDefaultAsync(s => s.Id == attachDetachSubjectToTeacherVm.SubjectId);
                if (subjectEntity == null)
                    throw new KeyNotFoundException($"There is no subject with id: {attachDetachSubjectToTeacherVm.SubjectId}");

                var teacherEntity = await DbContext.Users.OfType<Teacher>()
                    .FirstOrDefaultAsync(t => t.Id == attachDetachSubjectToTeacherVm.TeacherId);
                if (teacherEntity == null)
                    throw new KeyNotFoundException($"There is no teacher with id: {attachDetachSubjectToTeacherVm.TeacherId}");

                if(subjectEntity.TeacherId != attachDetachSubjectToTeacherVm.TeacherId)
                    throw new InvalidOperationException("Teacher is not assigned to this subject");

                subjectEntity.TeacherId = null;
                DbContext.Subjects.Update(subjectEntity);
                await DbContext.SaveChangesAsync();

                var subjectVm = Mapper.Map<SubjectVm>(subjectEntity);
                return subjectVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<GroupVm> GetGroupAsync(Expression<Func<Group, bool>> filterExpression)
        {
            try
            {
                if (filterExpression == null)
                    throw new ArgumentNullException($"FilterExpression is null");
                var groupEntity = await DbContext.Groups.FirstOrDefaultAsync(filterExpression);

                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<GroupVm>> GetGroupsAsync(Expression<Func<Group, bool>>? filterExpression = null)
        {
            try
            {
                var groupEntities = DbContext.Groups.AsQueryable();
                if (filterExpression != null)
                    groupEntities = groupEntities.Where(filterExpression);
                var groupsVms = Mapper.Map<IEnumerable<GroupVm>>(await groupEntities.ToListAsync());

                return groupsVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
